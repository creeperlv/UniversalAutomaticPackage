using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UniversalAutomaticPackage.ScriptSystem
{
    public class UAPScript
    {
        public DirectoryInfo WorkingDirectory;
        public static Dictionary<string, Func<string, UAPScript, Dictionary<string, string>,bool>> functions = new Dictionary<string, Func<string, UAPScript, Dictionary<string, string>, bool>>();
        string file;
        public UAPScript(string file)
        {
            this.file = file;
        }
        public void Execute()
        {
            var lines = File.ReadAllLines(file);
            Dictionary<string, string> para = new Dictionary<string, string>();
            Func<string, UAPScript, Dictionary<string, string>, bool> func=null;
            string para1="";
            int lineNumber = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("\t"))
                {
                    if (lines[i].IndexOf(":") > 0)
                    {
                        var key = lines[i].Substring(0, lines[i].IndexOf(":"));
                        var value = lines[i].Substring(lines[i].IndexOf(":") + 1);
                        para.Add(key, value);
                    }
                    else
                    {
                        para.Add("" + lineNumber, lines[i]);
                        lineNumber++;
                    }
                }
                else{
                    para = new Dictionary<string, string>();
                    if (func != null)
                    {
                        if(func(para1, this, para) != true)
                        {
                            Host.SetForeground(ConsoleColor.Red);
                            Host.WriteLine("Script error, interrupted!");
                            Host.SetForeground(ConsoleColor.White);
                            return;
                        }
                    }
                    lineNumber = 0;
                    para1 = lines[i].Substring(lines[i].IndexOf(':') + 1);
                    func = functions[lines[i].Substring(0, lines[i].IndexOf(":"))];
                    //func()
                }
            }
        }
    }
}
