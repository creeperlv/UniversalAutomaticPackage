using System;
using System.Collections.Generic;
using System.Text;

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
            return true;
        }
        public bool Install()
        {
            return true;
        }
    }
}
