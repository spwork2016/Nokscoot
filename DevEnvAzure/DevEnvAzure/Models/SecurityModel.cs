using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevEnvAzure.DataContracts;
using Newtonsoft.Json;
using SQLite.Net.Attributes;
namespace DevEnvAzure.Models
{
    //public class Metadata
    //{
    //    public string id { get; set; }
    //    public string uri { get; set; }
    //    public string etag { get; set; }
    //    public string type { get; set; }
    //}
    [Table(@"SecurityModel")]
    public class SecurityModel
    {

        //public SecurityModel()
        //{
        //    this.__metadata = new Metadata();
        //    this.__metadata.type = "SP.Data.Operational_x005f_Hazard_x005f_Event_x005f_Register_x005f_04042018ListItem";
        //}
        //public Metadata __metadata { get; set; }
        //[NotNull]
        //[PrimaryKey, AutoIncrement, Column("SecurityID")]
        //public long SecurityID
        //{ get; set; }

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
        public int AircraftRegis
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
    }
}

