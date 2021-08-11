using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using SurveyLib;
using System.Web.Services;
using Survey.Util;

namespace Survey
{
    public partial class _Default : System.Web.UI.Page
    {
        #region Properties
        public Guid ListId
        {
            get
            {
                if (Request.QueryString["listid"] != null)
                { return new Guid(Request.QueryString["listid"].ToString()); }
                else { return Guid.Empty; }
            }
        }
        public Guid SurveyId
        {
            get
            {
                if (Request.QueryString["aid"] != null)
                { return new Guid(Request.QueryString["aid"].ToString()); }
                else { return Guid.Empty; }
            }
        }
        public Guid UserId
        {
            get
            {
                if (Request.QueryString["uid"] != null)
                { return new Guid(Request.QueryString["uid"].ToString()); }
                else { return Guid.Empty; }
            }
        }
        public String Title
        {
            get
            {
                if (Request.QueryString["title"] != null)
                { return Request.QueryString["title"].ToString(); }
                else { return ""; }
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

       
    }
}
