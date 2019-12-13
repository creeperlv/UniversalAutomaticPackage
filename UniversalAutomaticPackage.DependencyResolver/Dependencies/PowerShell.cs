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
                                        return new Version(0, 0, 0, 0);
                                    }
                                }
                            }
                            ProcessStartInfo processStartInfo = new ProcessStartInfo(pscore + " -v");
                            processStartInfo.RedirectStandardOutput = true;
                            
                        }
                    }
                    break;
                case SystemType.Linux:
                    break;
                case SystemType.MacOS:
                    break;
                default:
                    break;
            }
            return base.Find();
        }
    }
}
