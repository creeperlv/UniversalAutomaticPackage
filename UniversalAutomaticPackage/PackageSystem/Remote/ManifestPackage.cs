using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UniversalAutomaticPackage.PackageSystem.Remote
{
    public class ManifestPackage : BasePackage
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
         * LicenseURL=[Url]
         */
        PackageInformation PackageInformation = new PackageInformation();
        string InstallationScript = "";
        public ManifestPackage(string location)
        {
            var progress = LiteManagedHttpDownload.Downloader.DownloadToText(location, "");
            progress.Wait();
            var content = progress.Result;
            StringReader stringReader = new StringReader(content);
            string line;
            while ((line = stringReader.ReadLine()) != null)
            {
                if (line.StartsWith("FriendlyName="))
                {
                    PackageInformation.FriendlyName = line.Substring("FriendlyName=".Length);
                }
                else if (line.StartsWith("PackageVersion="))
                {
                    PackageInformation.PackageVersion = PackageVersion.Parse(line.Substring("PackageVersion=".Length));
                }
                else if (line.StartsWith("PackageHomePage="))
                {
                    PackageInformation.PackageHomePage = line.Substring("PackageHomePage=".Length);
                }
                else if (line.StartsWith("PackageID="))
                {
                    PackageInformation.PackageID = Guid.Parse(line.Substring("PackageID=".Length));
                }
                else if (line.StartsWith("PackageType="))
                {
                    PackageInformation.PackageType = (PackageType)Enum.Parse(typeof(PackageType), line.Substring("PackageType=".Length));
                }
                else if (line.StartsWith("LicenseURL="))
                {
                    PackageInformation.LicenseURL = line.Substring("LicenseURL=".Length);
                }
                else if (line.StartsWith("InstallationScript="))
                {
                    InstallationScript = line.Substring("InstallationScript=".Length);
                }
                //Process...
            }
            PackageInformation.UpdateOrigin = location;
        }
        public override PackageInformation GetInfomation()
        {
            return PackageInformation;
        }
        public override InstallationResult Install(ref double Progress)
        {
            return base.Install(ref Progress);
        }
    }
}
