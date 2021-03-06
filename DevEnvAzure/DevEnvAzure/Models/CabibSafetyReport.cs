﻿using System;
using SQLite;

namespace DevEnvAzure.Models
{
    public class CabibSafetyReport
    {
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


        public int turbulenceIndex
        { get; set; }
        public bool seatBeltsign
        { get; set; }
        public bool adviceStop
        { get; set; }
        public int dangerPaxIndex
        { get; set; }
        public int identifiedWhenCabinIndex
        { get; set; }
        public int identifiedWhereCabinIndex
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
