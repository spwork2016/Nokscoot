﻿using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEnvAzure.Models
{
    [Table(@"FlightCrewVoyageReport")]
    public class FlightCrewVoyageRecordModel
    {
        public string VoyageRecord
        { get; set; }
        public int SectorNumber
        { get; set; }
        public DateTime ScheduledDeparture
        { get; set; }
        public string FlightNumber
        { get; set; }
        public int AircraftRegistration
        { get; set; }
        public string DepartureStation
        { get; set; }
        public string ArrivalStation
        { get; set; }
        public string LandingBy
        { get; set; }
        public int ManualorAuto
        { get; set; }
        public string TitleofReport
        { get; set; }
        public int ReportCategories
        { get; set; }
        public string ReportDetails
        { get; set; }
        public string ReportRaisedBy
        { get; set; }
        public int ReplyRequired
        { get; set; }
        public string StaffNumber
        { get; set; }
        public int Rank
        { get; set; }
    }
}