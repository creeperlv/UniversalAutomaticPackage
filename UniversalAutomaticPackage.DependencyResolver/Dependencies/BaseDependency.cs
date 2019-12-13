using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalAutomaticPackage.DependencyResolver.Dependencies
{
    public class BaseDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>0.0.0.0 when do not exist,(int.MaxValue,int.MaxValue,int.MaxValue,int.MaxValue) when fail to get version</returns>
        public virtual Version Find()
        {
            
            return new Version(0, 0, 0, 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>False - fail, True - succeed</returns>
        public virtual bool Install()
        {
            return false;
        }
    }
}
