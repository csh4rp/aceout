using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Web.Localization
{
    public static class GetRoutesMiddlewareExtensions
    {
        public static IApplicationBuilder UseRouteLocalizationMiddleware(this IApplicationBuilder app, Action<IRouteBuilder> configureRoutes)
        {
            var routes = new RouteBuilder(app)
            {
                DefaultHandler = app.ApplicationServices.GetRequiredService<MvcRouteHandler>(),
            };

            configureRoutes(routes);
            routes.Routes.Insert(0, AttributeRouting.CreateAttributeMegaRoute(app.ApplicationServices));
            var router = routes.Build();

            return app.UseMiddleware<GetRoutesMiddleware>(router);
        }
    }
}
