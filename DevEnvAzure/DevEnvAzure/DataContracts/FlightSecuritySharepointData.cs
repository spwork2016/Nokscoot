using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace DevEnvAzure.DataContracts
{
    class FlightSecuritySharepointData
    {

        public FlightSecuritySharepointData()
        {
            this.__metadata = new Metadata();
            this.__metadata.type = ClientConfiguration.Default.SHORTFORMURL;
        }
        public Metadata __metadata { get; set; }
        [JsonProperty(PropertyName = "Title_x0020_of_x0020_Event_Hazar")]
        public string Type_x0020_of_x0020_Report { get; set; }

        [JsonProperty(PropertyName = "Title")]
        public string Event_x0020_Title { get; set; }
        public string Date_x0020_of_x0020_Event { get; set; }
        public string Aircraft_x0020_Registration { get; set; }
        public string Departure_x0020_Station { get; set; }
        public string Arrival_x0020_Station { get; set; }
        public string Divert_x0020_Station { get; set; }
        [JsonProperty(PropertyName = "Flight_x0020_Number1")]
        public string FlightNumber
        { get; set; }
        [JsonProperty(PropertyName = "Location_Station_Area_FIR_x0020_")]
        public string Location_Station_Area_FIR_x0020_of_x0020_Event { get; set; }
        [JsonProperty(PropertyName = "Details_x0020_of_x0020_Event_x00")]
        public string Details_x0020_of_x0020_Event_x0020__x0020_Hazard { get; set; }
        public string Attachments { get; set; }
        public string MOR_x0020_TypeId { get; set; }
        [JsonProperty(PropertyName = "Confidential_x0020_Report.value")]
        public string CONFIDENTIAL_x0020_REPORT { get; set; }
        [JsonProperty(PropertyName = "SendNotifications.value")]
        public string Send_x0020_Notifications { get; set; }
        //   [JsonProperty(PropertyName = "Others_x0020_Persons_x0020_Invol")]
        [JsonProperty(PropertyName = "Others_x0020_Involved")]
        public string Others_x0020_Persons_x0020_Invol { get; set; }

        [JsonProperty(PropertyName = "If_x0020_on_x0020_ground_x0020__")]
        public string groudWhere { get; set; }
        [JsonProperty(PropertyName = "If_x0020_flight_x0020_event_x002")]
        public string FlightEvent { get; set; }
        [JsonProperty(PropertyName = "SecurityEvent_x002d_Type")]
        public string securityEvent { get; set; }
        [JsonProperty(PropertyName = "Person_x0020_submitting_x0020_re")]
        public string NameStaffNumber
        { get; set; }
        [JsonProperty(PropertyName = "Email_x002d_IDOfSubmitter")]
        public string SubmitterEmail
        { get; set; }
        [JsonProperty(PropertyName = "PoliceReport")]
        public SPFieldURL PoliceReport { get; set; }

        [JsonProperty(PropertyName = "MobileEntry")]
        public bool MobileEntry { get; set; }

        //public string Employee_x0020_Name { get; set; }
        //public int? Employee_x0020_Age { get; set; }
        //public int? DepartmentId { get; set; }
        //public string Employee_x0020_Details { get; set; }
        //public string Gender { get; set; }
        //public int? Salary { get; set; }
        //public DateTime? Joining_x0020_Date { get; set; }
        //public bool Active_x0020_Employee { get; set; }



        //this.__metadata = new Metadata();
        //this.__metadata.type = "SP.Data.TestFormListItem";
    }

    //   protected Metadata __metadata { get; set; }
    //protected class Metadata
    //{
    //    public string id { get; set; }
    //    public string uri { get; set; }
    //    public string etag { get; set; }
    //    public string type { get; set; }
    //}
}
