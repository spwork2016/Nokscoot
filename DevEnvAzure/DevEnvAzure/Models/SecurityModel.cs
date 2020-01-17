using SQLite;
using System;
namespace DevEnvAzure.Models
{
    public class OfflineItem
    {
        [NotNull]
        [PrimaryKey, AutoIncrement, Column("contentID")]
        public long ContentID
        { get; set; }
        public string Value
        { set; get; }
        public int ReportType { get; set; }
        public DateTime Created { get; set; }
        public string Error { get; set; }
        public bool InProgress { get; set; }
        public string Attachments { get; set; }
    }

    public class MasterInfo
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public long Id
        { get; set; }
        [NotNull]
        public string Name
        { get; set; }
        [NotNull]
        public string content
        { get; set; }
    }

    public class SecurityModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        { get; set; }

        public string ReportType
        { get; set; }
        public string EventTitle
        { get; set; }
        public DateTime DateOfEvent
        { get; set; }
        public string FlightNumber
        { get; set; }
        public string AircraftRegistration
        { get; set; }
        public string DepartureStation
        { get; set; }
        public string ArrivalStation
        { get; set; }
        public string DivertStation
        { get; set; }
        public string Area_FIR
        { get; set; }
        public string DescribeEvent
        { get; set; }
        public string Attachment
        { get; set; }
        public string MOR
        { get; set; }
        public bool ConfiReport
        { get; set; }
        public string pax
        { get; set; }
        public bool ssQ
        { get; set; }

        public int flightEvent
        { get; set; }
        public int onGround
        { get; set; }
        public string securityEvent
        { get; set; }
        public string policereport
        { get; set; }
        public bool IsExtendedView { get; set; }
        public string NameStaffNumber
        { get; set; }
        public string SubmitterEmail
        { get; set; }


        //local usage - to show datetime in drafts page
        public DateTime Created
        {
            get; set;
        }

        //[JsonProperty(PropertyName = "Details_x0020_of_x0020_Event_x0020_/_x0020_Hazard")]
        //public string policeReport
        //{ get; set; }
        //[JsonProperty(PropertyName = "Details_x0020_of_x0020_Event_x0020_/_x0020_Hazard")]
        //public int flightphase
        //{ get; set; }
        //[JsonProperty(PropertyName = "Details_x0020_of_x0020_Event_x0020_/_x0020_Hazard")]
        //public int flightwhere
        //{ get; set; }

        public string Attachments { get; set; }
    }
}

