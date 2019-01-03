using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Web.Filters
{
    public class ActionLogger : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var currentDate = DateTime.Now.ToString("yyyyMMdd_HHmm");
            string path = @"C:\temp\test\" + currentDate + ".json";
            var originalBodyStream = context.HttpContext.Request.Body;
            using (var responseBody = new MemoryStream())
            {
                context.HttpContext.Request.EnableRewind();
                var buffer = new byte[Convert.ToInt32(context.HttpContext.Request.ContentLength)];
                await context.HttpContext.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                var bodyAsText = Encoding.UTF8.GetString(buffer);

                var json = JsonConvert.SerializeObject(new
                {
                    context.HttpContext.Request.Headers,
                    bodyAsText,
                    context.HttpContext.Request.Path
                }, Formatting.Indented);
                json += "\n";

                if (File.Exists(path))
                {
                    File.AppendAllText(path, json);
                }
                else
                {
                    File.WriteAllText(path, json);
                }
            }

            var resultContext = await next();

            using (var responseBody = new MemoryStream())
            {
                await responseBody.CopyToAsync(resultContext.HttpContext.Response.Body);

                string bodyText = await new StreamReader(responseBody).ReadToEndAsync();

                var json = JsonConvert.SerializeObject(new
                {
                    resultContext.HttpContext.Request.Headers,
                    bodyText,
                    resultContext.HttpContext.Request.Path
                }, Formatting.Indented);
                json += "\n";

                if (File.Exists(path))
                {
                    File.AppendAllText(path, json);
                }
                else
                {
                    File.WriteAllText(path, json);
                }
            }
        }
    }
}
