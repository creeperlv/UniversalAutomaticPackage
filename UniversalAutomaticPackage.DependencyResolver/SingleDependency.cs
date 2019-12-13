using System;
using System.Collections.Generic;
using System.Text;
using UniversalAutomaticPackage.DependencyResolver.Dependencies;

namespace UniversalAutomaticPackage.DependencyResolver
{
    public class SingleDependency
    {
        string name;
        string version;
        public SingleDependency(string name,string version)
        {
            this.name = name;
            this.version = version;
        }
        public bool Check()
        {
            Version DepVersion = new Version(0, 0, 0, 0);
            switch (name.ToUpper())
            {
                case "PWSH":case "POWERSHELL":
                    {

                        PowerShell dep = new PowerShell();
                        DepVersion = dep.Find();
                    }
                    break;
                default:
                    Console.WriteLine("Dependency not in database.");
                    return true;
            }
            if(DepVersion==new Version(0, 0, 0, 0))
            {
                return false;
            }
            else
            {
                if (version.ToUpper() == "ALL")
                {
                    return true;
                }
                else
                {
                    Version tar = new Version(version);
                    if (DepVersion.CompareTo(tar) > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool Install()
        {
            switch (name.ToUpper())
            {
                case "PWSH":
                case "POWERSHELL":
                    {

                        PowerShell dep = new PowerShell();
                        return dep.Install();
                    }
                    break;
                default:
                    break;
            }
            return false;
        }
    }
}
