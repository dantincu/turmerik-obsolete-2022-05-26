using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turmerik.AspNetCore.Infrastructure
{
    public static class KeyboardKeyCodes
    {
        public const string ENTER = "Enter";
        public const string ESCAPE = "Escape";

        public static bool KeyCodeIsEnter(this string keyCode)
        {
            bool retVal = keyCode == ENTER;
            return retVal;
        }

        public static bool KeyCodeIsEscape(this string keyCode)
        {
            bool retVal = keyCode == ESCAPE;
            return retVal;
        }
    }
}
