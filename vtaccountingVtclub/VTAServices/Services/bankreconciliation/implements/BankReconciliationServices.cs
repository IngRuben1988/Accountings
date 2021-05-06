using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.Cores.Globales;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.GenericRepository.Repositories;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Helpers.xls;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.accounts;
using VTAworldpass.VTAServices.Services.bankreconciliation.helpers;
using VTAworldpass.VTAServices.Services.bankreconciliation.model;
using VTAworldpass.VTAServices.Services.drive;
using VTAworldpass.VTAServices.Services.drive.model;
using VTAworldpass.VTAServices.Services.Logger;
using VTAworldpass.VTAServices.Services.Models;
using VTAworldpass.Web.VTACore.Utils;
using static VTAworldpass.VTACore.Cores.Globales.Enumerables;

namespace VTAworldpass.VTAServices.Services.bankreconciliation.implements
{
    public class BankReconciliationServices : IBankReconciliationServices
    {
        private readonly Fileservices fileServices = new Fileservices();
        private readonly IAccountServices accountServices;
        private readonly ILogsServices logsServices;
        public BankReconciliationServices()
        { }
        public BankReconciliationServices(IAccountServices _accountservice, ILogsServices _logsServices)
        {
            accountServices = _accountservice;
            logsServices = _logsServices;
        }

        private static readonly UnitOfWork unity = new UnitOfWork();

        #region Bank Reconciliation V2

        public List<bankstatements> getBankStatements2(DateTime? dateReportStart, DateTime? dateReportEnd, int PaymentMethod, bool sourcedata, bool financialstateitem)
        {
            IEnumerable<bankstatements> list = new List<bankstatements>();
            list = GeneralModelHelper.ConvertTbltoHelper(BankReconciliationRepository.getBankStatements(dateReportStart, dateReportEnd, PaymentMethod, 0, 0), sourcedata, financialstateitem);

            return list.ToList();
        }

        /************ MAIN ACTIONS ****************/

        public List<bankreconciliationModel> getBakReconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, BankAccountReconciliationStatus status, bool sourcedata, bool financialstateitem)
        {
            IEnumerable<bankreconciliationModel> list = new List<bankreconciliationModel>();
            list = GeneralModelHelper.ConvertTbltoHelper(BankReconciliationRepository.getbankstatements(dateReportStart, dateReportEnd, Tpv, externalgroup, company, PaymentMethod, hotel, 0, 0), sourcedata, financialstateitem);
            //se comenta esta sección para tomar todas las conciliaciones aun si no estan completas
            /*if ((int)status != -1)
            { list = list.Where(b => b.statusconciliation == (int)status); }*/

            if (hotel != 0)
            { list = list.Where(b => b.idhotelpartner == hotel); }

            return list.ToList();
        }

        public List<bankstatements> getBankStat2Reconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, BankAccountReconciliationStatus status, bool sourcedata, bool financialstateitem)
        {
            IEnumerable<bankstatements> list = new List<bankstatements>();
            list = GeneralModelHelper.ConvertTblToHelper(BankReconciliationRepository.getBankstatements(dateReportStart, dateReportEnd, Tpv, externalgroup, company, PaymentMethod, hotel, 0, 0), sourcedata, financialstateitem);


            return list.ToList();
        }

        public List<bankreconciliationModel> getBakReconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, int status, bool sourcedata, bool financialstateitem)
        {
            IEnumerable<bankreconciliationModel> list = new List<bankreconciliationModel>();
            list = GeneralModelHelper.ConvertTbltoHelper(BankReconciliationRepository.getbankstatements(dateReportStart, dateReportEnd, Tpv, externalgroup, PaymentMethod != 0 ? 0 : company, PaymentMethod, hotel, 0, 0), sourcedata, financialstateitem);

            if (status != -1)
            { list = list.Where(b => b.statusconciliation == status); }

            if (hotel != 0)
            { list = list.Where(b => b.idhotelpartner == hotel); }

            return list.ToList();
        }

        public List<bankreconciliationModel> getBakReconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, int status, decimal ammountStart, decimal ammountEnd, bool sourcedata, bool financialstateitem)
        {
            IEnumerable<bankreconciliationModel> list = new List<bankreconciliationModel>();
            list = GeneralModelHelper.ConvertTbltoHelper(BankReconciliationRepository.getbankstatements(dateReportStart, dateReportEnd, Tpv, externalgroup, PaymentMethod != 0 ? 0 : company, PaymentMethod, hotel, ammountStart, ammountEnd), sourcedata, financialstateitem);

            if (status != -1)
            {
                list = list.Where(b => b.statusconciliation == status);
            }

            if (hotel != 0)
            {
                list = list.Where(b => b.idhotelpartner == hotel);
            }

            return list.ToList();
        }

        public List<bankstatements> getBankStat2Reconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int type, string descripcion, int status, decimal ammountStart, decimal ammountEnd, bool sourcedata, bool financialstateitem)
        {
            IEnumerable<bankstatements> list = new List<bankstatements>();
            list = GeneralModelHelper.ConvertTblToHelper(BankReconciliationRepository.getBankStatements(dateReportStart, dateReportEnd, ammountStart, ammountEnd, Tpv, externalgroup, PaymentMethod != 0 ? 0 : company, PaymentMethod, type, descripcion), sourcedata, financialstateitem);

            if (status != -1)
            {
                list = list.Where(b => b.statusconciliation == status);
            }

            return list.ToList();
        }

        public List<bankstatements> getBankStat2Reconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, int status, decimal ammountStart, decimal ammountEnd, bool sourcedata, bool financialstateitem)
        {
            IEnumerable<bankstatements> list = new List<bankstatements>();
            list = GeneralModelHelper.ConvertTblToHelper(BankReconciliationRepository.getBankstatements(dateReportStart, dateReportEnd, Tpv, externalgroup, PaymentMethod != 0 ? 0 : company, PaymentMethod, hotel, ammountStart, ammountEnd), sourcedata, financialstateitem);

            if (status != -1)
            {
                list = list.Where(b => b.statusconciliation == status);
            }

            return list.ToList();
        }


        public List<bankreconciliationModel> getBakReconcilitions(int month, int year, int Tpv, int currency, int externalgroup, int company, int BankAccount, int hotel, BankAccountReconcilitionStatus status, bool sourcedata, bool financialstateitem)
        {
            IEnumerable<bankreconciliationModel> list = new List<bankreconciliationModel>();
            list = GeneralModelHelper.ConvertTbltoHelper(BankReconciliationRepository.getbankstatements(month, year, Tpv, currency, externalgroup, BankAccount), sourcedata, financialstateitem);

            if ((int)status != -1)
            {
                list = list.Where(b => b.statusconciliation == (int)status);
            }

            if (hotel != 0)
            {
                list = list.Where(b => b.idhotelpartner == hotel);
            }

            return list.ToList();
        }

        public bankreconciliationModel getBakReconciliationsDetailsbyId(long idBankStatements)
        {
            bankreconciliationModel helper = new bankreconciliationModel();
            helper.GenerateConciliation(idBankStatements, false);

            return helper;
        }

        public bankstatements getBankStat2ReconciliationsDetailsById(long idBankStatements)
        {
            bankstatements helper = new bankstatements();
            helper.GenerateConciliation(idBankStatements, false);

            return helper;
        }

        public bankreconciliationModel getBakReconciliationsDetailsbyRefenceReferenceItem(int sourcedata, long reference, long referenceitem, int rsvType)
        {
            try
            {
                long idBankStatements = 0;

                switch (sourcedata)
                {
                    case 7:// Reservas
                        {
                            if (rsvType == (int)BankstateRsvType.RsvMember)
                            {
                                idBankStatements = unity.BankstateReservRepository.Get(v => v.tblreservationspayment.idReservationPayment == referenceitem && v.tblreservationspayment.tblreservations.idReservation == reference).Select(c => c.idBankStatements).First();
                            }
                            else
                            {
                                idBankStatements = unity.BankstateParentReservRepository.Get(v => v.tblreservationsparentpayment.idReservationParentPayment == referenceitem && v.tblreservationsparentpayment.tblreservationsparent.idReservationParent == reference).Select(c => c.idBankStatements).First();
                            }
                        }
                        break;
                    case 8: // Pagos Membership
                        { idBankStatements = unity.BankstatePurchaseRepository.Get(v => v.tblpaymentspurchases.idPaymentPurchase == referenceitem && v.tblpaymentspurchases.tblpurchases.idPurchase == reference).Select(c => c.idBankStatements).First(); }
                        break;
                }

                if (idBankStatements == 0) throw new Exception("No se encuentra la conciliacion de este registro.");

                bankreconciliationModel helper = new bankreconciliationModel();
                helper.GenerateConciliation(idBankStatements, false);

                return helper;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public long saveBankStatement(bankreconciliationModel bankreconciliation)
        {
            try
            {
                tblbankstatements model = GeneralModelHelper.PrepareToSave(bankreconciliation);
                unity.BankstatementsRepository.Insert(model);
                unity.Commit();

                return model.idBankStatements;
            }

            catch (Exception e)
            {
                throw new Exception("No se puede guardar registro tblbankstatement", e);
            }
        }

        public long saveBankStatement(bankreconciliationModel bankreconciliation, out string message)
        {
            try
            {
                tblbankstatements model = GeneralModelHelper.PrepareToSave(bankreconciliation);
                unity.BankstatementsRepository.Insert(model);
                unity.Commit();
                message = "";
                return model.idBankStatements;
            }

            catch (Exception e)
            {
                Log.Error("No se puede guardar registro tblbankstatement", e);
                message = string.Format("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}", bankreconciliation.tpvname,
                         bankreconciliation.hotelpartnername, bankreconciliation.currencyname, bankreconciliation.baccountname, bankreconciliation.bankstatementAplicationDate, bankreconciliation.bankstatementAppliedAmmount,
                         bankreconciliation.bankstatementBankFee,
                         bankreconciliation.bankstatementAppliedAmmountFinal);
                return 0;
            }
        }

        public void updateBankStatement(tblbankstatements model)
        {
            unity.BankstatementsRepository.Update(model);
            unity.Commit();
        }

        public void updateBankStatement(tblbankstatements2 model)
        {
            unity.Bankstatements2Repository.Update(model);
            unity.Commit();
        }

        public int saveBankStatementItem(int idBankStatement, financialstateitemModel item)
        {
            switch (item.SourceData)
            {
                case 2: // Incomes
                    {
                        try
                        {
                            unity.BankstateIncomeRepository.Insert(new tblbankstateincome()
                            {
                                idBankStatements = (int)idBankStatement,
                                idincomeMovement = item.ReferenceItem
                            });

                            tblbankstatements bstat = unity.BankstatementsRepository.Get(y => y.idBankStatements == idBankStatement).FirstOrDefault();
                            bstat.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                            unity.BankstatementsRepository.Update(bstat);
                            unity.Commit();

                            return 1;
                        }

                        catch (Exception ex)
                        {
                            throw new Exception("No se puede realizar la inserción de dato tblbankstateincome." + " Error ---> ", ex);
                        }
                    }
                case 7: // Reservas
                    {
                        try
                        {



                            if (item.typeRsv == (int)BankstateRsvType.RsvMember)
                            {
                                tblbankstatereserv model = new tblbankstatereserv();
                                model.idBankStatements = (int)idBankStatement;
                                model.idReservationPayment = (int)item.ReferenceItem;

                                unity.BankstateReservRepository.Insert(model);
                                unity.Commit();
                            }
                            else
                            {
                                tblbankstateparentreserv model1 = new tblbankstateparentreserv();
                                model1.idBankStatements = (int)idBankStatement;
                                model1.idReservationParentPayment = (int)item.ReferenceItem;

                                unity.BankstateParentReservRepository.Insert(model1);
                                unity.Commit();
                            }

                            var _model = unity.BankstatementsRepository.Get(x => x.idBankStatements == idBankStatement).FirstOrDefault();
                            _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                            this.updateBankStatement(_model);

                            return 1;
                        }

                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la inserción de dato tblbankstatereserv ", e);
                        }
                    }

                case 8: // Pagos Membership
                    {
                        try
                        {
                            tblbankstatepurchase model = new tblbankstatepurchase();
                            model.idBankStatements = (int)idBankStatement;
                            model.idPaymentPurchase = (int)item.ReferenceItem;

                            unity.BankstatePurchaseRepository.Insert(model);
                            unity.Commit();

                            var _model = unity.BankstatementsRepository.Get(x => x.idBankStatements == idBankStatement).FirstOrDefault();
                            _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                            this.updateBankStatement(_model);

                            return 1;
                        }

                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la inserción de dato tblbankstatepurchase." + " Error ---> ", e);
                        }
                    }

                default:
                    throw new Exception("No se puede determinar el origen del registro");
            }

        }

        public int saveBankStatementItem2(int idBankStatement, financialstateitemModel item)
        {
            switch (item.SourceData)
            {
                case 2: // Incomes
                    {
                        try
                        {
                            unity.Statements2INCOMERepository.Insert(new tblbankstat2income()
                            {
                                idBankStatements2 = Convert.ToInt64(idBankStatement),
                                idincomeMovement = item.ReferenceItem
                            });

                            tblbankstatements2 bstat = unity.Bankstatements2Repository.Get(y => y.idBankStatements2 == idBankStatement).FirstOrDefault();
                            bstat.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                            unity.Bankstatements2Repository.Update(bstat);
                            unity.Commit();

                            return 1;
                        }

                        catch (Exception ex)
                        {
                            throw new Exception("No se puede realizar la inserción de dato tblbankstateincome." + " Error ---> ", ex);
                        }
                    }
                case 4: // Pagos
                    {
                        try
                        {
                            tblbankstat2invoice model = new tblbankstat2invoice();
                            model.idBankStatements2 = idBankStatement;
                            model.idpayment = item.ReferenceItem;

                            unity.Statements2INVOICERepository.Insert(model);
                            unity.Commit();

                            var _model = unity.Bankstatements2Repository.Get(x => x.idBankStatements2 == idBankStatement).FirstOrDefault();
                            _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                            _model.bankstatements2Reconciled = Constants.ActiveRecord;
                            this.updateBankStatement(_model);

                            return 1;
                        }
                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la inserción de dato tblbankstat2invoice ", e);
                        }
                    }
                case 5: // Fondos
                    {
                        try
                        {
                            var _model = unity.Bankstatements2Repository.Get(x => x.idBankStatements2 == idBankStatement).FirstOrDefault();
                            var fondo = unity.Statements2FONDORepository.Get(x => x.idFondos == (int)item.ReferenceItem).FirstOrDefault();

                            var fondoin = unity.Statements2FONDORepository.Get(x => x.idBankStatements2In == idBankStatement).FirstOrDefault();
                            var fondoout = unity.Statements2FONDORepository.Get(x => x.idBankStatements2Out == idBankStatement).FirstOrDefault();

                            tblbankstat2fondo model = new tblbankstat2fondo();
                            if (fondo != null)
                            {
                                if (fondo.idBankStatements2In == null && fondo.idBankStatements2Out != null)
                                {
                                    var BAccIn = _model.idBAccount;
                                    var fondoOri = unity.FondosRepository.Get(x => x.idPaymentMethod == BAccIn && x.fondofechaEntrega <= _model.bankstatements2AplicationDate && x.fondofechaEntrega >= _model.bankstatements2AplicationDate && x.fondoMonto <= _model.bankstatements2Pay);

                                    if (fondoOri != null)
                                    {
                                        foreach (tblfondos element in fondoOri)
                                        {
                                            if (element.idFinancialMethod == _model.idBAccount && element.fondofechaEntrega == _model.bankstatements2AplicationDate && element.fondoMonto == _model.bankstatements2Pay)
                                            {
                                                break;
                                            }
                                        }
                                    }

                                    fondo.idBankStatements2In = idBankStatement;
                                    unity.Statements2FONDORepository.Update(fondo);
                                    unity.Commit();

                                    _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                    _model.bankstatements2Reconciled = Constants.ActiveRecord;
                                    this.updateBankStatement(_model);
                                }
                                else if (fondo.idBankStatements2Out == null && fondo.idBankStatements2In != null)
                                {
                                    var BAccOut = _model.idBAccount;
                                    var fondoDest = unity.FondosRepository.Get(x => x.idFinancialMethod == BAccOut && x.fondofechaEntrega <= _model.bankstatements2AplicationDate && x.fondofechaEntrega >= _model.bankstatements2AplicationDate && x.fondoMonto <= _model.bankstatements2Charge);

                                    if (fondoDest != null)
                                    {
                                        foreach (tblfondos element in fondoDest)
                                        {
                                            if (element.idFinancialMethod == _model.idBAccount && element.fondofechaEntrega == _model.bankstatements2AplicationDate && element.fondoMonto == _model.bankstatements2Charge)
                                            {

                                                break;
                                            }
                                        }
                                    }
                                    fondo.idBankStatements2Out = idBankStatement;
                                    unity.Statements2FONDORepository.Update(fondo);
                                    unity.Commit();

                                    _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                    _model.bankstatements2Reconciled = Constants.ActiveRecord;
                                    this.updateBankStatement(_model);

                                }

                                return 1;
                            }
                            else if (fondoin == null && fondoout == null)
                            {

                                var statement = _model.bankstatements2Charge == 0m ? true : false;

                                if (statement) { model.idBankStatements2In = idBankStatement; }
                                else { model.idBankStatements2Out = idBankStatement; }

                                model.idFondos = (int)item.ReferenceItem;

                                unity.Statements2FONDORepository.Insert(model);
                                unity.Commit();

                                _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                _model.bankstatements2Reconciled = Constants.ActiveRecord;
                                this.updateBankStatement(_model);

                                return 1;
                            }
                            else
                            {
                                return 0;
                            }


                        }
                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la inserción de dato tblbankstateupscl ", e);
                        }
                    }
                case 7: // Reservas
                    {
                        try
                        {
                            tblbankstat2reserv model = new tblbankstat2reserv();
                            tblbankstat2parentreserv model1 = new tblbankstat2parentreserv();

                            if (item.typeRsv == (int)BankstateRsvType.RsvMember)
                            {
                                model.idBankStatements2 = Convert.ToInt64(idBankStatement);
                                model.idReservationPayment = Convert.ToInt32(item.ReferenceItem);

                                unity.Statements2RESERVRepository.Insert(model);
                                unity.Commit();
                            }
                            else
                            {
                                model1.idBankStatements2 = Convert.ToInt64(idBankStatement);
                                model1.idReservationParentPayment = Convert.ToInt32(item.ReferenceItem);

                                unity.Statements2PARENTRESERVRepository.Insert(model1);
                                unity.Commit();
                            }
                            var _model = unity.Bankstatements2Repository.Get(x => x.idBankStatements2 == idBankStatement).FirstOrDefault();
                            _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                            this.updateBankStatement(_model);

                            return 1;
                        }

                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la inserción de dato tblbankstateupscl ", e);
                        }
                    }
                case 8: // Pagos Membership
                    {
                        try
                        {
                            tblbankstat2purchase model = new tblbankstat2purchase();
                            model.idBankStatements2 = Convert.ToInt64(idBankStatement);
                            model.idPaymentPurchase = Convert.ToInt32(item.ReferenceItem);

                            unity.Statements2PURCHASESRepository.Insert(model);
                            unity.Commit();

                            var _model = unity.Bankstatements2Repository.Get(x => x.idBankStatements2 == idBankStatement).FirstOrDefault();
                            _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                            this.updateBankStatement(_model);

                            return 1;
                        }

                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la inserción de dato tblbankstatereserv." + " Error ---> ", e);
                        }
                    }
                default:
                    throw new Exception("No se puede determinar el origen del registro");
            }

        }

        /*public int saveBankStatementItem(bankreconciliationdetailScotiaPos helper, out string message)
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
        }*/

        public int saveBankStatementItemXLS(int idBankStatement, financialstateitemModel item)
        {
            switch (item.SourceData)
            {
                case 7: // Reservas
                    {
                        try
                        {
                            tblbankstatereserv model = new tblbankstatereserv();
                            tblbankstateparentreserv model1 = new tblbankstateparentreserv();
                            if (item.typeRsv == (int)BankstateRsvType.RsvMember)
                            {
                                model.idBankStatements = Convert.ToInt64(idBankStatement);
                                model.idReservationPayment = Convert.ToInt32(item.ReferenceItem);

                                unity.BankstateReservRepository.Insert(model);
                                unity.Commit();
                            }
                            else
                            {
                                model1.idBankStatements = Convert.ToInt64(idBankStatement);
                                model1.idReservationParentPayment = Convert.ToInt32(item.ReferenceItem);

                                unity.BankstateParentReservRepository.Insert(model1);
                                unity.Commit();
                            }

                            return 1;
                        }
                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la inserción de dato tblbankstateupscl", e);
                        }
                    }
                case 8: // Pagos Membership
                    {
                        try
                        {
                            tblbankstatepurchase model = new tblbankstatepurchase();
                            model.idBankStatements = Convert.ToInt64(idBankStatement);
                            model.idPaymentPurchase = Convert.ToInt32(item.ReferenceItem);

                            unity.BankstatePurchaseRepository.Insert(model);
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

        public int deleteBankStatementItem(int idBankStatement, financialstateitemModel item)
        {
            switch (item.SourceData)
            {
                case 2:
                    {
                        try
                        {
                            tblbankstateincome result = unity.BankstateIncomeRepository.Get(i => i.idBankStatements == idBankStatement && i.idincomeMovement == item.ReferenceItem).FirstOrDefault();

                            if (result != null)
                            {
                                tblbankstatements bstat = unity.BankstatementsRepository.Get(b => b.idBankStatements == idBankStatement).FirstOrDefault();
                                bstat.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;

                                unity.BankstateIncomeRepository.Delete(result);
                                unity.BankstatementsRepository.Update(bstat);
                                unity.Commit();

                                return 1;
                            }

                            else
                            {
                                throw new Exception("No se encontro el registro.");
                            }
                        }

                        catch (Exception ex)
                        {
                            throw new Exception("No se puede realizar la eliminación de dato tblbankstateincome." + " Error ---> ", ex);
                        }
                    }
                case 7: // Reservas
                    {
                        try
                        {
                            if (item.typeRsv == (int)BankstateRsvType.RsvMember)
                            {
                                var result = unity.BankstateReservRepository.Get(x => x.idBankStatements == idBankStatement && x.idReservationPayment == item.ReferenceItem).FirstOrDefault();
                                if (result != null)
                                {
                                    unity.BankstateReservRepository.Delete(result);
                                    unity.Commit();

                                    var _model = unity.BankstatementsRepository.Get(x => x.idBankStatements == idBankStatement).FirstOrDefault();
                                    _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                    this.updateBankStatement(_model);

                                    return 1;
                                }
                                else
                                {
                                    throw new Exception("No se encontro el registro.");
                                }
                            }
                            else
                            {
                                var result = unity.BankstateParentReservRepository.Get(x => x.idBankStatements == idBankStatement && x.idReservationParentPayment == item.ReferenceItem).FirstOrDefault();

                                if (result != null)
                                {
                                    unity.BankstateParentReservRepository.Delete(result);
                                    unity.Commit();

                                    var _model = unity.BankstatementsRepository.Get(x => x.idBankStatements == idBankStatement).FirstOrDefault();
                                    _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                    this.updateBankStatement(_model);

                                    return 1;
                                }
                                else
                                {
                                    throw new Exception("No se encontro el registro.");
                                }
                            }
                        }

                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la eliminación de dato tblbankstateupscl", e);
                        }
                    }

                case 8: // Pagos Membership
                    {
                        try
                        {
                            var result = unity.BankstatePurchaseRepository.Get(x => x.idBankStatements == idBankStatement && x.idPaymentPurchase == item.ReferenceItem).FirstOrDefault();

                            if (result != null)
                            {
                                unity.BankstatePurchaseRepository.Delete(result);
                                unity.Commit();

                                var _model = unity.BankstatementsRepository.Get(x => x.idBankStatements == idBankStatement).FirstOrDefault();
                                _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                this.updateBankStatement(_model);

                                return 1;
                            }

                            else
                            {
                                throw new Exception("No se encontro el registro.");
                            }
                        }

                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la eliminación de dato tblbankstatereserv." + " Error ---> ", e);
                        }
                    }

                default:
                    throw new Exception("No se puede realziar la acción");
            }
        }

        public int deleteBankStatementItem2(int idBankStatement, financialstateitemModel item)
        {
            switch (item.SourceData)
            {
                case 2: // Income
                    {
                        try
                        {
                            var result = unity.Statements2INCOMERepository.Get(i => i.idBankStatements2 == idBankStatement && i.idincomeMovement == item.ReferenceItem).FirstOrDefault();

                            if (result != null)
                            {
                                tblbankstatements2 _model = unity.Bankstatements2Repository.Get(b => b.idBankStatements2 == idBankStatement).FirstOrDefault();
                                _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                _model.bankstatements2Reconciled = Constants.UnactiveRecord;

                                unity.Statements2INCOMERepository.Delete(result);
                                unity.Bankstatements2Repository.Update(_model);
                                unity.Commit();

                                return 1;
                            }

                            else
                            {
                                throw new Exception("No se encontro el registro.");
                            }
                        }

                        catch (Exception ex)
                        {
                            throw new Exception("No se puede realizar la eliminación de dato tblbankstateincome." + " Error ---> ", ex);
                        }
                    }
                case 4: // Pagos
                    {
                        try
                        {
                            var result = unity.Statements2INVOICERepository.Get(x => x.idBankStatements2 == idBankStatement && x.idpayment == item.ReferenceItem).FirstOrDefault();

                            if (result != null)
                            {
                                unity.Statements2INVOICERepository.Delete(result);
                                unity.Commit();

                                var _model = unity.Bankstatements2Repository.Get(x => x.idBankStatements2 == idBankStatement).FirstOrDefault();
                                _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                _model.bankstatements2Reconciled = Constants.UnactiveRecord;

                                this.updateBankStatement(_model);

                                return 1;
                            }

                            else
                            {
                                throw new Exception("No se encontro el registro");
                            }
                        }
                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la eliminación de dato tblbankstateupscl", e);
                        }
                    }
                case 5: // Fondos
                    {
                        try
                        {
                            var result = unity.Statements2FONDORepository.Get(x => x.idBankStatements2In == idBankStatement && x.idFondos == item.ReferenceItem).FirstOrDefault();
                            var result2 = unity.Statements2FONDORepository.Get(x => x.idBankStatements2Out == idBankStatement && x.idFondos == item.ReferenceItem).FirstOrDefault();

                            if (result != null)
                            {
                                var _model = unity.Bankstatements2Repository.Get(x => x.idBankStatements2 == idBankStatement).FirstOrDefault();
                                _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                _model.bankstatements2Reconciled = Constants.UnactiveRecord;

                                if (result.idBankStatements2In != null && result.idBankStatements2Out != null)
                                {
                                    result.idBankStatements2In = null;
                                    unity.Statements2FONDORepository.Update(result);
                                    unity.Commit();

                                    this.updateBankStatement(_model);
                                }
                                else
                                {
                                    unity.Statements2FONDORepository.Delete(result);
                                    unity.Commit();

                                    this.updateBankStatement(_model);
                                }
                                return 1;
                            }
                            else if (result2 != null)
                            {

                                var _model = unity.Bankstatements2Repository.Get(x => x.idBankStatements2 == idBankStatement).FirstOrDefault();
                                _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                _model.bankstatements2Reconciled = Constants.UnactiveRecord;

                                if (result2.idBankStatements2In != null && result2.idBankStatements2Out != null)
                                {
                                    result2.idBankStatements2Out = null;
                                    unity.Statements2FONDORepository.Update(result2);
                                    unity.Commit();

                                    this.updateBankStatement(_model);
                                }
                                else
                                {
                                    unity.Statements2FONDORepository.Delete(result2);
                                    unity.Commit();

                                    this.updateBankStatement(_model);
                                }
                                return 1;
                            }

                            else
                            {
                                throw new Exception("No se encontro el registro");
                            }
                        }
                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la eliminación de dato tblbankstateupscl", e);
                        }
                    }
                case 7: // Reservas
                    {
                        try
                        {
                            if (item.typeRsv == (int)BankstateRsvType.RsvMember)
                            {
                                var result = unity.Statements2RESERVRepository.Get(x => x.idBankStatements2 == idBankStatement && x.idReservationPayment == item.ReferenceItem).FirstOrDefault();
                                if (result != null)
                                {
                                    unity.Statements2RESERVRepository.Delete(result);
                                    unity.Commit();

                                    var _model = unity.Bankstatements2Repository.Get(x => x.idBankStatements2 == idBankStatement).FirstOrDefault();
                                    _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                    _model.bankstatements2Reconciled = Constants.UnactiveRecord;
                                    this.updateBankStatement(_model);

                                    return 1;
                                }
                                else
                                {
                                    throw new Exception("No se encontro el registro.");
                                }
                            }
                            else
                            {
                                var result = unity.BankstateParentReservRepository.Get(x => x.idBankStatements == idBankStatement && x.idReservationParentPayment == item.ReferenceItem).FirstOrDefault();

                                if (result != null)
                                {
                                    unity.BankstateParentReservRepository.Delete(result);
                                    unity.Commit();

                                    var _model = unity.Bankstatements2Repository.Get(x => x.idBankStatements2 == idBankStatement).FirstOrDefault();
                                    _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                    _model.bankstatements2Reconciled = Constants.UnactiveRecord;
                                    this.updateBankStatement(_model);

                                    return 1;
                                }
                                else
                                {
                                    throw new Exception("No se encontro el registro.");
                                }
                            }
                        }

                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la eliminación de dato tblbankstateupscl", e);
                        }
                    }
                case 8: // Pagos Membership
                    {
                        try
                        {
                            var result = unity.Statements2PURCHASESRepository.Get(x => x.idBankStatements2 == idBankStatement && x.idPaymentPurchase == item.ReferenceItem).FirstOrDefault();

                            if (result != null)
                            {
                                unity.Statements2PURCHASESRepository.Delete(result);
                                unity.Commit();

                                var _model = unity.Bankstatements2Repository.Get(x => x.idBankStatements2 == idBankStatement).FirstOrDefault();
                                _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                _model.bankstatements2Reconciled = Constants.UnactiveRecord;
                                this.updateBankStatement(_model);

                                return 1;
                            }

                            else
                            {
                                throw new Exception("No se encontro el registro.");
                            }
                        }

                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la eliminación de dato tblbankstatereserv." + " Error ---> ", e);
                        }
                    }

                default:
                    throw new Exception("No se puede realziar la acción");
            }
        }

        public int deleteBankStatementItem(int SourceData, long Reference, long ReferenceItem, int rsvType)
        {

            switch (SourceData)
            {
                case 7: // Reservas
                    {
                        try
                        {
                            if (rsvType == (int)BankstateRsvType.RsvMember)
                            {
                                var result = unity.ReservationsPaymentsRepository.Get(c => c.idReservationPayment == ReferenceItem && c.tblreservations.idReservation == Reference).Select(f => f.tblbankstatereserv.FirstOrDefault()).FirstOrDefault();
                                if (result != null)
                                {
                                    unity.BankstateReservRepository.Delete(result);
                                    unity.Commit();

                                    var _model = unity.BankstatementsRepository.Get(x => x.idBankStatements == result.idBankStatements).FirstOrDefault();
                                    _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                    this.updateBankStatement(_model);

                                    return 1;
                                }
                                else { throw new Exception("No se encontro el registro"); }
                            }
                            else
                            {
                                var result = unity.ReservationsParentPaymentsRepository.Get(c => c.idReservationParentPayment == ReferenceItem && c.tblreservationsparent.idReservationParent == Reference).Select(f => f.tblbankstateparentreserv.FirstOrDefault()).FirstOrDefault();
                                if (result != null)
                                {
                                    unity.BankstateParentReservRepository.Delete(result);
                                    unity.Commit();

                                    var _model = unity.BankstatementsRepository.Get(x => x.idBankStatements == result.idBankStatements).FirstOrDefault();
                                    _model.idBankStatementMethod = (int)BankAccountReconciliationMethod.Manual;
                                    this.updateBankStatement(_model);

                                    return 1;
                                }
                                else { throw new Exception("No se encontro el registro"); }
                            }
                        }
                        catch (Exception e)
                        {
                            throw new Exception("No se puede realizar la eliminación de dato tblbankstateupscl por Refence y ReferenceItem", e);
                        }
                    }
                case 8: // Pagos Membership
                    {
                        try
                        {
                            var result = unity.PaymentsPurchasesRepository.Get(x => x.idPaymentPurchase == ReferenceItem && x.idPurchase == Reference).Select(v => v.tblbankstatepurchase.FirstOrDefault()).FirstOrDefault();
                            if (result != null)
                            {
                                unity.BankstatePurchaseRepository.Delete(result);
                                unity.Commit();

                                var _model = unity.BankstatementsRepository.Get(x => x.idBankStatements == result.idBankStatements).FirstOrDefault();
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

        public List<financialstateitemModel> getSearchFinancialStateItemList(DateTime start, DateTime end, int idBankAccount, int tpv, decimal ammountStart, decimal ammountEnd, int idHotel)
        {
            financialstateModel financialState = new financialstateModel(start, end, idBankAccount, tpv, ammountStart, ammountEnd, idHotel, FinancialStateReport.AccountHistory, true);
            financialState.deleteFinancialStateItemLinked();
            return financialState.financialstateitemlist.ToList();
        }

        public List<financialstateitemModel> getSearchFinancialState2ItemList(DateTime start, DateTime end, int idBankAccount, int tpv, decimal ammountStart, decimal ammountEnd, int typeSourceData, string descripcion)
        {
            financialstateModel financialState = new financialstateModel(start, end, idBankAccount, ammountStart, ammountEnd, FinancialStateReport.AccountHistory, true, typeSourceData);
            financialState.deleteFinancialStateItemLinked();

            List<financialstateitemModel> listitem = new List<financialstateitemModel>();

            foreach (var item in financialState.financialstateitemlist)
            {
                if (descripcion != "")
                {
                    if (item.description.Contains(descripcion))
                    {
                        listitem.Add(item);
                    }
                }
                else
                {
                    listitem.Add(item);
                }
            }

            return listitem.ToList();
        }

        public void deleteBankStatement(long id)
        {
            var _result = unity.BankstatementsRepository.Get(c => c.idBankStatements == id, null, "tblbankstatepurchase,tblbankstatereserv,tblbankstateincome,tblbankstatementsdet").FirstOrDefault();

            if (_result != null)
            {
                bool deleted = false;

                try
                {
                    List<tblbankstatepurchase> listUpsc = new List<tblbankstatepurchase>();
                    listUpsc = _result.tblbankstatepurchase.ToList();

                    // Deleting Upscales-Statements
                    foreach (tblbankstatepurchase model in listUpsc)
                    {
                        this.deleteBankStatementItem(model);
                        Log.Info(string.Format("Borrando registro de tblbankstatepurchase Purchase {0}, tblStatement {1}", model.idBankStatements, model.idBankstatPurchase));
                    }

                    List<tblbankstatereserv> listReserv = new List<tblbankstatereserv>();
                    listReserv = _result.tblbankstatereserv.ToList();

                    // Deleting Reservations-Statements
                    foreach (tblbankstatereserv model in listReserv)
                    {
                        this.deleteBankStatementItem(model);
                        Log.Info(string.Format("Borrando registro de tblbankstatereserv RESERV {0}, tblStatement {1}", model.idBankStatements, model.idBankstatReserv));
                    }

                    List<tblbankstateparentreserv> listReservParent = new List<tblbankstateparentreserv>();
                    listReserv = _result.tblbankstatereserv.ToList();

                    // Deleting Reservations-Statements
                    foreach (tblbankstateparentreserv model in listReservParent)
                    {
                        this.deleteBankStatementItem(model);
                        Log.Info(string.Format("Borrando registro de tblbankstateparentreserv RESERV {0}, tblStatement {1}", model.idBankStatements, model.idBankstatParentReserv));
                    }

                    List<tblbankstateincome> listIncom = new List<tblbankstateincome>();
                    listIncom = _result.tblbankstateincome.ToList();

                    // Deleting Incomes-Statements
                    foreach (tblbankstateincome model in listIncom)
                    {
                        unity.BankstateIncomeRepository.Delete(model);
                        unity.Commit();
                        Log.Info(string.Format("Borrando registro de tblbankstateincome INCOME {0}, tblStatement {1}", model.idBankStatements, model.idincomeMovement));
                    }

                    List<tblbankstatementsdet> listDetails = new List<tblbankstatementsdet>();
                    listDetails = _result.tblbankstatementsdet.ToList();

                    // Deleting Statement Details
                    foreach (tblbankstatementsdet model in listDetails)
                    {
                        unity.BankstatementsDetRepository.Delete(model);
                        unity.Commit();
                        Log.Info(string.Format("Borrando registro de detalles de conciliación {0}, tblStatement {1}", model.idBankStatements, model.idBankStatementsDet));
                    }

                    deleted = true;
                }

                catch (Exception e)
                {
                    Log.Error("No se puede borrar el registro de tblStatementItem", e);
                    deleted = false;
                    throw new Exception("No se puede borrar registros de Upscales, Reservaciones o Ingresos.");
                }

                if (deleted)
                {
                    try
                    {
                        unity.BankstatementsRepository.Delete(_result);
                        unity.Commit();
                    }

                    catch (Exception e)
                    {
                        throw new Exception("No se puede borrar el registro de conciliación. Aún existen registros asociados de Upscales, Reservaciones o Ingresos");
                    }
                }
            }

        }

        public void deleteBankStatement2(long id)
        {
            var _result = unity.Bankstatements2Repository.Get(c => c.idBankStatements2 == id, null, "tblbankstat2purchase,tblbankstat2reserv,tblbankstat2income,tblbankstat2parentreserv,tblbankstat2fondo").FirstOrDefault();

            if (_result != null)
            {
                bool deleted = false;

                try
                {
                    List<tblbankstat2fondo> listFondo = new List<tblbankstat2fondo>();
                    listFondo = _result.tblbankstat2fondo.ToList();
                    listFondo = _result.tblbankstat2fondo1.ToList();
                    // Deleting fondo-Statements
                    foreach (tblbankstat2fondo model in listFondo)
                    {
                        unity.Statements2FONDORepository.Delete(model);
                        unity.Commit();
                        Log.Info(string.Format("Borrando registro de tblbankstat2fondo FONDO {0}, tblStatement {1}", model.idBankStatements2In, model.idBankStat2Fondo));
                    }

                    List<tblbankstat2purchase> listpurchase = new List<tblbankstat2purchase>();
                    listpurchase = _result.tblbankstat2purchase.ToList();

                    // Deleting purchase-Statements
                    foreach (tblbankstat2purchase model in listpurchase)
                    {
                        this.deleteBankStatementItem(model);
                        Log.Info(string.Format("Borrando registro de tblbankstat2purchase Purchase {0}, tblStatement {1}", model.idBankStatements2, model.idBankStat2Purchase));
                    }

                    List<tblbankstat2reserv> listReserv = new List<tblbankstat2reserv>();
                    listReserv = _result.tblbankstat2reserv.ToList();

                    // Deleting Reservations-Statements
                    foreach (tblbankstat2reserv model in listReserv)
                    {
                        this.deleteBankStatementItem(model);
                        Log.Info(string.Format("Borrando registro de tblbankstat2reserv RESERV {0}, tblStatement {1}", model.idBankStatements2, model.idBankStat2Reserv));
                    }

                    List<tblbankstat2parentreserv> listReservParent = new List<tblbankstat2parentreserv>();
                    listReservParent = _result.tblbankstat2parentreserv.ToList();

                    // Deleting Reservations without memberships-Statements
                    foreach (tblbankstat2parentreserv model in listReservParent)
                    {
                        this.deleteBankStatementItem(model);
                        Log.Info(string.Format("Borrando registro de tblbankstat2parentreserv RESERV {0}, tblStatement {1}", model.idBankStatements2, model.idBankStat2ParentReserv));
                    }

                    List<tblbankstat2income> listIncom = new List<tblbankstat2income>();
                    listIncom = _result.tblbankstat2income.ToList();

                    // Deleting Incomes-Statements
                    foreach (tblbankstat2income model in listIncom)
                    {
                        unity.Statements2INCOMERepository.Delete(model);
                        unity.Commit();
                        Log.Info(string.Format("Borrando registro de tblbankstateincome INCOME {0}, tblStatement {1}", model.idBankStatements2, model.idincomeMovement));
                    }

                    List<tblbankstat2invoice> listPayment = new List<tblbankstat2invoice>();
                    listPayment = _result.tblbankstat2invoice.ToList();

                    // Deleting invoice payment-Statement Details
                    foreach (tblbankstat2invoice model in listPayment)
                    {
                        unity.Statements2INVOICERepository.Delete(model);
                        unity.Commit();
                        Log.Info(string.Format("Borrando registro de detalles de conciliación {0}, tblStatement {1}", model.idBankStatements2, model.idBankStat2Invoice));
                    }

                    deleted = true;
                }

                catch (Exception e)
                {
                    Log.Error("No se puede borrar el registro de tblStatementItem", e);
                    deleted = false;
                    throw new Exception("No se puede borrar registros de Upscales, Reservaciones o Ingresos.");
                }

                if (deleted)
                {
                    try
                    {
                        unity.Bankstatements2Repository.Delete(_result);
                        unity.Commit();
                    }

                    catch (Exception e)
                    {
                        throw new Exception("No se puede borrar el registro de conciliación. Aún existen registros asociados de Upscales, Reservaciones o Ingresos");
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

        public void deleteBankStatement2(long[] id)
        {
            foreach (long eval in id)
            {
                this.deleteBankStatement2(eval);
            }

        }

        public void deleteBankStatementItem(tblbankstatepurchase model)
        {
            unity.BankstatePurchaseRepository.Delete(model);
            unity.Commit();
        }

        public void deleteBankStatementItem(tblbankstatereserv model)
        {
            unity.BankstateReservRepository.Delete(model);
            unity.Commit();
        }

        public void deleteBankStatementItem(tblbankstateparentreserv model)
        {
            unity.BankstateParentReservRepository.Delete(model);
            unity.Commit();
        }

        public void deleteBankStatementItem(tblbankstat2purchase model)
        {
            unity.Statements2PURCHASESRepository.Delete(model);
            unity.Commit();
        }

        public void deleteBankStatementItem(tblbankstat2reserv model)
        {
            unity.Statements2RESERVRepository.Delete(model);
            unity.Commit();
        }

        public void deleteBankStatementItem(tblbankstat2parentreserv model)
        {
            unity.Statements2PARENTRESERVRepository.Delete(model);
            unity.Commit();
        }

        public Dictionary<string, string> generateReportReconciliationsExcel(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, int status, decimal ammountStart, decimal ammountEnd)
        {
            try
            {
                List<bankreconciliationModel> list = new List<bankreconciliationModel>();
                list = this.getBakReconcilitions(dateReportStart, dateReportEnd, Tpv, externalgroup, company, PaymentMethod, hotel, status, ammountStart, ammountEnd, false, false);

                GeneradorXLS xls = new GeneradorXLS("downloads", "bankReconciliation_" + DateTimeUtils.ParseDatetoStringFull(DateTime.Now) + ".xlsx");
                xls.GeneradorXLSbankReconciliation(list);

                Dictionary<string, string> fileProperties = new Dictionary<string, string>();
                fileProperties.Add("nameFile", string.Format("Consulta_Conciliaciones-{0}", DateTimeUtils.ParseDatetoStringFull(DateTime.Now).Replace("/", "-")));
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

        public Dictionary<string, string> generateReportBaReconciliationsExcel(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, int status, decimal ammountStart, decimal ammountEnd)
        {
            try
            {
                List<bankstatements> list = new List<bankstatements>();
                list = this.getBankStat2Reconcilitions(dateReportStart, dateReportEnd, Tpv, externalgroup, company, PaymentMethod, hotel, status, ammountStart, ammountEnd, false, false);

                GeneradorXLS xls = new GeneradorXLS("downloads", "bankAccountReconciliation_" + DateTimeUtils.ParseDatetoStringFull(DateTime.Now) + ".xlsx");
                xls.GeneradorXLSbankReconciliation(list);

                Dictionary<string, string> fileProperties = new Dictionary<string, string>();
                fileProperties.Add("nameFile", string.Format("Consulta_Conciliaciones-{0}", DateTimeUtils.ParseDatetoStringFull(DateTime.Now).Replace("/", "-")));
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

        public List<file> getFiles(HttpFileCollectionBase files, ref XLSFileImportErrors errorReport)
        {
            try
            {
                List<file> list = (List<file>)fileServices.parseFilesBasetoFile(files, ref errorReport);
                return list;
            }
            catch (Exception e)
            {
                Log.Error("No se pueden procesar la petición para obtener los archivos", e);
                throw new Exception("No se pueden procesar la petición para obtener los archivos", e);
            }
        }

        public List<bankreconciliationModel> addXLScotiaPos(List<file> files)
        {
            List<bankreconciliationModel> allRocords = new List<bankreconciliationModel>();
            List<bankreconciliationModel> rejected = new List<bankreconciliationModel>();

            try
            {
                // Getting Records
                foreach (file helper in files)
                {
                    var listProcess = this.parseXMLStreamtoBankStatementsScotiaBank(helper);

                    foreach (bankreconciliationModel model in listProcess)
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
            foreach (bankreconciliationModel helper in allRocords)
            {
                try
                {
                    var _result = this.saveBankStatement(helper);

                    if (_result != 0)
                    {
                        foreach (financialstateitemModel helperitem in helper.financialstateitemlist)
                        {
                            this.saveBankStatementItemXLS((int)_result, helperitem);
                        }
                    }
                }

                catch (Exception e)
                {
                    Log.Error("No se guardo el siguiente registro bankreconciliationModel: ------->" + helper.ToString() + " <--------------", e);
                    rejected.Add(helper);
                }
            }

            return rejected;
        }

        private List<bankreconciliationModel> parseXMLStreamtoBankStatementsScotiaBank(file file)
        {
            Dictionary<long, int> dtpvs = new Dictionary<long, int>();
            Dictionary<string, int> dcompanies = new Dictionary<string, int>();
            Dictionary<string, int> dcurrency = new Dictionary<string, int>();
            Dictionary<string, int> dbankaccount = new Dictionary<string, int>();
            Dictionary<int, tblpartners> dcompanyhotel = new Dictionary<int, tblpartners>();

            try
            {
                // Selecting TPVS
                dtpvs = (unity.TpvRepository.Get(p => p.tpvactive == Globals.activeRecord, null, "").Select(y => new { y.tpvidlocation, y.idtpv })).ToDictionary(c => Convert.ToInt64(c.tpvidlocation), t => t.idtpv);
                // Selecting Company
                dcompanies = (unity.CompaniesRepository.Get(p => p.companyactive == Globals.activeRecord, null, "").Select(y => new { y.companyshortname, y.idcompany })).ToDictionary(c => c.companyshortname, t => t.idcompany);
                // Selecting VTH Hotels by Company
                //dcompanyhotel = (unity.CompaniesHotelsRepository.Get(p => p.tblcompanies.companyActive == Constantes.activeRecord, null, "").Select(y => new { y.idCompany, y.tblhotels })).ToDictionary(c => c.idCompany, t => t.tblhotels);
                // Currency
                dcurrency = (unity.CurrencyRepository.Get(x => x.currencyActive == Globals.activeRecord, null, "").Select(y => new { y.currencyAlphabeticCode, y.idCurrency })).ToDictionary(c => c.currencyAlphabeticCode, t => t.idCurrency);
                // Bank account
                dbankaccount = (unity.BankAccountRepository.Get(p => p.baccountactive == Globals.activeRecord && p.idbankaccntclasification == 4, null, "").Select(y => new { y.baccountshortname, y.idbaccount })).ToDictionary(c => c.baccountshortname, t => t.idbaccount);
            }

            catch (Exception e)
            {
                Log.Error("No se pueden obtener los datos para transformar las conciliaciones", e);
                throw new Exception("No se pueden obtener los datos para transformar las conciliaciones");
            }

            List<bankreconciliationModel> list = new List<bankreconciliationModel>();

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
                        bankreconciliationModel _dinamicHelper = new bankreconciliationModel();
                        _dinamicHelper.xlscorrectlyformed = true;
                        _dinamicHelper.statusconciliation = (int)BankAccountReconciliationStatus.Completo;
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

                                                        _dinamicHelper.idCompany = dcompanies.Where(z => z.Key.Equals(_val)).Select(c => c.Value).FirstOrDefault();

                                                        _dinamicHelper.idhotelpartner = dcompanyhotel.Where(z => z.Key.Equals(_dinamicHelper.idCompany)).Select(v => v.Value.idPartner).FirstOrDefault();
                                                        _dinamicHelper.hotelpartnername = dcompanyhotel.Where(z => z.Key.Equals(_dinamicHelper.idCompany)).Select(v => v.Value.partnerName).FirstOrDefault();
                                                    }
                                                }
                                                else
                                                {
                                                    // Debug.WriteLine("Hotel : " + currentcellFor.InnerText);
                                                }
                                            }
                                            break;
                                        case 3:
                                            {
                                                // Debug.WriteLine("Moneda : " + currentcellFor.InnerText);
                                                if (cdt != null)
                                                {
                                                    if (cdt == CellValues.SharedString)
                                                    {
                                                        var _val = getValueString(currentcellFor, wbPart);
                                                        //Debug.WriteLine("Moneda : " + _val);
                                                        _dinamicHelper.currencyname = _val;
                                                        _dinamicHelper.currency = dcurrency.Where(z => z.Key.Equals(_val)).Select(c => c.Value).FirstOrDefault();
                                                    }
                                                }
                                                else
                                                {
                                                    // Debug.WriteLine("Moneda : " + currentcellFor.InnerText);
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
                                                        _dinamicHelper.idBAccount = dbankaccount.Where(z => z.Key.Equals(_val)).Select(c => c.Value).FirstOrDefault();
                                                        _dinamicHelper.baccount = _dinamicHelper.idBAccount;
                                                    }
                                                }
                                                else
                                                {
                                                    // Debug.WriteLine("Cuenta : " + currentcellFor.InnerText);
                                                }
                                            }
                                            break;
                                        case 5:
                                            {
                                                // Debug.WriteLine("Fecha : " + DatetimeUtils.ParseAOTNumberToDate(Convert.ToInt32(currentcellFor.InnerText)));
                                                _dinamicHelper.bankstatementAplicationDate = DateTimeUtils.ParseAOTNumberToDate(Convert.ToInt32(currentcellFor.InnerText));
                                            }
                                            break;
                                        case 6:
                                            {
                                                // Debug.WriteLine("V. Total : " + currentcellFor.InnerText);
                                                decimal number;
                                                Decimal.TryParse(currentcellFor.InnerText, out number);
                                                _dinamicHelper.bankstatementAppliedAmmount = Decimal.Round(number, 2);
                                            }
                                            break;
                                        case 7:
                                            break;
                                        case 8:
                                            break;
                                        case 9:
                                            {
                                                // Debug.WriteLine("Comisiones : " + currentcellFor.InnerText);
                                                decimal number;
                                                Decimal.TryParse(currentcellFor.InnerText, out number);
                                                _dinamicHelper.bankstatementBankFee = Decimal.Round(number, 2);
                                            }
                                            break;
                                        case 10:
                                            break;
                                        case 11:
                                            break;
                                        case 12:
                                            {
                                                // Debug.WriteLine("Depositos : " + currentcellFor.InnerText);
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
                                    _dinamicHelper.statusconciliation = (int)BankAccountReconciliationStatus.Error;
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

        #endregion

        #region Bank Reconciliation V2

        public List<string> AddFiletoDatabase(List<file> files)
        {
            List<string> rejected = new List<string>();
            List<bankreconciliationModel> bankreconciliations = new List<bankreconciliationModel>();
            //List<bankreconciliationdetailScotiaPos> bankreconciliationdetailScotiaPos = new List<bankreconciliationdetailScotiaPos>();
            List<financialstateitemstatementsModel> fin_items = new List<financialstateitemstatementsModel>();

            /************ Obteniendo los datos del Excel ******************************************************/
            /**************************************************************************************************/

            try
            {
                // Getting Records
                foreach (file helper in files)
                {
                    bankreconciliations = this.parseXMLStreamtoBankStatementsScotiaBank(helper);
                }
            }

            catch (Exception e) { throw new Exception("Error al procesar el archivo excel en hoja 1 - ScotiaPos", e); }

            /*try
             {
                 // Getting Records
                 foreach (file helper in files)
                 {
                     bankreconciliationdetailScotiaPos = this.parseXMLStreamtoBankStatementDetailScotiaBank(helper);
                 }
             }

             catch (Exception e) { throw new Exception("Error al procesar el archivo excel en hoja 2 - Detalles ScotiaPos", e); }
             */
            // Saving both resources

            rejected.Add("Movimientos por lotes.");

            for (int i = 0; i <= bankreconciliations.Count - 1; i++)
            {
                string _message = "";
                long bnkstat_id = saveBankStatement(bankreconciliations.ElementAt(i));

                if (bnkstat_id > 0) fin_items.Add(new financialstateitemstatementsModel
                {
                    idBankStatement = bnkstat_id,
                    fin_states = ((getBakReconciliationsDetailsbyId(bnkstat_id)).financialstateitemlist).ToList()
                });

                if (_message.Length != 0) rejected.Add(_message);
            }

            rejected.Add("Movimientos detalles de lotes.");

            /*for (int i = 0; i <= bankreconciliationdetailScotiaPos.Count - 1; i++)
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
            }*/

            return rejected;
        }

        /*public List<bankreconciliationdetailScotiaPos> getBankReconciliationsDetailsScotiaPos()
        {
            List<bankreconciliationdetailScotiaPos> lst = new List<bankreconciliationdetailScotiaPos>();


            return lst;

        }*/


        /*private List<bankreconciliationdetailScotiaPos> parseXMLStreamtoBankStatementDetailScotiaBank(file file)
        {
            Dictionary<long, int> dtpvs = new Dictionary<long, int>();
            Dictionary<string, int> dcurrency = new Dictionary<string, int>();
            List<bankreconciliationdetailScotiaPos> list = new List<bankreconciliationdetailScotiaPos>();

            try
            {
                // Selecting TPVS
                dtpvs = (unity.TpvRepository.Get(p => p.tpvActive == Constantes.activeRecord, null, "").Select(y => new { y.tpvIdLocation, y.idTPV })).ToDictionary(c => Convert.ToInt64(c.tpvIdLocation), t => t.idTPV);
                dcurrency = (unity.CurrencyRepository.Get(p => p.active == Constantes.activeRecord, null, "").Select(y => new { y.currencyAlphabeticCodeScotia, y.idCurrency })).ToDictionary(c => c.currencyAlphabeticCodeScotia, t => t.idCurrency);
            }
            catch (Exception e)
            {
                _Log.Error("No se pueden obtener los datos para transformar las conciliaciones - Conciliiaciones de Detalles ", e);
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
                                                _Log.Error("No se encuentra valor para la columna Moneda.");
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
                                            else
                                            {

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
                                                    _dinamicHelper.scotiastatementprocessingdate = (DateTime)DatetimeUtils.ParseStringToDate(_val);
                                                }
                                            }
                                            else
                                            {

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
                                            else
                                            {

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
                                            else
                                            {

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
                                            else
                                            {

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
                                            else
                                            {

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
                                            else
                                            {

                                            }

                                            // _dinamicHelper.scotiastatementprocessingdate = DatetimeUtils.ParseAOTNumberToDate(Convert.ToInt32(currentcellFor.InnerText));
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
                                            else
                                            {

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
                                            else
                                            {

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
                                            else
                                            {
                                                // Debug.WriteLine("Cuenta : " + currentcellFor.InnerText);
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
                                            else
                                            {

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
                                            else
                                            {

                                            }
                                        }
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                                _Log.Info(string.Format("No se puede procesar la linea {0} -  - del archivo {1} . --- >> ", cellCount, file.fileName), e);
                                _dinamicHelper.xlscorrectlyformed = false;
                                _dinamicHelper.statusconciliation = (int)BankAccountReconciliationStatus.Error;
                                _dinamicHelper.methodconciliation = (int)BankAccountReconciliationMethod.SinConciliar;
                                list.Add(_dinamicHelper);
                            }
                        }
                        list.Add(_dinamicHelper);
                        //}
                    }
                }
                return list.OrderBy(v => v.scotiastatementtransdate).ToList();
            }
            catch (Exception ex)
            {
                _Log.Error("No se puede procesar el archivo con los detalles de soctiaPos" + file.fileName, ex);
                throw new Exception("No se puede procesar el archivo con los detalles de soctiaPos" + file.fileName);
            }
        }*/

        /*private void savePaymentbyAuthCode(bankreconciliationdetailScotiaPos helper, List<financialstateitemstatements> finstats)
        {
            List<tblreservationpayments> rsv_pays = (unity.PaymentsRESERVRepository.Get(c => c.authCode == helper.scotiastatementauthorizationcode && c.idTerminal == helper.scotiastatementtpv)).ToList();
            List<Payment> ups_pays = (unity.PaymentsUPSCLRepository.Get(c => c.authCode == helper.scotiastatementauthorizationcode && ((IEnumerable<int>)c.PaymentInstrument.Select(v => v.idTerminal)).Contains((int)helper.scotiastatementtpv))).ToList();

            foreach (financialstateitemstatements f_stat in finstats)
            {
                foreach (financialstateitem f_item in f_stat.fin_states)
                {
                    List<tblreservationpayments> rsvpayms = rsv_pays.FindAll(r => r.idReservationPayment == f_item.ReferenceItem && f_item.SourceData == 5);
                    List<Payment> upspayms = ups_pays.FindAll(u => u.idPayment == f_item.ReferenceItem && f_item.SourceData == 4);

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

                    foreach (Payment u_pay in upspayms)
                    {
                        unity.StatementsUPSCLRepository.Insert(new tblbankstateupscl
                        {
                            idBankStatements = f_stat.idBankStatement,
                            idPayment = u_pay.idPayment,
                            authCode = u_pay.authCode
                        });

                        unity.Commit();
                    }
                }
            }
        }
        
        private static string getValueString(Cell cell, WorkbookPart wbPart)
        {
            int id = -1;
            string currentcellvalue = string.Empty;

            if (Int32.TryParse(cell.InnerText, out id))
            {
                SharedStringItem item = wbPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);

                if (item.Text != null)
                {
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
*/
        /* private docpaymentupscl searchDocPaymentUpscl(bankreconciliationdetailScotiaPos helper)
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
         }*/

        /*private docpaymentreserv searchDocPaymentReserv(bankreconciliationdetailScotiaPos helper)
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
        }*/

        /*private bool validtoAddPaymentData(bankreconciliationdetailScotiaPos helper)
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
        }*/

        #endregion

        #region Bank Reconciliations V3

        public void ReconcileFromFiles(List<file> inputFiles, ref XLSFileImportErrors errorReport)
        {
            foreach (file el_file in inputFiles)
            {
                XLSBookObj xlsobj = parseXlsFileToObj(el_file, inputFiles.FindIndex(f => f.fileName == el_file.fileName), ref errorReport);

                if (xlsobj == null) continue;

                SavedStatementsLists saved_conciliations = storeConciliationsToDB(xlsobj, ref errorReport);
                List<long> retry_us = reconcileByReferences(saved_conciliations, ref errorReport);
                List<long> retry_us2 = reconcileByReferences2(saved_conciliations, ref errorReport);
                if ((retry_us != null) && (retry_us.Count > 0)) reconcileByGuessing(retry_us, ref errorReport);
                if ((retry_us2 != null) && (retry_us2.Count > 0)) reconcileByGuessing2(retry_us2, ref errorReport);
            }

        }

        public SavedStatementsLists storeConciliationsToDB(XLSBookObj XLSInfo, ref XLSFileImportErrors errorReport)
        {
            BankStatementsSheets hoja;
            List<tblbankstatements> general_stamnts = new List<tblbankstatements>();
            List<tblbankstatementsLog> statement_Log = new List<tblbankstatementsLog>();
            List<tblbankstatements2> general_baccount = new List<tblbankstatements2>();
            List<tblbankstatements2Log> baccount_Log = new List<tblbankstatements2Log>();
            List<tblbankstatementsdet> detail_stamnts = new List<tblbankstatementsdet>();
            IEnumerable<tbltpv> tpvs = unity.TpvRepository.Get(t => t.tpvactive == true);
            IEnumerable<tblbankaccount> BAccount = unity.BankAccountRepository.Get(x => x.baccountactive == Globals.activeRecord);

            foreach (XLSSheetObj la_sheet in XLSInfo.book_sheets)
            {
                if (System.Enum.TryParse(la_sheet.sheet_name, true, out hoja))
                {
                    switch ((int)hoja)
                    {
                        case 1:
                            {
                                general_stamnts.AddRange(parseDepositsSheet(la_sheet, tpvs, ref errorReport));
                                general_baccount.AddRange(parseBAccountMovementSheet(la_sheet, BAccount, ref errorReport));
                            }
                            break;

                        case 2:
                            detail_stamnts.AddRange(parseDetailsSheet(la_sheet, tpvs, ref errorReport));
                            break;

                        default:
                            break;
                    }
                }
            }
            try
            {
                // se inserta en la tabla tblbankstatements
                general_stamnts = general_stamnts.OrderBy(k => k.idTPV).ThenBy(v => v.bankstatementAplicationDate).ToList();
                detail_stamnts = detail_stamnts.OrderBy(w => w.idTPV).ThenBy(q => q.bankStatementsDetSaleDate).ToList();
                unity.BankstatementsRepository.InsertMulti(general_stamnts);
                unity.Commit();
                // se inserta en la tabla tblbankstatementsdet
                unity.BankstatementsDetRepository.InsertMulti(detail_stamnts);
                unity.Commit();
                // se inserta en la tabla tblbankstatements2
                general_baccount = general_baccount.OrderBy(x => x.idBAccount).ThenBy(y => y.bankstatements2AplicationDate).ToList();
                unity.Bankstatements2Repository.InsertMulti(general_baccount);
                unity.Commit();
            }
            catch (Exception ex)
            {
                errorReport.AddCriticalStop("Error de escritura en base de datos.", ex);
                general_stamnts = null;
                detail_stamnts = null;
                general_baccount = null;
            }

            this.logsServices.addTblLog(general_stamnts, "Alta de Registro tbltblbankstatements Conciliaciones");
            this.logsServices.addTblLog(general_baccount, "Alta de Registro tbltblbankstatements2 Movimientos cuenta");

            return (new SavedStatementsLists()
            {
                gstatements = general_stamnts,
                dstatements = detail_stamnts,
                baccountstatements = general_baccount
            });
        }

        public XLSBookObj parseXlsFileToObj(file el_file, int fileIdx, ref XLSFileImportErrors errorReport)
        {
            int idx_sheet = -1;
            int idx_row = -1;
            int idx_cell = -1;

            XLSBookObj la_resp = new XLSBookObj()
            {
                book_filename = el_file.fileName
            };

            try
            {
                using (SpreadsheetDocument el_doc = SpreadsheetDocument.Open(el_file.stream, false))
                {
                    idx_sheet = -1;
                    Sheets las_sheets = el_doc.WorkbookPart.Workbook.Sheets;
                    List<XLSSheetObj> bsheets = new List<XLSSheetObj>();

                    foreach (Sheet la_sheet in las_sheets.ChildElements)
                    {
                        idx_sheet++;

                        try
                        {
                            BankStatementsSheets is_in_enum;

                            if (!(System.Enum.TryParse(la_sheet.Name, out is_in_enum)))
                            {
                                errorReport.AddXlsSheetSkipped("La hoja no tiene una etiqueta válida.", fileIdx, el_file.fileName, idx_sheet, la_sheet.Name);
                                continue;
                            }

                            SheetData las_rows = null;

                            for (int ii = 0; ii < (((WorksheetPart)el_doc.WorkbookPart.GetPartById(la_sheet.Id)).Worksheet).ChildElements.Count; ii++)
                            {
                                var sheetpart = (((WorksheetPart)el_doc.WorkbookPart.GetPartById(la_sheet.Id)).Worksheet).ChildElements.GetItem(ii);

                                if (sheetpart.GetType() == typeof(SheetData))
                                {
                                    las_rows = (SheetData)sheetpart;
                                    break;
                                }
                            }

                            if (las_rows == null)
                            {
                                errorReport.AddXlsSheetSkipped("No se encontraron datos válidos en la hoja.", fileIdx, el_file.fileName, idx_sheet, la_sheet.Name);
                                continue;
                            }

                            List<XLSRowObj> srows = new List<XLSRowObj>();

                            XLSSheetObj b_sheet = new XLSSheetObj
                            {
                                sheet_idx = idx_sheet,
                                sheet_id = la_sheet.Id,
                                sheet_name = la_sheet.Name
                            };

                            idx_row = -1;

                            foreach (Row la_row in las_rows.ChildElements)
                            {
                                idx_row++;

                                if (idx_row == 0) continue; // Para omitir la fila de los encabezados y evitar que la detecte como errónea cada vez.

                                try
                                {
                                    Cell la_cell1 = (Cell)la_row.ChildElements.ElementAt(0);
                                    string valuesString = ((la_cell1.DataType != null) && (la_cell1.DataType == CellValues.SharedString)) ? getValueString(la_cell1, el_doc.WorkbookPart).Trim() : ((la_cell1.CellValue != null) ? la_cell1.CellValue.Text : string.Empty);

                                    if (valuesString != "0" && is_in_enum.Equals(BankStatementsSheets.Resumen))
                                    {
                                        if (!(checkStatementRowBA(la_row, el_doc.WorkbookPart)))
                                        {
                                            errorReport.AddXlsRowSkipped("No se encontró el número de cuenta o no es válido.", fileIdx, el_file.fileName, idx_sheet, la_sheet.Name, idx_row);
                                            continue;
                                        }
                                    }

                                    else
                                    {
                                        if (!(checkStatementRow(la_row, el_doc.WorkbookPart, is_in_enum)))
                                        {
                                            errorReport.AddXlsRowSkipped("No se encontró el número de terminal o no es válido.", fileIdx, el_file.fileName, idx_sheet, la_sheet.Name, idx_row);
                                            continue;
                                        }
                                    }

                                    List<XLSCellObj> rcells = new List<XLSCellObj>();

                                    XLSRowObj s_row = new XLSRowObj
                                    {
                                        row_idx = idx_row,
                                        row_groupby = valuesString != "0" ? (int)BankTypeClasification.BankMovement : (int)BankTypeClasification.BankConciliation // 1 = Movimiento en cuentas bancarias, 2 = Conciliaciones
                                    };

                                    idx_cell = -1;

                                    foreach (Cell la_cell in la_row.ChildElements)
                                    {
                                        idx_cell++;

                                        try
                                        {
                                            if (idx_cell > 8) break;  //  valor original = 5

                                            Regex regx = new Regex(@"(\d+)");
                                            bool has_string = ((la_cell.DataType != null) && (la_cell.DataType == CellValues.SharedString));
                                            string[] cell_positions = regx.Split(la_cell.CellReference);

                                            if (la_cell.CellValue == null)
                                            {
                                                if (!((s_row.row_groupby == (int)BankTypeClasification.BankConciliation) && (idx_cell == 8)))
                                                {
                                                    errorReport.AddXlsCellSkipped("La celda no contiene datos válidos.", fileIdx, el_file.fileName, idx_sheet, la_sheet.Name, idx_row, idx_cell, null);
                                                }

                                                continue;
                                            }

                                            XLSCellObj r_cell = new XLSCellObj
                                            {
                                                cell_idx = idx_cell,
                                                cell_strval = has_string,
                                                cell_val = has_string ? getValueString(la_cell, el_doc.WorkbookPart) : (la_cell.CellValue.Text ?? string.Empty),
                                                cell_name = la_cell.CellReference,
                                                cell_col = cell_positions[0],
                                                cell_row = int.Parse(cell_positions[1])
                                            };

                                            rcells.Add(r_cell);
                                        }

                                        catch (Exception ex)
                                        {
                                            errorReport.AddXlsCellSkipped("Ocurrió un error al procesar el contenido de la celda.", fileIdx, el_file.fileName, idx_sheet, la_sheet.Name, idx_row, idx_cell, la_cell.CellValue.InnerText, ex);
                                            continue;
                                        }
                                    }

                                    s_row.row_cellcount = rcells.Count;
                                    s_row.row_cells = rcells;
                                    srows.Add(s_row);
                                }

                                catch (Exception ex)
                                {
                                    errorReport.AddXlsRowSkipped("Ocurrió un error al procesar el registro.", fileIdx, el_file.fileName, idx_sheet, la_sheet.Name, idx_row, ex);
                                    continue;
                                }
                            }

                            b_sheet.sheet_rowcount = srows.Count;
                            b_sheet.sheet_rows = srows;
                            bsheets.Add(b_sheet);
                        }

                        catch (Exception ex)
                        {
                            errorReport.AddXlsSheetSkipped("Ocurrió un error al procesar la hoja.", fileIdx, el_file.fileName, idx_sheet, la_sheet.Name, ex);
                            continue;
                        }
                    }

                    la_resp.book_sheetcount = bsheets.Count;
                    la_resp.book_sheets = bsheets;
                }
            }

            catch (Exception ex)
            {
                errorReport.AddFileUploadError("No se pudo leer el archivo o no es un archivo válido.", fileIdx, el_file.fileName, ex);
                la_resp = null;
            }

            return la_resp;
        }

        public List<tblbankstatements> parseDepositsSheet(XLSSheetObj la_sheet, IEnumerable<tbltpv> tpvs, ref XLSFileImportErrors errorReport)
        {
            List<tblbankstatements> la_resp = new List<tblbankstatements>();

            foreach (XLSRowObj la_row in la_sheet.sheet_rows)
            {
                try
                {
                    if (la_row.row_cellcount < 6)
                    {
                        errorReport.AddStatRecordFailure("El registro está incompleto o dañado.", la_sheet.sheet_name, la_row.row_idx, BankTypeClasification.Unknown, null);
                        continue;
                    }
                    if (la_row.row_groupby == (int)BankTypeClasification.BankConciliation)// el grupo de rows con id 2 son conciliaciones
                    {
                        tbltpv la_tpv = tpvs.Where(t => t.tpvidlocation == ((la_row.row_cells.ElementAt(1)).cell_val)).FirstOrDefault();

                        if (la_tpv == null)
                        {
                            errorReport.AddStatRecordFailure("No se encuentra la terminal especificada.", la_sheet.sheet_name, la_row.row_idx, BankTypeClasification.BankConciliation, null);
                            continue;
                        }

                        int la_cia = la_tpv.idphysicallyat ?? 0;
                        DateTime applied_date = DateTimeUtils.parseXLSDate(la_row.row_cells.ElementAt(2).cell_val);

                        if (la_cia <= 0)
                        {
                            errorReport.AddStatRecordFailure("No se pudo determinar la ubicación de la terminal especificada.", la_sheet.sheet_name, la_row.row_idx, BankTypeClasification.BankConciliation, null);
                            continue;
                        }
                        if (applied_date == DateTime.MinValue)
                        {
                            errorReport.AddStatRecordFailure("No se comprendió la fecha de la operación.", la_sheet.sheet_name, la_row.row_idx, BankTypeClasification.BankConciliation, null);
                            continue;
                        }
                        decimal bank_fee = (decimal.TryParse(la_row.row_cells.ElementAt(3).cell_val, out bank_fee)) ? Math.Abs(bank_fee) : 0m;
                        decimal applied_amnt = (decimal.TryParse(la_row.row_cells.ElementAt(5).cell_val, out applied_amnt)) ? applied_amnt : 0m;

                        la_resp.Add(new tblbankstatements()
                        {
                            idBAccount = la_tpv.tblbaccounttpv.Select(y => y.idbaccount).FirstOrDefault(),
                            idTPV = la_tpv.idtpv,
                            idCompany = la_cia,
                            idBankStatementMethod = 1,
                            bankstatementAplicationDate = applied_date,
                            bankstatementAppliedAmmount = applied_amnt,
                            bankstatementBankFee = (bank_fee * -1),
                            bankstatementAppliedAmmountFinal = (applied_amnt - bank_fee),
                            bankStatementsTC = StringUtils.padNumString(la_row.row_cells.ElementAt(6).cell_val, 4),
                            bankStatementsAuthCode = la_row.row_cells.ElementAt(7).cell_val
                        });
                    }
                }

                catch (Exception ex)
                {
                    errorReport.AddStatRecordFailure("Ocurrió un error al importar el registro.", la_sheet.sheet_name, la_row.row_idx, BankTypeClasification.Unknown, ex);
                    continue;
                }
            }

            return la_resp;
        }

        public List<tblbankstatements2> parseBAccountMovementSheet(XLSSheetObj la_sheet, IEnumerable<tblbankaccount> baccount, ref XLSFileImportErrors errorReport)
        {
            List<tblbankstatements2> la_resp = new List<tblbankstatements2>();
            foreach (XLSRowObj la_row in la_sheet.sheet_rows)
            {
                try
                {
                    if (la_row.row_cellcount < 6)
                    {
                        errorReport.AddStatRecordFailure("El registro está incompleto o dañado.", la_sheet.sheet_name, la_row.row_idx, BankTypeClasification.Unknown, null);
                        continue;
                    }
                    if (la_row.row_groupby == (int)BankTypeClasification.BankMovement) // el gropo de rows con id 1 son movimientos de cuenta
                    {
                        tblbankaccount la_baccount = baccount.Where(t => t.baccountname == ((la_row.row_cells.ElementAt(0)).cell_val.Trim())).FirstOrDefault();

                        if (la_baccount == null)
                        {
                            errorReport.AddStatRecordFailure("No se encuentra la cuenta bancaria especificada.", la_sheet.sheet_name, la_row.row_idx, BankTypeClasification.BankMovement, null);
                            continue;
                        }
                        int idUser = this.accountServices.AccountIdentity();
                        int la_cia = la_baccount.idbaccount;
                        DateTime applied_date = DateTimeUtils.parseXLSDate(la_row.row_cells.ElementAt(2).cell_val);

                        if (la_cia <= 0)
                        {
                            errorReport.AddStatRecordFailure("No se encuentra la cuenta bancaria especificada.", la_sheet.sheet_name, la_row.row_idx, BankTypeClasification.BankConciliation, null);
                            continue;
                        }

                        if (applied_date == DateTime.MinValue)
                        {
                            errorReport.AddStatRecordFailure("No se comprendió la fecha de la operación.", la_sheet.sheet_name, la_row.row_idx, BankTypeClasification.BankMovement, null);
                            continue;
                        }
                        decimal bank_fee = (decimal.TryParse(la_row.row_cells.ElementAt(3).cell_val, out bank_fee)) ? Math.Abs(bank_fee) : 0m;
                        decimal bank_charge = (decimal.TryParse(la_row.row_cells.ElementAt(4).cell_val, out bank_charge)) ? Math.Abs(bank_charge) : 0m;
                        decimal applied_amnt = (decimal.TryParse(la_row.row_cells.ElementAt(5).cell_val, out applied_amnt)) ? applied_amnt : 0m;
                        string concept = la_row.row_cells.ElementAt(8).cell_val;
                        int MovementType = getMovementType(concept, bank_charge, applied_amnt);
                        la_resp.Add(new tblbankstatements2()
                        {
                            idBAccount = la_baccount.idbaccount,
                            idMovementType = MovementType,
                            bankstatements2AplicationDate = applied_date,
                            bankstatements2Concept = concept,
                            bankstatements2Charge = bank_charge,
                            bankstatements2Pay = applied_amnt,
                            bankstatements2CreatedBy = idUser,
                            bankstatements2CreatedOn = DateTime.Now,
                            bankstatements2UpdatedBy = idUser,
                            bankstatements2UpdatedOn = DateTime.Now,
                            bankstatements2Reconciled = false,
                            idBankStatementMethod = (int)BankAccountReconciliationMethod.SinConciliar,
                            bankstatements2BankFee = bank_fee

                        });
                    }
                }

                catch (Exception ex)
                {
                    errorReport.AddStatRecordFailure("Ocurrió un error al importar el registro.", la_sheet.sheet_name, la_row.row_idx, BankTypeClasification.Unknown, ex);
                    continue;
                }
            }
            return la_resp;
        }

        public static int getMovementType(string text, decimal cargo, decimal abono)
        {
            int movement = 0;
            var items = new List<string>() { "spei", "spei recibido", "spei enviado", "transf", "transferencia", "transferencias", "depósito", "deposito", "depositos", "depo", "depos", "traspaso", "traspasos", "fondeo", "retiro", "pago", "comisiones", "comision", "domiciliación", "domiciliada", "rendimientos", "cheque", "cheque cobrado", "compra", "cuota", "cfe", "serv", "otros servicios", "cargo", "iva" };

            var count = items.Count - 1;
            var i = 0;

            foreach (var search in items)
            {
                MatchCollection matches = Regex.Matches(text.ToLower(), search);
                foreach (Match match in matches)
                {
                    var value = match.Value.ToLower();
                    if (value == "transf" && cargo != 0) { value = value + " saliente"; }
                    else if (value == "transf" && abono != 0) { value = value + " entrante"; }
                    else if (value == "serv" && abono != 0) { value = value + " entrante"; }
                    else if (value == "serv" && cargo != 0) { value = value + " saliente"; }
                    else if (value == "pago" && abono != 0) { value = value + " entrante"; }
                    else if (value == "spei" && abono != 0) { value = value + " recibido"; }
                    else if (value == "spei" && cargo != 0) { value = value + " enviado"; }
                    else if (value == "deposito" && cargo != 0) { value = "cargo"; }
                    else if (value == "depositos" && cargo != 0) { value = "cargo"; }
                    switch (value)
                    {
                        case "fondeo":
                            movement = (int)BankMovementType.Fondeo;
                            break;
                        case "iva":
                        case "cfe":
                        case "cargo":
                        case "spei enviado":
                        case "serv saliente":
                        case "transf saliente":
                        case "transferencia saliente":
                        case "transferencia salida":
                            movement = (int)BankMovementType.Transferencia_saliente;
                            break;
                        case "spei recibido":
                        case "transf":
                        case "pago entrante":
                        case "serv entrante":
                        case "transf entrante":
                        case "transferencia":
                        case "transferencias":
                        case "transferencia entrante":
                        case "transferencia entrada":
                            movement = (int)BankMovementType.Transferencia_entrante;
                            break;
                        case "depósito":
                        case "deposito":
                        case "depositos":
                        case "depo":
                        case "depos":
                            movement = (int)BankMovementType.Deposito;
                            break;
                        case "retiro":
                            movement = (int)BankMovementType.Retiro;
                            break;
                        case "pago":
                            movement = (int)BankMovementType.Pago;
                            break;
                        case "comision":
                        case "comisiones":
                            movement = (int)BankMovementType.Comisiones;
                            break;
                        case "domiciliada":
                        case "domiciliacion":
                        case "domiciliación":
                            movement = (int)BankMovementType.Domiciliación;
                            break;
                        case "rendimientos":
                            movement = (int)BankMovementType.Rendimientos;
                            break;
                        case "cheque":
                        case "cheque cobrado":
                            movement = (int)BankMovementType.Cheque_cobrado;
                            break;
                        case "compra":
                            movement = (int)BankMovementType.Compra;
                            break;
                        case "cuota":
                            movement = (int)BankMovementType.Cuota;
                            break;
                        case "traspaso":
                        case "traspasos":
                            movement = (int)BankMovementType.Traspasos;
                            break;
                        default:
                            movement = (int)BankMovementType.Otros;
                            break;
                    }
                }
                //if (i == count) movement = (int)BankMovementType.Otros;

                i++;

                if (movement == 0) continue;

                if (movement != 0) break;
            }

            if (movement == 0 && cargo != 0) { movement = (int)BankMovementType.Transferencia_saliente; }

            if (movement == 0 && abono != 0) { movement = (int)BankMovementType.Transferencia_entrante; }


            return movement;
        }

        public List<tblbankstatementsdet> parseDetailsSheet(XLSSheetObj la_sheet, IEnumerable<tbltpv> tpvs, ref XLSFileImportErrors errorReport)
        {
            List<tblbankstatementsdet> la_resp = new List<tblbankstatementsdet>();

            foreach (XLSRowObj la_row in la_sheet.sheet_rows)
            {
                try
                {
                    if (la_row.row_cellcount < 5)
                    {
                        errorReport.AddStatRecordFailure("El registro está incompleto o dañado.", la_sheet.sheet_name, la_row.row_idx, BankTypeClasification.Unknown, null);
                        continue;
                    }

                    tbltpv la_tpv = tpvs.Where(t => t.tpvidlocation == ((la_row.row_cells.ElementAt(0)).cell_val)).FirstOrDefault();
                    DateTime sale_date = DateTimeUtils.parseXLSDate(la_row.row_cells.ElementAt(1).cell_val);

                    if (la_tpv == null)
                    {
                        errorReport.AddStatRecordFailure("No se encuentra la terminal especificada.", la_sheet.sheet_name, la_row.row_idx, BankTypeClasification.BankConciliation, null);
                        continue;
                    }

                    if (sale_date == DateTime.MinValue)
                    {
                        errorReport.AddStatRecordFailure("No se comprendió la fecha de la operación.", la_sheet.sheet_name, la_row.row_idx, BankTypeClasification.BankMovement, null);
                        continue;
                    }

                    decimal sale_amnt = (decimal.TryParse(la_row.row_cells.ElementAt(2).cell_val, out sale_amnt)) ? sale_amnt : 0m;

                    la_resp.Add(new tblbankstatementsdet()
                    {
                        idBankStatements = null,
                        idTPV = la_tpv.idtpv,
                        bankStatementsDetSaleDate = sale_date,
                        bankStatementsDetSaleAmnt = sale_amnt,
                        bankStatementsDetTC = StringUtils.padNumString(la_row.row_cells.ElementAt(3).cell_val, 4),
                        bankStatementsDetAuthCode = la_row.row_cells.ElementAt(4).cell_val
                    });
                }

                catch (Exception ex)
                {
                    errorReport.AddStatRecordFailure("Ocurrió un error al importar el registro.", la_sheet.sheet_name, la_row.row_idx, BankTypeClasification.Unknown, ex);
                    continue;
                }
            }

            return la_resp;
        }

        public List<long> reconcileByReferences(SavedStatementsLists saved_statmnts, ref XLSFileImportErrors errorReport)
        {
            List<long> retry_conciliations = new List<long>();
            List<tblbankstatereserv> rsvs = new List<tblbankstatereserv>();
            List<tblbankstateparentreserv> rsvsparent = new List<tblbankstateparentreserv>();
            List<tblbankstatepurchase> purchase = new List<tblbankstatepurchase>();
            List<tblbankstateincome> incoms = new List<tblbankstateincome>();
            List<tblbankstatements> update_statmnts = new List<tblbankstatements>();

            List<tblbankstatements> general_statmnts = saved_statmnts.gstatements.OrderBy(h => h.idTPV).ThenBy(g => g.bankstatementAplicationDate).ToList();
            List<int> exclude_rsvs = unity.BankstateReservRepository.Get().Select(a => a.idReservationPayment).ToList();
            List<int> exclude_parentrsv = unity.BankstateParentReservRepository.Get().Select(r => r.idReservationParentPayment).ToList();
            List<int> exclude_purchase = unity.BankstatePurchaseRepository.Get().Select(o => o.idPaymentPurchase).ToList();
            List<long> exclude_incom = unity.BankstateIncomeRepository.Get().Select(e => e.idincomeMovement).ToList();

            foreach (tblbankstatements bstatement in general_statmnts)
            {
                decimal maxdiff;
                decimal.TryParse((unity.ParametersVTARepository.Get(p => p.idParameter == 2).FirstOrDefault().parameterValue), out maxdiff);
                decimal bsgather_amnt = 0m;
                decimal bsremain_amnt = bstatement.bankstatementAppliedAmmount;
                List<DateTime> pay_days = ((((general_statmnts.Where(f => f.idTPV == bstatement.idTPV)).OrderBy(z => z.bankstatementAplicationDate)).Select(g => g.bankstatementAplicationDate)).Distinct()).ToList();
                int currDate_idx = pay_days.FindIndex(d => d.Date == bstatement.bankstatementAplicationDate);
                DateTime minDate = (currDate_idx <= 0) ? bstatement.bankstatementAplicationDate.AddDays(-15) : pay_days.ElementAt(currDate_idx - 1);
                DateTime minminDate = minDate.AddDays(-1);

                List<tblreservationspayment> rsv_pays = unity.ReservationsPaymentsRepository.Get(r => (r.terminal == bstatement.idTPV) && (r.reservationPaymentDate < bstatement.bankstatementAplicationDate && r.reservationPaymentDate >= minminDate) && (!exclude_rsvs.Contains(r.idReservationPayment))).ToList();
                List<tblreservationsparentpayment> parentrsv_pays = unity.ReservationsParentPaymentsRepository.Get(r => (r.terminal == bstatement.idTPV) && (r.reservationPaymentDate < bstatement.bankstatementAplicationDate && r.reservationPaymentDate >= minminDate) && (!exclude_parentrsv.Contains(r.idReservationParentPayment))).ToList();
                List<tblpaymentspurchases> purch_pays = unity.PaymentsPurchasesRepository.Get(u => u.terminal == bstatement.idTPV && (u.paymentDate < bstatement.bankstatementAplicationDate && u.paymentDate >= minminDate) && (!exclude_purchase.Contains(u.idPaymentPurchase))).ToList();
                List<tblincomemovement> inc_pays = unity.IncomeMovementsRepository.Get(i => (i.idtpv == bstatement.idTPV) && (i.incomemovapplicationdate < bstatement.bankstatementAplicationDate && i.incomemovapplicationdate >= minDate) && (!exclude_incom.Contains(i.idincomeMovement))).ToList();

                try
                {
                    int cardnum;
                    int deptype = ((bstatement.bankStatementsAuthCode == "0" || string.IsNullOrEmpty(bstatement.bankStatementsAuthCode.Trim())) && (bstatement.bankStatementsTC == "0" || string.IsNullOrEmpty(bstatement.bankStatementsTC.Trim()))) ? 2 : (int)bstatement.tbltpv.iddeposittype;
                    bool reconciled = false;
                    bool affected = false;
                    List<StatementDetailSearch> detSearch = new List<StatementDetailSearch>();

                    if (deptype == 2)
                    {
                        bool updated = false;
                        List<tblbankstatementsdet> my_moves = saved_statmnts.dstatements.Where(m => m.idTPV == bstatement.idTPV && ((m.bankStatementsDetSaleDate < bstatement.bankstatementAplicationDate) && (m.bankStatementsDetSaleDate >= minDate))).ToList();

                        foreach (tblbankstatementsdet a_move in my_moves)
                        {
                            cardnum = 0;
                            a_move.idBankStatements = bstatement.idBankStatements;
                            unity.BankstatementsDetRepository.Update(a_move);
                            int.TryParse(a_move.bankStatementsDetTC, out cardnum);

                            detSearch.Add(new StatementDetailSearch()
                            {
                                statementID = bstatement.idBankStatements,
                                cardNum = cardnum,
                                authCode = a_move.bankStatementsDetAuthCode.Trim(),
                                amountMax = a_move.bankStatementsDetSaleAmnt ?? 0
                            });

                            updated = true;
                        }

                        if (updated) unity.Commit();
                    }

                    else
                    {
                        int.TryParse(bstatement.bankStatementsTC, out cardnum);

                        detSearch.Add(new StatementDetailSearch()
                        {
                            statementID = bstatement.idBankStatements,
                            cardNum = cardnum,
                            authCode = bstatement.bankStatementsAuthCode.Trim(),
                            amountMax = bstatement.bankstatementAppliedAmmount
                        });
                    }

                    foreach (StatementDetailSearch detItem in detSearch)
                    {
                        decimal gathered_amnt = 0m;
                        decimal remain_amnt = detItem.amountMax;
                        List<tblreservationspayment> rpays = rsv_pays.Where(r => (((!string.IsNullOrEmpty(r.authCode)) && ((r.authCode == detItem.authCode) || (r.authCode.Contains(detItem.authCode)) || (detItem.authCode.Contains(r.authCode)))) || r.cardDigits == detItem.cardNum) && (((decimal)Math.Abs(detItem.amountMax - (decimal)r.reservationPaymentCost)) < maxdiff) && (!exclude_rsvs.Contains(r.idReservationPayment))).ToList();
                        List<tblreservationsparentpayment> parent_rpays = parentrsv_pays.Where(r => (((!string.IsNullOrEmpty(r.authCode)) && ((r.authCode == detItem.authCode) || (r.authCode.Contains(detItem.authCode)) || (detItem.authCode.Contains(r.authCode)))) || r.cardDigits == detItem.cardNum) && (((decimal)Math.Abs(detItem.amountMax - (decimal)r.reservationPaymentCost)) < maxdiff) && (!exclude_rsvs.Contains(r.idReservationParentPayment))).ToList();
                        List<tblpaymentspurchases> upays = purch_pays.Where(u => (u.cardDigits == StringUtils.forceLongParse(detItem.cardNum) || ((!string.IsNullOrEmpty(u.authCode)) && ((u.authCode == detItem.authCode) || (u.authCode.Contains(detItem.authCode)) || (detItem.authCode.Contains(u.authCode))))) && (((decimal)(Math.Abs(detItem.amountMax - (decimal)u.paymentCost))) < maxdiff) && (!exclude_purchase.Contains(u.idPaymentPurchase))).ToList();
                        List<tblincomemovement> ipays = inc_pays.Where(i => (((!string.IsNullOrEmpty(i.incomemovauthcode)) && ((i.incomemovauthcode == detItem.authCode) || (i.incomemovauthcode.Contains(detItem.authCode)) || (detItem.authCode.Contains(i.incomemovauthcode)))) || (StringUtils.padNumString(i.incomemovcard, 4) == StringUtils.padNumString(detItem.cardNum.ToString(), 4))) && (((decimal)Math.Abs(detItem.amountMax - (decimal)i.incomemovchargedamount)) < maxdiff) && (!exclude_incom.Contains(i.idincomeMovement))).ToList();
                        List<PaymentReconciliation> testPayments = new List<PaymentReconciliation>();

                        foreach (tblreservationspayment payr in rpays)
                        {
                            testPayments.Add(new PaymentReconciliation()
                            {
                                cardNum = detItem.cardNum,
                                sourceData = 7,
                                typeRsv = 1,
                                statementID = bstatement.idBankStatements,
                                paymentID = payr.idReservationPayment,
                                authCode = ((detItem.authCode != "0") && (!(string.IsNullOrEmpty(detItem.authCode.Trim())))) ? detItem.authCode : null,
                                payAmount = payr.reservationPaymentCost ?? 0m,
                                payDay = (DateTime)payr.reservationPaymentDate
                            });
                        }

                        foreach (tblreservationsparentpayment parentpayr in parent_rpays)
                        {
                            testPayments.Add(new PaymentReconciliation()
                            {
                                cardNum = detItem.cardNum,
                                sourceData = 7,
                                typeRsv = 2,
                                statementID = bstatement.idBankStatements,
                                paymentID = parentpayr.idReservationParentPayment,
                                authCode = ((detItem.authCode != "0") && (!(string.IsNullOrEmpty(detItem.authCode.Trim())))) ? detItem.authCode : null,
                                payAmount = parentpayr.reservationPaymentCost ?? 0m,
                                payDay = (DateTime)parentpayr.reservationPaymentDate
                            });
                        }

                        foreach (tblpaymentspurchases payu in upays)
                        {
                            testPayments.Add(new PaymentReconciliation()
                            {
                                cardNum = detItem.cardNum,
                                sourceData = 8,
                                statementID = bstatement.idBankStatements,
                                paymentID = payu.idPaymentPurchase,
                                authCode = ((detItem.authCode != "0") && (!(string.IsNullOrEmpty(detItem.authCode.Trim())))) ? detItem.authCode : null,
                                payAmount = (decimal)payu.paymentCost,
                                payDay = (DateTime)payu.paymentDate
                            });
                        }

                        foreach (tblincomemovement payi in ipays)
                        {
                            testPayments.Add(new PaymentReconciliation()
                            {
                                cardNum = detItem.cardNum,
                                sourceData = 2,
                                statementID = bstatement.idBankStatements,
                                paymentID = payi.idincomeMovement,
                                authCode = ((detItem.authCode != "0") && (!(string.IsNullOrEmpty(detItem.authCode.Trim())))) ? detItem.authCode : null,
                                payAmount = payi.incomemovchargedamount,
                                payDay = payi.incomemovapplicationdate
                            });
                        }

                        PaymentReconciliation firstTest = testPayments.Where(e => ((decimal)(Math.Abs(detItem.amountMax - e.payAmount)) < maxdiff)).FirstOrDefault();

                        if (firstTest != null)
                        {
                            switch (firstTest.sourceData)
                            {
                                case 2:
                                    incoms.Add(new tblbankstateincome()
                                    {
                                        idBankStatements = firstTest.statementID,
                                        idincomeMovement = firstTest.paymentID,
                                        authCode = firstTest.authCode,
                                        cardNumber = firstTest.cardNum.ToString("0000")
                                    });

                                    exclude_incom.Add(firstTest.paymentID);
                                    break;

                                case 7:
                                    {
                                        if (firstTest.typeRsv == 1)
                                        {
                                            rsvs.Add(new tblbankstatereserv()
                                            {
                                                idBankStatements = firstTest.statementID,
                                                idReservationPayment = (int)firstTest.paymentID,
                                                authCode = firstTest.authCode,
                                                cardNumber = firstTest.cardNum.ToString("0000")
                                            });
                                            exclude_rsvs.Add((int)firstTest.paymentID);
                                        }
                                        else
                                        {
                                            rsvsparent.Add(new tblbankstateparentreserv()
                                            {
                                                idBankStatements = firstTest.statementID,
                                                idReservationParentPayment = (int)firstTest.paymentID,
                                                authCode = firstTest.authCode,
                                                cardNumber = firstTest.cardNum.ToString("0000")
                                            });
                                            exclude_parentrsv.Add((int)firstTest.paymentID);
                                        }

                                    }
                                    break;
                                case 8:
                                    purchase.Add(new tblbankstatepurchase()
                                    {
                                        idBankStatements = firstTest.statementID,
                                        idPaymentPurchase = Convert.ToInt32(firstTest.paymentID),
                                        authCode = firstTest.authCode,
                                        cardNumber = firstTest.cardNum.ToString("0000")
                                    });

                                    exclude_purchase.Add(Convert.ToInt32(firstTest.paymentID));
                                    break;

                                default:
                                    break;
                            }

                            bsgather_amnt += firstTest.payAmount;
                            bsremain_amnt -= firstTest.payAmount;
                            affected = true;
                            continue;
                        }

                        testPayments = testPayments.OrderByDescending(t => t.payAmount).ThenByDescending(m => m.payDay).ToList();

                        foreach (PaymentReconciliation testpay in testPayments)
                        {
                            if (remain_amnt < maxdiff) break;

                            if (testpay.payAmount > (remain_amnt + maxdiff)) continue;

                            switch (testpay.sourceData)
                            {
                                case 2:
                                    incoms.Add(new tblbankstateincome()
                                    {
                                        idBankStatements = testpay.statementID,
                                        idincomeMovement = testpay.paymentID,
                                        authCode = testpay.authCode,
                                        cardNumber = testpay.cardNum.ToString("0000")
                                    });

                                    exclude_incom.Add(testpay.paymentID);
                                    break;

                                case 7:
                                    {
                                        if (testpay.typeRsv == 1)
                                        {
                                            rsvs.Add(new tblbankstatereserv()
                                            {
                                                idBankStatements = testpay.statementID,
                                                idReservationPayment = (int)testpay.paymentID,
                                                authCode = testpay.authCode,
                                                cardNumber = testpay.cardNum.ToString("0000")
                                            });
                                            exclude_rsvs.Add((int)testpay.paymentID);
                                        }
                                        else
                                        {
                                            rsvsparent.Add(new tblbankstateparentreserv()
                                            {
                                                idBankStatements = testpay.statementID,
                                                idReservationParentPayment = (int)testpay.paymentID,
                                                authCode = testpay.authCode,
                                                cardNumber = testpay.cardNum.ToString("0000")
                                            });
                                            exclude_parentrsv.Add((int)testpay.paymentID);
                                        }

                                    }
                                    break;

                                case 8:
                                    purchase.Add(new tblbankstatepurchase()
                                    {
                                        idBankStatements = testpay.statementID,
                                        idPaymentPurchase = Convert.ToInt32(firstTest.paymentID),
                                        authCode = testpay.authCode,
                                        cardNumber = testpay.cardNum.ToString("0000")
                                    });

                                    exclude_purchase.Add((int)firstTest.paymentID);
                                    break;

                                default:
                                    break;
                            }

                            gathered_amnt += testpay.payAmount;
                            remain_amnt -= testpay.payAmount;
                            affected = true;
                        }

                        bsgather_amnt += gathered_amnt;
                        bsremain_amnt -= gathered_amnt;
                    }

                    reconciled = (((bstatement.bankstatementAppliedAmmount - bsgather_amnt) < 1m) && (bsremain_amnt < 1m));

                    if (affected)
                    {
                        tblbankstatements el_statement = unity.BankstatementsRepository.Get(e => e.idBankStatements == bstatement.idBankStatements).FirstOrDefault();
                        el_statement.idBankStatementMethod = 3;
                        update_statmnts.Add(el_statement);
                    }

                    if (!reconciled)
                    {
                        retry_conciliations.Add(bstatement.idBankStatements);
                    }
                }

                catch (Exception ex)
                {
                    errorReport.AddStatConciliationError("Error al reconciliar el registro.", bstatement, ex);
                    continue;
                }
            }

            try
            {
                unity.BankstateReservRepository.InsertMulti(rsvs);
                unity.BankstateParentReservRepository.InsertMulti(rsvsparent);
                unity.BankstatePurchaseRepository.InsertMulti(purchase);
                unity.BankstateIncomeRepository.InsertMulti(incoms);

                foreach (tblbankstatements bstat in update_statmnts)
                {
                    unity.BankstatementsRepository.Update(bstat);
                }

                unity.Commit();
            }

            catch (Exception ex)
            {
                errorReport.AddCriticalStop("Error de escritura en base de datos.", ex);
                retry_conciliations = new List<long>();
            }

            return retry_conciliations;
        }

        public void reconcileByGuessing(List<long> retry_us, ref XLSFileImportErrors errorReport)
        {
            if ((retry_us == null) || (retry_us.Count < 1)) return;

            retry_us = retry_us.Distinct().ToList();

            foreach (long bs_id in retry_us)
            {
                tblbankstatements bs_record = unity.BankstatementsRepository.Get(t => t.idBankStatements == bs_id).FirstOrDefault();

                if (bs_record == null) continue;

                try
                {
                    bool affected = false;
                    List<tblbankstatereserv> rsvs = new List<tblbankstatereserv>();
                    List<tblbankstateparentreserv> rsvsparent = new List<tblbankstateparentreserv>();
                    List<tblbankstatepurchase> purchase = new List<tblbankstatepurchase>();
                    List<tblbankstateincome> incoms = new List<tblbankstateincome>();

                    bankreconciliationModel bs_recon = new bankreconciliationModel();
                    bs_recon.GenerateConciliation(bs_id, false);

                    if ((bs_recon.financialstateitemlist == null) || (bs_recon.financialstateitemlist.Count() == 0) || ((bs_recon.docpaymentreservitems.Count() + bs_recon.docpaymentparentreservitems.Count() + bs_recon.docpaymentpurchaseitems.Count() + bs_recon.docpaymentincomitems.Count()) >= bs_recon.financialstateitemlist.Count())) continue;

                    List<int> ups_ids = bs_recon.docpaymentpurchaseitems.Select(u => u.Purchase).Distinct().ToList();
                    List<long> rsv_ids = bs_recon.docpaymentreservitems.Select(r => r.ReservationPayment).Distinct().ToList();
                    List<long> rsvparent_ids = bs_recon.docpaymentparentreservitems.Select(r => r.ReservationPayment).Distinct().ToList();
                    List<long> inc_ids = bs_recon.docpaymentincomitems.Select(i => i.IncomeMovement).Distinct().ToList();
                    List<financialstateitemModel> finitems = bs_recon.financialstateitemlist.Where(f => (((f.SourceData == 4) && (!(ups_ids.Contains(Convert.ToInt32(f.ReferenceItem))))) || ((f.SourceData == 5) && (!(rsv_ids.Contains(f.ReferenceItem)))) || ((f.SourceData == 8) && (!(inc_ids.Contains(f.ReferenceItem)))))).ToList();

                    foreach (financialstateitemModel fin_item in finitems)
                    {
                        try
                        {
                            switch (fin_item.SourceData)
                            {
                                case 2:
                                    incoms.Add(new tblbankstateincome()
                                    {
                                        idBankStatements = bs_id,
                                        idincomeMovement = fin_item.ReferenceItem,
                                        authCode = null,
                                        cardNumber = null
                                    });

                                    break;

                                case 7:
                                    {
                                        if (fin_item.typeRsv == 1)
                                        {
                                            rsvs.Add(new tblbankstatereserv()
                                            {
                                                idBankStatements = bs_id,
                                                idReservationPayment = (int)fin_item.ReferenceItem,
                                                authCode = null,
                                                cardNumber = null
                                            });
                                        }
                                        else
                                        {
                                            rsvsparent.Add(new tblbankstateparentreserv()
                                            {
                                                idBankStatements = bs_id,
                                                idReservationParentPayment = (int)fin_item.ReferenceItem,
                                                authCode = null,
                                                cardNumber = null
                                            });
                                        }
                                    }
                                    break;

                                case 8:
                                    purchase.Add(new tblbankstatepurchase()
                                    {
                                        idBankStatements = bs_id,
                                        idPaymentPurchase = Convert.ToInt32(fin_item.ReferenceItem),
                                        authCode = null,
                                        cardNumber = null
                                    });

                                    break;

                                default:
                                    break;
                            }
                        }

                        catch
                        {
                            continue;
                        }
                    }

                    if ((rsvs != null) && (rsvs.Count > 0))
                    {
                        unity.BankstateReservRepository.InsertMulti(rsvs);
                        affected = true;
                    }

                    if ((rsvsparent != null) && (rsvsparent.Count > 0))
                    {
                        unity.BankstateParentReservRepository.InsertMulti(rsvsparent);
                        affected = true;
                    }

                    if ((purchase != null) && (purchase.Count > 0))
                    {
                        unity.BankstatePurchaseRepository.InsertMulti(purchase);
                        affected = true;
                    }

                    if ((incoms != null) && (incoms.Count > 0))
                    {
                        unity.BankstateIncomeRepository.InsertMulti(incoms);
                        affected = true;
                    }

                    if (affected)
                    {
                        bs_record.idBankStatementMethod = 3;
                        unity.BankstatementsRepository.Update(bs_record);
                        unity.Commit();
                    }
                }

                catch (Exception ex)
                {
                    errorReport.AddStatConciliationError("Error al reconciliar automáticamente el registro.", bs_record, ex);
                    continue;
                }
            }
        }

        public List<long> reconcileByReferences2(SavedStatementsLists saved_statmnts, ref XLSFileImportErrors errorReport)
        {
            List<long> retry_conciliations = new List<long>();
            List<tblbankstat2reserv> rsvs = new List<tblbankstat2reserv>();
            List<tblbankstat2parentreserv> rsvsparent = new List<tblbankstat2parentreserv>();
            List<tblbankstat2purchase> purchase = new List<tblbankstat2purchase>();
            List<tblbankstat2income> incoms = new List<tblbankstat2income>();
            List<tblbankstat2invoice> payment = new List<tblbankstat2invoice>();
            List<tblbankstat2fondo> fondo = new List<tblbankstat2fondo>();
            List<tblbankstatements2> update_statmnts = new List<tblbankstatements2>();

            List<tblbankstatements2> general_statmnts2 = saved_statmnts.baccountstatements.OrderBy(h => h.idBAccount).ThenBy(g => g.bankstatements2AplicationDate).ToList();
            List<int> exclude_rsvs = unity.Statements2RESERVRepository.Get().Select(a => a.idReservationPayment).ToList();
            List<int> exclude_parentrsv = unity.Statements2PARENTRESERVRepository.Get().Select(r => r.idReservationParentPayment).ToList();
            List<int> exclude_purchase = unity.Statements2PURCHASESRepository.Get().Select(o => o.idPaymentPurchase).ToList();
            List<long> exclude_incom = unity.Statements2INCOMERepository.Get().Select(e => e.idincomeMovement).ToList();
            List<long> exclude_payment = unity.Statements2INVOICERepository.Get().Select(p => p.idpayment).ToList();
            List<int> exclude_fondo = unity.Statements2FONDORepository.Get().Select(f => f.idFondos).ToList();

            var MovementType = unity.MovementTypeRepository.Get().ToList();
            foreach (tblbankstatements2 bstatement in general_statmnts2)
            {
                decimal maxdiff;
                decimal.TryParse((unity.ParametersVTARepository.Get(p => p.idParameter == 2).FirstOrDefault().parameterValue), out maxdiff);
                decimal bsgather_amnt = 0m;
                decimal bsremain_amnt = bstatement.bankstatements2Charge == 0m ? (decimal)bstatement.bankstatements2Pay : (decimal)bstatement.bankstatements2Charge;

                List<DateTime> pay_days = ((((general_statmnts2.Where(f => f.idBAccount == bstatement.idBAccount)).OrderBy(z => z.bankstatements2AplicationDate)).Select(g => g.bankstatements2AplicationDate)).Distinct()).ToList();
                int currDate_idx = pay_days.FindIndex(d => d.Date == bstatement.bankstatements2AplicationDate);
                DateTime minDate = (currDate_idx <= 0) ? bstatement.bankstatements2AplicationDate.AddDays(-15) : pay_days.ElementAt(currDate_idx - 1);
                DateTime minminDate = minDate.AddDays(-1);


                var operationType = MovementType.Where(x => x.idMovementType == bstatement.idMovementType).ToList().Select(y => y.idOperationType).First();

                List<tblreservationspayment> rsv_pays = new List<tblreservationspayment>();
                List<tblreservationsparentpayment> parentrsv_pays = new List<tblreservationsparentpayment>();
                List<tblpaymentspurchases> purch_pays = new List<tblpaymentspurchases>();
                List<tblincomemovement> inc_pays = new List<tblincomemovement>();
                List<tblfondos> fondo_pays = new List<tblfondos>();
                List<tblpayment> payment_pays = new List<tblpayment>();
                if (operationType == (int)BankOperationType.Entradas)
                {
                    rsv_pays = unity.ReservationsPaymentsRepository.Get(r => (Constants.methodPayment.Contains((int)r.idPaymentType)) && (r.reservationPaymentDate < bstatement.bankstatements2AplicationDate && r.reservationPaymentDate >= minminDate) && (!exclude_rsvs.Contains(r.idReservationPayment))).ToList();
                    parentrsv_pays = unity.ReservationsParentPaymentsRepository.Get(r => (Constants.methodPayment.Contains((int)r.idPaymentType)) && (r.reservationPaymentDate < bstatement.bankstatements2AplicationDate && r.reservationPaymentDate >= minminDate) && (!exclude_parentrsv.Contains(r.idReservationParentPayment))).ToList();
                    purch_pays = unity.PaymentsPurchasesRepository.Get(u => (Constants.methodPayment.Contains((int)u.idPaymentType)) && (u.paymentDate < bstatement.bankstatements2AplicationDate && u.paymentDate >= minminDate) && (!exclude_purchase.Contains(u.idPaymentPurchase))).ToList();
                    inc_pays = unity.IncomeMovementsRepository.Get(i => (Constants.methodPaymentVta.Contains(i.idbankaccnttype)) && (i.incomemovapplicationdate < bstatement.bankstatements2AplicationDate && i.incomemovapplicationdate >= minDate) && (!exclude_incom.Contains(i.idincomeMovement))).ToList();
                }
                if (operationType == (int)BankOperationType.Ambos_Sentidos)
                {
                    fondo_pays = unity.FondosRepository.Get(f => (f.fondofechaEntrega <= bstatement.bankstatements2AplicationDate) && (f.fondofechaEntrega >= minminDate) && (!exclude_fondo.Contains(f.idFondos))).ToList();
                }
                if (operationType == (int)BankOperationType.Salidas)
                {
                    payment_pays = unity.PaymentsVtaRepository.Get(p => (p.paymentdate <= bstatement.bankstatements2AplicationDate) && (p.paymentdate >= minminDate) && (!exclude_payment.Contains(p.idpayment))).ToList();
                }
                try
                {
                    bool reconciled = false;
                    bool affected = false;
                    List<StatementDetailSearch> detSearch = new List<StatementDetailSearch>();

                    detSearch.Add(new StatementDetailSearch()
                    {
                        statementID = bstatement.idBankStatements2,
                        amountMax = bstatement.bankstatements2Charge == 0m ? (decimal)bstatement.bankstatements2Pay : (decimal)bstatement.bankstatements2Charge
                    });


                    foreach (StatementDetailSearch detItem in detSearch)
                    {
                        decimal gathered_amnt = 0m;
                        decimal remain_amnt = detItem.amountMax;
                        List<tblreservationspayment> rpays = rsv_pays.Where(r => (((!string.IsNullOrEmpty(r.authCode)) && ((r.authCode == detItem.authCode) || (r.authCode.Contains(detItem.authCode)) || (detItem.authCode.Contains(r.authCode)))) || r.cardDigits == detItem.cardNum) && (((decimal)Math.Abs(detItem.amountMax - (decimal)r.reservationPaymentCost)) < maxdiff) && (!exclude_rsvs.Contains(r.idReservationPayment))).ToList();
                        List<tblreservationsparentpayment> parent_rpays = parentrsv_pays.Where(r => (((!string.IsNullOrEmpty(r.authCode)) && ((r.authCode == detItem.authCode) || (r.authCode.Contains(detItem.authCode)) || (detItem.authCode.Contains(r.authCode)))) || r.cardDigits == detItem.cardNum) && (((decimal)Math.Abs(detItem.amountMax - (decimal)r.reservationPaymentCost)) < maxdiff) && (!exclude_rsvs.Contains(r.idReservationParentPayment))).ToList();
                        List<tblpaymentspurchases> upays = purch_pays.Where(u => (u.cardDigits == StringUtils.forceLongParse(detItem.cardNum) || ((!string.IsNullOrEmpty(u.authCode)) && ((u.authCode == detItem.authCode) || (u.authCode.Contains(detItem.authCode)) || (detItem.authCode.Contains(u.authCode))))) && (((decimal)(Math.Abs(detItem.amountMax - (decimal)u.paymentCost))) < maxdiff) && (!exclude_purchase.Contains(u.idPaymentPurchase))).ToList();
                        List<tblincomemovement> ipays = inc_pays.Where(i => (((!string.IsNullOrEmpty(i.incomemovauthcode)) && ((i.incomemovauthcode == detItem.authCode) || (i.incomemovauthcode.Contains(detItem.authCode)) || (detItem.authCode.Contains(i.incomemovauthcode)))) || (StringUtils.padNumString(i.incomemovcard, 4) == StringUtils.padNumString(detItem.cardNum.ToString(), 4))) && (((decimal)Math.Abs(detItem.amountMax - (decimal)i.incomemovchargedamount)) < maxdiff) && (!exclude_incom.Contains(i.idincomeMovement))).ToList();
                        List<PaymentReconciliation> testPayments = new List<PaymentReconciliation>();

                        foreach (tblreservationspayment payr in rpays)
                        {
                            testPayments.Add(new PaymentReconciliation()
                            {
                                cardNum = detItem.cardNum,
                                sourceData = 7,
                                typeRsv = 1,
                                statementID = bstatement.idBankStatements2,
                                paymentID = payr.idReservationPayment,
                                authCode = ((detItem.authCode != "0") && (!(string.IsNullOrEmpty(detItem.authCode.Trim())))) ? detItem.authCode : null,
                                payAmount = payr.reservationPaymentCost ?? 0m,
                                payDay = (DateTime)payr.reservationPaymentDate
                            });
                        }

                        foreach (tblreservationsparentpayment parentpayr in parent_rpays)
                        {
                            testPayments.Add(new PaymentReconciliation()
                            {
                                cardNum = detItem.cardNum,
                                sourceData = 7,
                                typeRsv = 2,
                                statementID = bstatement.idBankStatements2,
                                paymentID = parentpayr.idReservationParentPayment,
                                authCode = ((detItem.authCode != "0") && (!(string.IsNullOrEmpty(detItem.authCode.Trim())))) ? detItem.authCode : null,
                                payAmount = parentpayr.reservationPaymentCost ?? 0m,
                                payDay = (DateTime)parentpayr.reservationPaymentDate
                            });
                        }

                        foreach (tblpaymentspurchases payu in upays)
                        {
                            testPayments.Add(new PaymentReconciliation()
                            {
                                cardNum = detItem.cardNum,
                                sourceData = 8,
                                statementID = bstatement.idBankStatements2,
                                paymentID = payu.idPaymentPurchase,
                                authCode = ((detItem.authCode != "0") && (!(string.IsNullOrEmpty(detItem.authCode.Trim())))) ? detItem.authCode : null,
                                payAmount = (decimal)payu.paymentCost,
                                payDay = (DateTime)payu.paymentDate
                            });
                        }

                        foreach (tblincomemovement payi in ipays)
                        {
                            testPayments.Add(new PaymentReconciliation()
                            {
                                cardNum = detItem.cardNum,
                                sourceData = 2,
                                statementID = bstatement.idBankStatements2,
                                paymentID = payi.idincomeMovement,
                                authCode = ((detItem.authCode != "0") && (!(string.IsNullOrEmpty(detItem.authCode.Trim())))) ? detItem.authCode : null,
                                payAmount = payi.incomemovchargedamount,
                                payDay = payi.incomemovapplicationdate
                            });
                        }

                        PaymentReconciliation firstTest = testPayments.Where(e => ((decimal)(Math.Abs(detItem.amountMax - e.payAmount)) < maxdiff)).FirstOrDefault();

                        if (firstTest != null)
                        {
                            switch (firstTest.sourceData)
                            {
                                case 2:
                                    incoms.Add(new tblbankstat2income()
                                    {
                                        idBankStatements2 = firstTest.statementID,
                                        idincomeMovement = firstTest.paymentID
                                    });

                                    exclude_incom.Add(firstTest.paymentID);
                                    break;
                                case 4:
                                    payment.Add(new tblbankstat2invoice()
                                    {
                                        idBankStatements2 = firstTest.statementID,
                                        idpayment = firstTest.paymentID
                                    });
                                    exclude_payment.Add(firstTest.paymentID);
                                    break;

                                case 5:
                                    //fondo.Add(new tblbankstat2fondo()
                                    //{
                                    //    idBankStatements2In = firstTest.statementID,
                                    //    idBankStatements2Out = firstTest.statementID,
                                    //    idFondos = (int)firstTest.paymentID
                                    //});
                                    //exclude_fondo.Add((int)firstTest.paymentID);
                                    break;

                                case 7:
                                    {
                                        if (firstTest.typeRsv == 1)
                                        {
                                            rsvs.Add(new tblbankstat2reserv()
                                            {
                                                idBankStatements2 = firstTest.statementID,
                                                idReservationPayment = (int)firstTest.paymentID
                                            });
                                            exclude_rsvs.Add((int)firstTest.paymentID);
                                        }
                                        else
                                        {
                                            rsvsparent.Add(new tblbankstat2parentreserv()
                                            {
                                                idBankStatements2 = firstTest.statementID,
                                                idReservationParentPayment = (int)firstTest.paymentID
                                            });
                                            exclude_parentrsv.Add((int)firstTest.paymentID);
                                        }

                                    }
                                    break;
                                case 8:
                                    purchase.Add(new tblbankstat2purchase()
                                    {
                                        idBankStatements2 = firstTest.statementID,
                                        idPaymentPurchase = Convert.ToInt32(firstTest.paymentID)
                                    });

                                    exclude_purchase.Add(Convert.ToInt32(firstTest.paymentID));
                                    break;

                                default:
                                    break;
                            }

                            bsgather_amnt += firstTest.payAmount;
                            bsremain_amnt -= firstTest.payAmount;
                            affected = true;
                            continue;
                        }

                        testPayments = testPayments.OrderByDescending(t => t.payAmount).ThenByDescending(m => m.payDay).ToList();

                        foreach (PaymentReconciliation testpay in testPayments)
                        {
                            if (remain_amnt < maxdiff) break;

                            if (testpay.payAmount > (remain_amnt + maxdiff)) continue;

                            switch (testpay.sourceData)
                            {
                                case 2:
                                    incoms.Add(new tblbankstat2income()
                                    {
                                        idBankStatements2 = testpay.statementID,
                                        idincomeMovement = testpay.paymentID
                                    });

                                    exclude_incom.Add(testpay.paymentID);
                                    break;

                                case 4:
                                    payment.Add(new tblbankstat2invoice()
                                    {
                                        idpayment = testpay.paymentID,
                                        idBankStatements2 = testpay.statementID
                                    });
                                    exclude_payment.Add(testpay.paymentID);
                                    break;
                                case 5:
                                    //    fondo.Add(new tblbankstat2fondo()
                                    //    {
                                    //        idFondos = (int)testpay.paymentID,
                                    //        idBankStatements2In = testpay.statementID,
                                    //        idBankStatements2Out = testpay.statementID
                                    //    });
                                    //    exclude_fondo.Add((int)testpay.paymentID);
                                    break;

                                case 7:
                                    {
                                        if (testpay.typeRsv == 1)
                                        {
                                            rsvs.Add(new tblbankstat2reserv()
                                            {
                                                idBankStatements2 = testpay.statementID,
                                                idReservationPayment = (int)testpay.paymentID
                                            });
                                            exclude_rsvs.Add((int)testpay.paymentID);
                                        }
                                        else
                                        {
                                            rsvsparent.Add(new tblbankstat2parentreserv()
                                            {
                                                idBankStatements2 = testpay.statementID,
                                                idReservationParentPayment = (int)testpay.paymentID
                                            });
                                            exclude_parentrsv.Add((int)testpay.paymentID);
                                        }

                                    }
                                    break;

                                case 8:
                                    purchase.Add(new tblbankstat2purchase()
                                    {
                                        idBankStatements2 = testpay.statementID,
                                        idPaymentPurchase = Convert.ToInt32(firstTest.paymentID)
                                    });

                                    exclude_purchase.Add((int)firstTest.paymentID);
                                    break;

                                default:
                                    break;
                            }

                            gathered_amnt += testpay.payAmount;
                            remain_amnt -= testpay.payAmount;
                            affected = true;
                        }

                        bsgather_amnt += gathered_amnt;
                        bsremain_amnt -= gathered_amnt;
                    }

                    reconciled = bstatement.bankstatements2Charge == 0m ? (((bstatement.bankstatements2Pay - bsgather_amnt) < 1m) && (bsremain_amnt < 1m)) : (((bstatement.bankstatements2Charge - bsgather_amnt) < 1m) && (bsremain_amnt < 1m));

                    if (affected)
                    {
                        tblbankstatements2 el_statement = unity.Bankstatements2Repository.Get(e => e.idBankStatements2 == bstatement.idBankStatements2).FirstOrDefault();
                        el_statement.idBankStatementMethod = 3;
                        el_statement.bankstatements2Reconciled = Constants.ActiveRecord;
                        update_statmnts.Add(el_statement);
                    }

                    if (!reconciled)
                    {
                        retry_conciliations.Add(bstatement.idBankStatements2);
                    }
                }

                catch (Exception ex)
                {
                    errorReport.AddStatConciliationError2("Error al reconciliar el registro.", bstatement, ex);
                    continue;
                }
            }

            try
            {
                unity.Statements2RESERVRepository.InsertMulti(rsvs);
                unity.Statements2PARENTRESERVRepository.InsertMulti(rsvsparent);
                unity.Statements2PURCHASESRepository.InsertMulti(purchase);
                unity.Statements2INCOMERepository.InsertMulti(incoms);
                unity.Statements2INVOICERepository.InsertMulti(payment);

                foreach (tblbankstatements2 bstat in update_statmnts)
                {
                    unity.Bankstatements2Repository.Update(bstat);
                }

                unity.Commit();
            }

            catch (Exception ex)
            {
                errorReport.AddCriticalStop("Error de escritura en base de datos.", ex);
                retry_conciliations = new List<long>();
            }

            return retry_conciliations;
        }

        public void reconcileByGuessing2(List<long> retry_us, ref XLSFileImportErrors errorReport)
        {
            if ((retry_us == null) || (retry_us.Count < 1)) return;

            retry_us = retry_us.Distinct().ToList();

            foreach (long bs_id in retry_us)
            {
                tblbankstatements bs_record = unity.BankstatementsRepository.Get(t => t.idBankStatements == bs_id).FirstOrDefault();

                if (bs_record == null) continue;

                try
                {
                    bool affected = false;
                    List<tblbankstatereserv> rsvs = new List<tblbankstatereserv>();
                    List<tblbankstateparentreserv> rsvsparent = new List<tblbankstateparentreserv>();
                    List<tblbankstatepurchase> purchase = new List<tblbankstatepurchase>();
                    List<tblbankstateincome> incoms = new List<tblbankstateincome>();

                    bankreconciliationModel bs_recon = new bankreconciliationModel();
                    bs_recon.GenerateConciliation(bs_id, false);

                    if ((bs_recon.financialstateitemlist == null) || (bs_recon.financialstateitemlist.Count() == 0) || ((bs_recon.docpaymentreservitems.Count() + bs_recon.docpaymentparentreservitems.Count() + bs_recon.docpaymentpurchaseitems.Count() + bs_recon.docpaymentincomitems.Count()) >= bs_recon.financialstateitemlist.Count())) continue;

                    List<int> ups_ids = bs_recon.docpaymentpurchaseitems.Select(u => u.Purchase).Distinct().ToList();
                    List<long> rsv_ids = bs_recon.docpaymentreservitems.Select(r => r.ReservationPayment).Distinct().ToList();
                    List<long> rsvparent_ids = bs_recon.docpaymentparentreservitems.Select(r => r.ReservationPayment).Distinct().ToList();
                    List<long> inc_ids = bs_recon.docpaymentincomitems.Select(i => i.IncomeMovement).Distinct().ToList();
                    List<financialstateitemModel> finitems = bs_recon.financialstateitemlist.Where(f => (((f.SourceData == 4) && (!(ups_ids.Contains(Convert.ToInt32(f.ReferenceItem))))) || ((f.SourceData == 5) && (!(rsv_ids.Contains(f.ReferenceItem)))) || ((f.SourceData == 8) && (!(inc_ids.Contains(f.ReferenceItem)))))).ToList();

                    foreach (financialstateitemModel fin_item in finitems)
                    {
                        try
                        {
                            switch (fin_item.SourceData)
                            {
                                case 2:
                                    incoms.Add(new tblbankstateincome()
                                    {
                                        idBankStatements = bs_id,
                                        idincomeMovement = fin_item.ReferenceItem,
                                        authCode = null,
                                        cardNumber = null
                                    });

                                    break;

                                case 7:
                                    {
                                        if (fin_item.typeRsv == 1)
                                        {
                                            rsvs.Add(new tblbankstatereserv()
                                            {
                                                idBankStatements = bs_id,
                                                idReservationPayment = (int)fin_item.ReferenceItem,
                                                authCode = null,
                                                cardNumber = null
                                            });
                                        }
                                        else
                                        {
                                            rsvsparent.Add(new tblbankstateparentreserv()
                                            {
                                                idBankStatements = bs_id,
                                                idReservationParentPayment = (int)fin_item.ReferenceItem,
                                                authCode = null,
                                                cardNumber = null
                                            });
                                        }
                                    }
                                    break;

                                case 8:
                                    purchase.Add(new tblbankstatepurchase()
                                    {
                                        idBankStatements = bs_id,
                                        idPaymentPurchase = Convert.ToInt32(fin_item.ReferenceItem),
                                        authCode = null,
                                        cardNumber = null
                                    });

                                    break;

                                default:
                                    break;
                            }
                        }

                        catch
                        {
                            continue;
                        }
                    }

                    if ((rsvs != null) && (rsvs.Count > 0))
                    {
                        unity.BankstateReservRepository.InsertMulti(rsvs);
                        affected = true;
                    }

                    if ((rsvsparent != null) && (rsvsparent.Count > 0))
                    {
                        unity.BankstateParentReservRepository.InsertMulti(rsvsparent);
                        affected = true;
                    }

                    if ((purchase != null) && (purchase.Count > 0))
                    {
                        unity.BankstatePurchaseRepository.InsertMulti(purchase);
                        affected = true;
                    }

                    if ((incoms != null) && (incoms.Count > 0))
                    {
                        unity.BankstateIncomeRepository.InsertMulti(incoms);
                        affected = true;
                    }

                    if (affected)
                    {
                        bs_record.idBankStatementMethod = 3;
                        unity.BankstatementsRepository.Update(bs_record);
                        unity.Commit();
                    }
                }

                catch (Exception ex)
                {
                    errorReport.AddStatConciliationError("Error al reconciliar automáticamente el registro.", bs_record, ex);
                    continue;
                }
            }
        }

        private static string getValueString(Cell cell, WorkbookPart wbPart)
        {
            int id = -1;
            string currentcellvalue = string.Empty;

            if (Int32.TryParse(cell.InnerText, out id))
            {
                SharedStringItem item = wbPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);

                if (item.Text != null)
                {
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

        public static bool checkStatementRowBA(Row la_row, WorkbookPart el_wb)
        {
            if ((la_row == null) || (la_row.GetType() != typeof(Row))) return false;

            try
            {
                Cell la_cell = (Cell)la_row.ChildElements.ElementAt(0);

                if ((la_cell == null) || (la_cell.GetType() != typeof(Cell))) return false;

                string cell_val = ((la_cell.DataType != null) && (la_cell.DataType == CellValues.SharedString)) ? getValueString(la_cell, el_wb).Trim() : la_cell.CellValue.Text.Trim();

                if (String.IsNullOrEmpty(cell_val)) return false;

                tblbankaccount la_BAccount = unity.BankAccountRepository.Get(x => x.baccountname == cell_val && x.baccountactive == Globals.activeRecord).FirstOrDefault();

                return ((la_BAccount != null) && (la_BAccount.idbaccount > 0));
            }

            catch
            {
                return false;
            }
        }

        public static bool checkStatementRow(Row la_row, WorkbookPart el_wb, BankStatementsSheets shets)
        {
            if ((la_row == null) || (la_row.GetType() != typeof(Row))) return false;

            try
            {
                Cell la_cell = null;
                if (shets.Equals(BankStatementsSheets.Resumen))
                {
                    la_cell = (Cell)la_row.ChildElements.ElementAt(1);
                }
                else
                {
                    la_cell = (Cell)la_row.ChildElements.ElementAt(0);
                }

                if ((la_cell == null) || (la_cell.GetType() != typeof(Cell))) return false;

                string cell_val = ((la_cell.DataType != null) && (la_cell.DataType == CellValues.SharedString)) ? getValueString(la_cell, el_wb).Trim() : la_cell.CellValue.Text.Trim();

                if (String.IsNullOrEmpty(cell_val)) return false;

                tbltpv la_tpv = unity.TpvRepository.Get(t => t.tpvidlocation == cell_val && t.tpvactive == Globals.activeRecord).FirstOrDefault();

                return ((la_tpv != null) && (la_tpv.idtpv > 0));
            }

            catch
            {
                return false;
            }
        }

        #endregion
    }
}