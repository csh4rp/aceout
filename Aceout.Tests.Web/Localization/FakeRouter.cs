using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Tests.Web.Localization
{
    class FakeRouter : IRouter
    {
        public string Lang { get; set; }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public Task RouteAsync(RouteContext context)
        {
            context.Handler = new RequestDelegate((x) => { return Task.FromResult(x); });
            context.RouteData = new RouteData();
            context.RouteData.Values.Add("controller", "Home");
            context.RouteData.Values.Add("action", "Index");
            if (!string.IsNullOrEmpty(Lang))
            {
                context.RouteData.Values.Add("lang", Lang);
            }
            return Task.CompletedTask;
        }
    }
}
