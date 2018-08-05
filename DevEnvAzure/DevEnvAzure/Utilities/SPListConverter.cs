using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace DevEnvAzure.Model
{
    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string etag { get; set; }
        public string type { get; set; }
    }

    public class Deferred
    {
        public string uri { get; set; }
    }

    public class FirstUniqueAncestorSecurableObject
    {
        public Deferred __deferred { get; set; }
    }

    public class RoleAssignments
    {
        public Deferred __deferred { get; set; }
    }

    public class Activities
    {
        public Deferred __deferred { get; set; }
    }

    public class ContentType
    {
        public Deferred __deferred { get; set; }
    }

    public class GetDlpPolicyTip
    {
        public Deferred __deferred { get; set; }
    }

    public class FieldValuesAsHtml
    {
        public Deferred __deferred { get; set; }
    }

    public class FieldValuesAsText
    {
        public Deferred __deferred { get; set; }
    }

    public class FieldValuesForEdit
    {
        public Deferred __deferred { get; set; }
    }

    //public class File
    //{
    //    public Deferred __deferred { get; set; }
    //}

    public class Folder
    {
        public Deferred __deferred { get; set; }
    }

    public class ParentList
    {
        public Deferred __deferred { get; set; }
    }

    public class Properties
    {
        public Deferred __deferred { get; set; }
    }

    public class Versions
    {
        public Deferred __deferred { get; set; }
    }

    public class Alerts
    {
        public Deferred __deferred { get; set; }
    }

    public class Groups
    {
        public Deferred __deferred { get; set; }
    }

    public class UserId
    {
        public Deferred __metadata { get; set; }
        public string NameId { get; set; }
        public string NameIdIssuer { get; set; }
    }

    public class Result
    {
        //user fields 
        public Metadata __metadata { get; set; }
        public Alerts Alerts { get; set; }
        public Groups Groups { get; set; }
        public int Id { get; set; }
        public bool IsHiddenInUI { get; set; }
        public string LoginName { get; set; }
        public string Title { get; set; }
        public int PrincipalType { get; set; }
        public string Email { get; set; }
        public bool IsEmailAuthenticationGuestUser { get; set; }
        public bool IsShareByEmailGuestUser { get; set; }
        public bool IsSiteAdmin { get; set; }
        public UserId UserId { get; set; }
        //user fields end

        public FirstUniqueAncestorSecurableObject FirstUniqueAncestorSecurableObject { get; set; }
        public RoleAssignments RoleAssignments { get; set; }
        public Activities Activities { get; set; }
        public ContentType ContentType { get; set; }
        public GetDlpPolicyTip GetDlpPolicyTip { get; set; }
        public FieldValuesAsHtml FieldValuesAsHtml { get; set; }
        public FieldValuesAsText FieldValuesAsText { get; set; }
        public FieldValuesForEdit FieldValuesForEdit { get; set; }
        //public File File { get; set; }
        public Folder Folder { get; set; }
        public ParentList ParentList { get; set; }
        public Properties Properties { get; set; }
        public Versions Versions { get; set; }
        public int FileSystemObjectType { get; set; }
        public object ServerRedirectedEmbedUri { get; set; }
        public string ServerRedirectedEmbedUrl { get; set; }
        public int ID { get; set; }
        public string ContentTypeId { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public int AuthorId { get; set; }
        public int EditorId { get; set; }
        public string OData__UIVersionString { get; set; }
        public bool Attachments { get; set; }
        public string GUID { get; set; }
        public object ComplianceAssetId { get; set; }
        public string DepartmentName { get; set; }
        public string Employee_x0020_Name { get; set; }
        public string Gender { get; set; }

        //Tasks
        [JsonProperty(PropertyName = "odata.editLink")]
        public string Link { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public int PercentComplete { get; set; }
        public int AssignedToId { get; set; }
        public string AssignedToStringId { get; set; }
        public string Body { get; set; }
        public object StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Checkmark { get; set; }
        public string RelatedItems { get; set; }
        public object PreviouslyAssignedToStringId { get; set; }
        public object TaskOutcome { get; set; }
        public object UpdateDe { get; set; }
        public object DEPT_x0020_CODE { get; set; }
        public object Change_x0020_Update { get; set; }

        public string Flight_x0020_Number { get; set; }
        public int Departure_x0020_StationId { get; set; }
        public int Arrival_x0020_StationId { get; set; }
        public string IATA_x0020_Code { get; set; }
        public string Aircraft_x0020_Registration { get; set; }
        public string AircraftRegistrationNew { get; set; }
        public string City_x0020_Name { get; set; }
        public string Country { get; set; }
        public string Airport_x0020_Type { get; set; }
    }

    public class UserProfileProperties
    {
        public IList<Result> results { get; set; }

    }
    public class DirectReports
    {
        public Metadata __metadata { get; set; }
        public IList<object> results { get; set; }
    }

    public class ExtendedManagers
    {
        public Metadata __metadata { get; set; }
        public IList<object> results { get; set; }
    }

    public class ExtendedReports
    {
        public Metadata __metadata { get; set; }
        public IList<string> results { get; set; }
    }

    public class Peers
    {
        public Metadata __metadata { get; set; }
        public IList<object> results { get; set; }
    }

    public class D
    {
        //user fields 
        public Metadata __metadata { get; set; }
        public string AccountName { get; set; }
        public DirectReports DirectReports { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public ExtendedManagers ExtendedManagers { get; set; }
        public ExtendedReports ExtendedReports { get; set; }
        public bool IsFollowed { get; set; }
        public object LatestPost { get; set; }
        public Peers Peers { get; set; }
        public string PersonalSiteHostUrl { get; set; }
        public string PersonalUrl { get; set; }
        public string PictureUrl { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public UserProfileProperties UserProfileProperties { get; set; }
        public string UserUrl { get; set; }

        public IList<Result> results { get; set; }
    }

    public class SPData
    {
        public D d { get; set; }
    }

    public class StationInformationSpRoot
    {
        public DataContracts.StationInformationSp d { get; set; }
    }

    public class FileNameAsPath
    {
        public string DecodedUrl { get; set; }
    }

    public class ServerRelativePath
    {
        public string DecodedUrl { get; set; }
    }

    public class Value
    {
        public string FileName { get; set; }
        public FileNameAsPath FileNameAsPath { get; set; }
        public ServerRelativePath ServerRelativePath { get; set; }
        public string ServerRelativeUrl { get; set; }
        public string Flight_x0020_Number { get; set; }

    }

    public class LookUp
    {
        public Deferred __deferred { get; set; }
        public List<Value> value { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PictureBytes { get; set; }
    }

}
