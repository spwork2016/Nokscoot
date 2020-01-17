using SQLite;
using System;
namespace DevEnvAzure.Models
{
    [Table(@"SafetyReport")]
    public class SafetyReportModel
    {
        [PrimaryKey, AutoIncrement, Column("SafetyID")]
        public long SafetyID
        { get; set; }
        [NotNull]
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



        //public string EventTitle
        //{ get; set; }
        //public string DateOfEvent
        //{ get; set; }
        //public string Item
        //{ get; set; }
        //public string FlightNumber
        //{ get; set; }
        //public string AircraftRegis
        //{ get; set; }
        //public string DepartureStation
        //{ get; set; }
        //public string ArrivalStation
        //{ get; set; }
    }

}
