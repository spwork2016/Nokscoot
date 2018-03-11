using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
namespace DevEnvAzure.Models
{
    [Table(@"FatigueReport")]
    public class FatigueReport
    {
        //[PrimaryKey, AutoIncrement, Column("fatigueId")]
        //public long fatigueId
        //{ get; set; }
        //[NotNull]
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




        public DateTime localReportDate
        { get; set; }
        public string hoursFromReport
        { get; set; }
        public bool disrupt
        { get; set; }
        public string describeFeel
        { get; set; }
        public int rateFeelIndex
        { get; set; }
        public bool fatiguePrior
        { get; set; }
        public bool poorFlightRest
        { get; set; }
        public bool dutyItself
        { get; set; }
        public bool roaster
        { get; set; }
        public bool personalStress
        { get; set; }
        public bool hotelDisturbance
        { get; set; }
        public string otherComments
        { get; set; }
        public string actionToreduce
        { get; set; }
        public string reportAttachment
        { get; set; }

    }
}
