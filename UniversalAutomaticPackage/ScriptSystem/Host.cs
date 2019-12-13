using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalAutomaticPackage.ScriptSystem
{
    public class Host
    {
        public static Action<string> WriteLine;
        public static Func<string> ReadLine;
        public static Action<string> Write;
        public static Action<ConsoleColor> SetForeground;
        public static Action ResetColor;
    }
}
