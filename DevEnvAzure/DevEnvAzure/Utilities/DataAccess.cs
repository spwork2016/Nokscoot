using DevEnvAzure.Models;
using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DevEnvAzure
{
    public class DataAccess
    {
        SQLiteConnection dbConn;
        public DataAccess()
        {
            var db = DependencyService.Get<IDataService>();
            try
            {
                dbConn = db.GetConnection();

                dbConn.CreateTable<OfflineItem>();
                dbConn.CreateTable<MasterInfo>();
                dbConn.CreateTable<CabibSafetyReport>();
                dbConn.CreateTable<FatigueReport>();
                dbConn.CreateTable<GroundSafetyReport>();
                dbConn.CreateTable<InjuryIllnessReport>();
                dbConn.CreateTable<KaizenReportModel>();
                dbConn.CreateTable<StationInformationModel>();
                dbConn.CreateTable<FlightCrewVoyageRecordModel>();
                dbConn.CreateTable<SecurityModel>();
                dbConn.CreateTable<FlightSafetyReportModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<U> GetAll<U>(string tableName) where U : new()
        {
            var result = dbConn.Query<U>("Select * From " + tableName + "");
            return result;
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

        private int GetPropertyValue<U>(U reportData, string propName) where U : class
        {
            System.Reflection.PropertyInfo pi = reportData.GetType().GetProperty(propName);
            if (pi != null)
            {
                int value = (int)(pi.GetValue(reportData, null));
                return value;
            }
            return 0;
        }

        public U SaveOrUpdate<U>(U reportData) where U : class
        {
            try
            {
                dbConn.CreateTable<U>();

                int id = GetPropertyValue(reportData, "Id");
                if (id == 0)
                    dbConn.Insert(reportData);
                else dbConn.Update(reportData);

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
}
