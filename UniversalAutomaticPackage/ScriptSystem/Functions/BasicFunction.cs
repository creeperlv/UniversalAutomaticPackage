using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UniversalAutomaticPackage.ScriptSystem.Functions
{
    public class BasicFunctions
    {
        public static void Init()
        {
            UAPScript.functions.Add("ScriptType", ScriptType);
        }
        static bool ScriptType(string s,UAPScript UAPScriptEnv,Dictionary<string,string> parameters)
        {
            if (s.ToUpper().Trim().Equals("INSTALL"))
            {
             
            }
            return true;
        }
        static bool CheckEnvironment(string s,UAPScript UAPScriptEnv, Dictionary<string, string> parameters)
        {
            return true;
        }
    }
}
