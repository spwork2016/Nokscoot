using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevEnvAzure.Model;
using DevEnvAzure.Models;
using DevEnvAzure.DataContracts;
using Newtonsoft.Json;

namespace DevEnvAzure.Utilities
{
    class Jsonpropertyinitialise
    {
        public FlightSecuritySharepointData getSecurity(SecurityModel sd)
        {
            FlightSecuritySharepointData sddp = new FlightSecuritySharepointData();
            sddp.Type_x0020_of_x0020_Report = sd.ReportType;
            sddp.Aircraft_x0020_Registration = SSIRShortForm.airregis;//sd.AircraftRegis!=0? Convert.ToString(sd.AircraftRegis + 1):null; //
            sddp.Event_x0020_Title = sd.EventTitle;
            sddp.Details_x0020_of_x0020_Event_x0020__x0020_Hazard = sd.DescribeEvent != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.DescribeEvent + "<br><\u002fdiv>" : null;
            sddp.FlightNumber = sd.FlightNumber;
            sddp.Date_x0020_of_x0020_Event = sd.DateOfEvent.ToString("yyyy-MM-dd") + "T07:00:00Z"; //"2018-03-14T07:00:00Z";//
            sddp.Departure_x0020_Station = sd.DepartureStation;
            sddp.Arrival_x0020_Station = sd.ArrivalStation;
            sddp.Divert_x0020_Station = sd.DivertStation;
            sddp.Location_Station_Area_FIR_x0020_of_x0020_Event = sd.Area_FIR;
            // sddp.Details_x0020_of_x0020_Event_x0020__x0020_Hazard = sd.DescribeEvent;
            sddp.Attachments = sd.Attachment;
            sddp.MOR_x0020_TypeId = SSIRShortForm.MORTypeID != 0 ? Convert.ToString(SSIRShortForm.MORTypeID + 1) : null;
            sddp.CONFIDENTIAL_x0020_REPORT = sd.ConfiReport == true ? "1" : "0";
            sddp.Send_x0020_Notifications = sd.ssQ == true ? "1" : "0";
            sddp.Others_x0020_Persons_x0020_Invol = sd.pax;

            sddp.groudWhere = securityReportView.flightwheresel; //sd.onGround!=0? Convert.ToString(sd.onGround + 1):null;
            sddp.FlightEvent = securityReportView.flightphase;//sd.flightEvent != 0 ? Convert.ToString(sd.flightEvent + 1) : null;
            sddp.securityEvent = sd.securityEvent; //!= 0 ? Convert.ToString(sd.securityEvent + 1) : null;

            return sddp;
        }


        public GroundSafetyReportSp getGroundSafety(GroundSafetyReport sd)
        {
            GroundSafetyReportSp sddp = new GroundSafetyReportSp();
            sddp.ReportType = sd.ReportType;
            sddp.AircraftRegis = SSIRShortForm.airregis;// Convert.ToString(sd.AircraftRegis + 1);
            sddp.EventTitle = sd.EventTitle;
            sddp.FlightNumber = sd.FlightNumber;
            sddp.DateOfEvent = sd.DateOfEvent.ToString("yyyy-MM-dd") + "T07:00:00Z";
            sddp.DepartureStation = sd.DepartureStation;
            sddp.ArrivalStation = sd.ArrivalStation;
            sddp.DivertStation = sd.DivertStation;
            sddp.Area_FIR = sd.Area_FIR;
            sddp.DescribeEvent = sd.DescribeEvent != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.DescribeEvent + "<br><\u002fdiv>" : null;
            sddp.Attachment = sd.Attachment;
            sddp.MOR = SSIRShortForm.MORTypeID != 0 ? Convert.ToString(SSIRShortForm.MORTypeID + 1) : null;//Convert.ToString(sd.MOR + 1);
            sddp.ConfiReport = sd.ConfiReport == true ? "1" : "0";
            sddp.ssQ = sd.ssQ == true ? "1" : "0";
            sddp.pax = sd.pax;



            sddp.lightIndex = GroundSafetyReportView.lightValue;//Convert.ToString(sd.lightIndex + 1);
            sddp.weatherIndex = GroundSafetyReportView.weatherValue;//Convert.ToString(sd.weatherIndex + 1);
            sddp.precipitationIndex = GroundSafetyReportView.precipitationValue;// Convert.ToString(sd.precipitationIndex + 1);
            sddp.groundconditions = GroundSafetyReportView.groundconditionValue;// Convert.ToString(sd.precipitationIndex + 1);
            sddp.typeofLiquidIndex = GroundSafetyReportView.typeofFuelValue; //Convert.ToString(sd.typeofLiquidIndex + 1);
            sddp.amountSpilled = sd.amountSpilled;
            sddp.wherespillindex = GroundSafetyReportView.WhereSpillValue;// Convert.ToString(sd.wherespillindex + 1);
            sddp.baggageIndex = GroundSafetyReportView.baggageValue; //Convert.ToString(sd.baggageIndex + 1);

            sddp.cargoIndex = GroundSafetyReportView.cargoValue;// Convert.ToString(sd.cargoIndex + 1);
            sddp.pax_CargoIndex = GroundSafetyReportView.paxValue;// Convert.ToString(sd.pax_CargoIndex + 1);
            sddp.identifiedWhenIndex = GroundSafetyReportView.identWhenValue;
            sddp.identifiedWhereIndex = GroundSafetyReportView.idenWhereValue;
            sddp.loadSheetIndex = GroundSafetyReportView.loadsheetValue;
            sddp.impactTocraftIndex = GroundSafetyReportView.impactairValue;
            return sddp;
        }
        public FatigueReportSp getFatigue(FatigueReport sd)
        {
            FatigueReportSp sddp = new FatigueReportSp();
            sddp.ReportType = sd.ReportType;
            sddp.AircraftRegis = SSIRShortForm.airregis;// Convert.ToString(sd.AircraftRegis + 1);
            sddp.EventTitle = sd.EventTitle;
            sddp.FlightNumber = sd.FlightNumber;
            sddp.DateOfEvent = sd.DateOfEvent.ToString("yyyy-MM-dd") + "T07:00:00Z";
            sddp.DepartureStation = sd.DepartureStation;
            sddp.ArrivalStation = sd.ArrivalStation;
            sddp.DivertStation = sd.DivertStation;
            sddp.Area_FIR = sd.Area_FIR;
            sddp.DescribeEvent = sd.DescribeEvent != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.DescribeEvent + "<br><\u002fdiv>" : null;
            sddp.Attachment = sd.Attachment;
            sddp.MOR = SSIRShortForm.MORTypeID != 0 ? Convert.ToString(SSIRShortForm.MORTypeID + 1) : null;//Convert.ToString(sd.MOR + 1);

            sddp.ConfiReport = sd.ConfiReport == true ? "1" : "0";
            sddp.ssQ = sd.ssQ == true ? "1" : "0";
            sddp.pax = sd.pax;

            sddp.localReportDate = sd.localReportDate.ToString("MM-dd-yyyy");// sd.localReportDate.ToString("yyyy-MM-dd") + "T07:00:00Z"; //
            sddp.hoursFromReport = sd.hoursFromReport;// == true ?"Yes" : "No";
            sddp.disrupt = sd.disrupt == true ? "1" : "0";
            sddp.describeFeel = sd.describeFeel;
            sddp.rateFeelIndex = sd.rateFeelIndex != 0 ? Convert.ToString(sd.rateFeelIndex + 1) : null; //Convert.ToString(sd.rateFeelIndex + 1);
            sddp.fatiguePrior = sd.fatiguePrior == true ? "1" : "0";
            sddp.poorFlightRest = sd.poorFlightRest == true ? "1" : "0";
            sddp.dutyItself = sd.dutyItself == true ? "1" : "0";
            sddp.roaster = sd.roaster == true ? "1" : "0";
            sddp.personalStress = sd.personalStress == true ? "1" : "0";
            sddp.hotelDisturbance = sd.hotelDisturbance == true ? "1" : "0";
            sddp.otherComments = sd.otherComments;
            sddp.actionToreduce = sd.actionToreduce;
            // sddp.reportAttachment = sd.reportAttachment;
            return sddp;
        }

        public CabibSafetyReportSp getCabinSfetyJson(CabibSafetyReport sd)
        {
            CabibSafetyReportSp sddp = new CabibSafetyReportSp();
            sddp.ReportType = sd.ReportType;
            sddp.AircraftRegis = SSIRShortForm.airregis;//sd.AircraftRegis!=0? Convert.ToString(sd.AircraftRegis + 1):null; //
            sddp.EventTitle = sd.EventTitle;
            sddp.DescribeEvent = sd.DescribeEvent != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.DescribeEvent + "<br><\u002fdiv>" : null;
            sddp.FlightNumber = sd.FlightNumber;
            sddp.DateOfEvent = sd.DateOfEvent.ToString("yyyy-MM-dd") + "T07:00:00Z"; //"2018-03-14T07:00:00Z";//
            sddp.DepartureStation = sd.DepartureStation;
            sddp.ArrivalStation = sd.ArrivalStation;
            sddp.DivertStation = sd.DivertStation;
            sddp.Area_FIR = sd.Area_FIR;
            sddp.Attachment = sd.Attachment;
            sddp.MOR = SSIRShortForm.MORTypeID != 0 ? Convert.ToString(SSIRShortForm.MORTypeID + 1) : null;
            sddp.ConfiReport = sd.ConfiReport == true ? "1" : "0";
            sddp.ssQ = sd.ssQ == true ? "1" : "0";
            sddp.pax = sd.pax;

            sddp.turbulenceIndex = CabinSafetyReportView.turbulenceValue;
            sddp.seatBeltsign = sd.seatBeltsign == true ? "1" : "0";
            sddp.adviceStop = sd.adviceStop == true ? "1" : "0";
            sddp.dangerPaxIndex = CabinSafetyReportView.dangergoodsValue;
            sddp.identifiedWhenCabinIndex = CabinSafetyReportView.dangerWhenValue;
            sddp.identifiedWhereCabinIndex = CabinSafetyReportView.dangerWhereValue;




            return sddp;
        }
        public InjuryIllnessReportSp getInjuryJson(InjuryIllnessReport sd)
        {
            InjuryIllnessReportSp sddp = new InjuryIllnessReportSp();
            sddp.ReportType = sd.ReportType;
            sddp.AircraftRegis = SSIRShortForm.airregis;// Convert.ToString(sd.AircraftRegis + 1);
            sddp.EventTitle = sd.EventTitle;
            sddp.FlightNumber = sd.FlightNumber;
            sddp.DateOfEvent = sd.DateOfEvent.ToString("yyyy-MM-dd") + "T07:00:00Z";
            sddp.DepartureStation = sd.DepartureStation;
            sddp.ArrivalStation = sd.ArrivalStation;
            sddp.DivertStation = sd.DivertStation;
            sddp.Area_FIR = sd.Area_FIR;
            sddp.DescribeEvent = sd.DescribeEvent != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.DescribeEvent + "<br><\u002fdiv>" : null;
            sddp.Attachment = sd.Attachment;
            sddp.MOR = SSIRShortForm.MORTypeID != 0 ? Convert.ToString(SSIRShortForm.MORTypeID + 1) : null;//Convert.ToString(sd.MOR + 1);
            sddp.ConfiReport = sd.ConfiReport == true ? "1" : "0";
            sddp.ssQ = sd.ssQ == true ? "1" : "0";
            sddp.pax = sd.pax;

            sddp.personInvolvedIndex = InjuryIllnessReportView.PersonInvolvedValue;
            sddp.unfitForDuty = sd.unfitForDuty == true ? "1" : "0";
            sddp.natureofInjuryIndex = InjuryIllnessReportView.NatureofInjuryValue;
            // sddp.bodyPart = sd.bodyPart; //not editing in sp
            sddp.howinjuryOccurredIndex = InjuryIllnessReportView.HowInjuryOccurredValue;
            sddp.objectInjuredIndex = InjuryIllnessReportView.ObjectValue;
            sddp.typeofTreatment = sd.typeofTreatment;
            sddp.treatmentByIndex = InjuryIllnessReportView.TreatmentByValue;
            sddp.whereEventOccur = sd.whereEventOccur;
            sddp.bodyPartInjuredIndex = InjuryIllnessReportView.PartofbodyinjuredValue;
            sddp.IllnessTypeIndex = InjuryIllnessReportView.TypeofIllnessInjuryValue;
            sddp.occurTypeIndex = InjuryIllnessReportView.TypeofoccurrenceValue;


            return sddp;
        }

        public FlightSafetyReportModelSp getflightSafetyJson(FlightSafetyReportModel sd)
        {
            FlightSafetyReportModelSp sddp = new FlightSafetyReportModelSp();
            sddp.ReportType = sd.ReportType;
            sddp.AircraftRegis = SSIRShortForm.airregis;// Convert.ToString(sd.AircraftRegis + 1);
            sddp.EventTitle = sd.EventTitle;
            sddp.FlightNumber = sd.FlightNumber;
            sddp.DateOfEvent = sd.DateOfEvent.ToString("yyyy-MM-dd") + "T07:00:00Z";
            sddp.DepartureStation = sd.DepartureStation;
            sddp.ArrivalStation = sd.ArrivalStation;
            sddp.DivertStation = sd.DivertStation;
            sddp.Area_FIR = sd.Area_FIR;
            sddp.DescribeEvent = sd.DescribeEvent != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.DescribeEvent + "<br><\u002fdiv>" : null;
            sddp.Attachment = sd.Attachment;
            sddp.MOR = SSIRShortForm.MORTypeID != 0 ? Convert.ToString(SSIRShortForm.MORTypeID + 1) : null;//Convert.ToString(sd.MOR + 1);
            sddp.ConfiReport = sd.ConfiReport == true ? "1" : "0";
            sddp.ssQ = sd.ssQ == true ? "1" : "0";
            sddp.pax = sd.pax;

            //sddp.CommanderPForPM = FlightSafetyReportView.CommanderPForPMpickerValue;
            //sddp.CommandersEmail = sd.CommandersEmail;
            //sddp.FlightCrew1 = sd.FlightCrew1;
            //sddp.FlightCrew1PFPMOBs = FlightSafetyReportView.FlightCrew1PFPMOBspickerValue;
            //sddp.FlightCrew2 = sd.FlightCrew2;
            //sddp.FlightCrew2PFPMOBs = FlightSafetyReportView.FlightCrew2PFPMOBspickerValue;
            //sddp.Ifflighteventselectphase = FlightSafetyReportView.IfflighteventselectphasepickerValue;
            //sddp.Ifongroundselectwhere = FlightSafetyReportView.IfongroundselectwherepickerValue;
            //sddp.Altitude = sd.Altitude;
            //sddp.IASMach = sd.IASMach;
            //sddp.AutopilotOn = sd.AutopilotOn == true ? "1" : "0";
            //sddp.ATOn = sd.ATOn == true ? "1" : "0";
            //sddp.ApproachType = sd.ApproachType != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.ApproachType + "<br><\u002fdiv>" : null;
            //sddp.Heading = sd.Heading;
            //sddp.VS = sd.VS;
            //sddp.Gear = FlightSafetyReportView.GearpickerValue;
            //sddp.Speedbrake = FlightSafetyReportView.SpeedbrakepickerValue;
            //sddp.FlapPosition = sd.FlapPosition;
            //sddp.Weight = sd.Weight;
            //sddp.FuelDumping = sd.FuelDumping == true ? "1" : "0";
            //sddp.SeatbeltSign = sd.SeatbeltSign == true ? "1" : "0";
            //sddp.MeteorologicalReport = FlightSafetyReportView.MeteorologicalReportpickerValue;
            //sddp.Wind = sd.Wind;
            //sddp.VisRVR = sd.VisRVR;
            //sddp.Temp = sd.Temp;
            //sddp.Light = FlightSafetyReportView.LightpickerValue;
            //sddp.Weather = FlightSafetyReportView.WeatherpickerValue;
            //sddp.Precipitation = FlightSafetyReportView.PrecipitationpickerValue;
            //sddp.Turbulence = FlightSafetyReportView.TurbulencepickerValue;
            //sddp.RWYDirection = sd.RWYDirection;
            //sddp.Conditions = FlightSafetyReportView.ConditionspickerValue;
            //sddp.NAVAIDS = FlightSafetyReportView.NAVAIDSpickerValue;
            //sddp.ClearedAltitude = sd.ClearedAltitude;
            //sddp.DeviationHorizontal = sd.DeviationHorizontal;
            //sddp.Vertical = sd.Vertical;
            //sddp.ReasonforDeviation = sd.ReasonforDeviation != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.ReasonforDeviation + "<br><\u002fdiv>" : null;
            //sddp.TAAlert = sd.TAAlert == true ? "1" : "0";
            //sddp.RAAlert = sd.RAAlert == true ? "1" : "0";
            //sddp.RACommand = sd.RACommand;
            //sddp.IntruderACType = sd.IntruderACType;
            //sddp.Callsign = sd.Callsign;
            //sddp.Relativeposition = sd.Relativeposition != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.Relativeposition + "<br><\u002fdiv>" : null;
            //sddp.Bearing = sd.Bearing;
            //sddp.Range = sd.Range;
            //sddp.ATC = FlightSafetyReportView.ATCorAirportReportFiledpickerValue;
            //sddp.ATCUnit = sd.ATCUnit;
            //sddp.Frequency = sd.Frequency;
            //sddp.TypeofWarnings = sd.TypeofWarnings != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.TypeofWarnings + "<br><\u002fdiv>" : null;
            //sddp.WildlifeType = sd.WildlifeType;
            //sddp.Numberofbirds = FlightSafetyReportView.NumberofbirdspickerValue;
            //sddp.SizeofWildlife = FlightSafetyReportView.SizeofWildlifepickerValue;
            //sddp.AircraftDamage = FlightSafetyReportView.AircraftDamagepickerValue;
            //sddp.ImpactAreaDamage = sd.ImpactAreaDamage != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.ImpactAreaDamage + "<br><\u002fdiv>" : null;


            return sddp;
        }
    }
}
