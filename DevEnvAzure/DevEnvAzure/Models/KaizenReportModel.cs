using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEnvAzure.Models
{
    [Table(@"KaizenReportModel")]
    public class KaizenReportModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        { get; set; }
        public string After
        { get; set; }
        public string ApprovedBy
        { get; set; }
        public string AreaLocation
        { get; set; }
        public string Before
        { get; set; }
        public int BenefitsCategory
        { get; set; }
        public string BenefitsDescription
        { get; set; }
        public string ContactDetails
        { get; set; }
        public DateTime DateofCompletion
        { get; set; }
        public string DepartmentDivision
        { get; set; }
        public DateTime ImplementationDate
        { get; set; }
        public string InitialCondition
        { get; set; }
        public string ProcessProject
        { get; set; }
        public string Reference
        { get; set; }
        public string SolutionDescription
        { get; set; }
        public string StandardizationRemarks
        { get; set; }
        public string Subject
        { get; set; }
        public string TeamMembers
        { get; set; }
        public string ValidatedBy
        { get; set; }

    }
}
