using Aceout.WebUI.Model;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Aceout.Web.Mvc
{
    public class BaseController : Controller
    {

        

        protected System.Globalization.CultureInfo GetCulture()
        {
            return Thread.CurrentThread.CurrentCulture;
        }

        protected string Language => GetCulture().TwoLetterISOLanguageName;
    }


}
