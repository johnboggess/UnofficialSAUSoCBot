using System;
using System.Collections.Generic;
using System.Text;

namespace SAUSoCDiscordBot
{
    class Utilities
    {
        public static string FormatString(string str, bool captalize, string prefix, string punctuation)
        {
            if(str == null || str.Length == 0) { return str; }
            if (captalize)
            {
                char c = str[0];
                str = str.Remove(0, 1);
                str = char.ToUpper(c) + str;
            }
            else
            {
                char c = str[0];
                str = str.Remove(0, 1);
                str = char.ToLower(c) + str;
            }
            return prefix + str + punctuation;
        }
    }
}
