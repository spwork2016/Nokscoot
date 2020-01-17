using SQLite;
using System;

namespace DevEnvAzure.Models
{
    [Preserve(AllMembers = true)]
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
        public string Improvements
        { get; set; }
        public int SelectedDepartmentIndex
        { get; set; }


        public string[] DepartmentLookupSource
        {
            get
            {
                return new string[] { "", "CEO",
                                        "CAB",
                                        "COM",
                                        "FIN",
                                        "FLT",
                                        "GRH/CGO",
                                        "HR",
                                        "MNT",
                                        "PLN",
                                        "SSQ",
                                        "TRG",
                                        "ALL OPS" };
            }
        }
        //local usage - to show datetime in drafts page
        public DateTime Created
        {
            get; set;
        }

        public string Attachments { get; set; }
    }
}
