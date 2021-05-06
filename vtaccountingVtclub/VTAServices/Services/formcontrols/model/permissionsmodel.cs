using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.formcontrols.model
{
    public class permissionsmodel
    {
        public int idPermission { get; set; }
        public string permissionTitle { get; set; }
        public string permissionDescription { get; set; }
        public List<permissionsmodel> subpermisionmodel { get; set; }

        public permissionsmodel()
        {
            subpermisionmodel = new List<permissionsmodel>();
        }

        public permissionsmodel(int idPermission, string permissionTitle, string permissionDescription, List<permissionsmodel> subpermisionmodel)
        {
            this.idPermission = idPermission;
            this.permissionTitle = permissionTitle;
            this.permissionDescription = permissionDescription;
            this.subpermisionmodel = subpermisionmodel;
        }
    }
}