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
    public class SecurityModel
    {
        public SecurityModel()
        {
            this.__metadata = new Metadata();
            this.__metadata.type = "SP.Data.Operational_Hazard_Event_Register_x0020_25_x0020_Jan_x0020_18ListItem";
        }
        public Metadata __metadata { get; set; }
        [PrimaryKey, AutoIncrement, Column("SecurityID")]
        public long SecurityID
        { get; set; }
        [NotNull]
        [JsonProperty(PropertyName = "Type_x0020_of_x0020_Report")]
        public string ReportType
        { get; set; }
        [JsonProperty(PropertyName = "Event_x0020_Title")]
        public string EventTitle
        { get; set; }
        [JsonProperty(PropertyName = "Date_x0020_of_x0020_Event")]
        public DateTime DateOfEvent
        { get; set; }
        [JsonProperty(PropertyName = "Flight_x0020_Number")]
        public string FlightNumber
        { get; set; }
        [JsonProperty(PropertyName = "Aircraft_x0020_Registration")]
        public int AircraftRegis
        { get; set; }
        [JsonProperty(PropertyName = "Departure_x0020_Station")]
        public string DepartureStation
        { get; set; }
        [JsonProperty(PropertyName = "Arrival_x0020_Station")]
        public string ArrivalStation
        { get; set; }
        [JsonProperty(PropertyName = "Divert_x0020_Station")]
        public string DivertStation
        { get; set; }
        [JsonProperty(PropertyName = "Location_Station_Area_FIR_x0020_")]
        public string Area_FIR
        { get; set; }
        [JsonProperty(PropertyName = "Details_x0020_of_x0020_Event_x00")]
        public string DescribeEvent
        { get; set; }
        [JsonProperty(PropertyName = "Attachments")]
        public string Attachment
        { get; set; }
        [JsonProperty(PropertyName = "MOR_x0020_Type")]
        public string MOR
        { get; set; }
        [JsonProperty(PropertyName = "CONFIDENTIAL_x0020_REPORT?")]
        public bool ConfiReport
        { get; set; }
        [JsonProperty(PropertyName = "Others_x0020_Persons_x0020_Invol")]
        public string pax
        { get; set; }
        [JsonProperty(PropertyName = "Send_x0020_Notifications")]
        public bool ssQ
        { get; set; }
        [JsonProperty(PropertyName = "If_x0020_flight_x0020_event_x002")]
        public string flightEvent
        { get; set; }
        [JsonProperty(PropertyName = "If_x0020_on_x0020_ground_x0020__")]
        public string onGround
        { get; set; }
        [JsonProperty(PropertyName = "Security_x0020_Event_x0020__x002")]
        public string securityEvent
        { get; set; }
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

