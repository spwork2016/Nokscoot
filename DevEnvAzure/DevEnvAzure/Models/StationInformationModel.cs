using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEnvAzure.Models
{
    [Table(@"StationInformationModel")]
    public class StationInformationModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        { get; set; }
        public string IATACode
        { get; set; }
        public string NameofAirport
        { get; set; }                  // check the hyper link binding
        public string GMT
        { get; set; }
        public string TerminalRowwhereweoperate
        { get; set; }
        public string StationRep1Name
        { get; set; }
        public string EmailRep1
        { get; set; }
        public string TelRep1
        { get; set; }
        public string StationRep2Name
        { get; set; }
        public string EmailRep2
        { get; set; }
        public string TelRep2
        { get; set; }
        public string AddressThaiConsulate 
        { get; set; }
        public string TelThaiConsulate 
        { get; set; }
        public string FaxThaiConsulate
        { get; set; }       
        public string EmailThaiConsulate
        { get; set; }
        public string WorkingHoursThaiConsulate
        { get; set; }
        public string CAAContactNameNumber
        { get; set; }
        public string HotelName
        { get; set; }
        public string HotelContactName
        { get; set; }
        public string Hoteladdress
        { get; set; }
        public string HotelTel  
        { get; set; }
        public string Contactemail
        { get; set; }
        public string Calltime
        { get; set; }
        public string Pickuptime
        { get; set; }
        public string Transportationcontactinfo
        { get; set; }
        public string Doctorathotel
        { get; set; }
        public string Doctoratairport
        { get; set; }
        public string Recommendedhospital
        { get; set; }
        public string HealthConsiderations
        { get; set; }
        public string RestaurantInfo
        { get; set; }
        public string AircraftInsecticidebyCabinCrew
        { get; set; }
        public string BridgeConnect
        { get; set; }
        public string DoorOperation
        { get; set; }
        public string FirstLastDoor
        { get; set; }
        public string noofEquipmentinCompartment
        { get; set; }
        public bool Engineeronboard     
        { get; set; }
        public string DebriefingProceedto 
        { get; set; }
        public string SafetyHazards
        { get; set; }
        public string RecommendedSafetyprecautions
        { get; set; }
        public string SecurityThreats
        { get; set; }
        public string RecommendedSecurityPrecautions
        { get; set; }
        public string ImmigrationCustomsConsiderations
        { get; set; }   


    }
}
