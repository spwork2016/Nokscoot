using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEnvAzure.DataContracts
{
    class FlightCrewVoyageRecordSp
    {
        public FlightCrewVoyageRecordSp()
        {
            this.__metadata = new Metadata();
            //this.__metadata.type = "SP.Data.Flight_x0020_Crew_x0020_Voyage_x0020_RecordListItem";
            this.__metadata.type = "SP.Data.Flight_x0020_Crew_x0020_Voyage_x0020_RecordListItems";
            //
        }
        public Metadata __metadata { get; set; }

        [JsonProperty(PropertyName = "Title")]
        public string VoyageRecord
        { get; set; }
        [JsonProperty(PropertyName = "SectorNumber")]
        public string SectorNumber
        { get; set; }
        [JsonProperty(PropertyName = "ScheduledDeparture")]
        public string ScheduledDeparture
        { get; set; }
        [JsonProperty(PropertyName = "FltNumber")]
        public string FlightNumber
        { get; set; }
        [JsonProperty(PropertyName = "AircraftRegistration")]
        public string AircraftRegistration
        { get; set; }
        [JsonProperty(PropertyName = "DepartureStation")]
        public string DepartureStation
        { get; set; }
        [JsonProperty(PropertyName = "ArrivalStation")]
        public string ArrivalStation
        { get; set; }
        [JsonProperty(PropertyName = "LandingBy")]
        public string LandingBy
        { get; set; }
        [JsonProperty(PropertyName = "LandingManualOrAuto")]
        public string ManualorAuto
        { get; set; }
        [JsonProperty(PropertyName = "TitleOfReport")]
        public string TitleofReport
        { get; set; }
        [JsonProperty(PropertyName = "ReportCategories")]
        public string ReportCategories
        { get; set; }
        [JsonProperty(PropertyName = "ReportDetails")]
        public string ReportDetails
        { get; set; }
        [JsonProperty(PropertyName = "ReportRaisedByStringId")]
        public string ReportRaisedBy
        { get; set; }
        [JsonProperty(PropertyName = "ReplyRequired")]
        public string ReplyRequired
        { get; set; }
        [JsonProperty(PropertyName = "StaffNumber")]
        public string StaffNumber
        { get; set; }
        [JsonProperty(PropertyName = "Rank")]
        public string Rank
        { get; set; }
        [JsonProperty(PropertyName = "CommandersE_x002d_mailStringId")]
        public string CmdEmail
        { get; set; }

        
    }
}
