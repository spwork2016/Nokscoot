using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEnvAzure.DataContracts
{

    class KaizenReportSp
    {
        public KaizenReportSp()
        {
            this.__metadata = new Metadata();
            this.__metadata.type = "SP.Data.Kaizen_x0020_ReportListItem";
            
        }
        public Metadata __metadata { get; set; }

        [JsonProperty(PropertyName = "After")]
        public string After
        { get; set; }
        [JsonProperty(PropertyName = "Approved_x0020_ByStringId")]
        public string ApprovedBy
        { get; set; }
        [JsonProperty(PropertyName = "Area_x0020__x002f__x0020_Locatio")]
        public string AreaLocation
        { get; set; }
        [JsonProperty(PropertyName = "Before")]
        public string Before
        { get; set; }
        [JsonProperty(PropertyName = "Benefits_x0020_Category")]
        public string BenefitsCategory
        { get; set; }
        [JsonProperty(PropertyName = "Benefits_x0020_Description")]
        public string BenefitsDescription
        { get; set; }
        [JsonProperty(PropertyName = "Contact_x0020_Details")]
        public string ContactDetails
        { get; set; }
        [JsonProperty(PropertyName = "Date_x0020_of_x0020_Completion")]
        public string DateofCompletion
        { get; set; }
        [JsonProperty(PropertyName = "Department_x0020__x002f__x0020_D")]
        public string DepartmentDivision
        { get; set; }
        [JsonProperty(PropertyName = "Implementation_x0020_Date")]
        public string ImplementationDate
        { get; set; }
        [JsonProperty(PropertyName = "Initial_x0020_Condition")]
        public string InitialCondition
        { get; set; }
        [JsonProperty(PropertyName = "Process_x0020__x002f__x0020_Proj")]
        public string ProcessProject
        { get; set; }
        [JsonProperty(PropertyName = "Reference_x0023_")]
        public string Reference
        { get; set; }
        [JsonProperty(PropertyName = "Solution_x0020_Description")]
        public string SolutionDescription
        { get; set; }
        [JsonProperty(PropertyName = "Standardization_x0020_Remarks")]
        public string StandardizationRemarks
        { get; set; }
        [JsonProperty(PropertyName = "Subject")]
        public string Subject
        { get; set; }
        [JsonProperty(PropertyName = "Team_x0020_Members")]
        public string TeamMembers
        { get; set; }
        [JsonProperty(PropertyName = "Validated_x0020_ByStringId")]
        public string ValidatedBy
        { get; set; }

    }
}
