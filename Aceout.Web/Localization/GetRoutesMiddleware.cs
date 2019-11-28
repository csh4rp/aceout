using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Web.Localization
{
    public class GetRoutesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRouter _router;

        public GetRoutesMiddleware(RequestDelegate next, IRouter router)
        {
            _next = next;
            _router = router;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var context = new RouteContext(httpContext);
            context.RouteData.Routers.Add(_router);

            await _router.RouteAsync(context);

            if(context.Handler != null)
            {
                httpContext.Features[typeof(IRoutingFeature)] = new RoutingFeature
                {
                    RouteData = context.RouteData
                };
            }

            await _next(httpContext);
        }
    }
}
