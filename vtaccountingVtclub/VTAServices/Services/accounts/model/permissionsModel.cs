using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.accounts.model
{
    public class permissionsModel
    {
        public int idPermission { get; set; }
        public string permissionTitle { get; set; }
        public string permissionDescription { get; set; }
        public List<permissionsModel> subpermisionmodel { get; set; }

        public permissionsModel()
        {
            subpermisionmodel = new List<permissionsModel>();
        }

        public permissionsModel(int idPermission, string permissionTitle, string permissionDescription, List<permissionsModel> subpermisionmodel)
        {
            this.idPermission = idPermission;
            this.permissionTitle = permissionTitle;
            this.permissionDescription = permissionDescription;
            this.subpermisionmodel = subpermisionmodel;
        }
    }
}