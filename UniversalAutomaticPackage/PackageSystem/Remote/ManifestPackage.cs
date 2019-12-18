using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UniversalAutomaticPackage.DependencyResolver;
using UniversalAutomaticPackage.ScriptSystem;

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
            var progress = LiteManagedHttpDownload.Downloader.DownloadToTextAsync(location);
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
                    InstallationScript = line.Substring("InstallationScript=".Length).Replace("{Platform}",SystemEnvironment.currentSystem.ToString());
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
            DirectoryInfo directoryInfo = new DirectoryInfo("./Temporary/" + Guid.NewGuid().ToString());
            if(!directoryInfo.Exists) directoryInfo.Create();
            //UAPScriptEnv.WorkingDirectory = directoryInfo;
            Host.WriteLine("Getting install script:"+ InstallationScript);
            LiteManagedHttpDownload.Downloader.DownloadToFileAsync(InstallationScript, Path.Combine(directoryInfo.FullName,"InstalScript.uapscript")).Wait();
            UAPScript script = new UAPScript(Path.Combine(directoryInfo.FullName, "InstalScript.uapscript"),this);
            script.WorkingDirectory = directoryInfo.CreateSubdirectory("WorkingSpace");
            var d=directoryInfo.CreateSubdirectory("TargetBinaries");
            script.Execute();
            InstallationResult installationResult = new InstallationResult();
            installationResult.BinFolder = d;
            installationResult.Status = InstallationStatus.Succeed;
            return installationResult;
        }
    }
}
