using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Survey.Model;
using Survey.Util;

namespace Survey
{
    public partial class RejectSurrvey1 : System.Web.UI.Page
    {
        public string UserId { get; set; }

        public string SurveyId { get; set; }

        public class RejectSurvey
        {
            public string UserId { get; set; }

            public string SurveyId { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserId = Request.QueryString["userID"].ToString();
            SurveyId = Request.QueryString["surveyID"].ToString();
            //UserId = "00000000-0000-0000-000000000000";
            //SurveyId = "88888888-0000-0000-000000000005";
            DoRejectSurvey();

        }

        protected void DoRejectSurvey()
        {

            string surveyHost = SurveyLib.Module.Settings.GetConfigValue("SurveyHost");
            string responseBody = "";
            string token = "";
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("surveyID ==>" + surveyId + "<br />");
            //context.Response.Write("userID ==>" + userId);

            try
            {
                //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.ServerCertificateValidationCallback = CertificateCheck;//delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.DefaultConnectionLimit = 9999;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(surveyHost);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Utility util = new Utility();
                token = util.GetToken(UserId);
                if (token != "")
                {
                    RejectSurvey data = new RejectSurvey { SurveyId = SurveyId, UserId = UserId };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", token);
                    HttpResponseMessage response = client.PutAsync("/V2/api/Survey/Question/CollectionWay/Reject", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        responseBody = response.Content.ReadAsStringAsync().Result.Replace(@"\", "");
                        ResultData ret = (ResultData)JsonConvert.DeserializeObject(responseBody, typeof(ResultData));
                        if (ret.code == "200")
                        {
                            //Response.ContentType = "text/plain";
                            //Response.Write(ret.message);
                        }
                        else
                        {
                            //Response.ContentType = "text/plain";
                            //Response.Write(ret.message);
                        }
                    }
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSaveMessage",
                                 "<script language='javascript'>window.open('', '_self', '');window.close();</script>");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSaveMessage",
                                "<script language='javascript'>window.open('', '_self', '');window.close();</script>");
                throw ex;
            }
        }

        private bool CertificateCheck(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            //return !certificate.Issuer.Equals("Iron Man");
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }
            return false;
        }

    }
}