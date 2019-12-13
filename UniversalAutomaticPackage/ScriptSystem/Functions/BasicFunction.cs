using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UniversalAutomaticPackage.DependencyResolver;

namespace UniversalAutomaticPackage.ScriptSystem.Functions
{
    public class BasicFunctions
    {
        public static void Init()
        {
            UAPScript.functions.Add("ScriptType", ScriptType);
            UAPScript.functions.Add("Environment", CheckEnvironment);
            UAPScript.functions.Add("Set-Main-Executable", SetMainExecutable);
            UAPScript.functions.Add("Dependencies", CheckDependencies);
        }
        public static void CopyFolder(string src,string target)
        {
            DirectoryInfo source = new DirectoryInfo(src);
            DirectoryInfo tgt= new DirectoryInfo(target);
            if (!tgt.Exists) tgt.Create();
            {
                var folders = source.EnumerateDirectories();
                foreach (var item in folders)
                {
                    var sub=tgt.CreateSubdirectory(item.Name);
                    CopyFolder(item.FullName, sub.FullName);
                }

            }
            {
                var folders = source.EnumerateFiles();
                foreach (var item in folders)
                {
                    item.CopyTo(Path.Combine(tgt.FullName, item.Name), true);
                }
            }
        }
        public static bool CopyFolder(string s,UAPScript UAPScriptEnv, List<KeyValuePair<string, string>> parameters)
        {
            string src="";
            string tgt="";
            foreach (var item in parameters)
            {
                if (item.Key == "-Source")
                {
                    src = item.Value;
                }else
                if (item.Key == "-Target")
                {
                    src = item.Value;
                }
            }
            try
            {
                CopyFolder(src, tgt);
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }
        static bool CheckDependencies(string s, UAPScript UAPScriptEnv, List<KeyValuePair<string, string>> parameters)
        {
            foreach (var item in parameters)
            {
                SingleDependency dependency = new SingleDependency(item.Key, item.Value);
                if (!dependency.Check())
                {
                    Host.SetForeground(ConsoleColor.Red);
                    Host.WriteLine("Missing dependency:"+item.Key);
                    Host.SetForeground(ConsoleColor.White);
                }
            }
            return true;
        }
        static bool ScriptType(string s,UAPScript UAPScriptEnv, List<KeyValuePair<string, string>> parameters)
        {
            if (s.ToUpper().Trim().Equals("INSTALL"))
            {
             
            }
            return true;
        }
        static bool CheckEnvironment(string s,UAPScript UAPScriptEnv, List<KeyValuePair<string, string>> parameters)
        {
            Host.WriteLine("Gathering system information...");
            long memory=0;
            {
                //Run cmd to judge memory.
                {

                    switch (SystemEnvironment.currentSystem)
                    {
                        case SystemType.Windows:
                            {

                                ProcessStartInfo processStartInfo = new ProcessStartInfo("wmic MEMORYCHIP get Capacity");
                                processStartInfo.RedirectStandardOutput = true;
                                var p = Process.Start(processStartInfo);
                                var outs = p.StandardOutput;
                                string line;
                                while ((line = outs.ReadLine()) != null)
                                {
                                    int temp = 0;
                                    int.TryParse(line, out temp);
                                    memory += temp;
                                }
                                p.WaitForExit();
                                memory /= (1024 * 1024);
                            }
                            break;
                        case SystemType.Linux:
                            {

                                ProcessStartInfo processStartInfo = new ProcessStartInfo("cat /proc/meminfo");
                                processStartInfo.RedirectStandardOutput = true;
                                var p = Process.Start(processStartInfo);
                                var outs = p.StandardOutput;
                                string line= outs.ReadLine();
                                line= line.Substring(line.IndexOf(":") + 1);
                                line = line.Substring(0, line.Length - 3);
                                line = line.Trim();
                                p.WaitForExit();
                                memory=(int.Parse(line))/ (1024 * 1024);
                            }
                            break;
                        case SystemType.MacOS:
                            break;
                        default:
                            break;
                    }
                    //MB now;
                }
            }
            foreach (var item in parameters)
            {
                switch (item.Key)
                {
                    case "-SystemVersion":
                        {
                            Version min = new Version(item.Value.Trim());
                            if (min.CompareTo(SystemEnvironment.SystemVersion)<0)
                            {
                                Host.SetForeground(ConsoleColor.Red);
                                Host.WriteLine("Your OS is too old for this software.");
                                Host.SetForeground(ConsoleColor.White);
                                return false;
                            }
                        }
                        break;
                    case "-Memory":
                        {
                            long minMem=long.Parse(item.Value);
                            if (memory < minMem)
                            {
                                Host.SetForeground(ConsoleColor.Red);
                                Host.WriteLine("Your PC's memory is too small for this software.");
                                Host.SetForeground(ConsoleColor.White);
                                return false;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            return true;
        }
        static bool SetMainExecutable(string s,UAPScript UAPScriptEnv, List<KeyValuePair<string, string>> parameters)
        {
            foreach (var item in parameters)
            {
                if (item.Key == "-Soruce")
                {

                    var exe = item.Value;
                    UAPScriptEnv.Parent.MainExecutable = exe;
                    return true;
                }
            }
            return false;
        }
    }
}
