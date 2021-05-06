using System;
using System.Collections.Generic;
using System.Linq;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.Cores.Globales;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.GenericRepository.Repositories;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTAServices.Services.formcontrols.helpers;
using VTAworldpass.VTAServices.Services.formcontrols.model;
using static VTAworldpass.VTACore.Collections.CollectionsUtils;
using static VTAworldpass.VTACore.Cores.Globales.Enumerables;

namespace VTAworldpass.VTAServices.Services.formcontrols
{
    public class FormControlServices : FormControlHelper
    {
        private VirtualRepository<tblusercompanies> UserCompaniesRepository;
        private UnitOfWork unit = null;
        private GeneralRepository generalRepository = null;

        public FormControlServices()
        {
            unit = new UnitOfWork();
            generalRepository = new GeneralRepository();
            this.UserCompaniesRepository = new VirtualRepository<tblusercompanies>(new vtclubdbEntities());
        }


        #region Action by User-Companies
        public List<formselectmodel> getSegmentbyCompanies(int idUser)
        {
            try
            {
                List<tblsegment> lst = new List<tblsegment>();
                var result = this.unit.UserCompaniesRepository.Get(x => x.tblusers.idUser == idUser && x.usercompanyactive == Globals.activeRecord && x.tblcompanies.tblsegment.segmentactive == Globals.activeRecord && x.tblcompanies.companyactive == Globals.activeRecord, null, "tblusers,tblcompanies.tblsegment").Select(y => y.tblcompanies.tblsegment).ToList().Distinct();
                return ConvertToSelectModel(result.ToList());
            }
            catch (Exception e)
            {
                Log.Error("No se puede consultar los Segmentos por empresas del usuario", e);
                throw new Exception("No se puede consultar los Segmentos por empresas del usuario", e);
            }
        }

        public List<formselectmodel> getCompaniesbySegment(int idSegment, int iduser)
        {
            var result = this.unit.UserCompaniesRepository.Get(x => x.usercompanyactive == Globals.activeRecord && x.tblusers.idUser == iduser && x.tblcompanies.tblsegment.segmentactive == Globals.activeRecord && x.tblcompanies.tblsegment.idsegment == idSegment && x.tblcompanies.companyactive == Globals.activeRecord, null, "tblcompanies").Select(y => y.tblcompanies).ToList().Distinct();
            return ConvertToSelectModel((List<tblcompanies>)result.ToList());
        }

        public List<formselectmodel> getCurrenciesbyCompanies(int idcompany)
        {
            var result = this.unit.CompaniesCurrenciesRepository.Get(x => x.tblcompanies.idcompany == idcompany, null, "tblcurrencies").Select(y => y.tblcurrencies).ToList().Distinct();
            return ConvertToSelectModel(result.ToList());
        }

        public List<formselectmodel> SelectServicesCompaniesbyUser(int idUSer)
        {
            try
            {
                return ConvertToSelectModel(unit.UserCompaniesRepository.Get(x => x.iduser == idUSer && x.usercompanyactive == Globals.activeRecord && x.tblcompanies.companyactive == Globals.activeRecord, t => t.OrderBy(c => c.tblcompanies.companyname), "tblcompanies").ToList().Distinct().ToList());
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                throw new Exception("No se puede procesar la creación del Select, #formControlServices-SelectServicesHotelsbyUser, causas:  ---->>>" + sqlException.Message + "[Stack-Trace] ----->>>" + sqlException.StackTrace);
            }
            catch (Exception e)
            {
                throw new Exception("No se puede procesar la creación del Select, #FormControlServices-SelectServicesHotelsbyUser, causas ---->>> " + e.Message + "[Stack - Trace]----->>> " + e.StackTrace);
            }
        }

        #endregion

        #region Action  CONTABLES ACCOUNT By Segment and Profile
        public List<formselectmodel> getAccountL1byProfileSegment(int idProfile, int idUser, int idSegment, int idAccl1)
        {
            try
            {
                List<tblaccountsl1> list = new List<tblaccountsl1>();
                var resultProfile = generalRepository.getAccountL1byProfileSegment(idProfile, idSegment, idAccl1); // Consulta por perfil 
                var resultUser = generalRepository.getAccountL1byUserAccl4(idUser, idSegment, idAccl1); // Consulta por usuarios
                list = (List<tblaccountsl1>)IEnumerableUtils.AddList(list, resultProfile);
                foreach (tblaccountsl1 model in resultUser)
                {
                    var count = list.Where(v => v.idaccountl1 == model.idaccountl1).FirstOrDefault();
                    if (count == null) { list.Add(model); }
                }
                return ConvertToSelectModel(list);
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                throw new Exception("No se puede procesar la creación del Select, #formControlServices-getAccountLayerbyProfile, causas:  ---->>>" + sqlException.Message + "[Stack-Trace] ----->>>" + sqlException.StackTrace);
            }
            catch (Exception e)
            {
                throw new Exception("No se puede procesar la creación del Select, #FormControlServices-getAccountLayerbyProfile, causas ---->>> " + e.Message + "[Stack - Trace]----->>> " + e.StackTrace);
            }
        }

        public List<formselectmodel> getAccountLayer2byIdndSegment(int id, int idProfile, int idUser, int idSegment, int accl1)
        {
            try
            {
                List<tblaccountsl2> list = new List<tblaccountsl2>();
                var resultProfile = generalRepository.getAccountL2byidndSegment(id, idProfile, idSegment, accl1); // Searching by ProfileAccount
                var resultUser = generalRepository.getAccountL2byUserAccl4(id, idSegment, idUser, accl1); // Search by UserAccl4
                list = resultProfile.ToList();
                foreach (tblaccountsl2 model in resultUser)
                {
                    var count = list.FindAll(v => v.idaccountl2 == model.idaccountl2).Count(); if (count == 0) { list.Add(model); }
                }
                return ConvertToSelectModel(list);
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                throw new Exception("No se puede procesar la creación del Select, #formControlServices-getAccountLayer3byId, causas:  ---->>>" + sqlException.Message + "[Stack-Trace] ----->>>" + sqlException.StackTrace);
            }
            catch (Exception e)
            {
                throw new Exception("No se puede procesar la creación del Select, #FormControlServices-getAccountLayer3byId, causas ---->>> " + e.Message + "[Stack - Trace]----->>> " + e.StackTrace);
            }
        }

        public List<formselectmodel> getAccountLayer3byIdndSegment(int id, int idProfile, int idUser, int idSegment, int accl1)
        {
            try
            {
                List<tblaccountsl3> list = new List<tblaccountsl3>();
                var resultProfile = generalRepository.getAccountL3byidndSegment(id, idProfile, idSegment, accl1); // Searching by ProfileAccount
                var resultUser = generalRepository.getAccountL3byUserAccl4(id, idUser, idSegment, accl1); // Search by UserAccl4
                list = resultProfile.ToList();

                foreach (tblaccountsl3 model in resultUser)
                {
                    var count = list.FindAll(v => v.idaccountl3 == model.idaccountl3).Count();
                    if (count == 0) { list.Add(model); }
                }
                return ConvertToSelectModel(list.ToList());
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                throw new Exception("No se puede procesar la creación del Select, #formControlServices-getAccountLayer3byId, causas:  ---->>>" + sqlException.Message + "[Stack-Trace] ----->>>" + sqlException.StackTrace);
            }
            catch (Exception e)
            {
                throw new Exception("No se puede procesar la creación del Select, #FormControlServices-getAccountLayer3byId, causas ---->>> " + e.Message + "[Stack - Trace]----->>> " + e.StackTrace);
            }
        }

        public List<formselectmodel> getAccountLayer4byIdndSegment(int id, int idProfile, int idUser, int idSegment, int accl1)
        {
            try
            {
                List<tblaccountsl4> list = new List<tblaccountsl4>();
                var resultProfile = generalRepository.getAccountL4byidndSegment(id, idProfile, idSegment, accl1); // Searching by ProfileAccount
                var resultUser = generalRepository.getAccountL4byUserAccl4(id, idUser, idSegment, accl1); // Search by UserAccl4
                list = resultProfile.ToList();
                foreach (tblaccountsl4 model in resultUser)
                {
                    var count = list.FindAll(v => v.idAccountl4 == model.idAccountl4).Count();
                    if (count == 0)
                    {
                        list.Add(model);
                    }
                }
                return ConvertToSelectModel(list);
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                throw new Exception("No se puede procesar la creación del Select, #formControlServices-getAccountLayer3byId, causas:  ---->>>" + sqlException.Message + "[Stack-Trace] ----->>>" + sqlException.StackTrace);
            }
            catch (Exception e)
            {
                throw new Exception("No se puede procesar la creación del Select, #FormControlServices-getAccountLayer3byId, causas ---->>> " + e.Message + "[Stack - Trace]----->>> " + e.StackTrace);
            }
        }
        #endregion

        #region Commons

        public List<formselectmodel> getAppYearsAvailables()
        {
            return ConvertToSelectModel(this.unit.IncomeRepository.Get(null, null, "").Select(y => y.incomeapplicationdate.Year).Distinct().ToList());
        }

        public List<formselectmodel> getReportFinancialType()
        {
            var accountTypes = unit.AccountTypeRepository.Get(null, null, "").ToList();
            return ConvertToSelectModel(accountTypes);
        }

        #endregion
        public List<formselectmodel> SelectAttachmentTypes()
        {
            try
            {
                return ConvertToSelectModel(unit.AttachmentsVtaRepository.Get(x => x.attachmentactive == Globals.activeRecord, null, "").ToList());
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                throw new Exception("No se puede procesar la creación del Select, #formControlServices-SelectAttachmentTypes, causas:  ---->>>" + sqlException.Message + "[Stack-Trace] ----->>>" + sqlException.StackTrace);
            }
            catch (Exception e)
            {
                throw new Exception("No se puede procesar la creación del Select, #FormControlServices-SelectAttachmentTypes, causas ---->>> " + e.Message + "[Stack - Trace]----->>> " + e.StackTrace);
            }
        }

        public List<formselectmodel> SelectBudgetTypes()
        {
            try
            {
                return ConvertToSelectModel(unit.BudgetTypeRepository.Get(null, x => x.OrderBy(y => y.budgettypeorder), "").ToList());
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                throw new Exception("No se puede procesar la creación del Select, #formControlServices-SelectBudgetTypes, causas:  ---->>>" + sqlException.Message + "[Stack-Trace] ----->>>" + sqlException.StackTrace);
            }
            catch (Exception e)
            {
                throw new Exception("No se puede procesar la creación del Select, #FormControlServices-SelectBudgetTypes, causas ---->>> " + e.Message + "[Stack - Trace]----->>> " + e.StackTrace);
            }
        }

        public List<formselectmodel> getAccountLayer2byId(int id)
        {
            try
            {
                return ConvertToSelectModel(unit.AccountL2Repository.Get(y => y.idaccountl1 == id && y.accountl2active == Globals.activeRecord && y.tblaccountsl1.accountl1active == Globals.activeRecord, null, "").ToList());
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                throw new Exception("No se puede procesar la creación del Select, #formControlServices-getAccountLayer2byId, causas:  ---->>>" + sqlException.Message + "[Stack-Trace] ----->>>" + sqlException.StackTrace);
            }
            catch (Exception e)
            {
                throw new Exception("No se puede procesar la creación del Select, #FormControlServices-getAccountLayer2byId, causas ---->>> " + e.Message + "[Stack - Trace]----->>> " + e.StackTrace);
            }
        }

        /*************************Account Levels by Profile [Accounter and Coworkers]*********************************/

        public List<formselectmodel> getAccountLayer1byProfileandUser(int idProfile)
        {
            try
            {
                return ConvertToSelectModel(unit.ProfileAccountL3Repository.Get(y => y.idprofileaccount == idProfile && y.tblaccountsl3.accountl3active == Globals.activeRecord && y.tblaccountsl3.tblaccountsl2.accountl2active == Globals.activeRecord && y.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1active == Globals.activeRecord, null, "").Select(t => t.tblaccountsl3.tblaccountsl2.tblaccountsl1).Distinct().ToList());
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                throw new Exception("No se puede procesar la creación del Select, #formControlServices-getAccountLayerbyProfile, causas:  ---->>>" + sqlException.Message + "[Stack-Trace] ----->>>" + sqlException.StackTrace);
            }
            catch (Exception e)
            {
                throw new Exception("No se puede procesar la creación del Select, #FormControlServices-getAccountLayerbyProfile, causas ---->>> " + e.Message + "[Stack - Trace]----->>> " + e.StackTrace);
            }
        }

        public List<formselectmodel> getAccountLayer3byId(int id)
        {
            try
            {
                return ConvertToSelectModel(unit.AccountL3Repository.Get(y => y.idaccountl2 == id && y.accountl3active == Globals.activeRecord && y.tblaccountsl2.accountl2active == Globals.activeRecord && y.tblaccountsl2.tblaccountsl1.accountl1active == Globals.activeRecord, null, "").ToList());
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                throw new Exception("No se puede procesar la creación del Select, #formControlServices-getAccountLayer3byId, causas:  ---->>>" + sqlException.Message + "[Stack-Trace] ----->>>" + sqlException.StackTrace);
            }
            catch (Exception e)
            {
                throw new Exception("No se puede procesar la creación del Select, #FormControlServices-getAccountLayer3byId, causas ---->>> " + e.Message + "[Stack - Trace]----->>> " + e.StackTrace);
            }
        }

        public List<formselectmodel> getAccountLayer4byId(int id, int idProfile, int idUser, int Company, int accl1)
        {
            try
            {
                List<tblaccountsl4> list = new List<tblaccountsl4>();
                int segment = (int)unit.CompaniesRepository.Get(c => c.idcompany == Company, null, "").FirstOrDefault().idsegment;
                var resultProfile = generalRepository.getAccountL4byidndSegment(id, idProfile, segment, accl1);
                var resultUser = generalRepository.getAccountL4byUserAccl4(id, idUser, accl1);
                list = resultProfile.ToList();
                foreach (tblaccountsl4 model in resultUser)
                {
                    var count = list.FindAll(v => v.idAccountl4 == model.idAccountl4).Count();
                    if (count == 0) { list.Add(model); }
                }
                return ConvertToSelectModel(list);
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                throw new Exception("No se puede procesar la creación del Select, #formControlServices-getAccountLayer4byId, causas:  ---->>>" + sqlException.Message + "[Stack-Trace] ----->>>" + sqlException.StackTrace);
            }
            catch (Exception e)
            {
                throw new Exception("No se puede procesar la creación del Select, #FormControlServices-getAccountLayer4byId, causas ---->>> " + e.Message + "[Stack - Trace]----->>> " + e.StackTrace);
            }
        }

        public List<formselectmodel> getAccountbyUserProfileAndIdCurrency(int idUser, int idBankAccntType, int idCurrency, int idcompany)
        {
            try
            {
                var result = unit.UserBAccountsRepository.Get(x => x.tblbankaccount.tblcurrencies.idCurrency == idCurrency && x.tblbankaccount.idcompany == idcompany && x.tblbankaccount.baccountactive == Globals.activeRecord && x.tblbankaccount.tblbank.bankactive == Globals.activeRecord && x.tblbankaccount.tblcompanies.companyactive == Globals.activeRecord && x.iduser == idUser, y => y.OrderBy(t => t.tblbankaccount.baccountshortname), "tblbankaccount,tblbankaccount.tblbank,tblbankaccount.tblcurrencies, tblbankaccount.tblcompanies").Select(t => t.tblbankaccount).ToList();
                List<tblbankaccount> _list = new List<tblbankaccount>();
                foreach (tblbankaccount model in result)
                {
                    List<int> types = model.tblbaccounprodttype.Select(f => f.idbankprodttype).ToList();
                    if (types.Contains(idBankAccntType)) _list.Add(model);
                }
                return ConvertToSelectModel(_list);
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                throw new Exception("No se puede procesar la creación del Select, #formControlServices-getAccountbyUserProfileAndIdCurrency, causas:  ---->>>" + sqlException.Message + "[Stack-Trace] ----->>>" + sqlException.StackTrace);
            }
            catch (Exception e)
            {
                throw new Exception("No se puede procesar la creación del Select, #FormControlServices-getAccountbyUserProfileAndIdCurrency, causas ---->>> " + e.Message + "[Stack - Trace]----->>> " + e.StackTrace);
            }
        }

        public List<formselectmodel> getCurrenciesbyHotels(List<int> userHotels)
        {
            var result = this.unit.CompaniesCurrenciesRepository.Get(x => userHotels.Contains(x.idcompany), c => c.OrderBy(u => u.tblcurrencies.currencyName), "tblcurrencies").Select(y => y.tblcurrencies).ToList();
            List<tblcurrencies> lst = new List<tblcurrencies>();
            foreach (tblcurrencies model in result)
            {
                List<int> _ids = lst.Select(x => x.idCurrency).ToList();
                if (!_ids.Contains(model.idCurrency)) lst.Add(model);
            }
            return ConvertToSelectModel(lst);
        }

        public List<formselectmodel> getSupppliersbyByUserHotels(List<int> companies)
        {                                                                                                                   //tblsupplier
            var suppliers = unit.CompaniesSuppliersRepository.Get(x => companies.Contains(x.idcompany), x => x.OrderBy(y => y.tblSuppliers.supplierName), "tblSuppliers").Select(p => p.tblSuppliers).ToList();
            return ConvertToSelectModel(suppliers.Distinct().ToList());
        }

        public List<formselectmodel> getBAccountProductsbyUserClass(int idUser, int idCurrency, int idClassification)
        {
            var accounts = unit.UserBAccountsRepository.Get(
                x => x.iduser == idUser &&
                x.tblbankaccount.tblcurrencies.idCurrency == idCurrency &&
                x.tblbankaccount.tblbankaccntclasification.idbankaccntclasification == idClassification &&
                x.userbacountactive == Globals.activeRecord &&
                x.tblbankaccount.baccountactive == Globals.activeRecord &&
                x.tblbankaccount.tblbank.bankactive == Globals.activeRecord &&
                x.tblbankaccount.tblcompanies.companyactive == Globals.activeRecord &&
                x.tblbankaccount.tblbankaccntclasification.bankaccntclasactive == Globals.activeRecord, null,
                "tblbankaccount").SelectMany(y => y.tblbankaccount.tblbaccounprodttype.Distinct()).Distinct().ToList();
            if (accounts.Count != 0)
                return ConvertToSelectModel(accounts);
            else throw new Exception("La cuenta o Cuentas no tienen productos asociados");
        }

        public List<formselectmodel> getBAccountProductsbyUser(int idUser, int idCurrency)
        {
            var accounts = unit.UserBAccountsRepository.Get(x => x.iduser == idUser && x.tblbankaccount.tblcurrencies.idCurrency == idCurrency && x.tblbankaccount.tblbank.bankactive == Globals.activeRecord && x.tblbankaccount.tblcompanies.companyactive == Globals.activeRecord, null, "tblbankaccount").SelectMany(y => y.tblbankaccount.tblbaccounprodttype.Distinct()).Distinct().ToList();
            if (accounts.Count != 0)
                return ConvertToSelectModel(accounts);
            else throw new Exception("La cuenta o Cuentas no tienen productos asociados");
        }

        public List<formselectmodel> getAccountbyUserProfileAndIdCurrencyndClasification(int idUser, int idBankAccntType, int idCurrency, int idClasification, int idcompany)
        {
            try
            {
                var result = unit.UserBAccountsRepository.Get(x => x.tblbankaccount.tblcurrencies.idCurrency == idCurrency && x.userbacountactive == Globals.activeRecord && x.tblbankaccount.idcompany == idcompany && x.tblbankaccount.baccountactive == Globals.activeRecord && x.tblbankaccount.tblbank.bankactive == Globals.activeRecord && x.tblbankaccount.tblcompanies.companyactive == Globals.activeRecord && x.iduser == idUser && x.tblbankaccount.tblbankaccntclasification.idbankaccntclasification == idClasification && x.tblbankaccount.tblbankaccntclasification.bankaccntclasactive == Globals.activeRecord, y => y.OrderBy(t => t.tblbankaccount.baccountshortname), "tblbankaccount,tblbankaccount.tblbank,tblbankaccount.tblcurrencies, tblbankaccount.tblcompanies").Select(t => t.tblbankaccount).ToList();
                List<tblbankaccount> _list = new List<tblbankaccount>();
                foreach (tblbankaccount model in result)
                {
                    List<int> types = model.tblbaccounprodttype.Select(f => f.idbankprodttype).ToList();
                    if (types.Contains(idBankAccntType)) _list.Add(model);
                }
                return ConvertToSelectModel(_list);   // new tblbaccounttype { idBankAccntType = idBankAccntType }
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                throw new Exception("No se puede procesar la creación del Select, #formControlServices-getAccountbyUserProfileAndIdCurrency, causas:  ---->>>" + sqlException.Message + "[Stack-Trace] ----->>>" + sqlException.StackTrace);
            }
            catch (Exception e)
            {
                throw new Exception("No se puede procesar la creación del Select, #FormControlServices-getAccountbyUserProfileAndIdCurrency, causas ---->>> " + e.Message + "[Stack - Trace]----->>> " + e.StackTrace);
            }
        }

        public List<formselectmodel> getBankClasificationbyCurrencyUser(int idUser, int idCurrency)
        {
            var bankclasifications = unit.UserBAccountsRepository.Get(
                x => x.iduser == idUser &&
                x.tblbankaccount.tblcurrencies.idCurrency == idCurrency &&
                x.userbacountactive == Globals.activeRecord &&
                x.tblbankaccount.baccountactive == Globals.activeRecord &&
                x.tblbankaccount.tblbank.bankactive == Globals.activeRecord &&
                x.tblbankaccount.tblcompanies.companyactive == Globals.activeRecord,
                x => x.OrderBy(y => y.tblbankaccount.tblbankaccntclasification.bankaccntclasname),
                "tblbankaccount,tblbankaccount.tblbankaccntclasification").Select(y => y.tblbankaccount.tblbankaccntclasification).Distinct().ToList();
            if (bankclasifications.Count != 0)
                return ConvertToSelectModel(bankclasifications);
            else throw new Exception("La cuenta o Cuentas no tienen productos asociados");
        }

        public List<formselectmodel> getTpvsUserUserBankAccount(int idBankAccount)
        {
            var tpv = unit.BankAccountTpvRepository.Get(x => x.tblbankaccount.idbaccount == idBankAccount).Select(y => y.tbltpv).Distinct().ToList();
            return ConvertToSelectModel(tpv);
        }

        public List<formselectmodel> getBAccountByCompanyUserBankAccount(int company, int idUser)
        {
            var banAccounts = unit.UserBAccountsRepository.Get(y => y.iduser == idUser && y.userbacountactive == Globals.activeRecord && y.tblbankaccount.baccountactive == Globals.activeRecord && y.tblbankaccount.tblcompanies.idcompany == company && y.tblbankaccount.tblcurrencies.currencyActive == Globals.activeRecord && y.tblbankaccount.tblbank.bankactive == Globals.activeRecord && y.tblbankaccount.tblcompanies.companyactive == Globals.activeRecord && y.tblbankaccount.tblcompanies.tblsegment.segmentactive).Select(y => y.tblbankaccount).Distinct().ToList();
            return ConvertToSelectModel(banAccounts);
        }

        public List<formselectmodel> getCompaniesbyUserBankAccount(int idUser, int? Type)
        {
            List<tblcompanies> lstCompanies = new List<tblcompanies>();
            if (Type != null)
            {
                lstCompanies = unit.UserBAccountsRepository.Get(y => y.iduser == idUser && y.userbacountactive == Globals.activeRecord && y.tblbankaccount.tblcurrencies.currencyActive == Globals.activeRecord && y.tblbankaccount.tblbank.bankactive == Globals.activeRecord && y.tblbankaccount.tblcompanies.companyactive == Globals.activeRecord && y.tblbankaccount.tblcompanies.fundsGive == Globals.activeRecord && y.tblbankaccount.tblcompanies.tblsegment.segmentactive).Select(y => y.tblbankaccount.tblcompanies).Distinct().ToList();
            }
            else
            {
                lstCompanies = unit.UserBAccountsRepository.Get(y => y.iduser == idUser && y.userbacountactive == Globals.activeRecord && y.tblbankaccount.tblcurrencies.currencyActive == Globals.activeRecord && y.tblbankaccount.tblbank.bankactive == Globals.activeRecord && y.tblbankaccount.tblcompanies.companyactive == Globals.activeRecord && y.tblbankaccount.tblcompanies.tblsegment.segmentactive).Select(y => y.tblbankaccount.tblcompanies).Distinct().ToList();
            }
            return ConvertToSelectModel(lstCompanies);
        }

        public List<formselectmodel> getCompaniesbyUserBankAccountDest(int idUser, int? Type)
        {
            List<tblcompanies> lstCompanies = new List<tblcompanies>();
            if (Type != null)
            {
                lstCompanies = unit.UserBAccountsRepository.Get(y => y.iduser == idUser && y.userbacountactive == Globals.activeRecord && y.tblbankaccount.tblcurrencies.currencyActive == Globals.activeRecord && y.tblbankaccount.tblbank.bankactive == Globals.activeRecord && y.tblbankaccount.tblcompanies.companyactive == Globals.activeRecord && y.tblbankaccount.tblcompanies.fundsReceive == Globals.activeRecord && y.tblbankaccount.tblcompanies.tblsegment.segmentactive).Select(y => y.tblbankaccount.tblcompanies).Distinct().ToList();
            }
            else
            {
                lstCompanies = unit.UserBAccountsRepository.Get(y => y.iduser == idUser && y.userbacountactive == Globals.activeRecord && y.tblbankaccount.tblcurrencies.currencyActive == Globals.activeRecord && y.tblbankaccount.tblbank.bankactive == Globals.activeRecord && y.tblbankaccount.tblcompanies.companyactive == Globals.activeRecord && y.tblbankaccount.tblcompanies.tblsegment.segmentactive).Select(y => y.tblbankaccount.tblcompanies).Distinct().ToList();
            }
            return ConvertToSelectModel(lstCompanies);
        }

        public List<formselectmodel> getExternalGroupsbySegmentUserBankAccount(int externalgroup, int idUser)
        {

            List<tblcompanies> companies = new List<tblcompanies>();

            using (var db = new vtclubdbEntities())
            {
                // db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var companiesQuery = (from userba in db.tbluserbacount
                                      from extgroup in db.tblextgroupcompanies
                                      where userba.iduser == idUser

                                      && userba.tblbankaccount.baccountactive == Globals.activeRecord
                                      && userba.tblbankaccount.tblcompanies.companyactive == Globals.activeRecord

                                      && (userba.tblbankaccount.tblcompanies.tblextgroupcompanies.Select(c => c.IdExternalGroup).ToList()).Contains(externalgroup)
                                      // && (userba.tblbankaccount.tblcompanies.tblextgroupcompanies.Select(c => c.IdExternalGroup).ToList()).Contains( )

                                      && extgroup.idCompany == userba.tblbankaccount.idcompany


                                      && extgroup.tblexternalgroup.externalgroupActive == Globals.activeRecord
                                      && extgroup.tblcompanies.companyactive == Globals.activeRecord

                                      && extgroup.IdExternalGroup == externalgroup



                                      select extgroup.tblcompanies
                         ).ToList().Distinct();

                companies = companiesQuery.ToList();
            }


            return ConvertToSelectModel(companies);
        }

        public List<formselectmodel> getReconciliationStatus()
        {

            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize1Min());


            int i = 0;
            foreach (string name in System.Enum.GetNames(typeof(BankAccountReconciliationStatus)))
            {
                formselectmodel select = new formselectmodel();
                select.value = i.ToString();
                select.valueint = i;
                select.text = name.ToString().Replace('_', ' ');
                select.shorttext = name.ToString().Replace('_', ' ');
                listToSend.Add(select);
                i = i += 1;
            }

            return listToSend;
        }

        public List<formselectmodel> getEXternalGroupbyUserBankAccount(int iduser)
        {
           List<tblexternalgroup> externalGroupslst = new List<tblexternalgroup>();

            using (var db = new vtclubdbEntities())
            {
                var externalGroupsResult = (from userba in db.tbluserbacount
                                            from extgroup in db.tblextgroupcompanies
                                            where userba.iduser == iduser

                                            && userba.tblbankaccount.baccountactive == Globals.activeRecord
                                            && userba.tblbankaccount.tblcompanies.companyactive == Globals.activeRecord

                                            && extgroup.idCompany == userba.tblbankaccount.idcompany

                                            && extgroup.tblexternalgroup.externalgroupActive == Globals.activeRecord
                                            && extgroup.tblcompanies.companyactive == Globals.activeRecord

                                            select extgroup.tblexternalgroup
                         ).ToList().Distinct();

                externalGroupslst = externalGroupsResult.ToList();
            }

            return convertToSelectModel(externalGroupslst);

        }

        public List<formselectmodel> SelectCurrencies()
        {
            try
            {
                return convertToSelectModel(unit.CurrencyRepository.Get(x => x.currencyActive == Globals.activeRecord, null, "").ToList());
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                throw new Exception("No se puede procesar la creación del Select, #formControlServices-SelectCurrencies, causas:  ---->>>" + sqlException.Message + "[Stack-Trace] ----->>>" + sqlException.StackTrace);
            }
            catch (Exception e)
            {
                throw new Exception("No se puede procesar la creación del Select, #FormControlServices-SelectCurrencies, causas ---->>> " + e.Message + "[Stack - Trace]----->>> " + e.StackTrace);
            }
        }

        public List<formselectmodel> getUserFill()
        {

            var user = unit.UserRepository.Get(x => x.userActive == true, null, "").ToList();

            return convertToSelectModel(user);
        }

        public List<formselectmodel> getProfileFill()
        {

            var profile = unit.ProfileAccountRepository.Get(x => x.profileaccountactive == Globals.activeRecord, null, "").ToList();

            return convertToSelectModel(profile);
        }

        public List<formselectmodel> getCompaniesFill()
        {

            var companies = unit.CompaniesRepository.Get(x => x.companyactive == true, x => x.OrderBy(o => o.companyorder), "").ToList();

            return ConvertToSelectModelFill(companies);
        }

        public List<accountmodel> getAccountFill()
        {

            var account = unit.BankAccountRepository.Get(x => x.baccountactive == true, x => x.OrderBy(o => o.idcompany), "").ToList();

            return convertToSelectModelAccount(account);
        }

        public List<permissionsmodel> getPermissionsFill()
        {

            var permissions = unit.PermissionsVTARepository.Get(x => x.permissionEstatus == true, null, "").ToList();

            return convertToSelectModelPermissions(permissions);
        }

        public List<formselectmodel> getAccountLayer3bySegment(int idProfile, int idUser, int idSegment, int accl1)
        {
            try
            {
                List<tblaccountsl3> list = new List<tblaccountsl3>();
                var resultProfile = generalRepository.getAccountL3bySegment(idProfile, idSegment, accl1); // Searching by ProfileAccount
                var resultUser = generalRepository.getAccountL3byUserAccl4(idUser, idSegment, accl1); // Search by UserAccl4
                list = resultProfile.ToList();

                foreach (tblaccountsl3 model in resultUser)
                {
                    var count = list.FindAll(v => v.idaccountl3 == model.idaccountl3).Count();
                    if (count == 0) { list.Add(model); }
                }

                return ConvertToSelectModel(list);
                // return convertToSelectModel(unity.AccountL3Repository.Get(y => y.idAccountl2 == id && y.accountl3Active == Constantes.activeRecord && y.tblaccountsl2.accountl2Active == Constantes.activeRecord && y.tblaccountsl2.tblaccountsl1.accountl1Active == Constantes.activeRecord, null, "").ToList());
            }
            catch (System.Data.SqlClient.SqlException sqlException)
            {
                throw new Exception("No se puede procesar la creación del Select, #formControlServices-getAccountLayer3byId, causas:  ---->>>" + sqlException.Message + "[Stack-Trace] ----->>>" + sqlException.StackTrace);
            }
            catch (Exception e)
            {
                throw new Exception("No se puede procesar la creación del Select, #FormControlServices-getAccountLayer3byId, causas ---->>> " + e.Message + "[Stack - Trace]----->>> " + e.StackTrace);
            }
        }

        public List<formselectmodel> getSegmentByUserBankAccount(int idUser)
        {
            var segments = unit.UserBAccountsRepository.Get(y => y.iduser == idUser && y.userbacountactive == Globals.activeRecord && y.tblbankaccount.tblcurrencies.currencyActive == Globals.activeRecord && y.tblbankaccount.tblbank.bankactive == Globals.activeRecord && y.tblbankaccount.tblcompanies.companyactive == Globals.activeRecord).Select(y => y.tblbankaccount.tblcompanies.tblsegment).Distinct().ToList();
            return ConvertToSelectModel(segments);
        }

        public List<formselectmodel> getCompnaiesbySegmentUserBankAccount(int segment, int idUser)
        {
            var companies = unit.UserBAccountsRepository.Get(y => y.iduser == idUser && y.userbacountactive == Globals.activeRecord && y.tblbankaccount.tblcompanies.tblsegment.idsegment == segment && y.tblbankaccount.tblcurrencies.currencyActive == Globals.activeRecord && y.tblbankaccount.tblbank.bankactive == Globals.activeRecord && y.tblbankaccount.tblcompanies.companyactive == Globals.activeRecord && y.tblbankaccount.tblcompanies.tblsegment.segmentactive).Select(y => y.tblbankaccount.tblcompanies).Distinct().ToList();
            return ConvertToSelectModel(companies);
        }

        public List<formselectmodel> getSourceData()
        {
            var result = unit.SourceDataRepository.Get(x => Constants.sourceData.Contains(x.idsourcedata), null, "").ToList();

            if (result != null)
            {
                return ConvertToSelectModel(result);
            }
            else
            {
                throw new Exception("No tiene compañias asociadas.");
            }
        }

        public List<formselectmodel> getOperationType()
        {
            var result = unit.OperationTypeRepository.Get().ToList();

            if (result != null)
            {
                return ConvertToSelectModel(result);
            }
            else
            {
                throw new Exception("No tiene compañias asociadas.");
            }
        }
    }
}