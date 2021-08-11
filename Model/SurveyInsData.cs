using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Model
{
    public class SurveyInsData
    {
        public String code;
        public String message;
        public SurveyIns data;
    }

    public class SurveyIns
    {
        public string surveyId;
    }
}