using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevEnvAzure.Utilities.Jsonpropertyinitialise;

namespace DevEnvAzure.DataContracts
{
    public class Metadata1
    {
        public string type { get; set; }
    }

    public class ApproachType
    {
        public Metadata1 __metadata { get; set; }
        public List<string> results { get; set; }
    }
    public class Metadata2
    {
        public string type { get; set; }
    }

    public class ReasonForDeviation
    {
        public Metadata2 __metadata { get; set; }
        public List<string> results { get; set; }
    }

    public class Metadata3
    {
        public string type { get; set; }
    }

    public class IntruderACRelativePosition
    {
        public Metadata3 __metadata { get; set; }
        public List<string> results { get; set; }
    }
    class FlightSafetyReportModelSp
    {
        public FlightSafetyReportModelSp()
        {
            this.__metadata = new Metadata();
            this.__metadata.type = "SP.Data.Operational_x005f_Hazard_x005f_Event_x005f_Register_x005f_04042018ListItem";
        }
        public Metadata __metadata { get; set; }
      
        //[PrimaryKey, AutoIncrement, Column("SafetyID")]
        //public long SafetyID
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

        [JsonProperty(PropertyName = "Commander_x0020_PF_x0020_or_x002")]
        public string CommanderPForPM
        { get; set; }
        [JsonProperty(PropertyName = "CommandersEmailStringId")]
        public string CommandersEmail
        { get; set; }
        [JsonProperty(PropertyName = "FlightCrew1EmailStringId")]
        public string FlightCrew1
        { get; set; }
        [JsonProperty(PropertyName = "Flight_x0020_Crew_x0020_1_x0020_")]
        public string FlightCrew1PFPMOBs
        { get; set; }
        [JsonProperty(PropertyName = "FlightCrew2EmailStringId")]
        public string FlightCrew2
        { get; set; }
        [JsonProperty(PropertyName = "Flight_x0020_Crew_x0020_2_x0020_")]
        public string FlightCrew2PFPMOBs
        { get; set; }

        [JsonProperty(PropertyName = "If_x0020_flight_x0020_event_x002")]
        public string Ifflighteventselectphase
        { get; set; }
        [JsonProperty(PropertyName = "If_x0020_on_x0020_ground_x0020__")]
        public string Ifongroundselectwhere
        { get; set; }
        [JsonProperty(PropertyName = "Altitude")]
        public string Altitude
        { get; set; }
        [JsonProperty(PropertyName = "IASMach")]
        public string IASMach
        { get; set; }
        [JsonProperty(PropertyName = "AutopilotOn.value")]
        public string AutopilotOn
        { get; set; }
        [JsonProperty(PropertyName = "AutothrottlesOn.value")]
        public string ATOn
        { get; set; }
        [JsonProperty(PropertyName = "ApproachType")]
        public ApproachType ApproachType { get; set; }
        //public string ApproachType
        //{ get; set; }
        [JsonProperty(PropertyName = "Heading")]
        public string Heading
        { get; set; }
        [JsonProperty(PropertyName = "VerticalSpeed")]
        public string VS
        { get; set; }
        [JsonProperty(PropertyName = "Gear")]
        public string Gear
        { get; set; }

        [JsonProperty(PropertyName = "Speedbrake")]
        public string Speedbrake
        { get; set; }

        [JsonProperty(PropertyName = "FlapPostion")]
        public string FlapPosition
        { get; set; }

        [JsonProperty(PropertyName = "GrossWeight")]
        public string Weight
        { get; set; }

        [JsonProperty(PropertyName = "FuelDumping.value")]
        public string FuelDumping
        { get; set; }

        [JsonProperty(PropertyName = "SeatbeltSignON.value")]
        public string SeatbeltSign
        { get; set; }
        [JsonProperty(PropertyName = "MetConditions")]
        public string MeteorologicalReport
        { get; set; }
        [JsonProperty(PropertyName = "Wind")]
        public string Wind
        { get; set; }
        [JsonProperty(PropertyName = "VisRVR")]
        public string VisRVR
        { get; set; }
        [JsonProperty(PropertyName = "Temperature")]
        public string Temp
        { get; set; }
        [JsonProperty(PropertyName = "Light")]
        public string Light
        { get; set; }
        [JsonProperty(PropertyName = "Weather")]
        public string Weather
        { get; set; }
        [JsonProperty(PropertyName = "Precipitation")]
        public string Precipitation
        { get; set; }
        [JsonProperty(PropertyName = "Turbulence")]
        public string Turbulence
        { get; set; }
        [JsonProperty(PropertyName = "RunwayDirection")]
        public string RWYDirection
        { get; set; }
        [JsonProperty(PropertyName = "RunwayConditions")]
        public string Conditions
        { get; set; }
        [JsonProperty(PropertyName = "NAVAIDS")]
        public string NAVAIDS
        { get; set; }
        [JsonProperty(PropertyName = "ClearedAltitude")]
        public string ClearedAltitude
        { get; set; }
        [JsonProperty(PropertyName = "DeviationHorizontalNM")]
        public string DeviationHorizontal
        { get; set; }
        [JsonProperty(PropertyName = "DeviationVerticalFT")]
        public string Vertical
        { get; set; }
        [JsonProperty(PropertyName = "ReasonForDeviation")]
        public ReasonForDeviation ReasonforDeviation
        { get; set; }

        [JsonProperty(PropertyName = "TAAlert.value")]
        public string TAAlert
        { get; set; }
        [JsonProperty(PropertyName = "RAAlert.value")]
        public string RAAlert
        { get; set; }

        [JsonProperty(PropertyName = "RACommand")]
        public string RACommand
        { get; set; }

        [JsonProperty(PropertyName = "IntruderACType")]
        public string IntruderACType
        { get; set; }
        [JsonProperty(PropertyName = "IntruderACCallsign")]
        public string Callsign
        { get; set; }
        [JsonProperty(PropertyName = "IntruderACRelativePosition")]
        public IntruderACRelativePosition Relativeposition
        { get; set; }
        [JsonProperty(PropertyName = "IntruderACBearing")]
        public string Bearing
        { get; set; }
        [JsonProperty(PropertyName = "IntruderACRange")]
        public string Range
        { get; set; }
        [JsonProperty(PropertyName = "ATC_x0020_or_x0020_Airport_x0020")]
        public string ATC
        { get; set; }
        [JsonProperty(PropertyName = "ATCUnit")]
        public string ATCUnit
        { get; set; }
        [JsonProperty(PropertyName = "Frequency")]
        public string Frequency
        { get; set; }
        [JsonProperty(PropertyName = "EGPWSEventTypeOfWarningsAlert")]
        public string TypeofWarnings
        { get; set; }
        [JsonProperty(PropertyName = "WildlifeType")]
        public string WildlifeType
        { get; set; }
        [JsonProperty(PropertyName = "NumberOfBirds")]
        public string Numberofbirds
        { get; set; }
        [JsonProperty(PropertyName = "SizeOfWildlife")]
        public string SizeofWildlife
        { get; set; }
        [JsonProperty(PropertyName = "AircraftDamage")]
        public string AircraftDamage
        { get; set; }
        [JsonProperty(PropertyName = "ImpactAreaDamage")]
        public string ImpactAreaDamage
        { get; set; }
        [JsonProperty(PropertyName = "Person_x0020_submitting_x0020_re")]
        public string NameStaffNumber
        { get; set; }
        [JsonProperty(PropertyName = "Email_x002d_IDOfSubmitter")]
        public string SubmitterEmail
        { get; set; }
    }
}
