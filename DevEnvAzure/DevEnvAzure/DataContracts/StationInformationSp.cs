﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEnvAzure.DataContracts
{
    class StationInformationSp
    {
        public StationInformationSp()
        {
            this.__metadata = new Metadata();
            this.__metadata.type = "SP.Data.Ops_x0020_Line_x0020_Station_x0020_InformationListItem";
        }
        public Metadata __metadata { get; set; }

        [JsonProperty(PropertyName = "IATA_x0020_Code")]
        public string IATACode
        { get; set; }
        [JsonProperty(PropertyName = "Wikipedia_x0020_Airport_x0020_In")]
        public string NameofAirport
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
    }
}