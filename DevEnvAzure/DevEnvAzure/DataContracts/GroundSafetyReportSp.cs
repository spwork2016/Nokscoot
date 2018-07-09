using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEnvAzure.DataContracts
{
    class GroundSafetyReportSp
    {
        public GroundSafetyReportSp()
        {
            this.__metadata = new Metadata();
            this.__metadata.type = ClientConfiguration.Default.SHORTFORMURL;
        }
        public Metadata __metadata { get; set; }
        //[PrimaryKey, AutoIncrement, Column("groundId")]
        //public long groundId
        //{ get; set; }
        //[NotNull]
        [JsonProperty(PropertyName = "Title_x0020_of_x0020_Event_Hazar")]
        public string ReportType
        { get; set; }
        [JsonProperty(PropertyName = "Title")]
        public string EventTitle
        { get; set; }
        [JsonProperty(PropertyName = "Date_x0020_of_x0020_Event")]
        public string DateOfEvent
        { get; set; }
        [JsonProperty(PropertyName = "Flight_x0020_Number1")]
        public string FlightNumber
        { get; set; }
        [JsonProperty(PropertyName = "Aircraft_x0020_Registration")]
        public string AircraftRegis
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
        [JsonProperty(PropertyName = "MOR_x0020_TypeId")]
        public string MOR
        { get; set; }
        [JsonProperty(PropertyName = "CONFIDENTIAL_x0020_REPORT.value")]
        public string ConfiReport
        { get; set; }
        [JsonProperty(PropertyName = "Others_x0020_Involved")]
        public string pax
        { get; set; }
        [JsonProperty(PropertyName = "SendNotifications.value")]
        public string ssQ
        { get; set; }

        [JsonProperty(PropertyName = "Light")]
        public string lightIndex
        { get; set; }
        [JsonProperty(PropertyName = "Weather")]
        public string weatherIndex
        { get; set; }
        [JsonProperty(PropertyName = "Precipitation")]
        public string precipitationIndex
        { get; set; }
        [JsonProperty(PropertyName = "Ground_x0020_Conditions")]
        public string groundconditions
        { get; set; }

        [JsonProperty(PropertyName = "Type_x0020_of_x0020_Liquid_x0020")]
        public string typeofLiquidIndex
        { get; set; }
        [JsonProperty(PropertyName = "AmountSpilled")]
        public string amountSpilled
        { get; set; }
        [JsonProperty(PropertyName = "Where_x0020_Spill_x0020_Occurred")] //WhereSpillOccurred
        public string wherespillindex
        { get; set; }

        [JsonProperty(PropertyName = "Load_x0020_Event_x0020_Baggage")]
        public string baggageIndex
        { get; set; }
        [JsonProperty(PropertyName = "Loading_x0020_Event_x0020_Cargo")]
        public string cargoIndex
        { get; set; }
        [JsonProperty(PropertyName = "Loading_x0020_Event_x0020_Load_x")]
        public string loadSheetIndex
        { get; set; }
        [JsonProperty(PropertyName = "Loading_x0020_Event_x0020_Impact")]
        public string impactTocraftIndex
        { get; set; }
        [JsonProperty(PropertyName = "DangerousGoodsPaxOrCargo")]
        public string pax_CargoIndex
        { get; set; }
        [JsonProperty(PropertyName = "DangerousGoodsEventIdentifiedWhe")]
        public string identifiedWhenIndex
        { get; set; }
        [JsonProperty(PropertyName = "DangerousGoodsEventLocation")]
        public string identifiedWhereIndex
        { get; set; }

        [JsonProperty(PropertyName = "Person_x0020_submitting_x0020_re")]
        public string NameStaffNumber
        { get; set; }
        [JsonProperty(PropertyName = "Email_x002d_IDOfSubmitter")]
        public string SubmitterEmail
        { get; set; }

        [JsonProperty(PropertyName = "MobileEntry")]
        public bool MobileEntry { get; set; }
    }
}
