namespace DevEnvAzure.Models
{
    public class OperatingPlan
    {
        public string FlighNumber { get; set; }
        public int ArrivalStationId { get; set; }
        public int DepartureStationId { get; set; }

        public string ArrivalStation { get; set; }
        public string DepartureStation { get; set; }
    }
}
