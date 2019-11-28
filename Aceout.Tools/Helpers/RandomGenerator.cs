using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aceout.Tools.Helpers
{
    public class RandomGenerator
    {
        private static Random _random = new Random();

        public static string GetRandomString(int length)
        {
            const string chars = "abcdefghijklmnoprstuwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
