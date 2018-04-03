﻿//using SQLite.Net.Attributes;
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
                dbConn.CreateTable<FlightSafetyReportModel>();
                dbConn.CreateTable<SecurityModel>();
                dbConn.CreateTable<CabibSafetyReport>();
                dbConn.CreateTable<FatigueReport>();
                dbConn.CreateTable<GroundSafetyReport>();
                dbConn.CreateTable<InjuryIllnessReport>();
                dbConn.CreateTable<KaizenReportModel>();
                dbConn.CreateTable<StationInformationModel>();
                dbConn.CreateTable<FlightCrewVoyageRecordModel>();

                dbConn.CreateTable<DatatableData>();
            }
            catch (Exception ex)
            {

            }
        }


        public bool createTable<U>() where U : class
        {
            try
            {
                dbConn.CreateTable<U>();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<U> GetAll<U>(string classObj) where U : class
        {
            return dbConn.Query<U>("Select * From " + classObj + "");
        }
        public U Save<U>(U reportData) where U : class
        {
            try
            {
                dbConn.CreateTable<U>();
                dbConn.Insert(reportData);
                return reportData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public U Update<U>(U reportData) where U : class
        {
            try
            {
                dbConn.Update(reportData);
                return reportData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Delete<Y>(Y entity) where Y : class
        {
            return dbConn.Delete(entity);
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

        public void DeleteMasterInfo(string name)
        {
            dbConn.Query<MasterInfo>(string.Format("delete from [MasterInfo] where Name='{0}'", name));
        }

        public int RefreshMasterInfo(MasterInfo info)
        {
            DeleteMasterInfo(info.Name);
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
