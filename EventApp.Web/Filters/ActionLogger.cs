using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.IO;

namespace EventApp.Web.Filters
{
    public class ActionLogger : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var currentDate = DateTime.Now.ToString("yyyyMMdd_HHmm");
            string path = @"C:\temp\test\" + currentDate;

            var json = JsonConvert.SerializeObject(context.HttpContext.Request);
            if (File.Exists(path))
            {
                File.AppendText(path).WriteLine(json);
            }
            else
            {
                File.WriteAllText(path, json);
            }
        }
    }
}
