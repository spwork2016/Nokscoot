using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEnvAzure.DataContracts
{
    class InjuryIllnessReportSp
    {
        public InjuryIllnessReportSp()
        {
            this.__metadata = new Metadata();
            this.__metadata.type = "SP.Data.Operational_x005f_Hazard_x005f_Event_x005f_Register_x005f_04042018ListItem";
        }
        public Metadata __metadata { get; set; }
        //[PrimaryKey, AutoIncrement, Column("injuryIllId")]
        //public long injuryIllId
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



        [JsonProperty(PropertyName = "InjuryIllnessPersonInvolved")]
        public string personInvolvedIndex
        { get; set; }
        [JsonProperty(PropertyName = "Injury_x002d_Illness_x002d_Fatig.value")]
        public string unfitForDuty
        { get; set; }
        [JsonProperty(PropertyName = "NatureOfInjury")]
        public string natureofInjuryIndex
        { get; set; }
        [JsonProperty(PropertyName = "BodyPart")] //Body_x0020_Part
        public string bodyPart
        { get; set; }
        [JsonProperty(PropertyName = "HowInjuryOccurred")] //How_x0020_Injury_x0020_Occurred
        public string howinjuryOccurredIndex
        { get; set; }
        [JsonProperty(PropertyName = "ObjectThatInjured")]//Object_x0020_that_x0020_Injured
        public string objectInjuredIndex
        { get; set; }
        [JsonProperty(PropertyName = "TypeOfTreatment")] //Type_x0020_of_x0020_Treatment
        public string typeofTreatment
        { get; set; }
        [JsonProperty(PropertyName = "Injury_x002f_Ilness_x0020_Treatm")]
        public string treatmentByIndex
        { get; set; }
        [JsonProperty(PropertyName = "Where_x0020_the_x0020_event_x002")]
        public string whereEventOccur
        { get; set; }
        [JsonProperty(PropertyName = "Part_x0020_of_x0020_Body_x0020_i")]
        public string bodyPartInjuredIndex
        { get; set; }
        [JsonProperty(PropertyName = "Type_x0020_of_x0020_Illness_x002")]
        public string IllnessTypeIndex
        { get; set; }
        [JsonProperty(PropertyName = "Type_x0020_of_x0020_occurrence")]
        public string occurTypeIndex
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
