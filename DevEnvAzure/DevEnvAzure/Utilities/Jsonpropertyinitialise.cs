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
            sddp.MobileEntry = true;
            sddp.Type_x0020_of_x0020_Report = "Security Report";
            sddp.Aircraft_x0020_Registration = sd.AircraftRegistration;
            sddp.Event_x0020_Title = sd.EventTitle;
            sddp.Details_x0020_of_x0020_Event_x0020__x0020_Hazard = sd.DescribeEvent != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.DescribeEvent + "<br><\u002fdiv>" : null;
            sddp.FlightNumber = sd.FlightNumber;
            sddp.Date_x0020_of_x0020_Event = sd.DateOfEvent.ToString("yyyy-MM-dd") + "T07:00:00Z";
            sddp.Departure_x0020_Station = sd.DepartureStation;
            sddp.Arrival_x0020_Station = sd.ArrivalStation;
            sddp.Divert_x0020_Station = sd.DivertStation;
            sddp.Location_Station_Area_FIR_x0020_of_x0020_Event = sd.Area_FIR;
            sddp.Attachments = sd.Attachment;
            sddp.MOR_x0020_TypeId = SSIRShortForm.MORTypeID != null ? Convert.ToString(SSIRShortForm.MORTypeID + 1) : null;
            sddp.CONFIDENTIAL_x0020_REPORT = sd.ConfiReport == true ? "1" : "0";
            sddp.Send_x0020_Notifications = sd.ssQ == true ? "1" : "0";
            sddp.Others_x0020_Persons_x0020_Invol = sd.pax;

            sddp.groudWhere = securityReportView.flightwheresel;
            sddp.FlightEvent = securityReportView.flightphase;

            List<string> strData = sd.securityEvent != null ? sd.securityEvent.Trim(',').Split(',').ToList() : null;

            if (strData != null)
            {
                SecurityEventFieldChoice sfapp = new SecurityEventFieldChoice();
                sfapp.results = strData;
                Metadata1 md1 = new Metadata1();
                md1.type = "Collection(Edm.String)";
                sfapp.__metadata = md1;
                sddp.securityEvent = sfapp;
            }

            if (!string.IsNullOrEmpty(sd.policereport))
            {
                if (Uri.IsWellFormedUriString(sd.policereport, UriKind.Absolute))
                {
                    sddp.PoliceReport = new SPFieldURL
                    {
                        Url = sd.policereport
                    };
                }
                else
                {
                    throw new Exception(string.Format("'{0}' is not a valid url", sd.policereport));
                }
            }

            sddp.SubmitterEmail = sd.SubmitterEmail;
            sddp.NameStaffNumber = sd.NameStaffNumber;
            return sddp;
        }
        public GroundSafetyReportSp getGroundSafety(GroundSafetyReport sd)
        {
            GroundSafetyReportSp sddp = new GroundSafetyReportSp();
            sddp.MobileEntry = true;
            sddp.ReportType = "Ground Safety Report";// sd.ReportType;
            sddp.AircraftRegis = sd.AircraftRegistration;
            sddp.EventTitle = sd.EventTitle;
            sddp.FlightNumber = sd.FlightNumber;
            sddp.DateOfEvent = sd.DateOfEvent.ToString("yyyy-MM-dd") + "T07:00:00Z";
            sddp.DepartureStation = sd.DepartureStation;
            sddp.ArrivalStation = sd.ArrivalStation;
            sddp.DivertStation = sd.DivertStation;
            sddp.Area_FIR = sd.Area_FIR;
            sddp.DescribeEvent = sd.DescribeEvent != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.DescribeEvent + "<br><\u002fdiv>" : null;
            sddp.Attachment = sd.Attachment;
            sddp.MOR = SSIRShortForm.MORTypeID != null ? Convert.ToString(SSIRShortForm.MORTypeID + 1) : null;//Convert.ToString(sd.MOR + 1);
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

            sddp.SubmitterEmail = sd.SubmitterEmail;
            sddp.NameStaffNumber = sd.NameStaffNumber;
            return sddp;
        }
        public FatigueReportSp getFatigue(FatigueReport sd)
        {
            FatigueReportSp sddp = new FatigueReportSp();
            sddp.MobileEntry = true;
            sddp.ReportType = "Fatigue Report";// sd.ReportType;
            sddp.AircraftRegis = sd.AircraftRegistration;
            sddp.EventTitle = sd.EventTitle;
            sddp.FlightNumber = sd.FlightNumber;
            sddp.DateOfEvent = sd.DateOfEvent.ToString("yyyy-MM-dd") + "T07:00:00Z";
            sddp.DepartureStation = sd.DepartureStation;
            sddp.ArrivalStation = sd.ArrivalStation;
            sddp.DivertStation = sd.DivertStation;
            sddp.Area_FIR = sd.Area_FIR;
            sddp.DescribeEvent = sd.DescribeEvent != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.DescribeEvent + "<br><\u002fdiv>" : null;
            sddp.Attachment = sd.Attachment;
            sddp.MOR = SSIRShortForm.MORTypeID != null ? Convert.ToString(SSIRShortForm.MORTypeID + 1) : null;//Convert.ToString(sd.MOR + 1);

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

            sddp.SubmitterEmail = sd.SubmitterEmail;
            sddp.NameStaffNumber = sd.NameStaffNumber;
            return sddp;
        }
        public CabibSafetyReportSp getCabinSfetyJson(CabibSafetyReport sd)
        {
            CabibSafetyReportSp sddp = new CabibSafetyReportSp();
            sddp.MobileEntry = true;
            sddp.ReportType = "Cabin Safety Report"; //sd.ReportType;
            sddp.AircraftRegis = sd.AircraftRegistration;
            sddp.EventTitle = sd.EventTitle;
            sddp.DescribeEvent = sd.DescribeEvent != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.DescribeEvent + "<br><\u002fdiv>" : null;
            sddp.FlightNumber = sd.FlightNumber;
            sddp.DateOfEvent = sd.DateOfEvent.ToString("yyyy-MM-dd") + "T07:00:00Z"; //"2018-03-14T07:00:00Z";//
            sddp.DepartureStation = sd.DepartureStation;
            sddp.ArrivalStation = sd.ArrivalStation;
            sddp.DivertStation = sd.DivertStation;
            sddp.Area_FIR = sd.Area_FIR;
            sddp.Attachment = sd.Attachment;
            sddp.MOR = SSIRShortForm.MORTypeID != null ? Convert.ToString(SSIRShortForm.MORTypeID + 1) : null;
            sddp.ConfiReport = sd.ConfiReport == true ? "1" : "0";
            sddp.ssQ = sd.ssQ == true ? "1" : "0";
            sddp.pax = sd.pax;

            sddp.turbulenceIndex = CabinSafetyReportView.turbulenceValue;
            sddp.seatBeltsign = sd.seatBeltsign == true ? "1" : "0";
            sddp.adviceStop = sd.adviceStop == true ? "1" : "0";
            sddp.dangerPaxIndex = CabinSafetyReportView.dangergoodsValue;
            sddp.identifiedWhenCabinIndex = CabinSafetyReportView.dangerWhenValue;
            sddp.identifiedWhereCabinIndex = CabinSafetyReportView.dangerWhereValue;

            sddp.SubmitterEmail = sd.SubmitterEmail;
            sddp.NameStaffNumber = sd.NameStaffNumber;

            return sddp;
        }
        public InjuryIllnessReportSp getInjuryJson(InjuryIllnessReport sd)
        {
            InjuryIllnessReportSp sddp = new InjuryIllnessReportSp();
            sddp.MobileEntry = true;
            sddp.ReportType = "Injury-Illness Report"; //sd.ReportType;
            sddp.AircraftRegis = sd.AircraftRegistration;
            sddp.EventTitle = sd.EventTitle;
            sddp.FlightNumber = sd.FlightNumber;
            sddp.DateOfEvent = sd.DateOfEvent.ToString("yyyy-MM-dd") + "T07:00:00Z";
            sddp.DepartureStation = sd.DepartureStation;
            sddp.ArrivalStation = sd.ArrivalStation;
            sddp.DivertStation = sd.DivertStation;
            sddp.Area_FIR = sd.Area_FIR;
            sddp.DescribeEvent = sd.DescribeEvent != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.DescribeEvent + "<br><\u002fdiv>" : null;
            sddp.Attachment = sd.Attachment;
            sddp.MOR = SSIRShortForm.MORTypeID != null ? Convert.ToString(SSIRShortForm.MORTypeID + 1) : null;//Convert.ToString(sd.MOR + 1);
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

            sddp.SubmitterEmail = sd.SubmitterEmail;
            sddp.NameStaffNumber = sd.NameStaffNumber;
            return sddp;
        }
        public FlightSafetyReportModelSp getflightSafetyJson(FlightSafetyReportModel sd)
        {
            FlightSafetyReportModelSp sddp = new FlightSafetyReportModelSp();
            sddp.MobileEntry = true;
            List<string> strData = sd.ApproachType != null ? sd.ApproachType.Trim(',').Split(',').ToList() : null;

            List<string> relativeStr = sd.Relativeposition != null ? sd.Relativeposition.Trim(',').Split(',').ToList() : null;

            List<string> deviation = sd.ReasonforDeviation != null ? sd.ReasonforDeviation.Trim(',').Split(',').ToList() : null;



            if (strData != null)
            {
                ApproachType sfapp = new ApproachType();
                sfapp.results = strData;
                Metadata1 md1 = new Metadata1();
                md1.type = "Collection(Edm.String)";
                sfapp.__metadata = md1;
                sddp.ApproachType = sfapp;
            }



            if (relativeStr != null)
            {
                ReasonForDeviation rsDev = new ReasonForDeviation();
                rsDev.results = deviation;
                Metadata2 md2 = new Metadata2();
                md2.type = "Collection(Edm.String)";
                rsDev.__metadata = md2;
                sddp.ReasonforDeviation = rsDev;
            }

            if (deviation != null)
            {
                IntruderACRelativePosition intru = new IntruderACRelativePosition();
                intru.results = deviation;
                Metadata3 md3 = new Metadata3();
                md3.type = "Collection(Edm.String)";
                intru.__metadata = md3;
                sddp.Relativeposition = intru;
            }


            sddp.ReportType = "Flight Safety Report";// sd.ReportType;
            sddp.AircraftRegis = sd.AircraftRegistration;
            sddp.EventTitle = sd.EventTitle;
            sddp.FlightNumber = sd.FlightNumber;
            sddp.DateOfEvent = sd.DateOfEvent.ToString("yyyy-MM-dd") + "T07:00:00Z";
            sddp.DepartureStation = sd.DepartureStation;
            sddp.ArrivalStation = sd.ArrivalStation;
            sddp.DivertStation = sd.DivertStation;
            sddp.Area_FIR = sd.Area_FIR;
            sddp.DescribeEvent = sd.DescribeEvent != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.DescribeEvent + "<br><\u002fdiv>" : null;
            sddp.Attachment = sd.Attachment;
            sddp.MOR = SSIRShortForm.MORTypeID != null ? Convert.ToString(SSIRShortForm.MORTypeID + 1) : null;//Convert.ToString(sd.MOR + 1);
            sddp.ConfiReport = sd.ConfiReport == true ? "1" : "0";
            sddp.ssQ = sd.ssQ == true ? "1" : "0";
            sddp.pax = sd.pax;

            sddp.CommanderPForPM = FlightSafetyReportView.CommanderPForPMpickerValue;
            if (FlightSafetyReportView.PeoplePickerCommander != null)
                sddp.CommandersEmail = FlightSafetyReportView.PeoplePickerCommander.Id.ToString();

            if (FlightSafetyReportView.PeoplePickercrew1email != null)
                sddp.FlightCrew1 = FlightSafetyReportView.PeoplePickercrew1email.Id.ToString();

            if (FlightSafetyReportView.PeoplePickercrew2email != null)
                sddp.FlightCrew2 = FlightSafetyReportView.PeoplePickercrew2email.Id.ToString();

            sddp.FlightCrew1PFPMOBs = FlightSafetyReportView.FlightCrew1PFPMOBspickerValue;
            sddp.FlightCrew2PFPMOBs = FlightSafetyReportView.FlightCrew2PFPMOBspickerValue;
            sddp.Ifflighteventselectphase = FlightSafetyReportView.IfflighteventselectphasepickerValue;
            sddp.Ifongroundselectwhere = FlightSafetyReportView.IfongroundselectwherepickerValue;
            sddp.Altitude = sd.Altitude;
            sddp.IASMach = sd.IASMach;
            sddp.AutopilotOn = sd.AutopilotOn == true ? "1" : "0";
            sddp.ATOn = sd.ATOn == true ? "1" : "0";

            //  sddp.ApproachType = "{\"__metadata\":{\"type\":\"Collection(Edm.String)\"},\"results\":\"" + apptypestr + "\"}";
            sddp.Heading = sd.Heading;
            sddp.VS = sd.VS;
            sddp.Gear = FlightSafetyReportView.GearpickerValue;
            sddp.Speedbrake = FlightSafetyReportView.SpeedbrakepickerValue;
            sddp.FlapPosition = sd.FlapPosition;
            sddp.Weight = sd.Weight;
            sddp.FuelDumping = sd.FuelDumping == true ? "1" : "0";
            sddp.SeatbeltSign = sd.SeatbeltSign == true ? "1" : "0";
            sddp.MeteorologicalReport = FlightSafetyReportView.MeteorologicalReportpickerValue;
            sddp.Wind = sd.Wind;
            sddp.VisRVR = sd.VisRVR;
            sddp.Temp = sd.Temp;
            sddp.Light = FlightSafetyReportView.LightpickerValue;
            sddp.Weather = FlightSafetyReportView.WeatherpickerValue;
            sddp.Precipitation = FlightSafetyReportView.PrecipitationpickerValue;
            sddp.Turbulence = FlightSafetyReportView.TurbulencepickerValue;
            sddp.RWYDirection = sd.RWYDirection;
            sddp.Conditions = FlightSafetyReportView.ConditionspickerValue;
            sddp.NAVAIDS = FlightSafetyReportView.NAVAIDSpickerValue;
            sddp.ClearedAltitude = sd.ClearedAltitude;
            sddp.DeviationHorizontal = sd.DeviationHorizontal;
            sddp.Vertical = sd.Vertical;
            //  sddp.ReasonforDeviation = sd.ReasonforDeviation != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.ReasonforDeviation + "<br><\u002fdiv>" : null;
            sddp.TAAlert = sd.TAAlert == true ? "1" : "0";
            sddp.RAAlert = sd.RAAlert == true ? "1" : "0";
            sddp.RACommand = sd.RACommand;
            sddp.IntruderACType = sd.IntruderACType;
            sddp.Callsign = sd.Callsign;
            //  sddp.Relativeposition = sd.Relativeposition != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.Relativeposition + "<br><\u002fdiv>" : null;
            sddp.Bearing = sd.Bearing;
            sddp.Range = sd.Range;
            sddp.ATC = FlightSafetyReportView.ATCorAirportReportFiledpickerValue;
            sddp.ATCUnit = sd.ATCUnit;
            sddp.Frequency = sd.Frequency;
            sddp.TypeofWarnings = sd.TypeofWarnings != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.TypeofWarnings + "<br><\u002fdiv>" : null;
            sddp.WildlifeType = sd.WildlifeType;
            sddp.Numberofbirds = FlightSafetyReportView.NumberofbirdspickerValue;
            sddp.SizeofWildlife = FlightSafetyReportView.SizeofWildlifepickerValue;
            sddp.AircraftDamage = FlightSafetyReportView.AircraftDamagepickerValue;

            sddp.ImpactAreaDamage = sd.ImpactAreaDamage != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.ImpactAreaDamage + "<br><\u002fdiv>" : null;
            sddp.SubmitterEmail = sd.SubmitterEmail;
            sddp.NameStaffNumber = sd.NameStaffNumber;
            return sddp;
        }
        public FlightCrewVoyageRecordSp getFlightCrewVoyageJson(FlightCrewVoyageRecordModel sd)
        {
            FlightCrewVoyageRecordSp sddp = new FlightCrewVoyageRecordSp();
            sddp.MobileEntry = true;
            sddp.VoyageRecord = sd.VoyageRecord;
            sddp.SectorNumber = FlightCrewVoyageRecord.SectorNumberpickerValue;
            sddp.ScheduledDeparture = sd.ScheduledDeparture.ToString("yyyy-MM-dd") + "T07:00:00Z";
            sddp.FlightNumber = SPUtility.GetLookupIdFromValue(SPUtility.LookupType.FlightNumbers, sd.FlightNumber);
            sddp.AircraftRegistration = SPUtility.GetLookupIdFromValue(SPUtility.LookupType.AircraftRegistraions, sd.AircraftRegistration);
            sddp.DepartureStation = sd.DepartureStation;
            sddp.ArrivalStation = sd.ArrivalStation;
            sddp.LandingBy = sd.LandingBy;
            sddp.ManualorAuto = FlightCrewVoyageRecord.LandingManualOrAutopickerValue;
            sddp.TitleofReport = sd.TitleofReport;
            sddp.ReportCategories = FlightCrewVoyageRecord.ReportCategoriespickerValue;
            sddp.ReportDetails = sd.ReportDetails;// != null ? "<div class=\"ExternalClass733EA004DCC641EFAFED516F5D12CCA7\"><br>\u200b" + sd.ReportDetails + "<br><\u002fdiv>" : null;
            sddp.ReportRaisedBy = sd.ReportRaisedBy;
            sddp.ReplyRequired = FlightCrewVoyageRecord.ReplyRequiredpickerValue;
            sddp.StaffNumber = sd.StaffNumber;
            sddp.Rank = FlightCrewVoyageRecord.RankpickerValue;

            return sddp;
        }
        public KaizenReportSp getKaizenReportJson(KaizenReportModel sd)
        {
            KaizenReportSp sddp = new KaizenReportSp();
            sddp.MobileEntry = true;
            sddp.AreaLocation = sd.AreaLocation;
            sddp.Before = sd.Before != null ? "<div class=\"ExternalClass7A1654F355EA4DB8AE0EFF5A3A06BE7C\"><br>\u200b" + sd.Before + "<br><\u002fdiv>" : null;
            sddp.BenefitsCategory = KaizenReport.BenefitsCategorypickerValue;
            sddp.InitialCondition = sd.InitialCondition != null ? "<div class=\"ExternalClass975C4DB74D7B48519471F19A3D26C255\"><br>\u200b" + sd.InitialCondition + "<br><\u002fdiv>" : null;
            sddp.Subject = sd.Subject;
            sddp.Improvements = sd.Improvements;
            sddp.Department = sd.DepartmentLookupSource[sd.SelectedDepartmentIndex];
            return sddp;
        }
        public StationInformationSp getStationInformationJson(StationInformationModel sd)
        {
            StationInformationSp sddp = new StationInformationSp();
            sddp.MobileEntry = true;
            sddp.IATACode = sd.IATACode;
            if (!string.IsNullOrEmpty(sd.NameofAirport))
            {
                if (Uri.IsWellFormedUriString(sd.NameofAirport, UriKind.Absolute))
                {
                    sddp.NameofAirport = new SPFieldURL
                    {
                        Url = sd.NameofAirport
                    };
                }
                else
                {
                    throw new Exception(string.Format("'{0}' is not a valid url", sd.NameofAirport));
                }
            }
            sddp.GMT = sd.GMT;
            sddp.TerminalRowwhereweoperate = sd.TerminalRowwhereweoperate;
            sddp.StationRep1Name = sd.StationRep1Name;
            sddp.EmailRep1 = sd.EmailRep1;
            sddp.TelRep1 = sd.TelRep1;
            sddp.StationRep2Name = sd.StationRep2Name;
            sddp.EmailRep2 = sd.EmailRep2;
            sddp.TelRep2 = sd.TelRep2;
            sddp.AddressThaiConsulate = sd.AddressThaiConsulate;
            sddp.TelThaiConsulate = sd.TelThaiConsulate;
            sddp.FaxThaiConsulate = sd.FaxThaiConsulate;
            sddp.EmailThaiConsulate = sd.EmailThaiConsulate;
            sddp.WorkingHoursThaiConsulate = sd.WorkingHoursThaiConsulate;
            sddp.CAAContactNameNumber = sd.CAAContactNameNumber;
            sddp.HotelName = sd.HotelName;
            sddp.HotelContactName = sd.HotelContactName;
            sddp.Hoteladdress = sd.Hoteladdress;
            sddp.HotelTel = sd.HotelTel;
            sddp.Contactemail = sd.Contactemail;
            sddp.Calltime = sd.Calltime;
            sddp.Pickuptime = sd.Pickuptime;
            sddp.Transportationcontactinfo = sd.Transportationcontactinfo;
            sddp.Doctorathotel = sd.Doctorathotel;
            sddp.Doctoratairport = sd.Doctoratairport;
            sddp.Recommendedhospital = sd.Recommendedhospital;
            sddp.HealthConsiderations = sd.HealthConsiderations != null ? "<div class=\"ExternalClassEBA91DC7839445C8847E9035E270F71E\"><br>\u200b" + sd.HealthConsiderations + "<br><\u002fdiv>" : null;
            sddp.RestaurantInfo = sd.RestaurantInfo != null ? "<div class=\"ExternalClass73EBF64B70DD4890867E910DD64FB38E\"><br>\u200b" + sd.RestaurantInfo + "<br><\u002fdiv>" : null;
            sddp.AircraftInsecticidebyCabinCrew = sd.AircraftInsecticidebyCabinCrew;
            sddp.BridgeConnect = sd.BridgeConnect;
            sddp.DoorOperation = sd.DoorOperation;
            sddp.FirstLastDoor = sd.FirstLastDoor;
            sddp.noofEquipmentinCompartment = sd.noofEquipmentinCompartment;
            sddp.Engineeronboard = sd.Engineeronboard == true ? "1" : "0";
            sddp.DebriefingProceedto = sd.DebriefingProceedto;
            sddp.SafetyHazards = sd.SafetyHazards;
            sddp.RecommendedSafetyprecautions = sd.RecommendedSafetyprecautions != null ? "<div class=\"ExternalClass8752F7FD8DD1422B836DE5E49E9A0D57\"><br>\u200b" + sd.RecommendedSafetyprecautions + "<br><\u002fdiv>" : null;
            sddp.SecurityThreats = sd.SecurityThreats;
            sddp.RecommendedSecurityPrecautions = sd.RecommendedSecurityPrecautions != null ? "<div class=\"ExternalClassC307A37180794252BC050AD66739039F\"><br>\u200b" + sd.RecommendedSecurityPrecautions + "<br><\u002fdiv>" : null;
            sddp.ImmigrationCustomsConsiderations = sd.ImmigrationCustomsConsiderations != null ? "<div class=\"ExternalClass56BDD2DA3CD34913971207F6D11DE826\"><br>\u200b" + sd.ImmigrationCustomsConsiderations + "<br><\u002fdiv>" : null;
            return sddp;
        }
    }
}
