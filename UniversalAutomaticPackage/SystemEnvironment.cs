using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalAutomaticPackage
{
    public enum SystemType
    {
        Windows,Linux,MacOS
    }
    public class SystemEnvironment
    {
        public static SystemType currentSystem = SystemType.Windows;
    }
}
