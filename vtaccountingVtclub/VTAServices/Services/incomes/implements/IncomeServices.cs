using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.GenericRepository.Repositories;
using VTAworldpass.VTAServices.Services.accounts;
using VTAworldpass.VTAServices.Services.incomes.model;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTAServices.Services.Logger;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Logger;

namespace VTAworldpass.VTAServices.Services.incomes.implments
{
    public class IncomeServices : IIncomeServices
    {
        private readonly IAccountServices accountServices;
        private readonly ILogsServices logsServices;
        private readonly UnitOfWork unity;
        private readonly GeneralRepository generalRepository;

        public IncomeServices(IAccountServices _accountServices, ILogsServices _logsServices, GeneralRepository _generalRepository)
        {
            this.accountServices = _accountServices;
            this.logsServices = _logsServices;
            this.unity = new UnitOfWork();
            this.generalRepository = _generalRepository;
        }


        public async Task<IEnumerable<income>> getIncomesSearchAsync(int? number, int? company, decimal? ammountIni, decimal? ammountEnd, DateTime? applicationDateIni, DateTime? applicationDateFin, DateTime? creationDateIni, DateTime? creationDateFin)
        {
            List<income> lstDocuments = new List<income>();
            int[] lstCompanies = new int[] { };
            lstCompanies = accountServices.AccountCompanies().ToArray();
            int[] listAccl3 = new int[9]; // por implementar
            var _result = await this.generalRepository.gettblncomeSearchAsync(number, ammountIni, ammountEnd, company, applicationDateIni, applicationDateFin, creationDateIni, creationDateFin, lstCompanies, listAccl3);
            lstDocuments = GeneralModelHelper.ConvertTbltoHelper(_result.ToList());
            return lstDocuments;
        }

        public async Task<income> GetIncome(long id)
        {
            income item = new income();
            var list = await unity.IncomeRepository.GetAsync(x => x.idincome == id, null, "tblcurrencies,tblcompanies,tblusers.tblprofilesaccounts,tblusers1.tblprofilesaccounts");
            var result = list.FirstOrDefault();
            if (result != null)
            {
                try
                {
                    item = GeneralModelHelper.ConvertTbltoHelper(result);
                    return item;
                }
                catch (Exception e)
                {
                    throw new Exception("No se puede procesar el Ingreso.", new Exception(e.Message + e.StackTrace));
                }
            }
            else
            {
                throw new Exception("No se encuentra el registro solicitado.");
            }
        }

        public async Task<income> SaveIncome(income item)
        {
            try
            {  //Converting file type
                tblincome model = GeneralModelHelper.PrepareToSave(item);
                // Saving 
                model.incomecreatedby = this.accountServices.AccountIdentity();
                model.incomeupdatedby = this.accountServices.AccountIdentity();
                model.incomenumber = await LastNumberIdentifierIncomeAsync(model.idcompany);
                await unity.IncomeRepository.InsertAsync(model);
                unity.CommitAsync();
                item = await this.GetIncome(model.idincome);
                await logsServices.addTblLog(model, "ALTA");
                return item;
            }
            catch (Exception e)
            {
                Log.Error("IncomeServices-SaveIncome", e);
                throw new Exception("No se puede guardar la los datos.", new Exception(e.Message, e.InnerException));
            }
        }

        public async Task<income> UpdateIncome(income item)
        {
            var result = await unity.IncomeRepository.GetAsync(c => c.idincome == item.item);
            tblincome model = new tblincome();

            if (result.FirstOrDefault() != null)
            {
                model = result.First();
            }
            else
            {
                throw new Exception("No se encuentra el registro para se editado");
            }

            try
            {   // Preparing to Update
                GeneralModelHelper.PrepareToUpdate(item, model);
                model.incomeupdatedby = this.accountServices.AccountIdentity();
                //// Updating 
                await unity.IncomeRepository.UpdateAsync(model);
                unity.CommitAsync();
                item = await this.GetIncome(model.idincome);
                await logsServices.addTblLog(model, "EDICION");
                return item;
            }
            catch (Exception e)
            {
                Log.Error("IncomeServices-UpdateIncome", e);
                throw new Exception("No se puede editar la los datos.", new Exception(e.Message, e.InnerException));
            }
        }

        public async Task<int> DeleteIncome(long id)
        {
            var result = await unity.IncomeRepository.GetAsync(c => c.idincome == id, null, "tblincomeitem");
            tblincome model = new tblincome();
            if (result.FirstOrDefault() != null)
            {
                model = result.First();
            }
            else
            { throw new Exception("No se encuentra el registro para se eliminado"); }

            try
            {
                // Getting childs incomeitems
                List<incomeitem> listincome = new List<incomeitem>();
                listincome = await this.GetIncomeItemList(id);
                foreach (incomeitem item in listincome)
                { // Deleting income items
                    await this.DeleteIncomeItem(item.item);
                }
                //Getting childs incomemovements
                List<incomepayment> listincomemovements = new List<incomepayment>();
                listincomemovements = await this.GetIncomeMovementsList(id);
                foreach (incomepayment item in listincomemovements)
                { // Deleting income movements 
                    await this.DeleteIncomeMovement(item.item);
                }
                // Deleting parent
                await unity.IncomeRepository.DeleteAsync(model);
                await logsServices.addTblLog(model, "BORRADO");
                unity.CommitAsync();
                return 1;
            }
            catch (Exception e)
            {
                Log.Error("IncomeServices-DeleteIncome", e);
                throw new Exception("No se puede eliminar el registro .", new Exception(e.Message, e.InnerException));
            }
        }


        public async Task<incomeitem> GetIncomeItem(long id)
        {
            incomeitem item = new incomeitem();
            var list = await unity.IncomeItemRepository.GetAsync(c => c.idincomeitem == id, null, "tblincome.tblcompanies,tblincome.tblusers.tblprofilesaccounts,tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1,tblincome,tblinvoiceitemstatus");
            var result = list.FirstOrDefault();
            if (result != null)
            {
                try
                {
                    item = GeneralModelHelper.ConvertTbltoHelper(result);
                    return item;
                }
                catch (Exception e)
                {
                    throw new Exception("No se puede procesar el detalle de ingreso.", new Exception(e.Message + e.StackTrace));
                }
            }
            else
            {
                throw new Exception("No se encuentra el registro solicitado de detalle de ingreso.");
            }
        }

        public async Task<List<incomeitem>> GetIncomeItemList(long id)
        {
            List<incomeitem> itemlist = new List<incomeitem>();
            var list = await unity.IncomeItemRepository.GetAsync(c => c.idincome == id, null, "tblincome.tblcompanies,tblincome.tblcompanies.tblsegment,tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1,tblincome,tblinvoiceitemstatus");
            if (list != null)
            {
                try
                {
                    itemlist = GeneralModelHelper.ConvertTbltoHelper(list.ToList());
                }
                catch (Exception e)
                {
                    throw new Exception("No se puede procesar la lista de detalles de ingreso.", new Exception(e.Message + e.StackTrace));
                }
            }
            return itemlist;
        }

        public async Task<incomeitem> SaveIncomeItem(incomeitem item)
        {
            try
            {
                tblincomeitem model = GeneralModelHelper.PrepareToSave(item);
                model.iduser = accountServices.AccountIdentity();
                await unity.IncomeItemRepository.InsertAsync(model);
                unity.CommitAsync();
                // return object
                item = await GetIncomeItem(model.idincomeitem);
                await logsServices.addTblLog(model, "ALTA");
                return item;
            }
            catch (Exception e)
            {
                Log.Error("IncomeServices-SaveIncomeItem", e);
                throw new Exception("No se puede guardar la los datos.", new Exception(e.Message, e.InnerException));
            }
        }

        public async Task<incomeitem> UpdateIncomeItem(incomeitem item)
        {
            var result = await unity.IncomeItemRepository.GetAsync(c => c.idincomeitem == item.item);
            tblincomeitem model = new tblincomeitem();
            if (result.FirstOrDefault() != null)
            {
                model = result.First();
            }
            else
            {
                throw new Exception("No se encuentra el registro para se editado");
            }

            try
            {   //// Converting file type
                GeneralModelHelper.PrepareToUpdate(item, model);
                //// Updating 
                await unity.IncomeItemRepository.UpdateAsync(model);
                unity.CommitAsync();
                item = await this.GetIncomeItem(model.idincomeitem);
                await logsServices.addTblLog(model, "EDICION");
                return item;
            }
            catch (Exception e)
            {
                Log.Error("IncomeServices-UpdateIncomeItem", e);
                throw new Exception("No se puede editar la los datos.", new Exception(e.Message, e.InnerException));
            }
        }

        public async Task<int> DeleteIncomeItem(long id)
        {
            var result = await unity.IncomeItemRepository.GetAsync(c => c.idincomeitem == id);
            tblincomeitem model = new tblincomeitem();
            if (result.FirstOrDefault() != null)
            {
                model = result.First();
            }
            else
            {
                throw new Exception("No se encuentra el registro para se eliminado");
            }

            try
            {   // Deleting
                await unity.IncomeItemRepository.DeleteAsync(model);
                await logsServices.addTblLog(model, "BORRADO");
                //unity.CommitAsync();
                return 1;
            }
            catch (Exception e)
            {
                Log.Error("IncomeServices-DeleteIncomeItem", e);
                throw new Exception("No se puede eliminar el registro .", new Exception(e.Message, e.InnerException));
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<int> LastNumberIdentifierIncomeAsync(int idCompany)
        {
            var result = await unity.IncomeRepository.GetAsync(x => x.idcompany == idCompany, t => t.OrderByDescending(y => y.idincome), "");
            return (result != null && result.Count() == 0) ? Globals.OneInt : result.Max(y => y.incomenumber) + Globals.OneInt;
        }

        public async Task<incomepayment> GetIncomeMovement(long id)
        {
            incomepayment item = new incomepayment();
            var list = await unity.IncomeMovementsRepository.GetAsync(x => x.idincomeMovement == id, null,
                "tblincome.tblcompanies.tblsegment, tblincome.tblusers.tblprofilesaccounts, tblbankprodttype, tblbankaccount, tbltpv, tblbankaccount.tblbank, tblbankaccount.tblcurrencies, tblbankaccount.tblcompanies");
            var result = list.FirstOrDefault();

            if (result != null)
            {
                try
                {
                    item = GeneralModelHelper.ConvertTbltoHelper(result);
                    item.baclass = 1;
                    return item;
                }
                catch (Exception e)
                {
                    throw new Exception("No se puede procesar el Movimiento de Ingreso.", new Exception(e.Message + e.StackTrace));
                }
            }
            else
            {
                throw new Exception("No se encuentra el registro solicitado.");
            }
        }

        public async Task<List<incomepayment>> GetIncomeMovementsList(long id)
        {
            List<incomepayment> itemlist = new List<incomepayment>();
            var list = await unity.IncomeMovementsRepository.GetAsync(c => c.tblincome.idincome == id, null, "tblincome.tblcompanies.tblsegment,tblincome.tblusers.tblprofilesaccounts,tblbankprodttype,tblbankaccount,tbltpv,tblbankaccount.tblbank,tblbankaccount.tblcurrencies,tblbankaccount.tblcompanies");
            if (list != null)
            {
                try
                {
                    itemlist = GeneralModelHelper.ConvertTbltoHelper(list.ToList());
                }
                catch (Exception e)
                {
                    throw new Exception("No se puede procesar la lista de detalles de movimientos ingreso.", new Exception(e.Message + e.StackTrace));
                }
            }
            return itemlist;
        }

        public async Task<incomepayment> SaveIncomeMovement(incomepayment item)
        {
            try
            {
                tblincomemovement model = GeneralModelHelper.PrepareToSave(item);
                model.incomemovcreatedby = accountServices.AccountIdentity();
                model.incomemovupdatedby = accountServices.AccountIdentity();
                await unity.IncomeMovementsRepository.InsertAsync(model);
                unity.CommitAsync();
                item = await GetIncomeMovement(model.idincomeMovement);
                //await logsServices.addTblLog(model, "ALTA");
                return item;
            }
            catch (Exception e)
            {
                Log.Error("IncomeServices-SaveIncomeMovement", e);
                throw new Exception("No se puede guardar la los datos.", new Exception(e.Message, e.InnerException));
            }
        }

        public async Task<int> DeleteIncomeMovement(long id)
        {
            var result = await unity.IncomeMovementsRepository.GetAsync(c => c.idincomeMovement == id);
            tblincomemovement model = new tblincomemovement();

            if (result.FirstOrDefault() != null)
            {
                model = result.First();
            }
            else
            {
                throw new Exception("No se encuentra el registro para se eliminado");
            }

            try
            {   // Deleting
                await unity.IncomeMovementsRepository.DeleteAsync(model);
                //await logsServices.addTblLog(model, "BORRADO");
                unity.CommitAsync();
                return 1;
            }
            catch (Exception e)
            {
                Log.Error("IncomeServices-DeleteIncomeMovement", e);
                throw new Exception("No se puede eliminar el registro .", new Exception(e.Message, e.InnerException));
            }
        }
    }
}