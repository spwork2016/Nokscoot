using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite.Net.Attributes;
namespace DevEnvAzure.Models
{
    [Table(@"SafetyReportModel")]
    public class FlightSafetyReportModel
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

        public int CommanderPForPM
        { get; set; }
        public string CommandersEmail
        { get; set; }
        public string FlightCrew1
        { get; set; }
        public int FlightCrew1PFPMOBs
        { get; set; }
        public string FlightCrew2
        { get; set; }
        public int FlightCrew2PFPMOBs
        { get; set; }

        public int Ifflighteventselectphase
        { get; set; }
        public int Ifongroundselectwhere
        { get; set; }
        public string Altitude
        { get; set; }
        public string IASMach
        { get; set; }
        public bool AutopilotOn
        { get; set; }
        public bool ATOn
        { get; set; }
        public string ApproachType
        { get; set; }
        public string Heading
        { get; set; }
        public string VS
        { get; set; }
        public int Gear
        { get; set; }
        public int Speedbrake
        { get; set; }
        public string FlapPosition
        { get; set; }
        public string Weight
        { get; set; }
        public bool FuelDumping
        { get; set; }
        public bool SeatbeltSign
        { get; set; }
        public int MeteorologicalReport
        { get; set; }
        public string Wind
        { get; set; }
        public string VisRVR
        { get; set; }
        public string Temp
        { get; set; }
        public int Light
        { get; set; }
        public int Weather
        { get; set; }
        public int Precipitation
        { get; set; }
        public int Turbulence
        { get; set; }
        public string RWYDirection
        { get; set; }
        public int Conditions
        { get; set; }
        public int NAVAIDS
        { get; set; }
        public string ClearedAltitude
        { get; set; }
        public string DeviationHorizontal
        { get; set; }
        public string Vertical
        { get; set; }
        public string ReasonforDeviation
        { get; set; }
        public bool TAAlert
        { get; set; }
        public bool RAAlert
        { get; set; }
        public string RACommand
        { get; set; }

        public string IntruderACType
        { get; set; }
        public string Callsign
        { get; set; }
        public string Relativeposition
        { get; set; }
        public string Bearing
        { get; set; }
        public string Range
        { get; set; }
        public int ATC
        { get; set; }
        public string ATCUnit
        { get; set; }
        public string Frequency
        { get; set; }
        public string TypeofWarnings
        { get; set; }
        public string WildlifeType
        { get; set; }
        public int Numberofbirds
        { get; set; }
        public int SizeofWildlife
        { get; set; }
        public int AircraftDamage
        { get; set; }
        public string ImpactAreaDamage
        { get; set; }
        public bool IsExtendedView
        { get; set; }
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
