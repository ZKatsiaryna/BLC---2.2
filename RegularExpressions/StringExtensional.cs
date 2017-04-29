using System;
using System.Collections.Generic;
using System.Linq;

namespace RegularExpressions
{
    static class StringExtensional
    {
        public static List<string> RemoveNewlines(this string value)
        {
            return value.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(s => s.Trim()).ToList();
        }
    }
}
