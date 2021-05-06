using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTAServices.Services.accounts.model;

namespace VTAworldpass.VTAServices.Services.accounts.helpers
{
    public class AccountHelper
    {
        SessionModel model = new SessionModel();

        protected void addSessionData(tblusers user)
        {
            try
            {
                model.idUser             = user.idUser;
                model.FullName           = string.Concat(user.userPersonName, "", user.userPersonLastName);
                model.idProfile          = (int)user.idProfile;
                model.userLoginName      = user.userLoginName;
                model.userEmail          = user.userEmail;
                model.userActive         = user.userActive;
                model.idProfileAccount   = user.idprofileaccount;
                model.passwordHash       = user.passwordHash;
                model.userPersonName     = user.userPersonName;
                model.userPersonLastName = user.userPersonLastName;

                List<string> listPermission = new List<string>();
                foreach (tbluserpermissions userpermision in user.tbluserpermissions)
                {
                    listPermission.Add(string.Concat(userpermision.tblpermissions.permissionController, "-", userpermision.tblpermissions.permissionAction));
                }
                model.permissions = listPermission;
                //*** Adding companies ***/
                List<int> listcompanies = new List<int>();
                foreach (tblusercompanies usercomp in user.tblusercompanies.Where(x => x.usercompanyactive == Globals.activeRecord && x.tblcompanies.companyactive == Globals.activeRecord).ToList())
                {
                    listcompanies.Add(usercomp.tblcompanies.idcompany);
                }
                model.companies = listcompanies;

                List<int> ListProfAccountl3 = new List<int>();
                foreach (tblprofaccclass3 profileaccl3 in user.tblprofilesaccounts.tblprofaccclass3.Where(x => x.tblaccountsl3.accountl3active == Globals.activeRecord ).ToList())
                {
                    ListProfAccountl3.Add(profileaccl3.tblaccountsl3.idaccountl3);
                }
                model.profaccountsl3 = ListProfAccountl3;

                HttpContext.Current.Session.Add(Globals.ApplicationSession, model);

                HttpCookie VtaCookie = new HttpCookie(Globals.ApplicationCookies, model.ToString());
                System.Web.Mvc.ControllerContext context = new System.Web.Mvc.ControllerContext();
                context.HttpContext.Response.SetCookie(VtaCookie);

            }
            catch (Exception e)
            {
                Log.Error("accountHelper-addSessionData -> " + "No tiene session iniciada", e);
                throw new Exception("No tiene session iniciada.");
            }
        }

        public List<String> userPermissions(tblusers users)
        {
            List<string> listPermission = new List<string>();
            foreach (tbluserpermissions userpermision in users.tbluserpermissions)
            {
                listPermission.Add(string.Concat(userpermision.tblpermissions.permissionController, "-", userpermision.tblpermissions.permissionAction));
            }
            return listPermission;
        }

        public int userGetId() {
            model=(SessionModel) HttpContext.Current.Session[Globals.ApplicationSession];
            return model.idUser;
        }

        public int userprofileGetId()
        {
            model = (SessionModel)HttpContext.Current.Session[Globals.ApplicationSession];
            return model.idProfile;
        }
    }
}
