using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.Model
{

    public class AuthInfo
    {
        public String code { get; set; }
        public String message { get; set; }
        public AuthData data { get; set; }
    }

    public class AuthData
    {
        public String Token { get; set; }

        public String UserID { get; set; }

        public String UserName { get; set; }

        public String RoleId { get; set; }

        public String RoleName { get; set; }
    }
}