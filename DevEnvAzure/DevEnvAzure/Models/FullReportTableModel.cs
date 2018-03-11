using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEnvAzure.Models
{
    [Table(@"FullReportTableModel")]
    public class FullReportTableModel
    {

        //public FullReportTableModel()
        //{
        //    this.__metadata = new Metadata();
        //    this.__metadata.type = "SP.Data.Operational_x005f_Hazard_x005f_Event_x005f_Register_x005f_04042018ListItem";
        //}
        //public Metadata __metadata { get; set; }
        //[NotNull]
        //[PrimaryKey, AutoIncrement, Column("SecurityID")]
        //public long SecurityID
        //{ get; set; }

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

        //flight security
        public int flightEvent
        { get; set; }
        public int onGround
        { get; set; }
        public string securityEvent
        { get; set; }

        //cabin
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

        //fatigue

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


        //ground
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

        //injury
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

        //safety
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
        public string Conditions
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


    }
}

