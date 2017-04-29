using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegularExpressions
{
    class Translit
    {
        public readonly Dictionary<string, string> russianToEnglishsymbols = new Dictionary<string, string>
            {
                { "а","a"}, {"А","A"}, {"В","B"}, {"с","c"}, {"С","C"}, {"е","e"}, {"Е","E"}, {"Н","H"},
                {"К","K"}, {"М","M"}, {"О","O"}, {"о","o"}, {"Р","P"}, {"р","p"}, {"Т","T"}
            };
    }
}
