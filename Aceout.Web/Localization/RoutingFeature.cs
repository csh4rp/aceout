using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Web.Localization
{
    public class RoutingFeature : IRoutingFeature
    {
        public RouteData RouteData { get; set; }
    }
}
