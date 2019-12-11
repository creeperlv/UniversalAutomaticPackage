using System;
using UniversalAutomaticPackage.PackageSystem;
using UniversalAutomaticPackage.PackageSystem.Remote;

namespace UniversalAutomaticPackage
{
    public class PackageCore
    {
        public PackageCore()
        {
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
            }
        }
    }
}
