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

    public class AttachmentFiles
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

    public class File
    {
        public Deferred __deferred { get; set; }
    }

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
        public AttachmentFiles AttachmentFiles { get; set; }
        public ContentType ContentType { get; set; }
        public GetDlpPolicyTip GetDlpPolicyTip { get; set; }
        public FieldValuesAsHtml FieldValuesAsHtml { get; set; }
        public FieldValuesAsText FieldValuesAsText { get; set; }
        public FieldValuesForEdit FieldValuesForEdit { get; set; }
        public File File { get; set; }
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
    }

    public class D
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
        public string PictureUrl { get; set; }

        public IList<Result> results { get; set; }
    }

    public class SPData
    {
        public D d { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PictureUrl { get; set; }
    }
}
