using Aceout.Web.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aceout.Web.Tests.Localization
{
    public class LocalizationTests
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
        public void LoadTranslations_SingleFile_Loads()
        {
            var messages = @"{
                ""msg1"": ""Wiadomość"",
                ""msg2"": ""Wiadomość {0}""
            }";

            var managerMock = new Mock<IDirectoryManager>();
            managerMock.Setup(x => x.GetFiles()).Returns(new[] { "pl-PL.json" });
            managerMock.Setup(x => x.ReadFile(It.IsAny<string>())).Returns(messages);
            var manager = managerMock.Object;

            var cache = GetCache();

            var factory = new FileStringLocalizerFactory(cache, manager, CultureInfo.GetCultureInfo("pl-PL"),
                new CultureInfo[] { CultureInfo.GetCultureInfo("pl-PL") });

            var localizer = factory.Create(null);
            Assert.Equal("Wiadomość", localizer["msg1"].Value);
            Assert.Equal("Wiadomość Tekst", localizer["msg2", "Tekst"].Value);

        }

        [Fact]
        public void LoadTranslations_NotAllFilesExist_ThrowsException()
        {
            var messages = @"{
                ""msg1"": ""Wiadomość"",
                ""msg2"": ""Wiadomość {0}""
            }";

            var managerMock = new Mock<IDirectoryManager>();
            managerMock.Setup(x => x.GetFiles()).Returns(new[] { "pl-PL.json" });
            managerMock.Setup(x => x.ReadFile(It.IsAny<string>())).Returns(messages);
            var manager = managerMock.Object;

            var cache = GetCache();

            var factory = new FileStringLocalizerFactory(cache, manager, CultureInfo.GetCultureInfo("pl-PL"),
                new CultureInfo[] { CultureInfo.GetCultureInfo("pl-PL"), CultureInfo.GetCultureInfo("en-US") });

            Assert.Throws<InvalidOperationException>(() => factory.Create(null));
        }


    }

}
