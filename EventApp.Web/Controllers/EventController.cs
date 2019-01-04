using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventApp.Services.EventService;
using EventApp.Services.EventService.EventDtos;
using EventApp.Services.GuestService;
using EventApp.Services.ImageStorageService;
using EventApp.Web.Filters;
using EventApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Omu.ValueInjecter;

namespace EventApp.Web.Controllers
{
    public class EventController : Controller
    {
        private IEventService eventService;
        private IGuestService guestService;
        private IImageStorageService imageStorageService;

        public EventController(IEventService eventService, IGuestService guestService, IImageStorageService imageStorageService)
        {
            this.eventService = eventService;
            this.guestService = guestService;
            this.imageStorageService = imageStorageService;
        }

        [HttpGet]
        [ActionLogger]
        public IActionResult Index()
        {
            var eventModels = eventService.GetEvents().Select(x => new EventModel().InjectFrom(x) as EventModel);
            return View(eventModels);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var eventFromId = eventService.GetEventById(id);
            if (eventFromId == null)
                return RedirectToAction("Error", "Home");

            var guestOfEvent = guestService.GetGuestsForAnEvent(eventFromId.Id).ToList();
            return View(new EventGuestsModel()
            {
                EventModel = new EventModel().InjectFrom(eventFromId) as EventModel,
                Guests = guestOfEvent.Select(x => new GuestModel().InjectFrom(x) as GuestModel)
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new EventModel());
        }

        [HttpPost]
        [ActionLogger]
        public async Task<ActionResult> Create(EventModel eventModel)
        {
            var eventToBeCreated = (new EventDTO().InjectFrom(eventModel) as EventDTO);

            using (var stream = new MemoryStream())
            {
                await eventModel.EventImage.CopyToAsync(stream);
                eventToBeCreated.ImageUri = await imageStorageService.StoreImage(eventModel.EventImage.FileName, stream.GetBuffer());
            }

            int eventId = eventService.CreateEvent(eventToBeCreated);

            if (eventId > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            EventDTO eventFromId = eventService.GetEventById(id);
            if (eventFromId == null)
                return RedirectToAction("Error", "Home");

            return View(new EventModel().InjectFrom(eventFromId) as EventModel);
        }

        [HttpPost]
        public IActionResult Edit(EventModel eventModel)
        {
            eventService.Update(new EventDTO().InjectFrom(eventModel) as EventDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            EventDTO eventFromId = eventService.GetEventById(id);
            if (eventFromId == null)
                return RedirectToAction("Error", "Home");

            return View(new EventModel().InjectFrom(eventFromId) as EventModel);

        }

        [HttpPost]
        public IActionResult Delete(EventModel eventModel)
        {
            eventService.DeleteById(eventModel.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Search(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var result = eventService.GetEventsByName(name.ToLower()).ToList().Select(x => new EventModel().InjectFrom(x) as EventModel);
                if (!result.Any())
                {
                    return Ok(new List<EventModel>());
                }
                return Ok(result);
            }
            else
            {
                var result = eventService.GetEvents().Select(x => new EventModel().InjectFrom(x) as EventModel);
                return Ok(result);
            }
        }
    }
}