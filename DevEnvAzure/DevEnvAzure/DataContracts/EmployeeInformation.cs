using Newtonsoft.Json;
using System;

namespace DevEnvAzure.DataContracts
{
    public class EmployeeInformation
    {
        public EmployeeInformation()
        {
            this.__metadata = new Metadata();
            this.__metadata.type = "SP.Data.TestFormListItem";
        }

        public Metadata __metadata { get; set; }

        public object Title { get; set; }
        public string Employee_x0020_Name { get; set; }
        public int? Employee_x0020_Age { get; set; }
        public int? DepartmentId { get; set; }
        public string Employee_x0020_Details { get; set; }
        public string Gender { get; set; }
        public int? Salary { get; set; }
        public DateTime? Joining_x0020_Date { get; set; }
        public bool Active_x0020_Employee { get; set; }
    }

    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string etag { get; set; }
        public string type { get; set; }
    }
}
