using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace UniversalAutomaticPackage.DependencyResolver.Dependencies
{
    public class PowerShell : BaseDependency
    {
        public override Version Find()
        {
            switch (SystemEnvironment.currentSystem)
            {
                case SystemType.Windows:
                    {
                        string pscexe = "./powershell/pwsh.exe";
                        {
                            //Find Ver 7 first, then Ver 6.
                        }
                        {
                            string pscore=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "PowerShell");
                            string pscore7pre = Path.Combine(pscore, "7-preview");
                            if (Directory.Exists(pscore7pre))
                            {
                                pscexe = Path.Combine(pscore7pre, "pwsh.exe");
                            }
                            else
                            {
                                string pscore7 = Path.Combine(pscore, "7");
                                if (Directory.Exists(pscore7))
                                {
                                    pscexe = Path.Combine(pscore7, "pwsh.exe");
                                }
                                else
                                {
                                    string pscore6 = Path.Combine(pscore, "6"); 
                                    if (Directory.Exists(pscore6))
                                    {
                                        pscexe = Path.Combine(pscore6, "pwsh.exe");
                                    }
                                    else
                                    {
                                        pscore = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "PowerShell");
                                        if (Directory.Exists(pscore7pre))
                                        {
                                            pscexe = Path.Combine(pscore7pre, "pwsh.exe");
                                        }
                                        else
                                        {
                                            pscore7 = Path.Combine(pscore, "7");
                                            if (Directory.Exists(pscore7))
                                            {
                                                pscexe = Path.Combine(pscore7, "pwsh.exe");
                                            }
                                            else
                                            {
                                                pscore6 = Path.Combine(pscore, "6");
                                                if (Directory.Exists(pscore6))
                                                {
                                                    pscexe = Path.Combine(pscore6, "pwsh.exe");
                                                }
                                                else
                                                {
                                                    return new Version(0, 0, 0, 0);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            ProcessStartInfo processStartInfo = new ProcessStartInfo(pscore + " -v");
                            processStartInfo.RedirectStandardOutput = true;
                            var p = Process.Start(processStartInfo);
                            var outs = p.StandardOutput;
                            string line = outs.ReadLine();
                            line = line.Substring("PowerShell ".Length);
                            if (line.IndexOf('-') > 0)
                            {
                                line = line.Substring(0, line.IndexOf('-'));
                            }
                            line = line.Trim();
                            p.WaitForExit();
                            return new Version(line);
                        }
                    }
                    break;
                case SystemType.Linux:
                    {
                        {
                            //Find Ver 7 first, then Ver 6.
                        }
                        {
                            bool checkpreview = false;
                            Version retire = new Version(0, 0, 0, 0);
                            {

                                ProcessStartInfo processStartInfo = new ProcessStartInfo("pwsh -v");
                                processStartInfo.RedirectStandardOutput = true;
                                var p = Process.Start(processStartInfo);
                                var outs = p.StandardOutput;
                                string line = outs.ReadLine();
                                if (line.StartsWith("PowerShell"))
                                {
                                    line = line.Substring("PowerShell ".Length);
                                    if (line.IndexOf('-') > 0)
                                    {
                                        line = line.Substring(0, line.IndexOf('-'));
                                    }
                                    line = line.Trim();
                                    retire = new Version(line);
                                }
                                else
                                {
                                    checkpreview = true;
                                }
                                p.WaitForExit();
                            }
                            if (checkpreview == true)
                            {

                                ProcessStartInfo processStartInfo = new ProcessStartInfo("pwsh-preview -v");
                                processStartInfo.RedirectStandardOutput = true;
                                var p = Process.Start(processStartInfo);
                                var outs = p.StandardOutput;
                                string line = outs.ReadLine();
                                if (line.StartsWith("PowerShell"))
                                {
                                    line = line.Substring("PowerShell ".Length);
                                    if (line.IndexOf('-') > 0)
                                    {
                                        line = line.Substring(0, line.IndexOf('-'));
                                    }
                                    line = line.Trim();
                                    retire = new Version(line);
                                }
                                else
                                {
                                    checkpreview = true;
                                }
                                p.WaitForExit();
                            }
                        }
                    }
                    break;
                case SystemType.MacOS:
                    break;
                default:
                    break;
            }
            return base.Find();
        }
        public override bool Install()
        {
            switch (SystemEnvironment.currentSystem)
            {
                case SystemType.Windows:
                    {
                        double p=0;
                        LiteManagedHttpDownload.Downloader.DownloadToFileWithProgressBuffered("https://github.com/PowerShell/PowerShell/releases/download/v6.2.3/PowerShell-6.2.3-win-x64.msi", "./Temporary/PowerShell6.msi", ref p, 4096);
                        Process.Start("msiexec.exe /package /Temporary/PowerShell6.msi /quiet ADD_EXPLORER_CONTEXT_MENU_OPENPOWERSHELL=1 ENABLE_PSREMOTING=1 REGISTER_MANIFEST=1").WaitForExit();
                    }
                    break;
                case SystemType.Linux:
                    {
                        Process.Start("sudo apt install snapd").WaitForExit();
                        Process.Start("sudo snapd install powershell --class").WaitForExit();
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
