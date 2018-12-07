using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Services.EventService;
using EventApp.Services.EventService.EventDtos;
using EventApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Omu.ValueInjecter;

namespace EventApp.Web.Controllers
{
    public class EventController : Controller
    {
        private IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public IActionResult Index()
        {
            var eventModels = eventService.GetEvents().Select(x => new EventModel().InjectFrom(x) as EventModel);
            return View(eventModels);
        }
    }
}