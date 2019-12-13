using System;
using System.Runtime.InteropServices;
using UniversalAutomaticPackage.DependencyResolver;
using UniversalAutomaticPackage.PackageSystem;
using UniversalAutomaticPackage.PackageSystem.Local;
using UniversalAutomaticPackage.PackageSystem.Remote;

namespace UniversalAutomaticPackage
{
    public class PackageCore
    {
        public static readonly Version CoreVersion = new Version(0, 0, 1, 0);
        public PackageCore()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                SystemEnvironment.currentSystem = SystemType.Windows;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                SystemEnvironment.currentSystem = SystemType.Linux;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                SystemEnvironment.currentSystem = SystemType.MacOS;
            }
            switch (SystemEnvironment.currentSystem)
            {
                case SystemType.Windows:
                    {
                        var ver = RuntimeInformation.OSDescription.Substring("Microsoft Windows ".Length);
                        SystemEnvironment.SystemVersion = new Version(ver);
                    }
                    break;
                case SystemType.Linux:
                    {
                        var ver = RuntimeInformation.OSDescription.Substring("Linux ".Length);
                        ver = ver.Substring(0, ver.IndexOf("-"));
                        SystemEnvironment.SystemVersion = new Version(ver);
                    }
                    break;
                case SystemType.MacOS:
                    break;
                default:
                    break;
            }
        }
        public BasePackage LoadPackage(string location)
        {
            if (location.StartsWith("HTTP"))
            {
                //Remote Package.
                var RemoteRequest = location.Split('?');
                if (RemoteRequest[0].ToUpper().EndsWith("UAPMANIFEST"))
                {
                    return new ManifestPackage(RemoteRequest[0]);
                }
                else
                {
                    return new EntirePackage(RemoteRequest[0]);
                }
            }
            else
            {
                return new CompressedPackage(location);
            }
        }
    }
}
