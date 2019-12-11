using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalAutomaticPackage.PackageSystem.Remote
{
    public class ManifestPackage:BasePackage
    {
        /**
         * UAPMANIFEST TEMPLATE:
         * 
         * FriendlyName=[NAME]
         * PackageID=[GUID]
         * PackageVersion=[MAJOR.MINOR.BUILD.REVISION](,CodeName)
         * PackageHomePage=[URL]
         * PackageType=[Source|Binary]
         * InstallationScript=[URL]
         * 
         */
        public ManifestPackage(string location)
        {

        }
    }
}
