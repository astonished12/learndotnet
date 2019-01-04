using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Services.ImageStorageService;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Web.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IImageStorageService imageServiceStorage;

        public GalleryController(IImageStorageService imageServiceStorage)
        {
            this.imageServiceStorage = imageServiceStorage;
        }

        public async Task<IActionResult> Index()
        {
            return View(await imageServiceStorage.GetImages());
        }
    }
}