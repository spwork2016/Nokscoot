using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEnvAzure.DataContracts
{
    class FatigueReportSp
    {
        public FatigueReportSp()
        {
            this.__metadata = new Metadata();
            this.__metadata.type = ClientConfiguration.Default.SHORTFORMURL;
        }
        public Metadata __metadata { get; set; }
        //[PrimaryKey, AutoIncrement, Column("fatigueId")]
        //public long fatigueId
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
        [JsonProperty(PropertyName = "AircraftRegistrationNew")]
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


        //Create table items in the site and add here Sravan

        [JsonProperty(PropertyName = "Local_x0020_Report_x0020_Date_x0")]
        public string localReportDate
        { get; set; }
        [JsonProperty(PropertyName = "Hours_when_x0020_fatigue_x0020_c")]
        public string hoursFromReport
        { get; set; }
        [JsonProperty(PropertyName = "Disrupt.value")]
        public string disrupt
        { get; set; }
        [JsonProperty(PropertyName = "Describe_x0020_how_x0020_you_x00")]
        public string describeFeel
        { get; set; }
        [JsonProperty(PropertyName = "How_x0020_you_x0020_felt_x0020_r")]
        public string rateFeelIndex
        { get; set; }
        [JsonProperty(PropertyName = "Fatigued_x0020_prior_x0020_to_x0.value")]
        public string fatiguePrior
        { get; set; }
        [JsonProperty(PropertyName = "Poor_x0020_In_x002d_Flight_x0020.value")]
        public string poorFlightRest
        { get; set; }
        [JsonProperty(PropertyName = "Duty_x0020_Itself.value")]
        public string dutyItself
        { get; set; }
        [JsonProperty(PropertyName = "Roster_x002f_Combination_x0020_o.value")]
        public string roaster
        { get; set; }
        [JsonProperty(PropertyName = "Personal.value")]
        public string personalStress
        { get; set; }
        [JsonProperty(PropertyName = "Hotel.value")]
        public string hotelDisturbance
        { get; set; }
        [JsonProperty(PropertyName = "Why_x0020_Other_x002f_Comments")]
        public string otherComments
        { get; set; }
        [JsonProperty(PropertyName = "Actions_x0020_taken_x0020_to_x00")]
        public string actionToreduce
        { get; set; }
        //attachment control is not updated in xaml
        //[JsonProperty(PropertyName = "Attachments")]
        //public string reportAttachment
        //{ get; set; }

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
