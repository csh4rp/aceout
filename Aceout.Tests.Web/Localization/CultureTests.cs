using Aceout.Web.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aceout.Tests.Web.Localization
{
    public class CultureTests
    {
        private IMemoryCache GetCache()
        {

            var memoryCache = Mock.Of<IMemoryCache>();
            var cachEntry = Mock.Of<ICacheEntry>();

            var mockMemoryCache = Mock.Get(memoryCache);
            mockMemoryCache
                .Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns(cachEntry);

            return memoryCache;
        }

        private HttpContext GetContext()
        {
            var features = new FeatureCollection();
            var httpRequest = Mock.Of<HttpRequest>();

            var httpContextMock = new Mock<HttpContext>();
            httpRequest.Path = new PathString("/pl/Home/Index");
            httpContextMock.Setup(x => x.Request).Returns(httpRequest);
            httpContextMock.Setup(x => x.Features).Returns(features);
            return httpContextMock.Object;
        }

        [Fact]
        public async void SetRouteData_DefaultCulture_SetsFeatureData()
        {
            var fakeRouter = new FakeRouter
            {
                Lang = "pl"
            };

            var httpContext = GetContext();
            var middleware = new GetRoutesMiddleware((x) => Task.FromResult(x), fakeRouter);

            await middleware.Invoke(httpContext);

            var feature = httpContext.Features[typeof(IRoutingFeature)] as IRoutingFeature;

            Assert.NotNull(feature);
            Assert.Equal("pl", feature.RouteData.Values["lang"]);
        }

        [Theory]
        [InlineData("pl")]
        [InlineData("en")]
        [InlineData("pl-PL")]
        [InlineData("en-US")]
        public async void SetCulture_CustomCultures_SetsRequestCulture(string culture)
        {
            var fakeRouter = new FakeRouter
            {
                Lang = culture
            };

            var httpContext = GetContext();

            var cultureProvider = new RouteCultureProvider(new ProviderCultureResult("pl"));
            var middleware = new GetRoutesMiddleware((x) => Task.FromResult(x), fakeRouter);

            await middleware.Invoke(httpContext);
            var cultureResult = await cultureProvider.DetermineProviderCultureResult(httpContext);

            Assert.True(cultureResult.Cultures.Contains(culture));
        }

        [Theory]
        [InlineData("pl")]
        [InlineData("en")]
        [InlineData("pl-PL")]
        [InlineData("en-US")]
        public async void SetDefaultCulture_NullRouteCulture_SetsRequestCulture(string defaultCulture)
        {
            var fakeRouter = new FakeRouter
            {
                Lang = null
            };

            var httpContext = GetContext();

            var cultureProvider = new RouteCultureProvider(new ProviderCultureResult(defaultCulture));
            var middleware = new GetRoutesMiddleware((x) => Task.FromResult(x), fakeRouter);

            await middleware.Invoke(httpContext);
            var cultureResult = await cultureProvider.DetermineProviderCultureResult(httpContext);

            Assert.True(cultureResult.Cultures.Contains(defaultCulture));
        }
    }
}
