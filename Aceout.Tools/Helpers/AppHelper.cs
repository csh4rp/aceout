using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Aceout.Tools.Helpers
{
    public static class AppHelper
    {
        public static string CurrentLanguage => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

    }
}
