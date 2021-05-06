using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTAServices.Services.accounts.helpers;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTAServices.Services.accounts.model;
using VTAworldpass.VTAServices.Services.accounts.security;

namespace VTAworldpass.VTAServices.Services.accounts.implements
{
    public class AccountServices : AccountHelper, IAccountServices
    {
        private readonly IManagerPasswordHash manager;
        private readonly UnitOfWork unit;
        SessionModel userSession;

        public AccountServices()
        {
            manager = new ManagerPasswordHash();
            unit = new UnitOfWork();
            userSession = new SessionModel((SessionModel)(HttpContext.Current.Session[Globals.ApplicationSession]));
        }

        /************* servicio de accounts ********************/
        public bool AccountVerifier(LoginViewModel model)
        {
            string passwordHash = manager.HashPassword(model.Password);
            List<int?> permisisons = new List<int?>();
            //string passwordHash = this.EncodePassword(loginView.Password);
            int Permissions = Convert.ToInt32(unit.ParametersRepository.Get(x => x.parameterDescription.Contains(Globals.AccesVTCPermission), null, "").First().parameterValue);
            tblusers user = this.unit.UserRepository.Get(x => x.userLoginName == model.UserName && x.userActive == Globals.activeRecord, null, "tbluserpermissions").FirstOrDefault();
            if (user == null)
            {
                throw new Exception(SystemControl.VTAUserNOAccess);
            }
            else
            { permisisons = user.tbluserpermissions.Select(t => t.idPermission).ToList(); }
            //compara el permiso de la tabla parameters con la tabla de permisos
            if (permisisons[0] != Permissions)
            {
                throw new Exception(SystemControl.VTAModuleNOAccess);
            }
            return manager.VerifyHashedPassword(user.passwordHash, model.Password);
        }

        public SessionModel AccountData(LoginViewModel model)
        {
            tblusers user = this.unit.UserRepository.Get(x => x.userLoginName == model.UserName, null, "tbluserpermissions,tblusercompanies.tblcompanies,tblprofilesaccounts.tblprofaccclass3").FirstOrDefault();
            try
            {
                userSession.idUser             = user.idUser;
                userSession.FullName           = string.Concat(user.userPersonName, "", user.userPersonLastName);
                userSession.idProfile          = (int)user.idProfile;
                userSession.userLoginName      = user.userLoginName;
                userSession.userEmail          = user.userEmail;
                userSession.userActive         = user.userActive;
                userSession.idProfileAccount   = user.idprofileaccount;
                userSession.passwordHash       = user.passwordHash;
                userSession.userPersonName     = user.userPersonName;
                userSession.userPersonLastName = user.userPersonLastName;
                HttpContext.Current.Session.Add(Globals.ApplicationSession, userSession);
                //HttpCookie VtaCookie = new HttpCookie(Globals.ApplicationCookies, userSession.ToString());
                //System.Web.Mvc.ControllerContext context = new System.Web.Mvc.ControllerContext();
                //context.HttpContext.Response.SetCookie(VtaCookie);
                return userSession;
            }
            catch (Exception e)
            {
                Log.Error("accountHelper-addSessionData -> " + "No tiene session iniciada", e);
                throw new Exception("No tiene session iniciada.");
            }
        }

        public List<int>    AccountCompanies()
        {
            int idUSer = AccountIdentity();
            List<int> companies = new List<int>();
            companies = companies = unit.UserCompaniesRepository.Get(y => y.iduser == idUSer && y.usercompanyactive == Globals.activeRecord && y.tblcompanies.companyactive == Globals.activeRecord, null, "").Select(c => c.idcompany).Distinct().ToList();
            return companies;
        }

        public List<int>    AccountLeves()
        {
            tblusers user = new tblusers();
            userSession.profaccountsl3 = null;
            List<int> ListProfAccountl3 = new List<int>();
            foreach (tblprofaccclass3 profileaccl3 in user.tblprofilesaccounts.tblprofaccclass3.Where(
                x => x.tblaccountsl3.accountl3active == Globals.activeRecord && x.idprofileaccount == AccountProfile()
                ).ToList())
            {
                ListProfAccountl3.Add(profileaccl3.tblaccountsl3.idaccountl3);
            }
            userSession.profaccountsl3 = ListProfAccountl3;
            return ListProfAccountl3;
        }

        public List<string> AccountPermissions()
        {
            tblusers user = new tblusers();
            List<string> listPermission = new List<string>();
            int idUser = AccountIdentity();
            var permissions = unit.UserPermissionsVTARepository.Get(x => x.idUser == idUser && x.userpermissionActive == Globals.activeRecord, null, "tblpermissions").ToList();
            foreach (tbluserpermissions userpermision in permissions)
            {
                listPermission.Add(string.Concat(userpermision.tblpermissions.permissionController, "-", userpermision.tblpermissions.permissionAction));
            }
            return listPermission;   
        }

        public List<string> AccountData()
        {
            List<string> accountdata = new List<string>();
            userSession = (SessionModel)HttpContext.Current.Session[Globals.ApplicationSession];
            return accountdata;
        }

        public int AccountProfile()
        {
            userSession = (SessionModel)HttpContext.Current.Session[Globals.ApplicationSession];
            return userSession.idProfileAccount == null ? 0 : (int)userSession.idProfileAccount;
        }

        public int AccountIdentity()
        {
            userSession = (SessionModel)HttpContext.Current.Session[Globals.ApplicationSession];
            return userSession.idUser;
        }

        public string getUserName()
        {
            int usr_id = AccountIdentity();

            return unit.UserRepository.Get(u => u.idUser == usr_id).FirstOrDefault().userLoginName;
        }

        public string AccountToken()
        {
            userSession = (SessionModel)HttpContext.Current.Session[Globals.ApplicationSession];
            var token = userSession.idUser.ToString() + Convert.ToString(DateTime.Now);
            return manager.TokenEncode(token);
        }

        public bool isOpertive()
        {
            try
            {
                var idProfile = this.AccountProfile();
                var profile = unit.ProfileAccountRepository.Get(x => x.idprofileaccount == idProfile, null, "").FirstOrDefault();
                var parameter = unit.ParametersRepository.Get(x => x.parameterDescription.Contains("Worldpass_Identifier_Profile"), null, "").FirstOrDefault();
                var result = profile.profileaccountshortame.ToLower().Equals(parameter.parameterValue.ToLower());
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("No se puede determinar si es cuenta de Hotel. " + e.Message);
                return false;
            }
        }

        public bool isInPermission(string permission)
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity; ;
            IEnumerable<Claim> claims = identity.Claims;

            try
            {
                var permissionsClaims = claims.Where(t => t.Type == "userpermission").ToList();
                if (permissionsClaims.Where(t => t.Value == permission).First() == null)
                { return false; }
                else return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

