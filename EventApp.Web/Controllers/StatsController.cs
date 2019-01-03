using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Services.StatisticsService;
using EventApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Web.Controllers
{
    public class StatsController : Controller
    {
        private IStatisticsService statisticsService;

        public StatsController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet]
        public async Task<JsonResult> GetJsonDataForEventType()
        {
            var statsByEventType = await statisticsService.GetEventTypeStas();
            var statsModel = statsByEventType.Select(x => new StatsModel() { Name = x.Name, Count = x.Count });
            return Json(statsModel);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}