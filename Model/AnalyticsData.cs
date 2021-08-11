using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Model
{
    public class AnalyticsData
    {
        public String code { get; set; }
        public String message { get; set; }
        public Analytics data { get; set; }
    }

    public class Analytics
    {
        public string surveyId { get; set; }

        public int TotalReplyNum { get; set; }
    }
}