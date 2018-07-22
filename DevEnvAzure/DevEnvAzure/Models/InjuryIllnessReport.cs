using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
namespace DevEnvAzure.Models
{
    [Table(@"InjuryIllnessReport")]
    public class InjuryIllnessReport
    {
        //[PrimaryKey, AutoIncrement, Column("injuryIllId")]
        //public long injuryIllId
        //{ get; set; }
        //[NotNull]
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




        public int personInvolvedIndex
        { get; set; }
        public bool unfitForDuty
        { get; set; }
        public int natureofInjuryIndex
        { get; set; }
        public string bodyPart
        { get; set; }
        public int howinjuryOccurredIndex
        { get; set; }
        public int objectInjuredIndex
        { get; set; }
        public string typeofTreatment
        { get; set; }
        public int treatmentByIndex
        { get; set; }
        public string whereEventOccur
        { get; set; }
        public int bodyPartInjuredIndex
        { get; set; }
        public int IllnessTypeIndex
        { get; set; }
        public int occurTypeIndex
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

        public string Attachments { get; set; }
    }
}
