using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalAutomaticPackage.PackageSystem
{
    public struct InstallationResult
    {
        public InstallationStatus Status;
        public string DetailedMessage;
    }
    public enum InstallationStatus
    {
        Succeed,
        Fail,
        SucceedWithWarnings
    }
    public class PackageVersion
    {
        public Version Version { get; private set; }
        public string CodeName { get; private set; }
        public PackageVersion(int major=0, int minor=0, int build=0, int revision=0,string CodeName="")
        {
            Version = new Version(major, minor, build, revision);
            this.CodeName = CodeName;
        }
        public static PackageVersion Parse(string s)
        {
            PackageVersion version = new PackageVersion();
            var vers = s.Split(',');
            version.Version = new Version(vers[0]);
            if (vers.Length > 1)
            {
                version.CodeName = s.Substring(s.IndexOf(',') + 1);
            }
            return version;
        }
    }
    public enum PackageType
    {
        Source,
        Binary
    }
    [System.Serializable]
    public class PackageInformation
    {
        public string FriendlyName = "Undefined";
        public Guid PackageID = Guid.Empty;
        public string PublisherName = "Undefined";
        public string PackageHomePage = "Undefined";
        public PackageVersion PackageVersion;
        public PackageType PackageType = PackageType.Source;
        public string LicenseURL = "Undefined";
        public string UpdateOrigin="Undefined";
    }
    public class BasePackage
    {
        /// <summary>
        /// Install method.
        /// </summary>
        /// <param name="Progress"> Indicates a progress </param>
        /// <returns></returns>
        public virtual InstallationResult Install(ref double Progress)
        {
            return new InstallationResult() { Status = InstallationStatus.Fail, DetailedMessage = "Not implemented." };
        }
        public virtual PackageInformation GetInfomation()
        {
            return new PackageInformation();
        }
    }
}
