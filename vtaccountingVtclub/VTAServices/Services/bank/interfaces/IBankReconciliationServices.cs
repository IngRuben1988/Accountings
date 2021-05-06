using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTAServices.Services.bank.model;
using VTAworldpass.VTAServices.Services.drive.model;

namespace VTAworldpass.VTAServices.Services.bank.interfaces
{
    interface IBankReconciliationServices
    {
        List<bankreconciliation> getBakReconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, BankAccountReconcilitionStatus status, bool sourcedata, bool financialstateitem);
        List<bankreconciliation> getBakReconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, int status, bool sourcedata, bool financialstateitem);
        List<bankreconciliation> getBakReconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, int status, decimal ammountStart, decimal ammountEnd, bool sourcedata, bool financialstateitem);
        List<bankreconciliation> getBakReconcilitions(int month, int year, int Tpv, int currency, int externalgroup, int company, int BankAccount, int hotel, BankAccountReconcilitionStatus status, bool sourcedata, bool financialstateitem);
        long saveBankStatement(bankreconciliation bankreconciliation);
        void updateBankStatement(tblbankstatements model);
        void deleteBankStatement(long id);
        void deleteBankStatement(long[] id);
        bankreconciliation getBakReconciliationsDetailsbyId(long idBankStatements);
        bankreconciliation getBakReconciliationsDetailsbyRefenceReferenceItem(int sourcedata, long reference, long referenceitem);
        int saveBankStatementItem(int idBankStatement, financialstateitem item);
        int deleteBankStatementItem(int idBankStatement, financialstateitem item);
        int deleteBankStatementItem(int SourceData, long Reference, long ReferenceItem);
        void deleteBankStatementItem(tblbankstateupscl model);
        void deleteBankStatementItem(tblbankstatereserv model);
        Dictionary<string, string> generateReportReconciliationsExcel(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, int status, decimal ammountStart, decimal ammountEnd);
        List<financialstateitem> getSearchFinancialStateItemList(DateTime start, DateTime end, int idBankAccount, int tpv, decimal ammountStart, decimal ammountEnd, int idHotel);
        List<filemodel> getFiles(HttpFileCollectionBase files);
        List<bankreconciliation> addXLScotiaPos(List<filemodel> files);

        #region Conciliations V2

        List<string> AddFiletoDatabase(List<filemodel> files);
        #endregion
    }
}
