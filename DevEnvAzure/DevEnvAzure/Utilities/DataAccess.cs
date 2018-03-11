//using SQLite.Net.Attributes;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite.Net.Interop;
using DevEnvAzure.Models;
using System.Net.Http;

namespace DevEnvAzure
{
    [Table(@"DatatableData1")]
    public class DatatableData
    {
        [NotNull]
        [PrimaryKey, AutoIncrement, Column("contentID")]
        public long contentID
        { get; set; }
        public string Value
        { set; get; }
    }
    public class DataAccess
    {
        SQLite.Net.SQLiteConnection dbConn;
        public DataAccess()
        {
            dbConn = DependencyService.Get<IFilePath>().GetConnection();
            try
            {
                dbConn.CreateTable<Employee>();
                dbConn.CreateTable<MasterInfo>();

               // dbConn.CreateTable<MasterInfo>();
                dbConn.CreateTable<FlightSafetyReportModel>();
                dbConn.CreateTable<SecurityModel>();
                dbConn.CreateTable<CabibSafetyReport>();
                dbConn.CreateTable<FatigueReport>();
                dbConn.CreateTable<GroundSafetyReport>();
                dbConn.CreateTable<InjuryIllnessReport>();

                dbConn.CreateTable<DatatableData>();
                //dbConn.
            }
            catch (Exception ex)
            {

            }
        }
        

        public bool createTable<U>() where U : class
        {
         //   var tableExistsQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='MovieId';";
         //   var result = dbConn.ExecuteScalar<string>(tableExistsQuery);
           // if (result.Length == 0)
            {
                dbConn.CreateTable<U>();
                return true;
            }
         //   else
          //  {
          //      return false;
          //  }

        }

        public List<U>GetAllEmployees<U>(string classObj) where U:class
        {
            return dbConn.Query<U>("Select * From " + classObj + "");
        }
        public int SaveEmployee<U>(U reportData) where U : class
        {
            try
            {
                dbConn.CreateTable<U>();
                return dbConn.Insert(reportData);
            }
            catch(Exception ex)
            {
                return 0;
            }

          
        }
        public int DeleteEmployee<U>(U aEmployee) where U:class
        {
            return dbConn.Delete(aEmployee);
        }
        public int DeleteEmployeeAdded(long empid)
        {
            return dbConn.Delete(empid);
        }
        public int EditEmployee(Employee aEmployee)
        {
            return dbConn.Update(aEmployee);
        }

        public MasterInfo GetMasterInfoByName(string name)
        {
            var info = dbConn.Query<MasterInfo>(string.Format("select * from [MasterInfo] where Name='{0}'", name));
            return info.Count > 0 ? info[0] : null;
        }

        public int SaveMasterInfo(MasterInfo info)
        {
            return dbConn.Insert(info);
        }

        public int RefreshMasterInfo(MasterInfo info)
        {
            //info.Created = DateTime.Now;
            dbConn.Query<MasterInfo>(string.Format("delete from [MasterInfo] where Name='{0}'", info.Name));
            var d = SaveMasterInfo(info);
            return d;
        }
    }

    // [SQLite.("Parent")]
    [Table(@"Employee")]
    public class Employee
    {
        //[SQLite.Net.Attributes.PrimaryKey, SQLite.Net.Attributes.AutoIncrement, SQLite.Net.Attributes.Column("ID")]
        [PrimaryKey, AutoIncrement, Column("EmpId")]
        public long EmpId
        { get; set; }
        [NotNull]
        public string vEmpName
        { get; set; }
        public string vEmpAge
        { get; set; }
        public string vEmpDept
        { get; set; }
        public string vEmpDetails
        { get; set; }
        public string vEmpGender
        { get; set; }
        public string vEmpSal
        { get; set; }
        public DateTime vEmpDate
        { get; set; }
        public string vEmpActive
        { get; set; }
    }

    public class MasterInfo
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public long Id
        { get; set; }
        [NotNull]
        public string Name
        { get; set; }
        [NotNull]
        public string content
        { get; set; }
       // [NotNull]
        //public DateTime Created
        //{ get; set; }
    }
}
