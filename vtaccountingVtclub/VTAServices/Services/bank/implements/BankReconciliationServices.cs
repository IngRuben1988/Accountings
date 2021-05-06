using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using vtaccountingVtclub.Business.Core.Helper;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.bank.interfaces;
using VTAworldpass.VTAServices.Services.bank.model;
using VTAworldpass.VTAServices.Services.drive;
using VTAworldpass.VTAServices.Services.drive.model;
using VTAworldpass.VTAServices.Services.invoices.model;

namespace VTAworldpass.VTAServices.Services.bank.implements
{
    public class BankReconciliationServices : IBankReconciliationServices
    {
        //private readonly IFileServices fileServices;

        public BankReconciliationServices()
        { }

        //public BankReconciliationServices(IFileServices _FileServices)
        //{
        //    fileServices = _FileServices;
        //}

        private readonly UnitOfWork unity = new UnitOfWork();

        #region Bank Reconciliation V2

        /************ MAIN ACTIONS ****************/

        public List<bankreconciliation> getBakReconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, BankAccountReconcilitionStatus status, bool sourcedata, bool financialstateitem)
        {
            IEnumerable<bankreconciliation> list = new List<bankreconciliation>();
            list = GeneralModelHelper.ConvertTbltoHelper(BankReconciliationRepository.getbankstatements(dateReportStart, dateReportEnd, Tpv, externalgroup, company, PaymentMethod, hotel, 0, 0), sourcedata, financialstateitem);

            if ((int)status != -1)
            { list = list.Where(b => b.statusconciliation == (int)status); }

            if (hotel != 0)
            { list = list.Where(b => b.idhotel == hotel); }

            return list.ToList();
        }

        public List<bankreconciliation> getBakReconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, int status, bool sourcedata, bool financialstateitem)
        {
            IEnumerable<bankreconciliation> list = new List<bankreconciliation>();
            list = GeneralModelHelper.ConvertTbltoHelper(BankReconciliationRepository.getbankstatements(dateReportStart, dateReportEnd, Tpv, externalgroup, PaymentMethod != 0 ? 0 : company, PaymentMethod, hotel, 0, 0), sourcedata, financialstateitem);

            if (status != -1)
            { list = list.Where(b => b.statusconciliation == status); }

            if (hotel != 0)
            { list = list.Where(b => b.idhotel == hotel); }

            return list.ToList();
        }

        public List<bankreconciliation> getBakReconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, int status, decimal ammountStart, decimal ammountEnd, bool sourcedata, bool financialstateitem)
        {
            IEnumerable<bankreconciliation> list = new List<bankreconciliation>();
            list = GeneralModelHelper.ConvertTbltoHelper(BankReconciliationRepository.getbankstatements(dateReportStart, dateReportEnd, Tpv, externalgroup, PaymentMethod != 0 ? 0 : company, PaymentMethod, hotel, ammountStart, ammountEnd), sourcedata, financialstateitem);

            if (status != -1)
            {
                list = list.Where(b => b.statusconciliation == status);
            }

            if (hotel != 0)
            {
                list = list.Where(b => b.idhotel == hotel);
            }

            return list.ToList();
        }

        public List<bankreconciliation> getBakReconcilitions(int month, int year, int Tpv, int currency, int externalgroup, int company, int BankAccount, int hotel, BankAccountReconcilitionStatus status, bool sourcedata, bool financialstateitem)
        {
            IEnumerable<bankreconciliation> list = new List<bankreconciliation>();
            list = GeneralModelHelper.ConvertTbltoHelper(BankReconciliationRepository.getbankstatements(month, year, Tpv, currency, externalgroup, BankAccount), sourcedata, financialstateitem);

            if ((int)status != -1)
            {
                list = list.Where(b => b.statusconciliation == (int)status);
            }

            if (hotel != 0)
            {
                list = list.Where(b => b.idhotel == hotel);
            }

            return list.ToList();
        }

        public bankreconciliation getBakReconciliationsDetailsbyId(long idBankStatements)
        {
            bankreconciliation helper = new bankreconciliation();
            helper.GenerateConciliation(idBankStatements, false);

            return helper;
        }

        public bankreconciliation getBakReconciliationsDetailsbyRefenceReferenceItem(int sourcedata, long reference, long referenceitem)
        {
            try
            {
                long idBankStatements = 0;
                switch (sourcedata)
                {
                    case 4:
                        { idBankStatements = unity.StatementsUPSCLRepository.Get(v => v.Payment.idPayment == referenceitem && v.Payment.Invoice1.idInvoice == reference).Select(c => c.idBankStatements).First(); }
                        break;
                    case 5:
                        { idBankStatements = unity.StatementsRESERVRepository.Get(v => v.tblreservationpayments.idReservationPayment == referenceitem && v.tblreservationpayments.tblreservations.idReservation == reference).Select(c => c.idBankStatements).First(); }
                        break;
                }

                if (idBankStatements == 0) throw new Exception("No se encuentra la conciliacion de este registro.");

                bankreconciliation helper = new bankreconciliation();
                helper.GenerateConciliation(idBankStatements, false);

                return helper;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public long saveBankStatement(bankreconciliation bankreconciliation)
        {
            try
            {
                tblbankstatements model = GeneralModelHelper.PrepareToSave(bankreconciliation);
                unity.StatementsRepository.Insert(model);
                unity.Commit();

                return model.idBankStatements;
            }

            catch (Exception e)
            {
                throw new Exception("No se puede guaradra regitro tblbankstatement", e);
            }
        }

        public long saveBankStatement(bankreconciliation bankreconciliation, out string message)
        {
            try
            {
                tblbankstatements model = GeneralModelHelper.PrepareToSave(bankreconciliation);
                unity.StatementsRepository.Insert(model);
                unity.Commit();
                message = "";
                return model.idBankStatements;
            }
            catch (Exception e)
            {
                Log.Error("No se puede guaradra regitro tblbankstatement", e);
                message = string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} | {11} ", bankreconciliation.tpvname,
                         bankreconciliation.hotelname, bankreconciliation.currencyname, bankreconciliation.baccountname, bankreconciliation.bankstatementAplicationDate, bankreconciliation.bankstatementAppliedAmmount,
                         bankreconciliation.bankstatementAdjust, bankreconciliation.bankstatementInterchangesCharges, bankreconciliation.bankstatementBankFee, bankreconciliation.bankstatementInterchangesCharges,
                         bankreconciliation.bankstatementRefund, bankreconciliation.bankstatementAppliedAmmountFinal);
                return 0;
            }

        }

        public void updateBankStatement(tblbankstatements model)
        {
            unity.StatementsRepository.Update(model);
            unity.Commit();
        }

        public int saveBankStatementItem(int idBankStatement, financialstateitem item)
        {
            switch (item.SourceData)
            {
                case 4: // UPSCALES
                    {
                        try
                        {
                            tblbankstateupscl model = new tblbankstateupscl();
                            model.idBankStatements = idBankStatement;
                            model.idPayment = item.ReferenceItem;

                            unity.StatementsUPSCLRepository.Insert(model);
                            unity.Commit();

                            var _model = unity.StatementsRepository.Get(x => x.idBankStatements == idBankStatement).FirstOrDefault();
                            _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                            this.updateBankStatement(_model);

                            return 1;
                        }

                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la inserción de dato tblbankstateupscl", e);
                        }
                    }

                case 5: // Reservations
                    {
                        try
                        {
                            tblbankstatereserv model = new tblbankstatereserv();
                            model.idBankStatements = idBankStatement;
                            model.idReservationPayment = (int)item.ReferenceItem;

                            unity.StatementsRESERVRepository.Insert(model);
                            unity.Commit();

                            var _model = unity.StatementsRepository.Get(x => x.idBankStatements == idBankStatement).FirstOrDefault();
                            _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                            this.updateBankStatement(_model);

                            return 1;
                        }

                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la inserción de dato tblbankstatereserv." + "Error --->", e);
                        }
                    }

                default:
                    throw new Exception("No se puede determinar el origen del registro");
            }

        }

        public int saveBankStatementItem(bankreconciliationdetailScotiaPos helper, out string message)
        {
            try
            {
                tblscotiabankstatements model = new tblscotiabankstatements();
                model = GeneralModelHelper.PrepareToSave(helper);
                unity.ScotiaPosDetailsRepository.Insert(model);
                unity.Commit();
                // var _model = unity.ScotiaPosDetailsRepository.Get(x => x.idscotiastatement == model.idscotiastatement).FirstOrDefault();
                message = "";
                return 1;
            }
            catch (Exception e)
            {
                Log.Error("No se puede guaradra regitro tblscotiabankstatements", e);
                message = string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} | {11} | {12} | {13} | {14} ", helper.scotiastatementtpvname,
                       helper.scotiastatementcurrencyname, helper.scotiastatementidterminal, helper.scotiastatementlot, helper.scotiastatementprocessingdate, helper.scotiastatementcardtype,
                       helper.scotiastatementcardnumber, helper.scotiastatementammount, helper.scotiastatementtranstype, helper.scotiastatementtransdate, helper.scotiastatementstatus,
                       helper.scotiastatementclasification, helper.scotiastatementauthorizationcode, helper.scotiastatementcurrencysentname, helper.scotiastatemensammountsent);
                return 0;
            }
        }

        public int saveBankStatementItemXLS(int idBankStatement, financialstateitem item)
        {
            switch (item.SourceData)
            {
                case 4: // UPSCALES
                    {
                        try
                        {
                            tblbankstateupscl model = new tblbankstateupscl();
                            model.idBankStatements = idBankStatement;
                            model.idPayment = item.ReferenceItem;

                            unity.StatementsUPSCLRepository.Insert(model);
                            unity.Commit();

                            return 1;
                        }
                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la inserción de dato tblbankstateupscl", e);
                        }
                    }
                case 5: // Reservations
                    {
                        try
                        {
                            tblbankstatereserv model = new tblbankstatereserv();
                            model.idBankStatements = idBankStatement;
                            model.idReservationPayment = (int)item.ReferenceItem;

                            unity.StatementsRESERVRepository.Insert(model);
                            unity.Commit();

                            return 1;
                        }
                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la inserción de dato tblbankstatereserv." + "Error --->", e);
                        }
                    }

                default:
                    throw new Exception("No se puede determinar el origen del registro");
            }

        }

        public int deleteBankStatementItem(int idBankStatement, financialstateitem item)
        {

            switch (item.SourceData)
            {
                case 4: // UPSCALES
                    {
                        try
                        {
                            var result = unity.StatementsUPSCLRepository.Get(x => x.idBankStatements == idBankStatement && x.idPayment == item.ReferenceItem).FirstOrDefault();
                            if (result != null)
                            {
                                unity.StatementsUPSCLRepository.Delete(result);
                                unity.Commit();

                                var _model = unity.StatementsRepository.Get(x => x.idBankStatements == idBankStatement).FirstOrDefault();
                                _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                this.updateBankStatement(_model);

                                return 1;
                            }
                            else { throw new Exception("No se encontro el registro"); }
                        }
                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la eliminación de dato tblbankstateupscl", e);
                        }
                    }
                case 5: // Reservations
                    {
                        try
                        {
                            var result = unity.StatementsRESERVRepository.Get(x => x.idBankStatements == idBankStatement && x.idReservationPayment == item.ReferenceItem).FirstOrDefault();
                            if (result != null)
                            {
                                unity.StatementsRESERVRepository.Delete(result);
                                unity.Commit();

                                var _model = unity.StatementsRepository.Get(x => x.idBankStatements == idBankStatement).FirstOrDefault();
                                _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                this.updateBankStatement(_model);

                                return 1;
                            }
                            else { throw new Exception("No se encontro el registro"); }
                        }
                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la eliminación de dato tblbankstatereserv." + "Error --->", e);
                        }
                    }

                default:
                    throw new Exception("No se puede realziar la acción");
            }
        }

        public int deleteBankStatementItem(int SourceData, long Reference, long ReferenceItem)
        {

            switch (SourceData)
            {
                case 4: // UPSCALES
                    {
                        try
                        {
                            var result = unity.PaymentsUPSCLRepository.Get(c => c.idPayment == ReferenceItem && c.Invoice1.idInvoice == Reference).Select(f => f.tblbankstateupscl.FirstOrDefault()).FirstOrDefault();
                            if (result != null)
                            {
                                unity.StatementsUPSCLRepository.Delete(result);
                                unity.Commit();

                                var _model = unity.StatementsRepository.Get(x => x.idBankStatements == result.idBankStatements).FirstOrDefault();
                                _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                this.updateBankStatement(_model);

                                return 1;
                            }
                            else { throw new Exception("No se encontro el registro"); }
                        }
                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la eliminación de dato tblbankstateupscl por Refence y ReferenceItem", e);
                        }
                    }
                case 5: // Reservations
                    {
                        try
                        {
                            var result = unity.PaymentsRESERVRepository.Get(x => x.idReservationPayment == ReferenceItem && x.idReservation == Reference).Select(v => v.tblbankstatereserv.FirstOrDefault()).FirstOrDefault();
                            if (result != null)
                            {
                                unity.StatementsRESERVRepository.Delete(result);
                                unity.Commit();

                                var _model = unity.StatementsRepository.Get(x => x.idBankStatements == result.idBankStatements).FirstOrDefault();
                                _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                this.updateBankStatement(_model);

                                return 1;
                            }
                            else { throw new Exception("No se encontro el registro"); }
                        }
                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la eliminación de dato tblbankstatereserv por Refence y ReferenceItem" + "Error --->", e);
                        }
                    }

                default:
                    throw new Exception("No se puede realziar la acción");
            }
        }

        public List<financialstateitem> getSearchFinancialStateItemList(DateTime start, DateTime end, int idBankAccount, int tpv, decimal ammountStart, decimal ammountEnd, int idHotel)
        {
            financialstate financialState = new financialstate(start, end, idBankAccount, tpv, ammountStart, ammountEnd, idHotel, FinancialStateReport.AccountHistory, true);
            financialState.deleteFinancialStateItemLinked();
            return financialState.financialstateitemlist.ToList();
        }

        public void deleteBankStatement(long id)
        {
            var _result = unity.StatementsRepository.Get(c => c.idBankStatements == id, null, "tblbankstateupscl,tblbankstatereserv").FirstOrDefault();
            if (_result != null)
            {
                bool deleted = false;
                try
                {
                    List<tblbankstateupscl> listUpsc = new List<tblbankstateupscl>();
                    listUpsc = _result.tblbankstateupscl.ToList();

                    // Deleting Upscales-Statements
                    foreach (tblbankstateupscl model in listUpsc)
                    {
                        this.deleteBankStatementItem(model);
                        Log.Info(string.Format("Borrando registro de tblStatementItem UPSCL {0}, tblStatement {1}", model.idBankStatements, model.idBankstatUpscl));
                    }


                    List<tblbankstatereserv> listReserv = new List<tblbankstatereserv>();
                    listReserv = _result.tblbankstatereserv.ToList();

                    // Deleting Reservations-Statements
                    foreach (tblbankstatereserv model in listReserv)
                    {
                        this.deleteBankStatementItem(model);
                        Log.Info(string.Format("Borrando registro de tblStatementItem RESERV {0}, tblStatement {1}", model.idBankStatements, model.idBankstatReserv));
                    }
                    deleted = true;
                }
                catch (Exception e)
                {
                    Log.Error("No se puede borrar el registro de tblStatementItem", e);
                    deleted = false;
                    throw new Exception("No se puede borrar registros de Upscl. o Reserv.");
                }

                if (deleted)
                {
                    try
                    {
                        this.unity.StatementsRepository.Delete(_result);
                        this.unity.Commit();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("No se puede borrar el registro de conciliación. Aún existen registros asociados de Pagos o Reservaciones");
                    }
                }
            }

        }

        public void deleteBankStatement(long[] id)
        {
            foreach (long eval in id)
            {
                this.deleteBankStatement(eval);
            }

        }

        public void deleteBankStatementItem(tblbankstateupscl model)
        {
            unity.StatementsUPSCLRepository.Delete(model);
            unity.Commit();
        }

        public void deleteBankStatementItem(tblbankstatereserv model)
        {
            unity.StatementsRESERVRepository.Delete(model);
            unity.Commit();
        }

        public Dictionary<string, string> generateReportReconciliationsExcel(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, int status, decimal ammountStart, decimal ammountEnd)
        {
            try
            {
                List<bankreconciliation> list = new List<bankreconciliation>();
                list = this.getBakReconcilitions(dateReportStart, dateReportEnd, Tpv, externalgroup, company, PaymentMethod, hotel, status, ammountStart, ammountEnd, false, false);

                GeneradorXLS xls = new GeneradorXLS("downloads", "bankReconciliation_" + DatetimeUtils.ParseDatetoStringFull(DateTime.Now) + ".xlsx");
                xls.GeneradorXLSbankReconciliation(list);

                Dictionary<string, string> fileProperties = new Dictionary<string, string>();
                fileProperties.Add("nameFile", string.Format("Consulta_Conciliaciones-{0}", DatetimeUtils.ParseDatetoStringFull(DateTime.Now).Replace("/", "-")));
                fileProperties.Add("url", xls.GetFileDir());
                fileProperties.Add("urlrelative", xls.GetFileDirRelative());

                return fileProperties;
                //return xls.GetFileDir();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }


        /*************** Utilities *************************/

        public List<filemodel> getFiles(HttpFileCollectionBase files)
        {
            try
            {
                List<filemodel> list = (List<filemodel>)Fileservices.parseFilesBasetoFile(files);
                return list;
            }
            catch (Exception e)
            {
                Log.Error("No se pueden procesar la petición para obtener los archivos", e);
                throw new Exception("No se pueden procesar la petición para obtener los archivos", e);
            }
        }

        public List<bankreconciliation> addXLScotiaPos(List<filemodel> files)
        {
            List<bankreconciliation> allRocords = new List<bankreconciliation>();
            List<bankreconciliation> rejected = new List<bankreconciliation>();

            try
            {
                // Getting Records
                foreach (filemodel helper in files)
                {
                    var listProcess = this.parseXMLStreamtoBankStatementsScotiaBank(helper);

                    foreach (bankreconciliation model in listProcess)
                    {
                        model.GenerateConciliationByExcelUpload();
                        allRocords.Add(model);
                    }
                }

                Log.Info("Se procesaron todos los archivos XLSX.");
            }

            catch (Exception e)
            {
                throw new Exception("Error al procesar el archivo excel", e);
            }

            // Saving Records
            foreach (bankreconciliation helper in allRocords)
            {
                try
                {
                    var _result = this.saveBankStatement(helper);

                    if (_result != 0)
                    {
                        foreach (financialstateitem helperitem in helper.financialstateitemlist)
                        {
                            this.saveBankStatementItemXLS((int)_result, helperitem);
                        }
                    }
                }

                catch (Exception e)
                {
                    Log.Error("No se guardo el siguiente registro bankreconciliation: ------->" + helper.ToString() + " <--------------", e);
                    rejected.Add(helper);
                }
            }

            return rejected;
        }

        private List<bankreconciliation> parseXMLStreamtoBankStatementsScotiaBank(filemodel file)
        {
            Dictionary<long, int> dtpvs = new Dictionary<long, int>();
            Dictionary<string, int> dcompanies = new Dictionary<string, int>();
            Dictionary<string, int> dcurrency = new Dictionary<string, int>();
            Dictionary<string, int> dbankaccount = new Dictionary<string, int>();
            Dictionary<int, tblhotels> dcompanyhotel = new Dictionary<int, tblhotels>();

            try
            {
                // Selecting TPVS
                dtpvs = (unity.TpvRepository.Get(p => p.tpvActive == Constantes.activeRecord, null, "").Select(y => new { y.tpvIdLocation, y.idTPV })).ToDictionary(c => Convert.ToInt64(c.tpvIdLocation), t => t.idTPV);
                // Selecting Company
                dcompanies = (unity.CompaniesRepository.Get(p => p.companyActive == Constantes.activeRecord, null, "").Select(y => new { y.companyShortName, y.idCompany })).ToDictionary(c => c.companyShortName, t => t.idCompany);
                // Selecting VTH Hotels by Company
                dcompanyhotel = (unity.CompaniesHotelsRepository.Get(p => p.tblcompanies.companyActive == Constantes.activeRecord, null, "").Select(y => new { y.idCompany, y.tblhotels })).ToDictionary(c => c.idCompany, t => t.tblhotels);
                // Currency
                dcurrency = (unity.CurrencyRepository.Get(p => p.active == Constantes.activeRecord, null, "").Select(y => new { y.currencyAlphabeticCodeScotia, y.idCurrency })).ToDictionary(c => c.currencyAlphabeticCodeScotia, t => t.idCurrency);
                // Bank account
                dbankaccount = (unity.BankAccountRepository.Get(p => p.baccountActive == Constantes.activeRecord && p.idBankAccntClasification == 4, null, "").Select(y => new { y.baccountShortName, y.idBAccount })).ToDictionary(c => c.baccountShortName, t => t.idBAccount);
            }

            catch (Exception e)
            {
                Log.Error("No se pueden obtener los datos para transformar las conciliaciones", e);
                throw new Exception("No se pueden obtener los datos para transformar las conciliaciones");
            }

            List<bankreconciliation> list = new List<bankreconciliation>();

            try
            {
                //open the excel using openxml sdk  
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(file.stream, false))
                {
                    //create the object for workbook part  
                    WorkbookPart wbPart = doc.WorkbookPart;

                    //statement to get the count of the worksheet  
                    int worksheetcount = doc.WorkbookPart.Workbook.Sheets.Count();

                    //statement to get the sheet object  
                    Sheet mysheet = (Sheet)doc.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(0);

                    //statement to get the worksheet object by using the sheet id  
                    Worksheet Worksheet = ((WorksheetPart)wbPart.GetPartById(mysheet.Id)).Worksheet;

                    //Note: worksheet has 8 children and the first child[1] = sheetviewdimension,....child[4]=sheetdata  
                    int wkschildno = 4;

                    //statement to get the sheetdata which contains the rows and cell in table  
                    SheetData Rows = (SheetData)Worksheet.ChildElements.GetItem(wkschildno);

                    for (int rowCount = 1; rowCount <= Rows.Count() - 1; rowCount++)
                    {
                        Row currentrowFor = (Row)Rows.ChildElements.GetItem(rowCount);
                        bankreconciliation _dinamicHelper = new bankreconciliation();
                        _dinamicHelper.xlscorrectlyformed = true;
                        _dinamicHelper.statusconciliation = (int)BankAccountReconcilitionStatus.Completo;
                        _dinamicHelper.methodconciliation = (int)BankAccountReconciliationMethod.Sistema;

                        Cell _tempCellToValidate = (Cell)currentrowFor.ChildElements.GetItem(0);
                        var _recordAdded = Convert.ToInt64(_tempCellToValidate.InnerText);

                        if (_recordAdded == 3) // (First Column in the XLS) status ready to add
                        {
                            for (int cellCount = 0; cellCount <= currentrowFor.ChildElements.Count - 1; cellCount++)
                            {
                                Cell currentcellFor = (Cell)currentrowFor.ChildElements.GetItem(cellCount);
                                try
                                {
                                    var cdt = currentcellFor.DataType;
                                    switch (cellCount)
                                    {
                                        case 1:
                                            {
                                                var _terminal = Convert.ToInt64(currentcellFor.InnerText);
                                                _dinamicHelper.tpvname = currentcellFor.InnerText.ToString();
                                                _dinamicHelper.idTPV = dtpvs.Where(z => z.Key.Equals(_terminal)).Select(c => c.Value).First();
                                            }
                                            break;
                                        case 2:
                                            {
                                                if (cdt != null)
                                                {
                                                    if (cdt == CellValues.SharedString)
                                                    {
                                                        var _val = getValueString(currentcellFor, wbPart);
                                                        // Debug.WriteLine("Hotel : " + _val);
                                                        _dinamicHelper.companyname = _val;

                                                        _dinamicHelper.idcompany = dcompanies.Where(z => z.Key.Equals(_val)).Select(c => c.Value).FirstOrDefault();

                                                        _dinamicHelper.idhotel = dcompanyhotel.Where(z => z.Key.Equals(_dinamicHelper.idcompany)).Select(v => v.value.idhotel).FirstOrDefault();
                                                        _dinamicHelper.hotelname = dcompanyhotel.Where(z => z.Key.Equals(_dinamicHelper.idcompany)).Select(v => v.value.hotelname).FirstOrDefault();
                                                    }
                                                }
                                            }
                                            break;
                                        case 3:
                                            {
                                                if (cdt != null)
                                                {
                                                    if (cdt == CellValues.SharedString)
                                                    {
                                                        var _val = getValueString(currentcellFor, wbPart);
                                                        _dinamicHelper.currencyname = _val;
                                                        _dinamicHelper.idcurrency = dcurrency.Where(z => z.Key.Equals(_val)).Select(c => c.Value).FirstOrDefault();
                                                    }
                                                }
                                            }
                                            break;
                                        case 4:
                                            {
                                                // Debug.WriteLine("Cuenta : " + currentcellFor.InnerText);
                                                if (cdt != null)
                                                {
                                                    if (cdt == CellValues.SharedString)
                                                    {
                                                        var _val = getValueString(currentcellFor, wbPart);
                                                        // Debug.WriteLine("Cuenta : " + _val);
                                                        _dinamicHelper.baccountname = _val;
                                                        _dinamicHelper.idbaccount = dbankaccount.Where(z => z.Key.Equals(_val)).Select(c => c.Value).FirstOrDefault();
                                                        _dinamicHelper.baccount = _dinamicHelper.idbaccount;
                                                    }
                                                }
                                            }
                                            break;
                                        case 5:
                                            {
                                                _dinamicHelper.bankstatementAplicationDate = DateTimeUtils.ParseAOTNumberToDate(Convert.ToInt32(currentcellFor.InnerText));
                                            }
                                            break;
                                        case 6:
                                            {
                                                decimal number;
                                                Decimal.TryParse(currentcellFor.InnerText, out number);
                                                _dinamicHelper.bankstatementAppliedAmmount = Decimal.Round(number, 2);
                                            }
                                            break;
                                        case 7:
                                            {
                                                decimal number;
                                                Decimal.TryParse(currentcellFor.InnerText, out number);
                                                _dinamicHelper.bankstatementAdjust = Decimal.Round(number, 2);
                                            }
                                            break;
                                        case 8:
                                            {
                                                decimal number;
                                                Decimal.TryParse(currentcellFor.InnerText, out number);
                                                _dinamicHelper.bankstatementInterchangesCharges = Decimal.Round(number, 2);
                                            }
                                            break;
                                        case 9:
                                            {
                                                decimal number;
                                                Decimal.TryParse(currentcellFor.InnerText, out number);
                                                _dinamicHelper.bankstatementBankFee = Decimal.Round(number, 2);
                                            }
                                            break;
                                        case 10:
                                            {
                                                decimal number;
                                                Decimal.TryParse(currentcellFor.InnerText, out number);
                                                _dinamicHelper.bankstatementChargesPayments = Decimal.Round(number, 2);
                                            }
                                            break;
                                        case 11:
                                            {
                                                decimal number;
                                                Decimal.TryParse(currentcellFor.InnerText, out number);
                                                _dinamicHelper.bankstatementRefund = Decimal.Round(number, 2);
                                            }
                                            break;
                                        case 12:
                                            {
                                                decimal number;
                                                Decimal.TryParse(currentcellFor.InnerText, out number);
                                                _dinamicHelper.bankstatementAppliedAmmountFinal = Decimal.Round(number, 2);
                                            }
                                            break;
                                    }
                                }

                                catch (Exception e)
                                {
                                    Log.Info(string.Format("No se puede procesar la linea {0}  del archivo {1}. --- >> ", cellCount, file.fileName), e);
                                    _dinamicHelper.xlscorrectlyformed = false;
                                    _dinamicHelper.statusconciliation = (int)BankAccountReconcilitionStatus.Error;
                                    _dinamicHelper.methodconciliation = (int)BankAccountReconciliationMethod.SinConciliar;
                                }
                            }
                            list.Add(_dinamicHelper);
                        }
                    }
                }
                return list.OrderBy(v => v.bankstatementAplicationDate).ToList();
            }
            catch (Exception ex)
            {
                Log.Error("No se puede procesar el archivo " + file.fileName, ex);
                throw new Exception("No se puede procesar el archivo " + file.fileName);
            }
        }

        private static SharedStringItem GetSharedStringItemById(WorkbookPart workbookPart, int id)
        {
            return workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
        }

        private static string getValueString(Cell cell, WorkbookPart wbPart)
        {
            int id = -1;

            string currentcellvalue = string.Empty;

            if (Int32.TryParse(cell.InnerText, out id))
            {
                SharedStringItem item = GetSharedStringItemById(wbPart, id);

                if (item.Text != null)
                {
                    //code to take the string value  
                    currentcellvalue = item.Text.Text;
                }
                else if (item.InnerText != null)
                {
                    currentcellvalue = item.InnerText;
                }
                else if (item.InnerXml != null)
                {
                    currentcellvalue = item.InnerXml;
                }
            }

            return currentcellvalue;
        }

        #endregion

        #region Bank Reconciliation V2

        public List<string> AddFiletoDatabase(List<filemodel> files)
        {
            List<string> rejected = new List<string>();
            List<bankreconciliation> bankreconciliations = new List<bankreconciliation>();
            List<bankreconciliationdetailScotiaPos> bankreconciliationdetailScotiaPos = new List<bankreconciliationdetailScotiaPos>();
            List<financialstateitemstatements> fin_items = new List<financialstateitemstatements>();

            /************ Obteniendo los datos del Excel ******************************************************/
            /**************************************************************************************************/

            try
            {
                // Getting Records
                foreach (filemodel helper in files)
                {
                    bankreconciliations = this.parseXMLStreamtoBankStatementsScotiaBank(helper);
                }
            }

            catch (Exception e) { throw new Exception("Error al procesar el archivo excel en hoja 1 - ScotiaPos", e); }

            try
            {
                // Getting Records
                foreach (file helper in files)
                {
                    bankreconciliationdetailScotiaPos = this.parseXMLStreamtoBankStatementDetailScotiaBank(helper);
                }
            }

            catch (Exception e) { throw new Exception("Error al procesar el archivo excel en hoja 2 - Detalles ScotiaPos", e); }

            // Saving both resources

            rejected.Add("Movimientos por lotes.");

            for (int i = 0; i <= bankreconciliations.Count - 1; i++)
            {
                string _message = "";
                long bnkstat_id = saveBankStatement(bankreconciliations.ElementAt(i));

                if (bnkstat_id > 0) fin_items.Add(new financialstateitemstatements
                {
                    idbankstatement = bnkstat_id,
                    fin_states = ((getBakReconciliationsDetailsbyId(bnkstat_id)).financialstateitemlist).ToList()
                });

                if (_message.Length != 0) rejected.Add(_message);
            }

            rejected.Add("Movimientos detalles de lotes.");

            for (int i = 0; i <= bankreconciliationdetailScotiaPos.Count - 1; i++)
            {
                string _message = "";
                saveBankStatementItem(bankreconciliationdetailScotiaPos.ElementAt(i), out _message);
                if (_message.Length != 0) rejected.Add(_message);
            }

            for (int i = 0; i <= bankreconciliationdetailScotiaPos.Count - 1; i++)
            {
                try
                {
                    savePaymentbyAuthCode(bankreconciliationdetailScotiaPos.ElementAt(i), fin_items);
                }

                catch (Exception e)
                {
                    Log.Error("Error al buscar la conciliacion");
                    throw new Exception("No se puede procesar la búsqueda de código de autorización. Detalles Scotia.");
                }
            }
            return rejected;
        }


        public List<bankreconciliationdetailScotiaPos> getBankReconciliationsDetailsScotiaPos()
        {
            List<bankreconciliationdetailScotiaPos> lst = new List<bankreconciliationdetailScotiaPos>();
            return lst;
        }

        private List<bankreconciliationdetailScotiaPos> parseXMLStreamtoBankStatementDetailScotiaBank(filemodel file)
        {
            Dictionary<long, int> dtpvs = new Dictionary<long, int>();
            Dictionary<string, int> dcurrency = new Dictionary<string, int>();
            List<bankreconciliationdetailScotiaPos> list = new List<bankreconciliationdetailScotiaPos>();

            try
            {
                // Selecting TPVS
                dtpvs = (unity.TpvRepository.Get(p => p.tpvActive == Constantes.activeRecord, null, "").Select(y => new { y.tpvIdLocation, y.idTPV })).ToDictionary(c => Convert.ToInt64(c.tpvIdLocation), t => t.idTPV);
                dcurrency = (unity.CurrencyRepository.Get(p => p.active == Globals.activeRecord, null, "").Select(y => new { y.currencyAlphabeticCodeScotia, y.idCurrency })).ToDictionary(c => c.currencyAlphabeticCodeScotia, t => t.idCurrency);
            }
            catch (Exception e)
            {
                Log.Error("No se pueden obtener los datos para transformar las conciliaciones - Conciliiaciones de Detalles ", e);
                throw new Exception("No se pueden obtener los datos para transformar las conciliaciones detalladas");
            }

            try
            {
                //open the excel using openxml sdk  
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(file.stream, false))
                {
                    //create the object for workbook part  
                    WorkbookPart wbPart = doc.WorkbookPart;

                    //statement to get the count of the worksheet  
                    int worksheetcount = doc.WorkbookPart.Workbook.Sheets.Count();

                    //statement to get the sheet object  
                    Sheet mysheet = (Sheet)doc.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(1); // Sheet 1 - ScotiaPos Details

                    //statement to get the worksheet object by using the sheet id  
                    Worksheet Worksheet = ((WorksheetPart)wbPart.GetPartById(mysheet.Id)).Worksheet;

                    //Note: worksheet has 8 children and the first child[1] = sheetviewdimension,....child[4]=sheetdata  
                    int wkschildno = 4;

                    //statement to get the sheetdata which contains the rows and cell in table  
                    SheetData Rows = (SheetData)Worksheet.ChildElements.GetItem(wkschildno);

                    for (int rowCount = 1; rowCount <= Rows.Count() - 1; rowCount++)
                    {
                        Row currentrowFor = (Row)Rows.ChildElements.GetItem(rowCount);
                        bankreconciliationdetailScotiaPos _dinamicHelper = new bankreconciliationdetailScotiaPos();
                        _dinamicHelper.xlscorrectlyformed = true;

                        for (int cellCount = 0; cellCount <= currentrowFor.ChildElements.Count - 1; cellCount++)
                        {
                            Cell currentcellFor = (Cell)currentrowFor.ChildElements.GetItem(cellCount);
                            try
                            {
                                var cdt = currentcellFor.DataType;
                                switch (cellCount)
                                {
                                    case 0:
                                        { // Terminal
                                            if (cdt != null)
                                            {
                                                if (cdt == CellValues.SharedString)
                                                {
                                                    var _terminal = Convert.ToInt64(currentcellFor.InnerText);
                                                    _dinamicHelper.scotiastatementtpv = dtpvs.Where(z => z.Key.Equals(_terminal)).Select(c => c.Value).First();
                                                    _dinamicHelper.scotiastatementtpvname = _terminal.ToString();
                                                }
                                            }
                                            else
                                            {
                                                var _terminal = Convert.ToInt64(currentcellFor.InnerText);
                                                _dinamicHelper.scotiastatementtpv = dtpvs.Where(z => z.Key.Equals(_terminal)).Select(c => c.Value).First();
                                                _dinamicHelper.scotiastatementtpvname = _terminal.ToString();
                                            }
                                        }
                                        break;
                                    case 1:
                                        { // Moneda
                                            if (cdt != null)
                                            {
                                                if (cdt == CellValues.SharedString)
                                                {
                                                    var _val = getValueString(currentcellFor, wbPart);
                                                    _dinamicHelper.scotiastatementcurrencyname = _val;
                                                    _dinamicHelper.scotiastatementcurrency = dcurrency.Where(z => z.Key.Equals(_val)).Select(c => c.Value).FirstOrDefault();
                                                }
                                            }
                                            else
                                            {
                                                Log.Error("No se encuentra valor para la columna Moneda.");
                                                throw new Exception("No se encuentra valor para la columna Moneda.");
                                            }
                                        }
                                        break;
                                    case 2: // Identificación de terminal
                                        {
                                            if (cdt != null)
                                            {
                                                if (cdt == CellValues.SharedString)
                                                {
                                                    var _val = getValueString(currentcellFor, wbPart);
                                                    _dinamicHelper.scotiastatementidterminal = _val;
                                                }
                                            }
                                            else
                                            {

                                            }
                                        }
                                        break;
                                    case 3: // Número de lote
                                        {
                                            if (cdt != null)
                                            {
                                                if (cdt == CellValues.SharedString)
                                                {
                                                    var _val = getValueString(currentcellFor, wbPart);
                                                    _dinamicHelper.scotiastatementlot = _val;
                                                }
                                            }
                                        }
                                        break;
                                    case 4: // Fecha de procesamiento
                                        {
                                            if (cdt != null)
                                            {
                                                if (cdt == CellValues.SharedString)
                                                {
                                                    var _val = getValueString(currentcellFor, wbPart);
                                                    _dinamicHelper.scotiastatementprocessingdate = (DateTime)DateTimeUtils.ParseStringToDate(_val);
                                                }
                                            }
                                        }
                                        break;
                                    case 5: // Tipo de tarjeta
                                        {
                                            if (cdt != null)
                                            {
                                                if (cdt == CellValues.SharedString)
                                                {
                                                    var _val = getValueString(currentcellFor, wbPart);
                                                    _dinamicHelper.scotiastatementcardtype = _val;
                                                }
                                            }
                                        }
                                        break;
                                    case 6: // numero de tarjeta
                                        {
                                            if (cdt != null)
                                            {
                                                if (cdt == CellValues.SharedString)
                                                {
                                                    var _val = getValueString(currentcellFor, wbPart);
                                                    _dinamicHelper.scotiastatementcardnumber = _val;
                                                }
                                            }
                                        }
                                        break;
                                    case 7: // Monto de transacción
                                        {
                                            if (cdt != null)
                                            {
                                                if (cdt == CellValues.SharedString)
                                                {
                                                    var _val = getValueString(currentcellFor, wbPart);
                                                    decimal number;
                                                    Decimal.TryParse(_val, out number);
                                                    _dinamicHelper.scotiastatementammount = Decimal.Round(number, 2);
                                                }
                                            }
                                        }
                                        break;
                                    case 8: // Tipo de transacción
                                        {
                                            if (cdt != null)
                                            {
                                                if (cdt == CellValues.SharedString)
                                                {
                                                    var _val = getValueString(currentcellFor, wbPart);
                                                    _dinamicHelper.scotiastatementtranstype = _val;
                                                }
                                            }
                                        }
                                        break;
                                    case 9: // Fecha de transacción
                                        {
                                            if (cdt != null)
                                            {
                                                if (cdt == CellValues.SharedString)
                                                {
                                                    var _val = getValueString(currentcellFor, wbPart);
                                                    _dinamicHelper.scotiastatementtransdate = (DateTime)DatetimeUtils.ParseStringToDate(_val);
                                                }
                                            }
                                        }
                                        break;
                                    case 10: // Estatus
                                        {
                                            if (cdt != null)
                                            {
                                                if (cdt == CellValues.SharedString)
                                                {
                                                    var _val = getValueString(currentcellFor, wbPart);
                                                    _dinamicHelper.scotiastatementstatus = _val;
                                                }
                                            }
                                        }
                                        break;
                                    case 11: // Clasificacion
                                        {
                                            if (cdt != null)
                                            {
                                                if (cdt == CellValues.SharedString)
                                                {
                                                    var _val = getValueString(currentcellFor, wbPart);
                                                    _dinamicHelper.scotiastatementclasification = _val;
                                                }
                                            }
                                        }
                                        break;
                                    case 12: // No de autorización
                                        {
                                            if (cdt != null)
                                            {
                                                if (cdt == CellValues.SharedString)
                                                {
                                                    var _val = getValueString(currentcellFor, wbPart);
                                                    _dinamicHelper.scotiastatementauthorizationcode = _val;
                                                }
                                            }
                                        }
                                        break;
                                    case 13: // Codigo de divisa enviado
                                        {
                                            if (cdt != null)
                                            {
                                                if (cdt == CellValues.SharedString)
                                                {
                                                    var _val = getValueString(currentcellFor, wbPart);
                                                    _dinamicHelper.scotiastatementcurrencysentname = _val;
                                                    _dinamicHelper.scotiastatementcurrencysent = dcurrency.Where(z => z.Key.Equals(_val)).Select(c => c.Value).FirstOrDefault();
                                                }
                                            }
                                        }
                                        break;
                                    case 14: // cantidad de divisa enviada
                                        {
                                            if (cdt != null)
                                            {
                                                if (cdt == CellValues.SharedString)
                                                {
                                                    var _val = getValueString(currentcellFor, wbPart);
                                                    decimal number;
                                                    Decimal.TryParse(_val, out number);
                                                    _dinamicHelper.scotiastatemensammountsent = Decimal.Round(number, 2);
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                                Log.Info(string.Format("No se puede procesar la linea {0} -  - del archivo {1} . --- >> ", cellCount, file.fileName), e);
                                _dinamicHelper.xlscorrectlyformed = false;
                                _dinamicHelper.statusconciliation = (int)BankAccountReconcilitionStatus.Error;
                                _dinamicHelper.methodconciliation = (int)BankAccountReconciliationMethod.SinConciliar;
                                list.Add(_dinamicHelper);
                            }
                        }
                        list.Add(_dinamicHelper);
                    }
                }
                return list.OrderBy(v => v.scotiastatementtransdate).ToList();
            }
            catch (Exception ex)
            {
                Log.Error("No se puede procesar el archivo con los detalles de soctiaPos" + file.fileName, ex);
                throw new Exception("No se puede procesar el archivo con los detalles de soctiaPos" + file.fileName);
            }
        }

        private void savePaymentbyAuthCode(bankreconciliationdetailScotiaPos helper, List<financialstateitemstatements> finstats)
        {
            List<tblreservationpayments> rsv_pays = (unity.PaymentsRESERVRepository.Get(c => c.authCode == helper.scotiastatementauthorizationcode && c.idTerminal == helper.scotiastatementtpv)).ToList();
            List<invoicepayment> ups_pays = (unity.PaymentsUPSCLRepository.Get(c => c.authCode == helper.scotiastatementauthorizationcode && ((IEnumerable<int>)c.PaymentInstrument.Select(v => v.idTerminal)).Contains((int)helper.scotiastatementtpv))).ToList();

            foreach (financialstateitemstatements f_stat in finstats)
            {
                foreach (financialstateitem f_item in f_stat.fin_states)
                {
                    List<tblreservationpayments> rsvpayms = rsv_pays.FindAll(r => r.idReservationPayment == f_item.ReferenceItem && f_item.SourceData == 5);
                    List<invoicepayment> upspayms = ups_pays.FindAll(u => u.idpayment == f_item.ReferenceItem && f_item.SourceData == 4);

                    foreach (tblreservationpayments r_pay in rsvpayms)
                    {
                        unity.StatementsRESERVRepository.Insert(new tblbankstatereserv
                        {
                            idBankStatements = f_stat.idBankStatement,
                            idReservationPayment = r_pay.idReservationPayment,
                            authCode = r_pay.authCode
                        });
                        unity.Commit();
                    }

                    foreach (invoicepayment u_pay in upspayms)
                    {
                        unity.StatementsUPSCLRepository.Insert(new tblbankstateupscl
                        {
                            idBankStatements = f_stat.idBankStatement,
                            idPayment = u_pay.idpayment,
                            authCode = u_pay.authcode
                        });
                        unity.Commit();
                    }
                }
            }
        }

        private docpaymentupscl searchDocPaymentUpscl(bankreconciliationdetailScotiaPos helper)
        {
            var result = unity.PaymentsUPSCLRepository.Get(c => c.authCode == helper.scotiastatementauthorizationcode && ((IEnumerable<int>)c.PaymentInstrument.Select(v => v.idTerminal)).Contains((int)helper.scotiastatementtpv)).FirstOrDefault();


            if (result != null)
            {
                helper.scotiastatementsourcedata = 4;

                if (this.validtoAddPaymentData(helper) == true)
                {
                    return GeneralModelHelper.ConvertTbltoHelper(result);
                }

                else
                {
                    return null;
                }
            }

            else
            {
                return null;
            }
        }

        private docpaymentreserv searchDocPaymentReserv(bankreconciliationdetailScotiaPos helper)
        {
            var result = unity.PaymentsRESERVRepository.Get(c => c.authCode == helper.scotiastatementauthorizationcode && c.idTerminal == helper.scotiastatementtpv).FirstOrDefault();


            if (result != null) // Buscando el registro que coincida con los datos // Reservaciones 5
            {
                helper.scotiastatementsourcedata = 5;

                if (this.validtoAddPaymentData(helper) == true)
                {
                    return GeneralModelHelper.ConvertTbltoHelper(result);
                }

                else
                {
                    return null;
                }
            }

            else
            {
                return null;
            }
        }

        private bool validtoAddPaymentData(bankreconciliationdetailScotiaPos helper)
        {
            switch (helper.scotiastatementsourcedata)
            {
                case 4:
                    {
                        var result = unity.ScotiaPosDetailsRepository.Get(c => c.scotiastatementtpv == helper.scotiastatementtpv && c.scotiastatementsiditem == helper.scotiastatementsiditem).FirstOrDefault();
                        return (result != null);
                    }

                case 5:
                    {
                        var result = unity.ScotiaPosDetailsRepository.Get(c => c.scotiastatementtpv == helper.scotiastatementtpv && c.scotiastatementsiditem == helper.scotiastatementsiditem).FirstOrDefault();
                        return (result != null);
                    }

                default:
                    return false;
            }
        }

        #endregion
    }
}