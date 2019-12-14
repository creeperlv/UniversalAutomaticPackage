using System;
using System.Diagnostics;
using System.IO;
using UniversalAutomaticPackage.DependencyResolver;

namespace UniversalAutomaticPackage.Utilities
{
    public class ShortcutCreator
    {
        public static void Create(string origin,string target)
        {
            switch (SystemEnvironment.currentSystem)    
            {
                case SystemType.Windows:
                    {
                        var shellType = Type.GetTypeFromProgID("WScript.Shell");
                        dynamic shell = Activator.CreateInstance(shellType);
                        var shortcut = shell.CreateShortcut(origin);
                        shortcut.TargetPath =Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.StartMenu),target);
                        shortcut.Arguments = "";
                        shortcut.WorkingDirectory = (new FileInfo(origin).DirectoryName);
                        shortcut.Save();
                    }
                    break;
                case SystemType.Linux:
                    {
                        string LinkCmd = $"sudo ln -s \"{origin}\" \"/usr/bin/{target}\"";
                        Process.Start(LinkCmd).WaitForExit();
                    }
                    break;
                case SystemType.MacOS:
                    break;
                default:break;
            }
        }
    }
}
