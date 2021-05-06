using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Database;

namespace VTAworldpass.VTAServices.Services.Models
{
    public class financialstateitemstatementsModel
    {
        public long idBankStatement { get; set; }
        public List<financialstateitemModel> fin_states { get; set; }

        public financialstateitemstatementsModel() { }
    }
    public class SavedStatementsLists
    {
        public List<tblbankstatements> gstatements { get; set; }
        public List<tblbankstatementsdet> dstatements { get; set; }
        public List<tblbankstatements2> baccountstatements { get; set; }
    }

    public class StatementDetailSearch
    {
        public int cardNum { get; set; }
        public int paymentID { get; set; }
        public long statementID { get; set; }
        public decimal amountMax { get; set; }
        public string authCode { get; set; }
    }

    public class PaymentReconciliation
    {
        public int cardNum { get; set; }
        public int sourceData { get; set; }
        public long statementID { get; set; }
        public long paymentID { get; set; }
        public decimal payAmount { get; set; }
        public string authCode { get; set; }
        public DateTime payDay { get; set; }
        public int typeRsv { get; set; }
    }
}