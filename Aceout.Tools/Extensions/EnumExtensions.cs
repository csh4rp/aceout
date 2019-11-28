using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Tools.Extensions
{
    public static class EnumExtensions
    {
        public static int ToInt(this Enum enumValue)
        {
            return Convert.ToInt32(enumValue);
        }
    }
}
