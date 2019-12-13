using System;

namespace UniversalAutomaticPackage.DependencyResolver
{
    public enum SystemType
    {
        Windows,Linux,MacOS
    }
    public class SystemEnvironment
    {
        public static SystemType currentSystem = SystemType.Windows;
        public static Version SystemVersion = new Version(10, 0, 10240);
    }
}
