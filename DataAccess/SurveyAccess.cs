using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace Survey.DataAccess
{
    public class SurveyAccess : Access
    {
        public static DataTable GetNewSurveyURL(Guid SurveyId)
        {
            string SQLStatement = "select * from [dbo].[QUE009_QuestionnaireProvideType] where SurveyId=@SurveyId and ProvideType=1";
            Dictionary<string, object> Params = new Dictionary<string, object>();
            Params.Add("@SurveyId", SurveyId);
            return NewSurveyDB.GetDataTable(SQLStatement, Params);
        }

        /// <summary>
        /// 檢查名單匯出申請表 by 行銷活動方式ID
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="SurveyId"></param>
        /// <returns></returns>
        public static bool CheckListByActivityId(Guid UserId, Guid SurveyId)
        {
            DataTable dtChkSign;
            Dictionary<string, object> Params;

            string SQLStatement;
            SQLStatement = @"SELECT top 1 New_ApplicationName,New_ExportApplicationId 
                      FROM CHT_MSCRM.dbo.New_ExportApplication WITH(NoLock)
                      WHERE New_CampaignActivityGUID= @ActivityId 
                      AND OwnerId = @UserId
                      AND New_State = '3'
                      AND New_ApplicationName IS NOT NULL
                      AND New_ExportApplicationId IS NOT NULL
                      AND New_exportkeydata = '7'
                      AND DeletionStateCode = '0' ";
            Params = new Dictionary<string, object>();
            String ActivityId = "{" + SurveyId.ToString()+"}";
            Params.Add("@ActivityId", ActivityId);
            Params.Add("@UserId", UserId);


            dtChkSign = CrmDB.GetDataTable(SQLStatement, Params);
            if (dtChkSign.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static string GetTitle(Guid SurveyId)
        {
            string retTitle = "";
            string SQLStatement = @"SELECT t.subject,t.ActivityId FROM CampaignActivity t where t.ActivityId = @ActivityId ";
            //SurveyId = new Guid("33333333-0000-0000-0000-000000000010");
            //string SQLStatement = @"SELECT t.Subcategory as subject,t.ActivityId FROM CampaignActivityBase t where t.ActivityId = @ActivityId ";
            Dictionary<string, object> Params = new Dictionary<string, object>();
            Params.Add("@ActivityId", SurveyId);
            DataTable dt = CrmDB.GetDataTable(SQLStatement, Params);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    retTitle = dr["subject"].ToString();
                }
            }
            return retTitle;
        }

        public static string GetProposalUserId(Guid SurveyId)
        {
            string userId = "";
            string typeCode = SurveyLib.Module.Settings.GetConfigValue("TypeCode");
            string sSQL = @" select ca.CreatedBy 
                             from CampaignActivity ca
                             where ca.TypeCode = @TypeCode and ca.ActivityId = @ActivityId";
            Dictionary<string, object> Params = new Dictionary<string, object>();
            Params.Add("@ActivityId", SurveyId);
            Params.Add("@TypeCode", typeCode);
            DataTable dt = CrmDB.GetDataTable(sSQL, Params);
            if (dt.Rows.Count > 0)
            {
                userId = dt.Rows[0]["CreatedBy"].ToString();
            }
            return userId;
        }
    }
}
