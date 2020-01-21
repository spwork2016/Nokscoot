using DevEnvAzure.Models;
using DevEnvAzure.Utilities;
using Newtonsoft.Json;
using System;

namespace DevEnvAzure.DataContracts
{
    [Preserve(AllMembers = true)]
    public class SPFieldURL
    {
        public SPFieldURL()
        {
            __metadata = new Metadata { type = "SP.FieldUrlValue" };
        }

        public Metadata __metadata { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class StationInformationSp
    {
        public StationInformationSp()
        {
            this.__metadata = new Metadata();
            //this.__metadata.type = "SP.Data.Ops_x0020_Line_x0020_Station_x0020_InformationListItem";
            this.__metadata.type = "SP.Data.Line_x0020_Station_x0020_Risk_x0020_AssessmentListItem";
        }

        public StationInformationModel GetModel()
        {
            StationInformationModel model = new StationInformationModel();
            model.Id = Id;

            model.AddressThaiConsulate = AddressThaiConsulate;
            model.AircraftInsecticidebyCabinCrew = AircraftInsecticidebyCabinCrew;
            model.BridgeConnect = BridgeConnect;
            model.CAAContactNameNumber = CAAContactNameNumber;
            model.Calltime = Calltime;
            model.Contactemail = Contactemail;
            model.DebriefingProceedto = DebriefingProceedto;
            model.Doctoratairport = Doctoratairport;
            model.Doctorathotel = Doctorathotel;
            model.DoorOperation = DoorOperation;
            model.EmailRep1 = EmailRep1;
            model.EmailRep2 = EmailRep2;
            model.EmailThaiConsulate = EmailThaiConsulate;
            model.Engineeronboard = Convert.ToBoolean(Engineeronboard);
            model.FaxThaiConsulate = FaxThaiConsulate;
            model.FirstLastDoor = FirstLastDoor;
            model.GMT = GMT;
            model.HealthConsiderations = HealthConsiderations;
            model.Hoteladdress = Hoteladdress;
            model.HotelContactName = HotelContactName;
            model.HotelName = HotelName;
            model.HotelTel = HotelTel;
            model.IATACode = IATACode;
            model.ImmigrationCustomsConsiderations = ImmigrationCustomsConsiderations;
            model.NameofAirport = NameofAirport != null ? NameofAirport.Description : "";
            model.NameofAirportLink = NameofAirport != null ? NameofAirport.Url : "";
            model.noofEquipmentinCompartment = noofEquipmentinCompartment;
            model.Pickuptime = Pickuptime;
            model.Recommendedhospital = Recommendedhospital;
            model.RecommendedSafetyprecautions = RecommendedSafetyprecautions;
            model.RecommendedSecurityPrecautions = RecommendedSecurityPrecautions;
            model.RestaurantInfo = RestaurantInfo;
            model.SafetyHazards = SafetyHazards;
            model.SecurityThreats = SecurityThreats;
            model.StationRep1Name = StationRep1Name;
            model.StationRep2Name = StationRep2Name;
            model.TelRep1 = TelRep1;
            model.TelRep2 = TelRep2;
            model.TelThaiConsulate = TelThaiConsulate;
            model.TerminalRowwhereweoperate = TerminalRowwhereweoperate;
            model.Transportationcontactinfo = Transportationcontactinfo;
            model.WorkingHoursThaiConsulate = WorkingHoursThaiConsulate;

            return model;
        }

        public Metadata __metadata { get; set; }
        public int Id { get; set; }
        [JsonProperty(PropertyName = "IATA_x0020_Code")]
        public string IATACode
        { get; set; }
        [JsonProperty(PropertyName = "Wikipedia_x0020_Airport_x0020_In")]
        public SPFieldURL NameofAirport
        { get; set; }                  // check the hyper link binding
        [JsonProperty(PropertyName = "GMT")]
        public string GMT
        { get; set; }
        [JsonProperty(PropertyName = "Terminal")]
        public string TerminalRowwhereweoperate
        { get; set; }
        [JsonProperty(PropertyName = "Station_x0020_Rep_x0020_1_x0020_")]
        public string StationRep1Name
        { get; set; }
        [JsonProperty(PropertyName = "Station_x0020_Rep_x0020__x0028_1")]
        public string EmailRep1
        { get; set; }
        [JsonProperty(PropertyName = "Station_x0020_Rep_x0020__x0028_10")]
        public string TelRep1
        { get; set; }
        [JsonProperty(PropertyName = "Station_x0020_Rep_x0020__x0028_2")]
        public string StationRep2Name
        { get; set; }
        [JsonProperty(PropertyName = "Station_x0020_Rep_x0020__x0028_20")]
        public string EmailRep2
        { get; set; }
        [JsonProperty(PropertyName = "Station_x0020_Rep_x0020__x0028_21")]
        public string TelRep2
        { get; set; }
        [JsonProperty(PropertyName = "Thai_x0020_Consulate_x0020_Addre")]
        public string AddressThaiConsulate
        { get; set; }
        [JsonProperty(PropertyName = "Thai_x0020_Consulate_x0020_Tel_x")]
        public string TelThaiConsulate
        { get; set; }
        [JsonProperty(PropertyName = "Thai_x0020_Consulate_x0020_Fax_x")]
        public string FaxThaiConsulate
        { get; set; }
        [JsonProperty(PropertyName = "Thai_x0020_Consulate_x0020_e_x00")]
        public string EmailThaiConsulate
        { get; set; }
        [JsonProperty(PropertyName = "Thai_x0020_Consulate_x0020_Worki")]
        public string WorkingHoursThaiConsulate
        { get; set; }
        [JsonProperty(PropertyName = "CAA_x0020_Contact_x0020_Name_x00")]
        public string CAAContactNameNumber
        { get; set; }
        [JsonProperty(PropertyName = "Hotel_x0020_Name")]
        public string HotelName
        { get; set; }
        [JsonProperty(PropertyName = "Hotel_x0020_contact_x0020_name")]
        public string HotelContactName
        { get; set; }
        [JsonProperty(PropertyName = "Hotel_x0020_address")]
        public string Hoteladdress
        { get; set; }
        [JsonProperty(PropertyName = "Hotel_x0020_contact_x0020_Tel_x0")]
        public string HotelTel
        { get; set; }
        [JsonProperty(PropertyName = "Hotel_x0020_contact_x0020_e_x002")]
        public string Contactemail
        { get; set; }
        [JsonProperty(PropertyName = "Call_x0020_time")]
        public string Calltime
        { get; set; }
        [JsonProperty(PropertyName = "Hotel_x0020_pick_x0020_up_x0020_")]
        public string Pickuptime
        { get; set; }
        [JsonProperty(PropertyName = "Transportation_x0020_contact_x00")]
        public string Transportationcontactinfo
        { get; set; }
        [JsonProperty(PropertyName = "Doctor_x0020_at_x0020_hotel")]
        public string Doctorathotel
        { get; set; }
        [JsonProperty(PropertyName = "Doctor_x0020_at_x0020_airport")]
        public string Doctoratairport
        { get; set; }
        [JsonProperty(PropertyName = "Recommended_x0020_hospital")]
        public string Recommendedhospital
        { get; set; }
        [JsonProperty(PropertyName = "Health_x0020_Considerations")]
        public string HealthConsiderations
        { get; set; }
        [JsonProperty(PropertyName = "Restaurant_x0020_Info")]
        public string RestaurantInfo
        { get; set; }
        [JsonProperty(PropertyName = "Aircraft_x0020_Insectiside_x0020")]
        public string AircraftInsecticidebyCabinCrew
        { get; set; }
        [JsonProperty(PropertyName = "Bridge_x0020_Connect")]
        public string BridgeConnect
        { get; set; }
        [JsonProperty(PropertyName = "Door_x0020_Operation")]
        public string DoorOperation
        { get; set; }
        [JsonProperty(PropertyName = "First_x0020__x002f__x0020_Last_x")]
        public string FirstLastDoor
        { get; set; }
        [JsonProperty(PropertyName = "OData__x0023__x0020_of_x0020_Equipment")]
        public string noofEquipmentinCompartment
        { get; set; }
        [JsonProperty(PropertyName = "Engineer_x0020_on_x0020_board_x0.value")]
        public string Engineeronboard
        { get; set; }
        [JsonProperty(PropertyName = "Debriefing_x0020_proceed_x0020_t")]
        public string DebriefingProceedto
        { get; set; }
        [JsonProperty(PropertyName = "Safety_x0020_Hazards")]
        public string SafetyHazards
        { get; set; }
        [JsonProperty(PropertyName = "Recommended_x0020_precautions")]
        public string RecommendedSafetyprecautions
        { get; set; }
        [JsonProperty(PropertyName = "Security_x0020_Threats")]
        public string SecurityThreats
        { get; set; }
        [JsonProperty(PropertyName = "Recommended_x0020_Security_x0020")]
        public string RecommendedSecurityPrecautions
        { get; set; }
        [JsonProperty(PropertyName = "Immigration")]
        public string ImmigrationCustomsConsiderations
        { get; set; }
        [JsonProperty(PropertyName = "MobileEntry")]
        public bool MobileEntry { get; set; }
    }
}
