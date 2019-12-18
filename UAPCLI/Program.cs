using System;
using System.IO;
using System.Runtime.InteropServices;
using UniversalAutomaticPackage;
using UniversalAutomaticPackage.DependencyResolver;
using UniversalAutomaticPackage.PackageSystem.Local;
using UniversalAutomaticPackage.PackageSystem.Remote;
using UniversalAutomaticPackage.ScriptSystem;
using UniversalAutomaticPackage.ScriptSystem.Functions;
using UniversalAutomaticPackage.Utilities;

namespace UAPCLI
{
    class Program
    {
        public static readonly Version CLIVerion = new Version(0, 0, 1, 0);
        static void ShowHelpContent()
        {
            Console.WriteLine("Usage: UAPCLI [FUNCTION]");
            Console.WriteLine("Functions:");
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\tINSTALL");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t<Package-To-Install>");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\tInstall package from given path. It can be a file in your disk or a effective url.");
            }
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\tREMOVE");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t<Package-To-Remove>");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\tRemove a package by GUID or name.");
            }
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\tINIT");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\tInitialize the environment.");
            }
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\tVERSION");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\tVersion UAP version.");
            }
        }
        static void Side()
        {
            //RuntimeInformation.OSArchitecture == Architecture.X64
            Console.WriteLine(RuntimeInformation.OSDescription);
            System.Diagnostics.PerformanceCounter performance = new System.Diagnostics.PerformanceCounter();
            Console.WriteLine(SystemEnvironment.SystemVersion.ToString());
        }
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            PackageCore packageCore = new PackageCore();
            Side();
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
                            var pkginfo = package.GetInfomation();
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
                                DirectoryInfo AppsFolder = new DirectoryInfo(AppFolder);
                                //package
                                var pkg = Path.Combine(AppsFolder.FullName,pkginfo.PackageID.ToString());
                                Console.WriteLine("Moveing from TemporaryFolder to assigned folder");
                                BasicFunctions.CopyFolder(result.BinFolder.FullName, pkg);
                                Console.WriteLine("Creating shortcut.");
                                ShortcutCreator.Create(Path.Combine(pkg, package.MainExecutable.fileName), package.MainExecutable.targetDisplayName);
                                Console.WriteLine("Registering Package.");
                                InstalledPackage.RegisterPackage(pkginfo.FriendlyName, pkg);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Completed.");
                                Console.ForegroundColor = ConsoleColor.White;
                                //package.MainExecutable;
                                //Directory.Move(result.BinFolder.FullName, pkg);
                            }
                            else
                            {
                                Console.WriteLine("Failed.");
                                Console.WriteLine(""+result.DetailedMessage);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error:"+e.Message);
                        }
                        break;
                    case "REMOVE":
                        {

                        }
                        break;
                    case "VERSION":
                        {
                            Console.Write("UAP,Core:");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(PackageCore.CoreVersion.ToString());
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("UAP,CLI:");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(CLIVerion.ToString());
                            Console.ForegroundColor = ConsoleColor.White;

                        }
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
                ShowHelpContent();
            }
        }
    }
}
