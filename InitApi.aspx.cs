using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Survey
{
    public partial class InitApi : System.Web.UI.Page
    {
        public string SurveyHost { get; set; }
        static object lockMe = new object();

        protected void Page_Load(object sender, EventArgs e)
        {
            WriteLog((DateTime.Now).ToString() + " 呼叫預熱");

            SurveyHost = SurveyLib.Module.Settings.GetConfigValue("SurveyHost");
            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.ServerCertificateValidationCallback = CertificateCheck;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.DefaultConnectionLimit = 9999;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(SurveyHost);

            client.SendAsync(new HttpRequestMessage
            {
                Method = new HttpMethod("HEAD"),
                RequestUri = new Uri(SurveyHost + "/")
            }).Result.EnsureSuccessStatusCode();

            WriteLog((DateTime.Now).ToString() + " 預熱結束");
        }

        public static void WriteLog(string sErrMsg)
        {
            String logFileName = DateTime.Now.Year.ToString() + int.Parse(DateTime.Now.Month.ToString()).ToString("00") + int.Parse(DateTime.Now.Day.ToString()).ToString("00") + ".txt";
            String docPath = "C:/Logs/NewSurvey/";
            lock (lockMe)
            {
                using (StreamWriter sw =
                        new StreamWriter(Path.Combine(docPath, logFileName), true))
                {
                    sw.WriteLine(sErrMsg);
                    sw.Close();
                }
            }
        }

        private static bool CertificateCheck(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }
            return false;
        }

    }
}