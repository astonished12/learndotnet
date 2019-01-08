using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Services.ImageStorageService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Blob;

namespace EventApp.Web.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IImageStorageService imageServiceStorage;

        public GalleryController(IImageStorageService imageServiceStorage)
        {
            this.imageServiceStorage = imageServiceStorage;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await imageServiceStorage.GetImages());
        }

        [HttpGet]
        public IActionResult UploadImages()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImages(List<IFormFile> formFiles)
        {
            var tasks = new List<Task<string>>();

            foreach (var file in formFiles)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    tasks.Add(imageServiceStorage.StoreImage(file.FileName, stream.GetBuffer()));
                }
            }

            await Task.WhenAll(tasks);

            return Redirect("Index");
        }

    }
}