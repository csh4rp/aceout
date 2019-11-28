using Aceout.WebUI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Sentry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Web.Filters
{
    public class ModelValidationFilterAttribute : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

                // App code

                if (context.ModelState.IsValid)
                {
                    await next();
                }
                else
                {
                    var errors = new List<string>();

                    foreach (var item in context.ModelState)
                    {
                        if (item.Value.Errors.Any())
                        {
                            errors.AddRange(item.Value.Errors.Select(x => x.ErrorMessage));
                        }
                    }

                    var response = context.HttpContext.Response;
                    response.StatusCode = 400;
                    await response.WriteAsync(JsonConvert.SerializeObject(new ErrorModel(1, errors.ToArray())));
                }
            

        }


    }
}
