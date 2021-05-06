using System;
using System.Collections.Generic;
using System.Linq;
using VTAworldpass.VTACore.Cores.Globales;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.incomes.model;
using VTAworldpass.VTAServices.Services.reports.model;
using static VTAworldpass.VTACore.Cores.Globales.Enumerables;
//using VTAworldpass.VTAServices.Services.utilsapp.model;

namespace VTAworldpass.VTACore.Cores.Converts
{
    public class GeneralConverts
    {
        public static income ConvertTbltoHelper(tblincome model)
        {
            income helper = new income(model.idincome, model.incomenumber, "", model.idcurrency, model.tblcurrencies.currencyAlphabeticCode, model.idcompany, model.tblcompanies.companyshortname, model.tblcompanies.tblsegment.idsegment, model.tblcompanies.tblsegment.segmentname, model.tblusers.idUser, model.tblusers1.idUser, model.tblincomeitem, model.incomeapplicationdate, model.incomecreactiondate, model.incometupdateon);
            helper.identifier = AccountsUtils.IdentifierComplete(helper.companyname, model.tblusers.tblprofilesaccounts.profileaccountshortame, helper.number);
            return helper;
        }

        public static incomeitem ConvertTbltoHelper(tblincomeitem incomeitem)
        {
            incomeitem helper = new incomeitem(incomeitem.idincomeitem, incomeitem.idincome, "", incomeitem.tblincome.tblcompanies.tblsegment.idsegment, incomeitem.tblincome.tblcompanies.tblsegment.segmentname, incomeitem.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1, incomeitem.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1description, incomeitem.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1order,
                                                incomeitem.tblaccountsl4.tblaccountsl3.tblaccountsl2.idaccountl2, incomeitem.tblaccountsl4.tblaccountsl3.tblaccountsl2.accountl2description, incomeitem.tblaccountsl4.tblaccountsl3.tblaccountsl2.accountl2order, incomeitem.tblaccountsl4.tblaccountsl3.idaccountl3,
                                                incomeitem.tblaccountsl4.tblaccountsl3.accountl3description, incomeitem.tblaccountsl4.tblaccountsl3.accountl3order, incomeitem.tblaccountsl4.idAccountl4, incomeitem.tblaccountsl4.accountl4description, incomeitem.tblaccountsl4.accountl4order, incomeitem.incomeitemdate, incomeitem.incomeitemsubtotal, incomeitem.incomedescription);
            helper.identifier = AccountsUtils.IdentifierComplete(incomeitem.tblincome.tblcompanies.companyshortname, incomeitem.tblusers.tblprofilesaccounts.profileaccountshortame, incomeitem.tblincome.incomenumber);
            return helper;
        }

        public static List<incomeitem> ConvertTbltoHelper(List<tblincomeitem> incomes)
        {
            List<incomeitem> list = new List<incomeitem>();
            var toread = incomes.ToArray();
            for (int i = 0; i <= incomes.Count() - 1; i++)
            {
                incomeitem helper = new incomeitem();
                helper = ConvertTbltoHelper(toread[i]);
                helper.index = i;
                helper.row = i + 1;
                list.Add(helper);
            }
            return list;
        }

        public static List<income> ConvertTbltoHelper(List<tblincome> incomes)
        {
            List<income> list = new List<income>();
            var toread = incomes.ToArray();
            for (int i = 0; i <= incomes.Count() - 1; i++)
            {
                income helper = new income();
                helper = ConvertTbltoHelper(toread[i]);
                helper.index = i; helper.row = i + 1; list.Add(helper);
            }
            return list;
        }

        public static expensereporitem ConvertTbltoHelper(tblinvoiceditem model)
        {
            expensereporitem helper = new expensereporitem();

            helper.accountl4 = model.tblaccountsl4.idAccountl4;
            helper.accountl4name = model.tblaccountsl4.accountl4description;
            helper.accountl3 = model.tblaccountsl4.tblaccountsl3.idaccountl3;
            helper.accountl3name = model.tblaccountsl4.tblaccountsl3.accountl3description;
            helper.accountl2 = model.tblaccountsl4.tblaccountsl3.tblaccountsl2.idaccountl2;
            helper.accountl2name = model.tblaccountsl4.tblaccountsl3.tblaccountsl2.accountl2description;
            helper.accountl1 = model.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1;
            helper.accountl1name = model.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1name;
            helper.typereport = model.tblaccountsl4.tblaccountl4accounttype.Select(y => y.idaccounttypereport).ToList();

            helper.ammounttotal = (decimal)model.itemsubtotal + (decimal)model.itemtax + (decimal)model.itemothertax;
            helper.ammountl4 = helper.ammounttotal;
            helper.ammountl3 = helper.ammounttotal;
            helper.ammountl2 = helper.ammounttotal;
            helper.ammountl1 = helper.ammounttotal;
            helper.ammount = (decimal)model.itemsubtotal;
            helper.taxes = (decimal)model.itemtax;
            helper.othertaxes = (decimal)model.itemothertax;
            helper.addDocitem(model);
            return helper;
        }

        /******************* Expense Reports *****************************************************************************/

        public static docpaymentreservexpreport ApplyPaymethodtoAccl4Convert(docpaymentreservexpreport model, IList<accountl4methodspay> listhelper)
        {

            if (listhelper.Count != 0)
            {
                var x = listhelper.Where(t => t.paymentmethod == model.PaymentMethod).FirstOrDefault();
                if (x != null)
                {
                    model.accountl1 = x.accountl1;
                    model.accountl1name = x.accl1name;
                    model.accountl1order = x.accountl1order;
                    model.accountl2 = x.accountl2;
                    model.accountl2name = x.accl2name;
                    model.accountl2order = x.accountl2order;
                    model.accountl3 = x.accountl3;
                    model.accountl3name = x.accl3name;
                    model.accountl3order = x.accountl3order;
                    model.accountl4 = x.accountl4;
                    model.accountl4name = x.accl4name;
                    model.accountl4order = x.accountl4order;
                }
                else
                {
                    model.accountl1 = 0;
                    model.accountl1name = "No commpatible";
                    model.accountl1order = 0;
                    model.accountl2 = 0;
                    model.accountl2name = "No commpatible";
                    model.accountl2order = 0;
                    model.accountl3 = 0;
                    model.accountl3name = "No commpatible";
                    model.accountl3order = 0;
                    model.accountl4 = 0;
                    model.accountl4name = "No commpatible";
                    model.accountl4order = 0;
                }
            }
            return model;
        }

        public static List<docpaymentreservexpreport> ApplyPaymethodtoAccl4Convert(List<docpaymentreservexpreport> listmodel, IList<accountl4methodspay> listhelper)
        {
            List<docpaymentreservexpreport> list = new List<docpaymentreservexpreport>();

            if (listhelper != null && listhelper.Count() != 0)
            {
                foreach (docpaymentreservexpreport model in listmodel)
                {
                    docpaymentreservexpreport helper = new docpaymentreservexpreport();
                    helper = ApplyPaymethodtoAccl4Convert(model, listhelper);
                    list.Add(helper);
                }
            }
            return list;
        }

        public static List<docpaymentreservexpreport> ApplyPaymethodtoAccl4Convert(IEnumerable<docpaymentreservexpreport> listmodel, IList<accountl4methodspay> listhelper)
        {
            List<docpaymentreservexpreport> list = new List<docpaymentreservexpreport>();

            foreach (docpaymentreservexpreport model in listmodel)
            {
                docpaymentreservexpreport helper = new docpaymentreservexpreport();
                helper = ApplyPaymethodtoAccl4Convert(model, listhelper);
                list.Add(helper);
            }
            return list;
        }

        //////////////////// PREPARE TO SAVE //////////////////////////////////////////////////

        public static tblincome PrepareToSave(income income)
        {
            tblincome model = new tblincome();
            model.idcompany = income.company;
            model.idcurrency = income.currency;
            model.incomeapplicationdate = (DateTime)DateTimeUtils.ParseStringToDate(income.applicationdatestring);
            model.incomecreactiondate = DateTime.Now;
            model.incometupdateon = DateTime.Now;
            return model;
        }

        public static tblincomeitem PrepareToSave(incomeitem item)
        {
            tblincomeitem model = new tblincomeitem();
            model.idincome = item.parent;
            model.idAccountl4 = item.accountl4;
            model.idincomeitemstatus = (int)InvoiceIncomeStatus.invoicedItemStatus_SinRevisar;
            model.incomeitemdate = DateTime.Now;
            model.incomeitemsubtotal = item.ammounttotal;
            model.incomedescription = item.description == null ? null : item.description.Length > Variables.MaxQuickDescriptionCharacters ? string.Concat((item.description).Substring(0, Variables.MaxQuickDescriptionCharacters), "...") : item.description;
            return model;
        }

        //////////////////// PREPARE TO UPDATE //////////////////////////////////////////////////

        public static void PrepareToUpdate(incomeitem item, tblincomeitem model)
        {
            model.idAccountl4 = item.accountl4;
            model.idincomeitemstatus = (int)InvoiceIncomeStatus.invoicedItemStatus_SinRevisar;
            model.incomeitemsubtotal = item.ammounttotal;
            model.incomedescription = item.description == null ? null : item.description.Length > Variables.MaxQuickDescriptionCharacters ? string.Concat((item.description).Substring(0, Variables.MaxQuickDescriptionCharacters), "...") : item.description;
        }

        public static void PrepareToUpdate(income item, tblincome model)
        {
            model.idcompany = item.company;
            model.incomeapplicationdate = (DateTime)DateTimeUtils.ParseStringToDate(item.applicationdatestring);
            model.incometupdateon = DateTime.Now;
            model.idcurrency = item.currency;
        }

        public static tblincomemovement PrepareToSave(incomepayment item)
        {
            tblincomemovement model = new tblincomemovement();
            model.idincome = item.parent;
            model.idbaccount = item.bankaccount;
            model.idbankaccnttype = item.bankaccnttype;
            model.idtpv = item.tpv == 0 ? null : (int?)item.tpv;
            model.incomemovcard = item.card;
            model.incomemovapplicationdate = (DateTime)DateTimeUtils.ParseStringToDate(item.aplicationdatestring);
            model.incomemovchargedamount = item.ammounttotal;
            model.incomemovauthref = item.description;
            model.incomemovcreationdate = DateTime.Now;
            model.incomemovupdatedon = DateTime.Now;

            return model;
        }

        public static incomepayment ConvertTbltoHelper(tblincomemovement item)
        {
            incomepayment helper = new incomepayment(item.idincomeMovement, item.idincome, "", item.tblincome.tblcompanies.tblsegment.idsegment, item.tblincome.tblcompanies.tblsegment.segmentname, item.tblincome.idcompany, item.tblincome.tblcompanies.companyshortname, (int)item.tblincome.tblcompanies.companyorder,
                item.tblbankaccount.idbaccount, string.Format("{0} {1} {2} {3}", item.tblbankaccount.baccountshortname, item.tblbankaccount.tblbank.bankshortname, item.tblbankaccount.tblcurrencies.currencyAlphabeticCode, item.tblbankaccount.tblcompanies.companyshortname), item.idbankaccnttype, item.tblbankprodttype.bankprodttypeshortname,
                item.tblbankaccount.tblcurrencies.idCurrency, item.tblbankaccount.tblcurrencies.currencyAlphabeticCode, item.idtpv == null ? 0 : (int)item.idtpv, item.idtpv == null ? "" : item.tbltpv.tpvidlocation, item.incomemovcard, item.incomemovchargedamount, item.incomemovauthref, item.incomemovcreationdate, item.incomemovapplicationdate, item.incomemovauthcode);
            helper.identifier = AccountsUtils.IdentifierComplete(item.tblincome.tblcompanies.companyshortname, item.tblincome.tblusers.tblprofilesaccounts.profileaccountshortame, item.tblincome.incomenumber);
            return helper;
        }

        public static List<incomepayment> ConvertTbltoHelper(List<tblincomemovement> items)
        {
            List<incomepayment> list = new List<incomepayment>();
            var toread = items.ToArray();
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                incomepayment helper = new incomepayment();
                helper = ConvertTbltoHelper(toread[i]);
                helper.index = i;
                helper.row = i + 1;
                list.Add(helper);
            }
            return list;
        }
    }
}