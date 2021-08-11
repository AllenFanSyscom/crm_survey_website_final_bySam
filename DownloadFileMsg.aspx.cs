using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Survey.Util;

namespace Survey
{
    public partial class DownloadFileMsg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility util = new Utility();
            string userId = Request.QueryString["userID"].ToString();
            string surveyId = Request.QueryString["surveyID"].ToString();
            int ret =  util.GetSurveyReplyCnt(surveyId, userId);
            if(ret >0 )
            {
                divHasData.Visible = true;
                divNodata.Visible = false;
            }
            else
            {
                divHasData.Visible = false;
                divNodata.Visible = true;
            }
        }
    }
}