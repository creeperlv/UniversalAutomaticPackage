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
    }
}
