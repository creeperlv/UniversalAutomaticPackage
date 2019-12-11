using System;
using System.Collections.Generic;
using System.IO;
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
            var progress=LiteManagedHttpDownload.Downloader.DownloadToText(location,"");
            progress.Wait();
            var content = progress.Result;
            StringReader stringReader = new StringReader(content);
            string line;
            while ((line=stringReader.ReadLine())!=null)
            {
                //Process...
            }
        }
    }
}
