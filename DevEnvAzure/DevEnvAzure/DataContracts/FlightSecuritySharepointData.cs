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
            this.__metadata.type = "SP.Data.Operational_Hazard_Event_Register 25 Jan 18ListItem";
        }
        public Metadata __metadata { get; set; }
        public string Type_x0020_of_x0020_Report { get; set; }
        public object Event_x0020_Title { get; set; }
        public string Date_x0020_of_x0020_Event { get; set; }
        public string Aircraft_x0020_Registration { get; set; }
        public string Departure_x0020_Station { get; set; }
        public string Arrival_x0020_Station { get; set; }
        public string Divert_x0020_Station { get; set; }
        public string Location_Station_Area_FIR_x0020_of_x0020_Event { get; set; }
        [JsonProperty(PropertyName = "Details_x0020_of_x0020_Event_x0020_/_x0020_Hazard")]
        public string Details_x0020_of_x0020_Event_x0020__x0020_Hazard { get; set; } 
        public string Attachments { get; set; }
        public string MOR_x0020_Type { get; set; }
        [JsonProperty(PropertyName = "CONFIDENTIAL_x0020_REPORT?")]
        public string CONFIDENTIAL_x0020_REPORT {get;set;}
        public string Send_x0020_Notifications { get; set; }
        [JsonProperty(PropertyName = "Others_x0020_Persons_x0020_Involved(Staff_x0020_or_x0020_Pax)")]
        public string Others_x0020_Persons_x0020_Involved { get; set; }
        [JsonProperty(PropertyName = "If_x0020_on_x0020_ground_x0020_-_x0020_select_x0020_where)")]
        public int? groudWhere { get; set; }
        [JsonProperty(PropertyName = "If_x0020_flight_x0020_event_x0020_-_x0020_select_x0020_phase")]
        public int? FlightEvent { get; set; }
        [JsonProperty(PropertyName = "Security_x0020_Event_x0020_–_x0020_Type")]
        public string securityEvent { get; set; }
      //  public string Police_x0020_Report{ get; set; }


     
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
