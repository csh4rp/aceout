using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Web.Localization
{
    public class RouteCultureProvider : IRequestCultureProvider
    {
        private readonly ProviderCultureResult _defaultCultureResult;

        public RouteCultureProvider(ProviderCultureResult defaultCultureResult)
        {
            _defaultCultureResult = defaultCultureResult;
        }

        public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var routingFeature = httpContext.Features[typeof(IRoutingFeature)] as RoutingFeature;

            if (routingFeature != null)
            {
                if (!routingFeature.RouteData.Values.ContainsKey("lang"))
                {
                    return Task.FromResult(_defaultCultureResult);
                }

                var lang = routingFeature.RouteData.Values["lang"] as string;

                return Task.FromResult(new ProviderCultureResult(lang));
            }

            return Task.FromResult(_defaultCultureResult);
        }
    }
}
