using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aceout.Web.Mvc;
using elFinder.NetCore;
using elFinder.NetCore.Drivers.FileSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Aceout.WebUI.Controllers
{
    [Route("FileSystem")]
    public class FileSystemController : BaseController
    {
        [Route("connector")]
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Connector()
        {
            try
            {
                var connector = GetConnector();
                var result = await connector.ProcessAsync(Request);
                //Response.Headers["Access-Control-Allow-Origin"] = "http://localhost:4200";
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Route("thumb/{hash}")]
        [HttpPost]
        public async Task<IActionResult> Thumbs(string hash)
        {
            var connector = GetConnector();
            return await connector.GetThumbnailAsync(HttpContext.Request, HttpContext.Response, hash);
        }

        private Connector GetConnector()
        {
            var driver = new FileSystemDriver();

            var absoluteUrl = UriHelper.BuildAbsolute(Request.Scheme, Request.Host, Request.PathBase);
            var uri = new Uri(absoluteUrl);

            var root = new RootVolume(
                Startup.MapPath("/Files"),
                $"{absoluteUrl}/Files/",
                $"{absoluteUrl}/Thumbs/")
            {
                //IsReadOnly = !User.IsInRole("Administrators")
                IsReadOnly = false, // Can be readonly according to user's membership permission
                IsLocked = false, // If locked, files and directories cannot be deleted, renamed or moved
                Alias = "Files", // Beautiful name given to the root/home folder
                //MaxUploadSizeInKb = 2048, // Limit imposed to user uploaded file <= 2048 KB
                //LockedFolders = new List<string>(new string[] { "Folder1" })
            };
            driver.AddRoot(root);
            
            return new Connector(driver);
        }

        [Route("browse")]
        [HttpGet]
        public IActionResult Browse()
        {
            return View("elfinder");
        }
    }
}