using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Survey.DataAccess
{
    /// <summary>
    /// 利用log4net產生log
    /// </summary>
    public class Log
    {

        public static void LogFile(String msg, Exception ex = null)
        {
            using (var sw = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\Log_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".log"))
            {
                sw.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + msg+"\r\n");
                if (ex != null) sw.WriteLine(ex.ToString());
                sw.Flush();
            }

        }
        //private static readonly ILog log = LogManager.GetLogger("SurveyWebAPILog", typeof(Common.Log));
        ///// <summary>
        ///// Debug訊息
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <param name="obj"></param>
        //public static void Debug(string msg, object obj=null)
        //{
        //    if(log.IsDebugEnabled && !string.IsNullOrWhiteSpace(msg))
        //    {
        //        if(obj==null)
        //        {
        //            Log.LogFile(msg);
        //        }
        //        else
        //        {
        //            Log.LogFileFormat(msg, obj);
        //        }
        //    }
        //}
        ///// <summary>
        ///// 一般訊息
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <param name="obj"></param>
        //public static void Info(string msg, object obj=null)
        //{
        //    if (log.IsInfoEnabled && !string.IsNullOrEmpty(msg))
        //    {
        //        if (obj == null)
        //        {
        //            Log.LogFile(msg);
        //        }
        //        else
        //        {
        //            Log.LogFileFormat(msg, obj);
        //        }
        //    }
        //}
        ///// <summary>
        ///// 錯誤訊息
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <param name="obj"></param>
        //public static void Error(string msg, object obj = null)
        //{
        //    if (log.IsErrorEnabled && !string.IsNullOrEmpty(msg))
        //    {
        //        if (obj == null)
        //        {
        //            Log.LogFile(msg);
        //        }
        //        else
        //        {
        //            Log.LogFileFormat(msg, obj);
        //        }
        //    }
        //}
        ///// <summary>
        ///// 重要訊息
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <param name="obj"></param>
        //public static void Fatal(string msg, object obj = null)
        //{
        //    if (log.IsFatalEnabled && !string.IsNullOrEmpty(msg))
        //    {
        //        if (obj == null)
        //        {
        //            log.Fatal(msg);
        //        }
        //        else
        //        {
        //            log.FatalFormat(msg, obj);
        //        }
        //    }
        //}
    }
}
