using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Services.EventService;
using EventApp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IEventService eventService;

        public UserController(IHttpContextAccessor httpContextAccessor, IEventService eventService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.eventService = eventService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Current(int id)
        {
            if (id != 1)
            {
                return StatusCode(404);
            }

            UserModel userModel = new UserModel() { Id = 1, Address = "Addres", Age = 12, Email = "d@d.d", FirstName = "Gelu", Height = 1.5f, Weight = 24, LastName = "Popescu", Phone = "0744521321" };
            return View(userModel);
        }

        [Route("admin/user/getusers", Name = "admin")]
        public IActionResult GetUsers()
        {
            IEnumerable<UserModel> users = new List<UserModel>() {
             new UserModel() { Id = 1, Address = "Addres", Age = 12, Email = "d@d.d", FirstName = "Gelu", Height = 1.5f, Weight = 24, LastName = "Popescu", Phone = "0744521321" },
             new UserModel() { Id = 2, Address = "Addres", Age = 12, Email = "d@d.d", FirstName = "Gelu1", Height = 1.5f, Weight = 24, LastName = "Popescu", Phone = "0744521321" },
             new UserModel() { Id = 3, Address = "Addres", Age = 12, Email = "d@d.d", FirstName = "Gelu2", Height = 1.5f, Weight = 24, LastName = "Popescu", Phone = "0744521321" },
             new UserModel() { Id = 4, Address = "Addres", Age = 12, Email = "d@d.d", FirstName = "Gelu3", Height = 1.5f, Weight = 24, LastName = "Popescu", Phone = "0744521321" }  };

            return View(users);
        }

        public IActionResult GetMyIp()
        {
            IpModel ipModel = new IpModel() { IpAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString() };
            return View(ipModel);
        }
    }
}