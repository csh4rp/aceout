using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Aceout.Tools.Extensions
{
    public static class StringExtensions
    {
        public static string IfEmpty(this string @string, string value)
        {
            return string.IsNullOrEmpty(@string) ? value : @string;
        }

        public static bool IsEmpty(this string @string)
        {
            return string.IsNullOrEmpty(@string);
        }

        public static int ToInt(this string @string)
        {
            return int.Parse(@string);
        }

        public static double ToDouble(this string @string)
        {
            return double.Parse(@string);
        }

        public static DateTime ToDate(this string @string)
        {
            return DateTime.Parse(@string);
        }

        public static DateTime ToDate(this string @string, string format)
        {
            return DateTime.ParseExact(@string, format, CultureInfo.CurrentCulture);
        }
    }
}
