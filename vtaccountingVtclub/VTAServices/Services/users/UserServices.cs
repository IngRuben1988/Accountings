using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.Business.Services.Security.Implementation;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTAServices.Services.accounts.implements;
using VTAworldpass.VTAServices.Services.accounts.model;

namespace VTAworldpass.VTAServices.Services.users
{
    public class UserServices
    {
        UnitOfWork unit;
        AccountServices accountService;
        ManagerPasswordHash manager;

        public UserServices()
        {
            unit = new UnitOfWork();
            accountService = new AccountServices();
            manager = new ManagerPasswordHash();
        }

        public List<userModel> getAllUsers()
        {
            List<userModel> lst;
            try
            {
                lst = new List<userModel>();
                var user = unit.UserRepository.Get(x => x.userActive == true, null, "tblprofilesaccounts").ToList();
                var result = user.Select(x => new userModel
                {
                    idUser = x.idUser,
                    userName = x.userLoginName,
                    userCompledName = x.userPersonName + " " + x.userPersonLastName,
                    userEmail = x.userEmail,
                    userProfile = x.idprofileaccount != null ? x.tblprofilesaccounts.profileaccountname : "N/A",
                    idProfile = x.idprofileaccount != null ? (int)x.idprofileaccount : 0
                }).ToList();
                lst = result.ToList();
            }
            catch (Exception e)
            {
                Log.Error("Unable to get the users list, UserServices.getAllUsers", e);
                throw new Exception("Unable to get the users list, UserServices.getAllUsers: " + e.Message + "[ ---- Starck Trace] ----" + e.StackTrace);
            }
            return lst;
        }

        public List<userModel> getUserSearch(int id, string userName)
        {
            List<userModel> lst;
            List<tblusers> USERS = new List<tblusers>();
            try
            {
                lst = new List<userModel>();

                USERS = unit.UserRepository.Get(y => y.tblprofilesaccounts.idprofileaccount == y.idprofileaccount).ToList();
                if (id != 0)
                {
                    USERS = USERS
                       .Where(x => x.idUser == id).ToList();
                }

                if (userName != "")
                {
                    USERS = USERS
                       .Where(x =>
                      x.userLoginName.StartsWith(userName) || x.userLoginName.EndsWith(userName) || x.userPersonName.StartsWith(userName) ||
                      x.userPersonName.EndsWith(userName) || x.userPersonLastName.StartsWith(userName) || x.userPersonLastName.EndsWith(userName) ||
                      x.userLoginName.Contains(userName) || x.userPersonName.Contains(userName) || x.userPersonLastName.Contains(userName)).ToList();
                }

                if (id != 0 && userName != "")
                {
                    USERS = USERS
                        .Where(x => x.idUser == id).ToList()
                        .Where(x =>
                        x.userLoginName.StartsWith(userName) || x.userLoginName.EndsWith(userName) || x.userPersonName.StartsWith(userName) ||
                        x.userPersonName.EndsWith(userName) || x.userPersonLastName.StartsWith(userName) || x.userPersonLastName.EndsWith(userName) ||
                        x.userLoginName.Contains(userName) || x.userPersonName.Contains(userName) || x.userPersonLastName.Contains(userName)).ToList();
                }
                if(USERS != null)
                {
                    lst = USERS.Select(x => new userModel
                    {
                        idUser = x.idUser,
                        userName = x.userLoginName,
                        userCompledName = x.userPersonName + " " + x.userPersonLastName,
                        userEmail = x.userEmail,
                        userProfile = x.idprofileaccount != null ? x.tblprofilesaccounts.profileaccountname : "N/A",
                        idProfile = x.idprofileaccount != null ? (int)x.idprofileaccount : 0
                    }).ToList();
                }

            }
            catch (Exception e)
            {
                Log.Error("Unable to get the users list, UserServices.getAllUsers", e);
                throw new Exception("Unable to get the users list, UserServices.getAllUsers: " + e.Message + "[ ---- Starck Trace] ----" + e.StackTrace);
            }
            return lst;
        }

        public List<userCompaniesModel> getCompaniesByIdUser(int idUser)
        {
            var usercompany = unit.UserCompaniesRepository.Get(x => x.iduser == idUser, null, "").ToList();

            var result = usercompany.Select(x => new userCompaniesModel
            {
                idusercompany = x.idusercompany,
                iduser = x.iduser,
                idcompany = x.idcompany,
                usercompanyactive = (bool)x.usercompanyactive
            }).ToList();

            return result;
        }

        public List<userAccountModel> getAccountByIdUser(int idUser)
        {
            var userAccount = unit.UserBAccountsRepository.Get(x => x.iduser == idUser, null, "").ToList();

            var result = userAccount.Select(x => new userAccountModel
            {
                idUserBAccount = x.iduserbacount,
                idUser = x.iduser,
                idBAccount = x.idbaccount,
                userbaccountActive = x.userbacountactive
            }).ToList();

            return result;
        }

        public List<userPermissionsModel> getPermissionsByIdUser(int idUser)
        {
            var userAccount = unit.UserPermissionsVTARepository.Get(x => x.idUser == idUser, null, "").ToList();

            var result = userAccount.Select(x => new userPermissionsModel
            {
                iduserpermission = x.IdUserPermission,
                iduser = (int)x.idUser,
                idpermission = (int)x.idPermission,
                userpermissionactive = (bool)x.userpermissionActive
            }).ToList();

            return result;
        }

        public userModel saveInfoUserByidUser(userModel helper)
        {
            userModel lst = new userModel();
            try
            {
                int idUser = helper.idUser;
                var user = unit.UserRepository.Get(x => x.idUser == idUser).ToList();
                if (user.Count != 0)
                {
                    var uc = user.Select(x => x.idUser).ToList();
                    tblusers tbluser = new tblusers();
                    if (uc.Contains(idUser))
                    {
                        tbluser = unit.UserRepository.Get(x => x.idUser == idUser).First();
                        tbluser.idprofileaccount = helper.idProfile;

                        if (helper.password != null)
                        {
                            if (helper.password.Equals(helper.passwordconfirm))
                            {
                                tbluser.passwordHash = manager.HashPassword(helper.password);
                            }
                            else
                            {
                                helper.data = "La contraseña no es igual.";
                            }
                        }
                        
                        unit.UserRepository.Update(tbluser);
                        unit.Commit();
                    }
                    lst = helper;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Unable to save user by companies, UserServices.saveCompaniesByidUser", ex);
            }
            return lst;
        }

        public List<userCompaniesModel> saveCompaniesByidUser(List<userCompaniesModel> helper)
        {
            List<userCompaniesModel> lst = null;
            try
            {
                lst = new List<userCompaniesModel>();
                int idUser = helper.Select(x => x.iduser).FirstOrDefault();
                var user = unit.UserCompaniesRepository.Get(x => x.iduser == idUser).ToList();
                if (user.Count != 0)
                {
                    foreach (userCompaniesModel ucm in helper)
                    {
                        var uc = user.Select(x => x.idcompany).ToList();
                        tblusercompanies usercomp = new tblusercompanies();
                        if (uc.Contains(ucm.idcompany))
                        {
                            usercomp = unit.UserCompaniesRepository.Get(x => x.iduser == idUser && x.idcompany == ucm.idcompany).First();
                            usercomp.usercompanyuserlastchange = accountService.userGetId();
                            usercomp.usercompanydatelastchange = DateTime.Now;
                            usercomp.usercompanyactive = ucm.usercompanyactive;
                            unit.UserCompaniesRepository.Update(usercomp);
                            unit.Commit();
                        }
                        else
                        {
                            if (ucm.usercompanyactive == true)
                            {
                                usercomp.idcompany = ucm.idcompany;
                                usercomp.iduser = ucm.iduser;
                                usercomp.usercompanyuserlastchange = accountService.userGetId();
                                usercomp.usercompanydatelastchange = DateTime.Now;
                                usercomp.usercompanyactive = ucm.usercompanyactive;
                                unit.UserCompaniesRepository.Insert(usercomp);
                                unit.Commit();
                            }
                        }
                        lst.Add(ucm);
                    }
                }
                else
                {
                    foreach (userCompaniesModel ucm in helper)
                    {
                        tblusercompanies usercomp = new tblusercompanies();
                        if (ucm.usercompanyactive == true)
                        {
                            usercomp.idcompany = ucm.idcompany;
                            usercomp.iduser = ucm.iduser;
                            usercomp.usercompanyuserlastchange = accountService.userGetId();
                            usercomp.usercompanydatelastchange = DateTime.Now;
                            usercomp.usercompanyactive = ucm.usercompanyactive;
                            unit.UserCompaniesRepository.Insert(usercomp);
                            unit.Commit();
                        }
                        lst.Add(ucm);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Unable to save user by companies, UserServices.saveCompaniesByidUser", ex);
            }
            return lst;
        }

        public List<userAccountModel> saveAccountsByidUser(List<userAccountModel> helper)
        {
            List<userAccountModel> lst = null;
            try
            {
                lst = new List<userAccountModel>();
                int idUser = helper.Select(x => x.idUser).FirstOrDefault();
                var user = unit.UserBAccountsRepository.Get(x => x.iduser == idUser).ToList();
                if (user.Count != 0)
                {
                    foreach (userAccountModel uam in helper)
                    {
                        var uc = user.Select(x => x.idbaccount).ToList();
                        tbluserbacount useraccount = new tbluserbacount();
                        if (uc.Contains(uam.idBAccount))
                        {
                            useraccount = unit.UserBAccountsRepository.Get(x => x.iduser == idUser && x.idbaccount == uam.idBAccount).First();
                            useraccount.userbacountcreatedby = accountService.userGetId();
                            //useraccount.userbacountCreationDate = DateTime.Now;
                            useraccount.userbacountactive = uam.userbaccountActive;
                            unit.UserBAccountsRepository.Update(useraccount);
                            unit.Commit();
                        }
                        else
                        {
                            if (uam.userbaccountActive == true)
                            {
                                useraccount.idbaccount = uam.idBAccount;
                                useraccount.iduser = uam.idUser;
                                useraccount.userbacountcreatedby = accountService.userGetId();
                                useraccount.userbacountcreationdate = DateTime.Now;
                                useraccount.userbacountactive = uam.userbaccountActive;
                                unit.UserBAccountsRepository.Insert(useraccount);
                                unit.Commit();
                            }
                        }
                        lst.Add(uam);
                    }
                }
                else
                {
                    foreach (userAccountModel uam in helper)
                    {
                        tbluserbacount useraccount = new tbluserbacount();
                        if (uam.userbaccountActive == true)
                        {
                            useraccount.idbaccount = uam.idBAccount;
                            useraccount.iduser = uam.idUser;
                            useraccount.userbacountcreatedby = accountService.userGetId();
                            useraccount.userbacountcreationdate = DateTime.Now;
                            useraccount.userbacountactive = uam.userbaccountActive;
                            unit.UserBAccountsRepository.Insert(useraccount);
                            unit.Commit();
                        }
                        lst.Add(uam);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Unable to save user accounts, UserServices.saveAccountsByidUser", ex);
            }
            return lst;
        }

        public List<userPermissionsModel> savePermissionsByidUser(List<userPermissionsModel> helper)
        {
            List<userPermissionsModel> lst = null;
            try
            {
                lst = new List<userPermissionsModel>();
                int idUser = helper.Select(x => x.iduser).FirstOrDefault();
                var user = unit.UserPermissionsVTARepository.Get(x => x.idUser == idUser).ToList();
                if (user.Count != 0)
                {
                    foreach (userPermissionsModel upm in helper)
                    {
                        var uc = user.Select(x => x.idPermission).ToList();
                        tbluserpermissions userperm = new tbluserpermissions();
                        if (uc.Contains(upm.idpermission))
                        {
                            userperm = unit.UserPermissionsVTARepository.Get(x => x.idUser == idUser && x.idPermission == upm.idpermission).First();
                            userperm.userpermissionActive = upm.userpermissionactive;
                            unit.UserPermissionsVTARepository.Update(userperm);
                            unit.Commit();
                        }
                        else
                        {
                            if (upm.userpermissionactive == true)
                            {
                                userperm.idPermission = upm.idpermission;
                                userperm.idUser = upm.iduser;
                                userperm.userpermissionActive = upm.userpermissionactive;
                                unit.UserPermissionsVTARepository.Insert(userperm);
                                unit.Commit();
                            }
                        }
                        lst.Add(upm);
                    }
                }
                else
                {
                    foreach (userPermissionsModel upm in helper)
                    {
                        tbluserpermissions userperm = new tbluserpermissions();
                        if (upm.userpermissionactive == true)
                        {
                            userperm.idPermission = upm.idpermission;
                            userperm.idUser = upm.iduser;
                            userperm.userpermissionActive = upm.userpermissionactive;
                            unit.UserPermissionsVTARepository.Insert(userperm);
                            unit.Commit();
                        }
                        lst.Add(upm);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Unable to save user permissions, UserServices.savePermissionsByidUser", ex);
            }
            return lst;
        }
    }
}