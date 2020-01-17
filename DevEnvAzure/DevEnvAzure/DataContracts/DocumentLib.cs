using DevEnvAzure.Model;
using System;
using System.Collections.Generic;

namespace DevEnvAzure.DataContracts
{
    public class Activities
    {
        public Deferred __deferred { get; set; }
    }

    public class Files
    {
        public Deferred __deferred { get; set; }
    }

    public class ListItemAllFields
    {
        public Deferred __deferred { get; set; }
    }

    public class ParentFolder
    {
        public Deferred __deferred { get; set; }
    }

    public class Properties
    {
        public Deferred __deferred { get; set; }
    }

    public class StorageMetrics
    {
        public Deferred __deferred { get; set; }
    }

    public class Folders
    {
        public Deferred __deferred { get; set; }
    }

    public class Author
    {
        public Deferred __deferred { get; set; }
    }

    public class CheckedOutByUser
    {
        public Deferred __deferred { get; set; }
    }

    public class EffectiveInformationRightsManagementSettings
    {
        public Deferred __deferred { get; set; }
    }

    public class InformationRightsManagementSettings
    {
        public Deferred __deferred { get; set; }
    }

    public class LockedByUser
    {
        public Deferred __deferred { get; set; }
    }

    public class ModifiedBy
    {
        public Deferred __deferred { get; set; }
    }

    public class VersionEvents
    {
        public Deferred __deferred { get; set; }
    }

    public class Versions
    {
        public Deferred __deferred { get; set; }
    }

    public class Result
    {
        public Metadata __metadata { get; set; }
        public Activities Activities { get; set; }
        public Files Files { get; set; }
        public ListItemAllFields ListItemAllFields { get; set; }
        public ParentFolder ParentFolder { get; set; }
        public Properties Properties { get; set; }
        public StorageMetrics StorageMetrics { get; set; }
        public Folders Folders { get; set; }
        public bool Exists { get; set; }
        public bool IsWOPIEnabled { get; set; }
        public int ItemCount { get; set; }
        public string Name { get; set; }
        public object ProgID { get; set; }
        public string ServerRelativeUrl { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeLastModified { get; set; }
        public string UniqueId { get; set; }
        public string WelcomePage { get; set; }

        //#Files
        public Author Author { get; set; }
        public CheckedOutByUser CheckedOutByUser { get; set; }
        public EffectiveInformationRightsManagementSettings EffectiveInformationRightsManagementSettings { get; set; }
        public InformationRightsManagementSettings InformationRightsManagementSettings { get; set; }
        public LockedByUser LockedByUser { get; set; }
        public ModifiedBy ModifiedBy { get; set; }
        public VersionEvents VersionEvents { get; set; }
        public Versions Versions { get; set; }
        public string CheckInComment { get; set; }
        public int CheckOutType { get; set; }
        public string ContentTag { get; set; }
        public int CustomizedPageStatus { get; set; }
        public string ETag { get; set; }
        public bool IrmEnabled { get; set; }
        public string Length { get; set; }
        public int Level { get; set; }
        public string LinkingUri { get; set; }
        public string LinkingUrl { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public string Title { get; set; }
        public int UIVersion { get; set; }
        public string UIVersionLabel { get; set; }

        public string Icon { get; set; }
    }

    public class D
    {
        public IList<Result> results { get; set; }
    }

    public class SPFolder
    {
        public D d { get; set; }
    }
}
