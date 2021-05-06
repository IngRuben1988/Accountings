using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Database;

namespace VTAworldpass.VTAServices.Services.Logger.helpers
{
    public class LogsHelper
    {
        protected void prepareToSaveLog(tblfondos model, tblfondosLog log)
        {
            log.LogDate = DateTime.Now;
            log.idPaymentMethod = model.idPaymentMethod;
            log.idFinancialMethod = model.idFinancialMethod;
            log.idFondos = model.idFondos;
            log.fondofechaEntrega = model.fondofechaEntrega;
            log.fondoFechaInicio = model.fondoFechaInicio;
            log.fondoFechaFin = model.fondoFechaFin;
            log.fondoMonto = model.fondoMonto;
            log.fondoCreatedby = model.fondoCreatedby;
            log.fondoCreationDate = model.fondoCreationDate;
            log.fondoComments = model.fondoComments;
            log.fondoFee = model.fondoFee;
            log.fondoInvoice = model.fondoInvoice;

        }

        protected void prepareToSaveLog(tblincome model, tblincomeLog log)
        {
            log.idincome = model.idincome;
            log.idcompany = model.idcompany;
            log.idcurrency = model.idcurrency;
            log.incomeapplicationdate = model.incomeapplicationdate;
            log.incomenumber = model.incomenumber;
            log.incomecreatedby = model.incomecreatedby;
            log.incomecreactionDate = model.incomecreactiondate;
            log.incomeupdatedby = model.incomeupdatedby;
            log.incometupdateon = model.incometupdateon;
        }

        protected void prepareToSaveLog(tblincomeitem model, tblincomeitemLog log)
        {
            log.idincomeitem = model.idincomeitem;
            log.idincome = model.idincome;
            log.idaccountl4 = model.idAccountl4;
            log.idincomeitemstatus = model.idincomeitemstatus;
            log.iduser = model.iduser;
            log.incomeitemdate = model.incomeitemdate;
            log.incomeitemsubtotal = model.incomeitemsubtotal;
            log.incomedescription = model.incomedescription;
        }

        protected void prepareToSaveLog(tblinvoice model, tblinvoiceLog log)
        {
            log.LogDate = DateTime.Now;
            log.idinvoice = model.idinvoice;
            log.idcurrency = (int)model.idcurrency;
            log.idcompany = model.idcompany;
            log.invoicedate = model.invoicedate;
            log.invoicecreatedby = model.invoicecreatedby;
            log.invoicecreateon = model.invoicecreateon;
            log.invoiceupdatedby = model.invoiceupdatedby;
            log.invoiceupdateon = model.invoiceupdateon;
            log.invoicedeleteon = DateTime.Now;
        }

        protected void prepareToSaveLog(tblinvoiceditem model, tblinvoiceditemLog log)
        {
            log.LogDate = DateTime.Now;
            log.idinvoiceitem = Convert.ToInt32(model.idinvoiceitem);
            log.idinvoice = Convert.ToInt32(model.idinvoice);
            log.idaccl4 = model.idaccountl4;
            log.idinvoiceitemstatus = model.idinvoiceitemstatus;
            log.Iduser = model.iduser;
            log.invoiceditemsubtotal = (decimal)model.itemsubtotal;
            log.description = model.itemdescription;
            log.deleted = false;
            log.invoiceditemistax = model.ditemistax;
            log.invoiceditemtaxes = model.itemtax;
            log.invoiceditembillidentifier = model.itemidentifier;
            log.idsupplier = model.idsupplier;
            log.invoiceditemsupplierother = model.itemsupplierother;
            log.idbudgettype = model.idbudgettype;
            log.invoiceditemothertaxesammount = model.itemothertax;
            log.invoiceditemsingleexibitionpayment = model.itemsinglepayment;
        }

        protected void prepareToSaveLog(tblpayment model, tblpaymentLog log)
        {
            log.LogDate = DateTime.Now;
            log.idPayment = model.idpayment;
            log.idInvoice = Convert.ToInt32(model.idinvoice);
            log.idBAccount = model.idbaccount;
            log.idBankAccntType = model.idbankprodttype;
            log.applicationDate = model.paymentdate;
            log.chargedAmount = model.paymentamount;
            log.authRef = model.paymentauthref;
            log.creationDate = model.paymentcreateon;
            log.createdBy = model.paymentcreatedby;
            log.updatedOn = model.paymentupdatedon;
            log.updatedBy = model.paymentupdatedby;

        }

        protected void prepareToSaveLog(tblincomemovement model, tblincomemovementLog log)
        {
            log.idincomeMovement = model.idincomeMovement;
            log.idincome = model.idincome;
            log.idbaccount = model.idbaccount;
            log.idBankAccntType = model.idbankaccnttype;
            log.idtpv = model.idtpv;
            log.incomemovcard = model.incomemovcard;
            log.incomemovapplicationdate = model.incomemovapplicationdate;
            log.incomemovchargedamount = model.incomemovchargedamount;
            log.incomemovauthref = model.incomemovauthref;
            log.incomemovcreationdate = model.incomemovcreationdate;
            log.incomemovcreatedby = model.incomemovcreatedby;
            log.incomemovupdatedon = model.incomemovupdatedon;
            log.incomemovupdatedby = model.incomemovupdatedby;
            log.incomemovcanceled = false;
            log.incomemovdeleted = false;

        }

        protected void prepareToSaveLog(tblfondosmaxammount model, tblfondosmaxammountLog log)
        {
            log.LogDate = DateTime.Now;
            log.idFondosMax = model.idFondosMax;
            log.idBAccount = model.idBAccount;
            log.fondosmaxAmmount = model.fondosmaxAmmount;
            log.fondosmaxCreatedby = model.fondosmaxCreatedby;
            log.fondosmaxCreationDate = model.fondosmaxCreationDate;

        }

        protected void prepareToSaveLog(tblbankstatements model, tblbankstatementsLog log)
        {
            log.Log_Date = DateTime.Now;
            log.idBankStatements = model.idBankStatements;
            log.idBAccount = model.idBAccount;
            log.idTPV = model.idTPV;
            log.idCompany = model.idCompany;
            log.idBankStatementMethod = model.idBankStatementMethod;
            log.bankstatementAplicationDate = model.bankstatementAplicationDate;
            log.bankstatementAppliedAmmount = model.bankstatementAppliedAmmount;
            log.bankstatementBankFee = model.bankstatementBankFee;
            log.bankstatementAppliedAmmountFinal = model.bankstatementAppliedAmmountFinal;
            log.bankStatementsTC = model.bankStatementsTC;
            log.bankStatementsAuthCode = model.bankStatementsAuthCode;

        }

        protected void prepareToSaveLog(tblbankstatements2 model, tblbankstatements2Log log)
        {
            log.Log_Date = DateTime.Now;
            log.idBankStatements2 = model.idBankStatements2;
            log.idBAccount = model.idBAccount;
            log.idMovementType = model.idMovementType;
            log.bankstatements2AplicationDate = model.bankstatements2AplicationDate;
            log.bankstatements2Concept = model.bankstatements2Concept;
            log.bankstatements2Charge = model.bankstatements2Charge;
            log.bankstatements2Pay = model.bankstatements2Pay;
            log.bankstatements2CreatedBy = model.bankstatements2CreatedBy;
            log.bankstatements2CreatedOn = model.bankstatements2CreatedOn;
            log.bankstatements2UpdatedBy = model.bankstatements2UpdatedBy;
            log.bankstatements2UpdatedOn = model.bankstatements2UpdatedOn;

        }
    }
}