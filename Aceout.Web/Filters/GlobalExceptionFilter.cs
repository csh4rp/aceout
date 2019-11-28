using Aceout.Web.ExceptionHandling;
using Aceout.WebUI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Sentry;

namespace Aceout.Web.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        //   private readonly IStringLocalizer _stringLocalizer;



        public async void OnException(ExceptionContext context)
        {
            SentrySdk.CaptureException(context.Exception);
            _logger.LogError("", context.Exception.Message);

            var errorModel = GetModel(context.Exception);

            if (errorModel == null) return;

            context.ExceptionHandled = true;
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.StatusCode = 400;
            await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(errorModel));
        }

        private ErrorModel GetModel(Exception exception)
        {
            var exceptionType = exception.GetType();

            var enumTypes = Assembly.GetEntryAssembly()
                .GetExportedTypes()
                .Where(x => x.GetCustomAttributes(typeof(ApiErrorEnumAttribute)).Any());

            foreach (var type in enumTypes)
            {
                foreach (var field in type.GetFields())
                {
                    var attribute = field.GetCustomAttribute<ErrorCodeAttribute>();

                    if(attribute != null && attribute.ExceptionType == exceptionType)
                    {
                        var value = (int)Enum.Parse(type, field.Name);
                        var message = attribute.Message;//_stringLocalizer[attribute.Message];

                        return new ErrorModel(value, message);
                    }
                }
            }

            return null;
        }
    }
}
