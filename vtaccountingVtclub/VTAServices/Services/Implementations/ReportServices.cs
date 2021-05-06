using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.reports.model;
using VTAworldpass.VTACore.Helpers;
using static VTAworldpass.VTACore.Cores.Globales.Enumerables;
using VTAworldpass.VTAServices.Services.invoices.model;
using System.Threading.Tasks;
using System;
using VTAworldpass.VTAServices.Services.invoices.implements;
using VTAworldpass.VTACore.GenericRepository.Repositories;
using VTAworldpass.VTAServices.Services.Models;
using VTAworldpass.VTAServices.Services.budgets;
using VTAworldpass.VTACore.Helpers.xls;

namespace VTAworldpass.Business.Services.Implementations
{
    public class ReportServices
    {
        private readonly UnitOfWork unity;
        private readonly InvoiceServices invoiceServices;
        private readonly GeneralRepository generalRepository;
        public ReportServices()
        {
            this.unity = new UnitOfWork();
            invoiceServices = new InvoiceServices();
            generalRepository = new GeneralRepository();
            // this.invoiceServices = new InvoiceServices();
            // this.accountServices = new AccountServices();
        }
        public List<currencyexpensesreport> generateExpenses(int year, int month, int idCompany, int TypeReport)
        {
            List<currencyexpensesreport> currexprep = new List<currencyexpensesreport>();
            var currencies = unity.CurrencyRepository.Get().ToList();
            //var currencies = unity.CurrencyRepository.Get(c=> c.idCurrency == 3).ToList();
            var TypeTemp = unity.AccountTypeRepository.Get(y => y.idaccounttypereport == TypeReport).FirstOrDefault();

            foreach (tblcurrencies curr in currencies)
            {
                if (TypeTemp != null) {

                    currencyexpensesreport helperCurrency = new currencyexpensesreport(curr.idCurrency, curr.currencyAlphabeticCode, year, TypeReport, TypeTemp.accounttypereportdescription, true);

                    if (month != 0)
                    {
                        string monthName = DateTimeUtils.getMonthNameCurrentEs(month);

                        expensesreport helperExpReport = new expensesreport(helperCurrency.year, month, monthName, idCompany, "", helperCurrency.currency, helperCurrency.currencyname, helperCurrency.typereport, helperCurrency.typereportname, true, true, ExpenseReport.ExpensesConliationsIn);
                        var _g = helperExpReport.expensereporitem.Count();
                        if (_g != 0)
                        {
                            helperCurrency.addExpenseReport(helperExpReport);
                            currexprep.Add(helperCurrency);
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= 12; i++)
                        {
                            string monthName = DateTimeUtils.getMonthNameCurrentEs(i);
                            expensesreport helperExpReport = new expensesreport(helperCurrency.year, i, monthName, idCompany, "", helperCurrency.currency, helperCurrency.currencyname, helperCurrency.typereport, helperCurrency.typereportname, false, false, ExpenseReport.ExpensesConliationsIn);
                            var _g = helperExpReport.expensereporitem.Count();
                            if (_g != 0)
                            {
                                helperCurrency.addExpenseReport(helperExpReport);
                            }
                        }
                        if (helperCurrency.expensesreport.Count() != 0)
                        {
                            currexprep.Add(helperCurrency);
                        }
                    }
                }
                else
                {
                    currexprep.Add(new currencyexpensesreport(curr.idCurrency, curr.currencyAlphabeticCode, year, 0, "", true)); // Empty
                }
            }

            return currexprep;
        }

        public List<currencyexpensesreport> generateExpenses(int year, int month, int company, int typereport, Currencies toExchangeCurrency)
        {

            /*****************************************************************************************/
            /**************************** GENERATING EXPENSE REPORT BY CURRENCIES ***********************************/
            /*****************************************************************************************/

            List<currencyexpensesreport> currExpRep = new List<currencyexpensesreport>();

            var currencies = unity.CurrencyRepository.Get().ToList();

            var companycurrency = unity.CompaniesCurrenciesRepository.Get(cp => cp.idcompany == company).ToList();
            var currency = companycurrency.Select(c => c.tblcurrencies).ToList();
            // var currencies = unity.CurrencyRepository.Get(c => c.idCurrency == 3).ToList();
            var TypeTemp = unity.AccountTypeRepository.Get(y => y.idaccounttypereport == typereport).FirstOrDefault();

            foreach (tblcurrencies curr in currency)
            {
                if (TypeTemp != null)
                {
                    currencyexpensesreport helperCurrency = new currencyexpensesreport(curr.idCurrency, curr.currencyAlphabeticCode, year, typereport, TypeTemp.accounttypereportdescription, true);
                    if (month != 0)
                    {
                        string monthName = DateTimeUtils.getMonthNameCurrentEs(month);
                        expensesreport helperExpReport = new expensesreport(helperCurrency.year, month, monthName, company, "", helperCurrency.currency, helperCurrency.currencyname, helperCurrency.typereport, helperCurrency.typereportname, true, true, ExpenseReport.ExpensesConliationsIn);
                        // expensesreport helperExpReport = new expensesreport(2019, 6, "Junio", 2, "Lm by Inmmense", 3, "Pesos MNX",1, "E-R", true, true, ExpenseReport.ExpensesConliationsIn);
                        if (helperExpReport.expensereporitem.Count() != 0)
                        {
                            helperCurrency.addExpenseReport(helperExpReport);
                            currExpRep.Add(helperCurrency);
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= 12; i++)
                        {
                            string monthName = DateTimeUtils.getMonthNameCurrentEs(i);
                            expensesreport helperExpReport = new expensesreport(helperCurrency.year, i, monthName, company, "", helperCurrency.currency, helperCurrency.currencyname, helperCurrency.typereport, helperCurrency.typereportname, true, true, ExpenseReport.ExpensesConliationsIn);

                            if (helperExpReport.expensereporitem.Count() != 0)
                            {
                                helperCurrency.addExpenseReport(helperExpReport);
                            }
                        }
                        if (helperCurrency.expensesreport.Count() != 0)
                        {
                            currExpRep.Add(helperCurrency);
                        }
                    }
                }
                else
                {
                    currExpRep.Add(new currencyexpensesreport(curr.idCurrency, curr.currencyAlphabeticCode, year, 0, "", true)); // Empty
                }
            }


            /*****************************************************************************************/
            /**************************** TRANSFORMING TO CURRENCY [Currencies toCurrency] ***********/
            /*****************************************************************************************/
            /*
            currencyexpensesreport mainCurrency = new currencyexpensesreport();

            if (currExpRep.Count() >= 2)
            {
                var _searchMainCurrency = currExpRep.Where(c => c.currency == (int)toExchangeCurrency).FirstOrDefault();

                List<currencyexpensesreport> tmptoAdd = new List<currencyexpensesreport>();
                tmptoAdd = currExpRep.Where(v => v.currency != (int)toExchangeCurrency).ToList();


                if (_searchMainCurrency == null)
                {
                    mainCurrency = new currencyexpensesreport((int)toExchangeCurrency, year, typereport, true);
                }
                else
                {
                    mainCurrency = _searchMainCurrency;
                }


                foreach (currencyexpensesreport helper in tmptoAdd)
                {
                    mainCurrency.AddExpenseReporttoExchange(helper.expensesreport.ToList(), (int)toExchangeCurrency);
                }

                currExpRep.Clear();
                currExpRep.Add(mainCurrency);
            }
            */
            return currExpRep;
        }

        public async Task<List<invoiceitems>> expenseConcentratedReport(int category, int Type, int Company, DateTime? applicationDateIni, DateTime? applicationDateFin, int results, int isTax, int singleExibitionPayment, int budgetType, DateTime? creationDateIni, DateTime? creationDateFin)
        {
            List<invoiceitems> result = new List<invoiceitems>();

            var _result = await generalRepository.expenseConcentratedReport(category,Type,Company,applicationDateIni, applicationDateFin, results, isTax, singleExibitionPayment, budgetType, creationDateIni, creationDateFin);

            foreach (tblinvoiceditem model in _result)
            {
                var x = this.invoiceServices.convertInvoiceItemtoHelper(model);
                result.Add(x);
            }

            return result;

        }

        public financialstateModel generateReportCashClosing(DateTime start, DateTime end, int idBankAccount, IBudgetServices budget, int type)
        {
            financialstateModel financialState;
            if (type == 1)
            {
                financialState = new financialstateModel(start, end, idBankAccount, FinancialStateReport.AccountHistory, true);
            }
            else
            {
                financialState = new financialstateModel(start, end, idBankAccount, FinancialStateReport.BAccountCash, true);

            }
            return financialState;
        }

        public Dictionary<string, string> generateReportCashClosingExcel(DateTime start, DateTime end, int idBankAccount, IBudgetServices budget, int type)
        {
            GeneradorXLS xls = new GeneradorXLS("downloads", "expenseReportConcentrated.xlsx");
            financialstateModel financialState;
            if (type == 1)
            {
                financialState = new financialstateModel(start, end, idBankAccount, FinancialStateReport.AccountHistory, true);
            }
            else
            {
                financialState = new financialstateModel(start, end, idBankAccount, FinancialStateReport.BAccountCash, true);

            }

            xls.GeneradorXLSFinancialStateComplete(financialState);

            Dictionary<string, string> fileProperties = new Dictionary<string, string>();
            fileProperties.Add("nameFile", string.Format("Cuenta-{0}_{1}_{2}", financialState.PaymentMethodName, financialState.FechaInicioString.Replace("/", "-"), financialState.FechaFinString.Replace("/", "-")));
            fileProperties.Add("url", xls.GetFileDir());
            fileProperties.Add("urlrelative", xls.GetFileDirRelative());

            return fileProperties;
        }

        public financialstateModel generateReportCashClosingReconciliation(DateTime start, DateTime end, int idBankAccount, IBudgetServices budget)
        {
            financialstateModel financialState = new financialstateModel(start, end, idBankAccount, FinancialStateReport.AccountHistoryOnlyConciliationsIn, true);
            return financialState;
        }

        public Dictionary<string, string> generateReportCashClosingExcelConciliations(DateTime start, DateTime end, int idBankAccount, IBudgetServices budget)
        {
            GeneradorXLS xls = new GeneradorXLS("downloads", "expenseReportConcentrated.xlsx");

            financialstateModel financialState = new financialstateModel(start, end, idBankAccount, FinancialStateReport.AccountHistoryOnlyConciliationsIn, true);
            xls.GeneradorXLSFinancialStateBankModal(financialState);

            Dictionary<string, string> fileProperties = new Dictionary<string, string>();
            fileProperties.Add("nameFile", string.Format("Cuenta-{0}_{1}_{2}", financialState.PaymentMethodName, financialState.FechaInicioString.Replace("/", "-"), financialState.FechaFinString.Replace("/", "-")));
            fileProperties.Add("url", xls.GetFileDir());
            fileProperties.Add("urlrelative", xls.GetFileDirRelative());

            return fileProperties;
            //return xls.GetFileDir();
        }
    }
}