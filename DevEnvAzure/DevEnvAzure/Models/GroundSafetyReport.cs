using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
namespace DevEnvAzure.Models
{

    [Table(@"GroundSafetyReport")]
    public class GroundSafetyReport
    {
        //[PrimaryKey, AutoIncrement, Column("groundId")]
        //public long groundId
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




        public int lightIndex
        { get; set; }
        public int weatherIndex
        { get; set; }
        public int precipitationIndex
        { get; set; }
        public int groundconditions
        { get; set; }

        public int typeofLiquidIndex
        { get; set; }

        public string amountSpilled
        { get; set; }
        public int wherespillindex
        { get; set; }
        public int baggageIndex
        { get; set; }
        public int cargoIndex
        { get; set; }
        public int loadSheetIndex
        { get; set; }
        public int impactTocraftIndex
        { get; set; }
        public int pax_CargoIndex
        { get; set; }
        public int identifiedWhenIndex
        { get; set; }
        public int identifiedWhereIndex
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
    }
}
