using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Survey.Model;
using Newtonsoft.Json;
using System.IO;
using System.Web.Services;
using Survey.Util;
using Survey.DataAccess;

namespace Survey
{
    public partial class OnLineSurvey : System.Web.UI.UserControl
    {
        #region Properties

        public _Default ParentPage
        {
            get { return (_Default)this.Parent.Page; }
        }

        #endregion

        public string SurveyHost { get; set; }

        

        public class SurveyInsData
        {
            public string Uid { get; set; }

            public string SurveyId { get; set; }

            public string Title { get; set; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SurveyHost = SurveyLib.Module.Settings.GetConfigValue("SurveyHost");
            txtBatPath.Value = SurveyLib.Module.Settings.GetConfigValue("BatPath");
        }

        static string encryptKey = SurveyLib.Module.Settings.GetConfigValue("DownloadKey");
        static string encryptIV = SurveyLib.Module.Settings.GetConfigValue("DownloadIV");
        //protected string Encrypt(string str)
        //{
        //    try
        //    {
        //        byte[] key = Encoding.Unicode.GetBytes(encryptKey);
        //        byte[] data = Encoding.Unicode.GetBytes(str);

        //        DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();
        //        System.IO.MemoryStream MStream = new System.IO.MemoryStream();

        //        CryptoStream CStream = new CryptoStream(MStream, descsp.CreateEncryptor(key, key), System.Security.Cryptography.CryptoStreamMode.Write);
        //        CStream.Write(data, 0, data.Length);
        //        CStream.FlushFinalBlock();
        //        byte[] temp = MStream.ToArray();
        //        CStream.Close();
        //        MStream.Close();

        //        return Convert.ToBase64String(temp);
        //    }
        //    catch (Exception ex)
        //    {
        //        return str;
        //    }
        //}
        protected string Encrypt(string str)
        {
            try
            {
                byte[] key = UTF8Encoding.UTF8.GetBytes(encryptKey);
                byte[] IVKey = UTF8Encoding.UTF8.GetBytes(encryptIV);
                byte[] data = UTF8Encoding.UTF8.GetBytes(str);

                using (RijndaelManaged rDel = new RijndaelManaged())
                {
                    //RijndaelManaged rDel = new RijndaelManaged();
                    rDel.Key = key;
                    rDel.IV = IVKey;
                    //rDel.Mode = CipherMode.CBC;
                    //rDel.Padding = PaddingMode.PKCS7;
                    ICryptoTransform cTransform = rDel.CreateEncryptor(key, IVKey);
                    byte[] resultArray = cTransform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
                }

            }
            catch (Exception ex)
            {
                return str;
            }
        }

        protected void btnAgree_Click(object sender, EventArgs e)
        {
            tr1.Visible = false;
            tr2.Visible = true;

            try
            {
              

                //設定問卷以及QRCode網址
                SetURL();

                //設定下載問卷按鈕狀態
                SetDownfileButton();

                //產生問卷
                InsertSurvey();
            }
            catch(Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSaveMessage",
                          "<script language='javascript'>alert('"+ ex.Message + "'); </script>");
                
            }
        }

        protected void SetURL()
        {
            string responseBody = "";
            string token = "";
            string surveyId = ParentPage.SurveyId.ToString();
            try
            {
                //設定問卷網址初始狀態
                lnkFinal.Text = "無";

                lnkTest.Text = "無";

                //設定QR Code網址初始狀態(比照問卷網址初始狀態)
                lnkFinalQRCode.Text = lnkFinal.Text;
                lnkTestQRCode.Text = lnkTest.Text;

                //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.ServerCertificateValidationCallback = CertificateCheck;//delegate { return true; };ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.DefaultConnectionLimit = 9999;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(SurveyHost);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                Utility util = new Utility();
                token = util.GetToken(ParentPage.UserId.ToString());

               
                if (token != "")
                {
                    client.DefaultRequestHeaders.Add("Authorization", token);
                    HttpResponseMessage response = client.GetAsync("/V2/api/Survey/Question/CollectionWay/Query?SurveyId=" + surveyId).Result;

                 
                    if (response.IsSuccessStatusCode)
                    {
                        responseBody = response.Content.ReadAsStringAsync().Result.Replace(@"\", "");
                        ResultData ret = (ResultData)JsonConvert.DeserializeObject(responseBody/*.Substring(1, responseBody.Length - 2)*/, typeof(ResultData));
                        
                        if (ret.code == "200")
                        {
                            List<SurveyUrlData> survryUrlData = JsonConvert.DeserializeObject<List<SurveyUrlData>>(JsonConvert.SerializeObject(ret.data));
                            foreach (var data in survryUrlData)
                            {
                                if (data.ProvideType == "1")
                                {
                                    //問卷網址
                                    lnkFinal.Text = data.FinalUrl;
                                    lnkTest.Text = data.TestUrl;
                                }
                                if (data.ProvideType == "2")
                                {
                                    //QR Code網址
                                    lnkFinalQRCode.Text = data.FinalUrl;
                                    lnkTestQRCode.Text = data.TestUrl;
                                }
                            }
                        }
                    }
                }
                else
                {

                    //need to do
                    // alert("無效的token,請洽管理員!!")
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showSaveMessage",
                    "<script language='javascript'>alert('無效的token,請洽管理員!!'); </script>");

                }
            }
            catch(Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSaveMessage",
                    "<script language='javascript'>alert('SetURL 異常!"+ex.Message+"'); </script>");
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
        protected void SetDownfileButton()
        {
            string DownloadKey = Encrypt(ParentPage.UserId.ToString());
            //確認button是否可用
            //檢查是否有名單申請核可資料，下載button是否可用
            //DataTable dtMSCRM = DataAccess.SurveyAccess.CheckList(ParentPage.UserId, ParentPage.ListId);
            if (DataAccess.SurveyAccess.CheckListByActivityId(ParentPage.UserId, ParentPage.SurveyId))
            { 
                Page.ClientScript.RegisterStartupScript(this.GetType(), "setButtonState", "setButtonState(true,'" + ParentPage.SurveyId.ToString() + "','" + ParentPage.UserId.ToString() + "');", true); 
            }
            else
            { 
                Page.ClientScript.RegisterStartupScript(this.GetType(), "setButtonState", "setButtonState(false,'" + ParentPage.SurveyId.ToString() + "','" + ParentPage.UserId.ToString() + "');", true); 
            }
        }

        protected void InsertSurvey()
        {
            string batPath = SurveyLib.Module.Settings.GetConfigValue("BatPath");
            string responseBody = "";
            string userId = ParentPage.UserId.ToString();
            string surveyId = ParentPage.SurveyId.ToString();
            string title = DataAccess.SurveyAccess.GetTitle(new Guid(ParentPage.SurveyId.ToString()));
            string token;
            try
            {
                //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.ServerCertificateValidationCallback = CertificateCheck;//delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.DefaultConnectionLimit = 9999;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(SurveyHost);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Utility util = new Utility();
                token = util.GetToken(ParentPage.UserId.ToString());
      
                if (token != "")
                {
                    SurveyInsData data = new SurveyInsData { SurveyId = surveyId, Uid = userId, Title = title };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", token);
                    HttpResponseMessage response = client.PostAsync("/V2/api/Survey/Info/Insert", content).Result;

                
                    

                    if (response.IsSuccessStatusCode)
                    {
                        responseBody = response.Content.ReadAsStringAsync().Result.Replace(@"\", "");
                        ResultData ret = (ResultData)JsonConvert.DeserializeObject(responseBody, typeof(ResultData));

                    
                        if (ret.code == "200")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "showSaveMessage",
                                "<script language='javascript'>alert('新增問卷成功!!');</script>");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "showSaveMessage",
                              "<script language='javascript'>alert('新增問卷失敗:" + ret.code + "');</script>");
                        }
                    }
                }
            }
            catch(Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSaveMessage",
                 "<script language='javascript'>alert('InsertSurvey 失敗'); </script>");
                throw ex;
            }
            
        }

        
        protected void btnGotoSurvey_Click(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "showSaveMessage",
            //                   "<script language='javascript'>objShell = new ActiveXObject('WScript.Shell'); objShell.Run('" + batPath + "shellQ.bat', 0, true);   </script>");
        }

        protected void btnDownLoadFile_Click(object sender, EventArgs e)
        {
            //下載前再次檢核行銷名單匯出申請表
            if (!DataAccess.SurveyAccess.CheckListByActivityId(ParentPage.UserId, ParentPage.SurveyId))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSaveMessage",
                            "<script language='javascript'>alert('目前不存在已簽核的名單匯出申請表，資料無法下載!'); </script>");
                btnDownLoadFile.Enabled = false;
                return;
            }

            string downloadKey = Encrypt(ParentPage.UserId.ToString());
            string token = "";
            string surveyId = ParentPage.SurveyId.ToString();
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = CertificateCheck;//delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.DefaultConnectionLimit = 9999;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(SurveyHost);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Utility util = new Utility();
            token = util.GetToken(ParentPage.UserId.ToString());
            if (token != "")
            {
                HttpResponseMessage response = client.GetAsync("/V2/api/Analytics/ExportSurvey?SurveyId=" + surveyId + "&token=" + downloadKey).Result;
                if (response.IsSuccessStatusCode)
                {
                    var fileBytes = response.Content.ReadAsByteArrayAsync().Result;
                    string filename = response.Content.Headers.ContentDisposition.FileName;
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment;FileName=" + Uri.EscapeDataString(filename));
                    Response.ContentType = "text/csv";
                    Response.BinaryWrite(fileBytes);
                    Response.End();
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "showSaveMessage",
                            "<script language='javascript'>alert('無效的token,請洽管理員!!'); </script>");
            }
        }

        
        protected int GetSurveyReplyCount1()
        {
            int ret = 0;
            Utility util = new Utility();
            string userId = ParentPage.UserId.ToString();
            string surveyId = ParentPage.SurveyId.ToString();
            ret =  util.GetSurveyReplyCnt(surveyId, userId);

            return ret;
        }

        
    }

    
}