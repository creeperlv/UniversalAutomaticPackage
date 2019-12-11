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
    }

    [System.Serializable]
    public class PackageInformation
    {
        public string FriendlyName = "Undefined";
        public string PackageID = "Undefined";
        public string PublisherName = "Undefined";
        public string PackageHomePage = "Undefined";
        public PackageVersion PackageVersion;
    }
    public class BasePackage
    {
        public virtual InstallationResult Install()
        {
            return new InstallationResult() { Status = InstallationStatus.Fail, DetailedMessage = "Not implemented." };
        }
        public virtual PackageInformation GetInfomation()
        {
            return new PackageInformation();
        }
    }
}
