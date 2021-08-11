using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Survey.DataAccess
{
    public abstract class Access
    {
        protected class NewSurveyDB : SurveyLib.Access.baseDS
        {
            //2017-9-1 modify by Ken for Password Management: Password in Configuration File
            //private static string connstr = SurveyLib.Module.Settings.GetSQLConfigValue("SurveyDBConnectString");
            private static string connstr = SurveyLib.Module.Settings.GetSQLConfigValue("NewSruveyDBConnectString").Replace("Passingword", "Password");
            private static int BatchCount = 1000;

            public static DataTable GetDataTableBySp(string SPName, Dictionary<string, object> Params)
            { return GetDTbySp(SPName, Params, connstr); }
            public static DataTable GetDataTable(string SQL, Dictionary<string, object> Params)
            { return GetDT(SQL, Params, connstr); }

            public static void ExecuteCmd(string SQL, Dictionary<string, object> Params)
            { ExecuteCommand(SQL, Params, connstr, false); }

            public static void ExecuteCmdBulk(string SQL, Dictionary<string, object> Params)
            { ExecuteCommand(SQL, Params, connstr, true); }

            public static void ExecuteSP(string SQL, Dictionary<string, object> Params)
            { ExececuteSP(SQL, Params, connstr); }
            //Neil 2017/09/22 修改
            public static DataTable GetSQLReader(string SQL, Dictionary<string, object> Params)
            // public static SqlDataReader GetSQLReader(string SQL, Dictionary<string, object> Params)
            { return ExecuteByReader(SQL, Params, connstr); }
            public static void BulkCopyToDB(string TableName, DataTable TotalDT)
            { BulkCopy(TableName, TotalDT, BatchCount, connstr); }

            public static void CloseConnection()
            { Close(); }
        }
        protected class CrmDB : SurveyLib.Access.baseDS
        {
            //2017-8-31 modify by Ken for Password Management: Password in Configuration File
            //private static string connstr = SurveyLib.Module.Settings.GetSQLConfigValue("MsCrmDBConnectString");
            private static string connstr = SurveyLib.Module.Settings.GetSQLConfigValue("MsCrmDBConnectString").Replace("Passingword", "Password");
            private static int BatchCount = 1000;

            public static DataTable GetDataTableBySp(string SPName, Dictionary<string, object> Params)
            { return GetDTbySp(SPName, Params, connstr); }
            public static DataTable GetDataTable(string SQL, Dictionary<string, object> Params)
            { return GetDT(SQL, Params, connstr); }

            public static void ExecuteCmd(string SQL, Dictionary<string, object> Params)
            { ExecuteCommand(SQL, Params, connstr, false); }

            public static void ExecuteCmdBulk(string SQL, Dictionary<string, object> Params)
            { ExecuteCommand(SQL, Params, connstr, true); }

            public static void ExecuteSP(string SQL, Dictionary<string, object> Params)
            { ExececuteSP(SQL, Params, connstr); }
            //Neil 2017/09/22 修改
            // public static SqlDataReader GetSQLReader(string SQL, Dictionary<string, object> Params)
            public static DataTable GetSQLReader(string SQL, Dictionary<string, object> Params)
            { return ExecuteByReader(SQL, Params, connstr); }
            public static void BulkCopyToDB(string TableName, DataTable TotalDT)
            { BulkCopy(TableName, TotalDT, BatchCount, connstr); }

            public static void CloseConnection()
            { Close(); }
        }
    }

}
