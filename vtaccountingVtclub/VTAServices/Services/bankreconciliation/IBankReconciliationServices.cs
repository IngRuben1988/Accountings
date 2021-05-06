using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using VTAworldpass.VTACore.Cores.Globales;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTAServices.Services.bankreconciliation.model;
using VTAworldpass.VTAServices.Services.drive.model;
using VTAworldpass.VTAServices.Services.Models;
using static VTAworldpass.VTACore.Cores.Globales.Enumerables;

namespace VTAworldpass.VTAServices.Services.bankreconciliation
{
    public interface IBankReconciliationServices
    {

        List<bankstatements> getBankStatements2(DateTime? dateReportStart, DateTime? dateReportEnd, int PaymentMethod, bool sourcedata, bool financialstateitem);
        List<bankreconciliationModel> getBakReconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, BankAccountReconciliationStatus status, bool sourcedata, bool financialstateitem);
        List<bankreconciliationModel> getBakReconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, int status, bool sourcedata, bool financialstateitem);
        List<bankreconciliationModel> getBakReconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, int status, decimal ammountStart, decimal ammountEnd, bool sourcedata, bool financialstateitem);
        //List<bankreconciliationModel> getBakReconcilitions(int month, int year, int Tpv, int currency, int externalgroup, int company, int BankAccount, int hotel, BankAccountReconciliationStatus status, bool sourcedata, bool financialstateitem);
        long saveBankStatement(bankreconciliationModel bankreconciliation);
        void updateBankStatement(tblbankstatements model);
        void deleteBankStatement(long id);
        void deleteBankStatement(long[] id);
        bankreconciliationModel getBakReconciliationsDetailsbyId(long idBankStatements);
        bankreconciliationModel getBakReconciliationsDetailsbyRefenceReferenceItem(int sourcedata, long reference, long referenceitem, int rsvType);
        int saveBankStatementItem(int idBankStatement, financialstateitemModel item);
        int deleteBankStatementItem(int idBankStatement, financialstateitemModel item);
        int deleteBankStatementItem(int SourceData, long Reference, long ReferenceItem, int rsvType);
        void deleteBankStatementItem(tblbankstatepurchase model);
        void deleteBankStatementItem(tblbankstatereserv model);
        Dictionary<string, string> generateReportReconciliationsExcel(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, int status, decimal ammountStart, decimal ammountEnd);
        List<financialstateitemModel> getSearchFinancialStateItemList(DateTime start, DateTime end, int idBankAccount, int tpv, decimal ammountStart, decimal ammountEnd, int idHotel);
        List<file> getFiles(HttpFileCollectionBase files, ref XLSFileImportErrors errorReport);
        List<bankreconciliationModel> addXLScotiaPos(List<file> files);

        #region Conciliations V2

        List<string> AddFiletoDatabase(List<file> files);
        #endregion

        #region Conciliatios V3

        void ReconcileFromFiles(List<file> inputFiles, ref XLSFileImportErrors errorReport);
        List<bankreconciliationModel> getBakReconcilitions(int month, int year, int Tpv, int currency, int externalgroup, int company, int BankAccount, int hotel, BankAccountReconcilitionStatus status, bool sourcedata, bool financialstateitem);
        List<bankstatements> getBankStat2Reconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int type, string descripcion, int status, decimal ammountStart, decimal ammountEnd, bool sourcedata, bool financialstateitem);
        List<bankstatements> getBankStat2Reconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, BankAccountReconciliationStatus status, bool sourcedata, bool financialstateitem);
        List<bankstatements> getBankStat2Reconcilitions(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, int status, decimal ammountStart, decimal ammountEnd, bool sourcedata, bool financialstateitem);
        bankstatements getBankStat2ReconciliationsDetailsById(long idBankStatements);
        List<financialstateitemModel> getSearchFinancialState2ItemList(DateTime start, DateTime end, int idBankAccount, int tpv, decimal ammountStart, decimal ammountEnd, int typeSourceData, string descripcion);
        int saveBankStatementItem2(int idBankStatement, financialstateitemModel item);
        int deleteBankStatementItem2(int idBankStatement, financialstateitemModel item);
        void deleteBankStatement2(long id);
        void deleteBankStatement2(long[] id);
        Dictionary<string, string> generateReportBaReconciliationsExcel(DateTime? dateReportStart, DateTime? dateReportEnd, int Tpv, int externalgroup, int company, int PaymentMethod, int hotel, int status, decimal ammountStart, decimal ammountEnd);
        #endregion
    }
}
