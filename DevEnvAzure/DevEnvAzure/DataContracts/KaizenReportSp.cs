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


        [JsonProperty(PropertyName = "Area_x0020__x002f__x0020_Locatio")]
        public string AreaLocation
        { get; set; }
        [JsonProperty(PropertyName = "Before")]
        public string Before
        { get; set; }
        [JsonProperty(PropertyName = "Benefits_x0020_Category")]
        public string BenefitsCategory
        { get; set; }

        [JsonProperty(PropertyName = "Initial_x0020_Condition")]
        public string InitialCondition
        { get; set; }

        [JsonProperty(PropertyName = "Subject")]
        public string Subject
        { get; set; }

        [JsonProperty(PropertyName = "MobileEntry")]
        public bool MobileEntry { get; set; }

        [JsonProperty(PropertyName = "Suggestions_x0020_for_x0020_impr")]
        public string Improvements { get; set; }

        [JsonProperty(PropertyName = "Department_x0020__x002f__x0020_D")]
        public string Department { get; set; }

    }
}
