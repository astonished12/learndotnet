using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventApp.Services.EventService;
using EventApp.Services.EventService.EventDtos;
using EventApp.Services.GuestService;
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

        public EventController(IEventService eventService, IGuestService guestService)
        {
            this.eventService = eventService;
            this.guestService = guestService;
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
            int eventId = eventService.CreateEvent(new EventDTO().InjectFrom(eventModel) as EventDTO);

            using (var stream = new FileStream(@"C:\temp\"+eventModel.EventImage.FileName, FileMode.Create))
            {
                await eventModel.EventImage.CopyToAsync(stream);
            }

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