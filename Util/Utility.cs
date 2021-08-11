using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Survey.Model;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Survey.Util
{
    public class Utility
    {
        public class UserID
        {
            public string UserId { get; set; }
        }

        public string GetToken(string userId)
        {
            string surveyHost = SurveyLib.Module.Settings.GetConfigValue("SurveyHost");
            string token = "";
            string responseBody = "";
            userId = ToBase64(userId);
            UserID data = new UserID { UserId = userId };
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(surveyHost);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.ServerCertificateValidationCallback = CertificateCheck;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.DefaultConnectionLimit = 9999;
                HttpResponseMessage response = client.PostAsync("/V2/api/system/auth", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    responseBody = response.Content.ReadAsStringAsync().Result.Replace(@"\", "");
                    AuthInfo ret = JsonConvert.DeserializeObject<AuthInfo>(responseBody);

                    if (ret.code == "200")
                    {
                        AuthData authData = JsonConvert.DeserializeObject<AuthData>(JsonConvert.SerializeObject(ret.data));
                        token = "Bearer " + authData.Token;
                    }
                    else
                    {
                        token = "";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return token;
        }
        private string removeSpecialCharactersPath(string str)
        {
            string returnvalue = "";
            string pattern = "([A-Z]|[a-z]|[]|\\d|\\s|[+,-\\\\.*()_\"'|:<>@!#$%^&={}]|[\u4e00-\u9fa5])";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.MatchCollection a = regex.Matches(str);
            for (int i = 0; i < a.Count; i++)
            {
                returnvalue += a[i].Value.ToString();
            }
            return returnvalue;
        }
        private bool CertificateCheck(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }
            return false;
            //return !certificate.Issuer.Equals("Iron Man");
        }

        public int GetSurveyReplyCnt(string survryId, string userId)
        {
            int retCnt = 0;

            string responseBody = "";

            string token;
            try
            {
                string surveyHost = SurveyLib.Module.Settings.GetConfigValue("SurveyHost");
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(surveyHost);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.ServerCertificateValidationCallback = CertificateCheck;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.DefaultConnectionLimit = 9999;
                token = GetToken(userId);
                if (token != "")
                {
                    client.DefaultRequestHeaders.Add("Authorization", token);
                    //HttpResponseMessage response = client.GetAsync("/V2/api/Analytics/QueryReplyNum?SurveyId=" + survryId + "&Env=2").Result;
                    //HttpResponseMessage response = client.GetAsync("/api/Analytics/QueryReplyNum?SurveyId=" + survryId + "&Env=2").Result;
                    var url = "/V2/api/Analytics/QueryReplyNum?SurveyId=" + survryId + "&Env=2";
                    HttpResponseMessage response = client.GetAsync(removeSpecialCharactersPath(url)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        responseBody = response.Content.ReadAsStringAsync().Result.Replace(@"\", "").Replace(@":""[", @":[").Replace(@"]"",", "],");
                        AnalyticsData ret = (AnalyticsData)JsonConvert.DeserializeObject(responseBody, typeof(AnalyticsData));
                        if (ret.code == "200")
                        {
                            retCnt = ret.data.TotalReplyNum;
                        }
                        else
                        {
                            //to do this 
                            //alert('新增問卷失敗!!');
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retCnt;
        }
        protected string ToBase64(string userID)
        {
            string ret = "";
            byte[] bytes = System.Text.Encoding.GetEncoding("utf-8").GetBytes(userID);
            ret = Convert.ToBase64String(bytes);
            return ret;
        }
    }
}