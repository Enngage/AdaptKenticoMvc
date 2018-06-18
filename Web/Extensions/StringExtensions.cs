using System;
using System.Linq;

namespace Web.Extensions
{
    public static class StringExtensions
    {
        public static string ToCodename(this string src)
        {
            return new string(src.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '-').ToArray());
        }
    }
}
