﻿using System;
using System.Collections.Generic;
using System.IO;

namespace UniversalAutomaticPackage.Configuration
{
    public class StandardConfigurationFile
    {
        public static readonly Version FileStandardVersion = new Version(1, 0, 0, 0);
        Dictionary<string, List<string>> Data = new Dictionary<string, List<string>>();
        public StandardConfigurationFile()
        {

        }
        string IniFile = "";
        public StandardConfigurationFile(string location)
        {
            IniFile = location;
            LoadFromFile();
        }
        public List<string> GetValues(string key, string Fallback = "")
        {
            if (Data.ContainsKey(key)) return Data[key];
            else
            {
                List<string> vs = new List<string>();
                vs.Add(Fallback);
                return vs;
            }
        }
        public void RemoveKey(string key)
        {
            if(Data.ContainsKey(key))
            Data.Remove(key);
        }
        public void RemoveAt(string key,int id)
        {
            if (Data.ContainsKey(key))
                Data[key].RemoveAt(id);
        }
        public void AddValue(string key, string value)
        {
            if (Data.ContainsKey(key)) Data[key].Add(value);
            else
            {
                Data.Add(key, new List<string>());
                Data[key].Add(value);
            }
        }
        public void UpdateValue(string key,int id, string value)
        {
            if (Data.ContainsKey(key))
            {
                if (Data[key].Count > id)
                {
                    Data[key][id] = value;
                }
            }
            else
            {
                Data.Add(key, new List<string>());
                Data[key].Add(value);
            }
        }
        public void LoadFromFile()
        {
            try
            {
                Data = new Dictionary<string, List<string>>();
                var lines = File.ReadAllLines(IniFile);
                foreach (var item in lines)
                {
                    if (item.IndexOf('=') > 0 && (!item.StartsWith("#")))
                    {
                        string key = item.Substring(0, item.IndexOf("="));
                        string value = item.Substring(item.IndexOf("=") + 1);
                        if (Data.ContainsKey(key))
                        {
                            Data[key].Add(value);
                        }
                        else
                        {
                            Data.Add(key, new List<string>());
                            Data[key].Add(value);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        public void Save(string comment = "")
        {
            File.WriteAllText(IniFile, $"[Generated by UniversalAutomaticPackage.Configuration,{FileStandardVersion.ToString()}]");
            if (comment != "")
            {
                File.AppendAllText(IniFile, "" + comment);
            }
            foreach (var item in Data)
            {
                foreach (var value in item.Value)
                {
                    File.AppendAllText(IniFile, Environment.NewLine + $"{item.Key}={value}");
                }
            }
        }
        public void SaveToFile(string location, string comment = "")
        {
            string OriLoca = IniFile;
            IniFile = location;
            Save(comment);
            IniFile = OriLoca;
        }
        public void LoadFromFile(string location)
        {
            IniFile = location;
            LoadFromFile();
        }
        public static StandardConfigurationFile InitFromFile(string location)
        {
            StandardConfigurationFile standardConfigurationFile = new StandardConfigurationFile(location);
            return standardConfigurationFile;
        }
    }
}
