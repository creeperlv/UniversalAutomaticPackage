using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace UniversalAutomaticPackage.DependencyResolver.Dependencies
{
    public class git:BaseDependency
    {
        public override Version Find()
        {
            Version retire = new Version(0,0,0,0);
            ProcessStartInfo processStartInfo = new ProcessStartInfo("git version");
            processStartInfo.RedirectStandardOutput = true;
            var p = Process.Start(processStartInfo);
            var outs = p.StandardOutput;
            string line = outs.ReadLine();
            if (line.StartsWith("git"))
            {
                line = line.Substring("git version ".Length);
                if (line.IndexOf(".w") > 0)
                {
                    line = line.Substring(0, line.IndexOf(".w"));
                }
                line = line.Trim();
                retire = new Version(line);
            }
            else
            {
            
            }
            p.WaitForExit();
            return retire;
        }
        public override bool Install()
        {
            switch (SystemEnvironment.currentSystem)
            {
                case SystemType.Windows:
                    {
                        ///VERYSILENT /NORESTART /NOCANCEL /SP- /CLOSEAPPLICATIONS /RESTARTAPPLICATIONS /COMPONENTS="icons,ext\reg\shellhere,assoc,assoc_sh"
                        double p=0;
                        LiteManagedHttpDownload.Downloader.DownloadToFileWithProgressBuffered("https://github.com/git-for-windows/git/releases/download/v2.24.1.windows.2/Git-2.24.1.2-64-bit.exe", "./Temporary/git.installer.exe", ref p, 4096);
                        Process.Start("./Temporary/git.installer.exe /VERYSILENT /NORESTART /NOCANCEL /SP- /CLOSEAPPLICATIONS /RESTARTAPPLICATIONS /COMPONENTS=\"icons,ext\\reg\\shellhere,assoc,assoc_sh\"").WaitForExit();
                        return true;
                    }
                case SystemType.Linux:
                    {
                        Process.Start("sudo apt install git").WaitForExit();
                        return true;
                    }
                    break;
                case SystemType.MacOS:
                    break;
                default:
                    break;
            }

            return base.Install();
        }
    }
}
