using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Web.Localization
{
    public class HeaderCultureProvider : IRequestCultureProvider
    {
        private readonly ProviderCultureResult _defaultCultureResult;
        private readonly string _headerName;

        public HeaderCultureProvider(string headerName, ProviderCultureResult defaultCultureResult)
        {
            _headerName = headerName;
            _defaultCultureResult = defaultCultureResult;
        }

        public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var headers = httpContext.Request.Headers;

            if (!headers.ContainsKey(_headerName))
            {
                return Task.FromResult(_defaultCultureResult);
            }

            var value = headers[_headerName].First();

            return Task.FromResult(new ProviderCultureResult(value));
        }
    }
}
