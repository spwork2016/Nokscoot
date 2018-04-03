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
        public string ReportType
        { get; set; }
        public DateTime? DateOfEvent
        { get; set; }

        public string AreaLocation
        { get; set; }
        public string Before
        { get; set; }
        public int BenefitsCategory
        { get; set; }
        public string InitialCondition
        { get; set; }
        public string Subject
        { get; set; }

    }
}
