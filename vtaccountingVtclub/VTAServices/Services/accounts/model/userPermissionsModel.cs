using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.accounts.model
{
    public class userPermissionsModel
    {
        public int iduserpermission { get; set; }
        public int iduser { get; set; }
        public int idpermission { get; set; }
        public bool userpermissionactive { get; set; }
    }
}