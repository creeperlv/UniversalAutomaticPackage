using System;
using System.IO;
using UniversalAutomaticPackage;
using UniversalAutomaticPackage.PackageSystem.Remote;
using UniversalAutomaticPackage.ScriptSystem;
using UniversalAutomaticPackage.ScriptSystem.Functions;

namespace UAPCLI
{
    class Program
    {
        public static readonly Version CLIVerion = new Version(0, 0, 1, 0);

        static void Main(string[] args)
        {
            PackageCore packageCore = new PackageCore();
            string AppFolder = "./UAP";
            switch (SystemEnvironment.currentSystem)
            {
                case SystemType.Windows:
                    AppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UAP");
                    break;
                case SystemType.Linux:
                    AppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "UAP");
                    break;
                case SystemType.MacOS:
                    break;
                default:
                    break;
            }
            BasicFunctions.Init();
            Host.ReadLine = Console.ReadLine;
            Host.WriteLine = Console.WriteLine;
            Host.Write = Console.Write;
            Host.ResetColor = Console.ResetColor;
            Host.SetForeground = (ConsoleColor c) => { Console.ForegroundColor = c; };
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToUpper())
                {
                    case "INSTALL":
                        i++;
                        try
                        {
                            var package = packageCore.LoadPackage(args[i]);
                            Console.WriteLine("Install from:" + args[i]);
                            if (package is ManifestPackage)
                            {
                                Console.Write("Package Type:");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("ManifestPackage");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            double p = 0;
                            //Console.ResetColor();
                            var result=package.Install(ref p);
                            if(result.Status!= UniversalAutomaticPackage.PackageSystem.InstallationStatus.Fail)
                            {

                                Console.WriteLine("Move from TemporaryFolder to assigned folder");
                            }
                        }
                        catch (Exception)
                        {
                        }
                        break;
                    case "REMOVE":
                        break;
                    case "INIT":
                        if (!Directory.Exists(AppFolder)) { Directory.CreateDirectory(AppFolder); }
                        break;
                    default:
                        break;
                }
            }
            if (args.Length == 0)
            {
            }
        }
    }
}
