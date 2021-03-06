﻿using System;

namespace DevEnvAzure
{
    public partial class ClientConfiguration
    {
        const string RootURL = "https://nokscootth.sharepoint.com/";
        public static ClientConfiguration Default { get { return NokScoot; } }
        public static ClientConfiguration NokScoot = new ClientConfiguration()
        {
            ActiveDirectoryResource = RootURL,
            // If wanted to communicate with http - no adal lib
            //ActiveDirectoryTenant = "https://login.microsoftonline.com/287f1604-8fc2-4b4b-8c61-4641962f35fd/oauth2/token",
            ActiveDirectoryTenant = "https://login.microsoftonline.com/287f1604-8fc2-4b4b-8c61-4641962f35fd",
            ActiveDirectoryClientAppId = "1e7bd4f8-f87f-442e-b5ab-5056de3aa58b",
            SPRootURL = string.Format("{0}_api/", RootURL),
            SPRootURLList = string.Format("{0}_api/web/lists/", RootURL),
            SPListURL = "https://nokscootth.sharepoint.com/_api/web/lists/GetByTitle('{0}')/items",
            SPDocumentLibraryURL = string.Format("{0}_api/web/GetFolderByServerRelativeUrl('/SampleDocuments')/Folders", RootURL),
            GraphAPIRootURL = "https://graph.microsoft.com/",
            GraphAPISPListsURL = "https://graph.microsoft.com/v1.0/sites/nokscootth.sharepoint.com,ec5ff37b-ef68-4ca7-abe1-c5bf5ad6ec34,de8acf80-b1e4-4538-8cf1-206f1ca37d49/lists/{0}/{1}",
            GraphAPISPListRootURL = "https://graph.microsoft.com/v1.0/sites/root/lists/{0}/{1}",
            APPNAME = "NokScoot-Sharepoint-Mobile",
            AttachmentOptions = new string[] { "Camera", "Gallery" },
            TLSVersion = "",
            SHORTFORMURL = "SP.Data.Test_x005f_SSRListItem",
            LogoutURL = "https://login.microsoftonline.com/287f1604-8fc2-4b4b-8c61-4641962f35fd/oauth2/logout"
        };

        //public static ClientConfiguration OneBox = new ClientConfiguration()
        //{
        //    ActiveDirectoryResource = "https://sptechnophiles.sharepoint.com/",
        //    ActiveDirectoryTenant = "https://login.microsoftonline.com/542381e6-b9d2-4fe3-a20b-e575f656c08c/oauth2/token",
        //    ActiveDirectoryClientAppId = "d4c9dc64-803f-4dce-842c-380ce91f60d4", //sptechnophiles            // Insert here the application secret when authenticate with AAD by the application  
        //    SPRootURL = "https://sptechnophiles.sharepoint.com/_api/",
        //    SPRootURLList = "https://sptechnophiles.sharepoint.com/_api/web/lists/",
        //    SPListURL = "https://sptechnophiles.sharepoint.com/_api/web/lists/GetByTitle('{0}')/items",
        //    SPDocumentLibraryURL = "https://sptechnophiles.sharepoint.com/_api/web/GetFolderByServerRelativeUrl('/SampleDocuments')/Folders",
        //    GraphAPIRootURL = "https://graph.windows.net/",
        //    GraphAPIURL = "https://graph.windows.net/542381e6-b9d2-4fe3-a20b-e575f656c08c",

        //    //ActiveDirectoryResource = "https://nok365.sharepoint.com/",
        //    //ActiveDirectoryTenant = "https://login.microsoftonline.com/9c32f22b-4146-43ec-b31c-b65699517707/oauth2/token",
        //    //SPRootURL = "https://nok365.sharepoint.com/_api/",
        //    //SPRootURLList = "https://nok365.sharepoint.com/_api/web/lists/"
        //    //ActiveDirectoryClientAppId = "561ad9e0-db4b-4cdd-b050-16a08d9f8709",

        //    TLSVersion = ""
        //};

        public string TLSVersion { get; set; }
        public string UriString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ActiveDirectoryResource { get; set; }
        public String ActiveDirectoryTenant { get; set; }
        public String ActiveDirectoryClientAppId { get; set; }
        //Client secret is for web app/api , the below is invalid - if you want web app access correct the secret and use GetAuthenticationHeader(useWebAppAuthentication = true)
        public string ActiveDirectoryClientAppSecret { get; set; }
        public string SPRootURL { get; set; }
        public string APPNAME { get; set; }
        public string SPRootURLList { get; set; }
        public string SPDocumentLibraryURL { get; set; }
        public string GraphAPIRootURL { get; set; }
        public string GraphAPISPListsURL { get; set; }
        public string GraphAPISPListRootURL { get; set; }
        public string[] AttachmentOptions { get; set; }
        public string SPListURL { get; private set; }
        public string SHORTFORMURL { get; set; }
        public string LogoutURL { get; set; }
    }
}