using System;
using System.Collections.Generic;
using System.Linq;

namespace mikrotik_switcher
{
    public class EntryCheck
    {
        public bool EntryNumCheck(string str)
        {
            List<bool> entNumChecks = str.Select(numCh => Char.IsNumber(str, str.Length - 1)).ToList();
            return entNumChecks.Min();
        }
    }
}
