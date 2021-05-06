using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace VTAworldpass.VTAServices.Services.accounts.security
{
    [ComVisible(false)]
    public class AccountingClaimsTypes
    {
        public const string Fullname      = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/Fullname";
        public const string Profile       = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/Profile";
        public const string UserLoginName = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/UserLoginName";
        public const string UserEmail     = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/UserEmail";
        public const string permissions   = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/permissions";
        //public const string Hotels        = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/Hotels";
    }
}