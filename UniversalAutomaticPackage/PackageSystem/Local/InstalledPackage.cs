using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UniversalAutomaticPackage.Configuration;

namespace UniversalAutomaticPackage.PackageSystem.Local
{
    public class InstalledPackage
    {
        StandardConfigurationFile Manifest;
        public InstalledPackage(string location)
        {
            Manifest = new StandardConfigurationFile(Path.Combine(location, "Package.UAPManifest"));
        }
        public static InstalledPackage FindInstalledPackage(string name) {
            StandardConfigurationFile standardConfigurationFile = new StandardConfigurationFile("./Packages.index");
            var nameMatched=standardConfigurationFile.GetValues(name);
            if (nameMatched[0] == "")
            {
                throw new Exception("No such a package.");
            }
            else
            {

                return new InstalledPackage(nameMatched[0]);

            }
        }
        public void Uninstall()
        {

        }
    }
}
