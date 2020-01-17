using Newtonsoft.Json;

namespace DevEnvAzure.DataContracts
{
    class FlightCrewVoyageRecordSp
    {
        public FlightCrewVoyageRecordSp()
        {
            __metadata = new Metadata();
            __metadata.type = "SP.Data.Flight_x0020_Crew_x0020_Voyage_x0020_RecordListItem";
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
        [JsonProperty(PropertyName = "Flt_x0020_NumberId")]
        public string FlightNumber
        { get; set; }
        [JsonProperty(PropertyName = "Aircraft_x0020_RegistrationId")]
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

        [JsonProperty(PropertyName = "MobileEntry")]
        public bool MobileEntry { get; set; }

    }
}
