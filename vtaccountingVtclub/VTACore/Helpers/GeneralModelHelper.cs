using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.bankreconciliation.model;
using VTAworldpass.VTAServices.Services.incomes.model;
using VTAworldpass.VTAServices.Services.invoices.model;
using VTAworldpass.VTAServices.Services.Models.commons;
using VTAworldpass.VTAServices.Services.reports.model;
using static VTAworldpass.VTACore.Cores.Globales.Enumerables;
//using VTAworldpass.VTAServices.Services.utilsapp.model;
using static VTAworldpass.VTACore.Helpers.Enum;

namespace VTAworldpass.VTACore.Helpers
{
    public class GeneralModelHelper
    {
        public static UnitOfWork unity = new UnitOfWork();

        public static income ConvertTbltoHelper(tblincome model)
        {
            income helper = new income(model.idincome, model.incomenumber, "", model.idcurrency, model.tblcurrencies.currencyAlphabeticCode, model.idcompany, model.tblcompanies.companyshortname, model.tblcompanies.tblsegment.idsegment, model.tblcompanies.tblsegment.segmentname, model.tblusers.idUser, model.tblusers1.idUser, model.tblincomeitem, model.incomeapplicationdate, model.incomecreactiondate, model.incometupdateon);
            helper.identifier = AccountsUtils.IdentifierComplete(helper.companyname, model.tblusers.tblprofilesaccounts.profileaccountshortame, helper.number);
            return helper;
        }

        //internal static IEnumerable<bankreconciliation> ConvertTbltoHelper(object p, bool sourcedata, bool financialstateitem)
        //{
        //    throw new NotImplementedException();
        //}

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
            model.incomedescription = item.description == null ? null : item.description.Length > Globals.MaxQuickDescriptionCharacters ? string.Concat((item.description).Substring(0, Globals.MaxQuickDescriptionCharacters), "...") : item.description;
            return model;
        }

        //////////////////// PREPARE TO UPDATE //////////////////////////////////////////////////

        public static void PrepareToUpdate(incomeitem item, tblincomeitem model)
        {
            model.idAccountl4 = item.accountl4;
            model.idincomeitemstatus = (int)InvoiceIncomeStatus.invoicedItemStatus_SinRevisar;
            model.incomeitemsubtotal = item.ammounttotal;
            model.incomedescription = item.description == null ? null : item.description.Length > Globals.MaxQuickDescriptionCharacters ? string.Concat((item.description).Substring(0, Globals.MaxQuickDescriptionCharacters), "...") : item.description;
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
            model.incomemovauthcode = item.authcode;
            return model;
        }

        public static incomepayment ConvertTbltoHelper(tblincomemovement item)
        {
            incomepayment helper = new incomepayment(item.idincomeMovement, item.idincome, "", item.tblincome.tblcompanies.tblsegment.idsegment, item.tblincome.tblcompanies.tblsegment.segmentname, item.tblincome.idcompany, item.tblincome.tblcompanies.companyshortname, (int)item.tblincome.tblcompanies.companyorder,
                item.tblbankaccount.idbaccount, string.Format("{0} {1} {2} {3}", item.tblbankaccount.baccountshortname, item.tblbankaccount.tblbank.bankshortname, item.tblbankaccount.tblcurrencies.currencyAlphabeticCode, item.tblbankaccount.tblcompanies.companyshortname), item.idbankaccnttype, item.tblbankprodttype.bankprodttypeshortname,
                item.tblbankaccount.tblcurrencies.idCurrency, item.tblbankaccount.tblcurrencies.currencyAlphabeticCode, item.idtpv == null ? 0 : (int)item.idtpv, item.idtpv == null ? "" : item.tbltpv.tpvidlocation, item.incomemovcard, item.incomemovchargedamount, item.incomemovauthref, item.incomemovcreationdate, item.incomemovapplicationdate,item.incomemovauthcode);
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

        public static List<docpaymentreserv> ConvertTbltoHelper(List<tblreservationspayment> list)
        {
            List<docpaymentreserv> listModel = new List<docpaymentreserv>();
            foreach (tblreservationspayment model in list)
            {
                docpaymentreserv helper = new docpaymentreserv();
                helper = ConvertTbltoHelper(model);
                listModel.Add(helper);
            }
            return listModel;
        }

        public static docpaymentreserv ConvertTbltoHelper(tblreservationspayment model)
        {

            docpaymentreserv helper = new docpaymentreserv();
            helper.ReservationPayment = model.idReservationPayment;
            helper.Reservation = (int)model.idReservation;
            helper.reservationPaymentQuantity = model.reservationPaymentCost == null ? 0 : (decimal)model.reservationPaymentCost;
            helper.reservationPaymentQuantityString = MoneyUtils.ParseDecimalToString(helper.reservationPaymentQuantity);
            helper.reservationPaymentDate = (DateTime)model.reservationPaymentDate;
            helper.reservationPaymentDateString = DateTimeUtils.ParseDatetoString(model.reservationPaymentDate);
            helper.CurrencyPay = (int)model.idCurrency;
            helper.CurrencyPayName = getCurrencyName((int)model.idCurrency);
            helper.PaymentMethod = (int)model.idPaymentType;
            helper.Hotel = (int)model.tblreservations.idPartner;
            helper.HotelName = getHotelPartenerName((int)model.tblreservations.idPartner);
            helper.PaymentMethodName = getPaymentMethodName((int)model.idPaymentType);
            helper.authRef = string.Format("Datos de reserva: Cliente: {0} , Hotel partner: {1}", model.tblreservations.reservationGuestName, getHotelPartenerName((int)model.tblreservations.idPartner));
            helper.authcode = model.authCode == null ? "0" : model.authCode;

            return helper;
        }

        public static List<docpaymentreserv> ConvertTbltoHelper(List<tblreservations> list)
        {
            List<docpaymentreserv> listModel = new List<docpaymentreserv>();
            foreach (tblreservations model in list)
            {
                docpaymentreserv helper = new docpaymentreserv();
                helper = ConvertTbltoHelper(model);
                listModel.Add(helper);
            }
            return listModel;
        }

        public static docpaymentreserv ConvertTbltoHelper(tblreservations model)
        {

            docpaymentreserv helper = new docpaymentreserv();
            helper.ReservationPayment = model.idReservation;
            helper.Reservation = (int)model.idReservation;
            helper.reservationPaymentQuantity = model.reservationSellPrice == null ? 0 : (decimal)model.reservationSellPrice;
            helper.reservationPaymentQuantityString = MoneyUtils.ParseDecimalToString(helper.reservationPaymentQuantity);
            helper.reservationPaymentDate = (DateTime)model.purchaseDate;
            helper.reservationPaymentDateString = DateTimeUtils.ParseDatetoString(model.purchaseDate);
            helper.CurrencyPay = (int)Currencies.US_Dollar;
            helper.CurrencyPayName = "USD";
            helper.PaymentMethod = (int)ReservaWeb.PaymentType;
            helper.Hotel = (int)model.idPartner;
            helper.HotelName = getHotelPartenerName((int)model.idPartner);
            helper.PaymentMethodName = getPaymentMethodName((int)ReservaWeb.PaymentType);
            helper.authRef = string.Format("Datos de reserva: Cliente: {0} , Hotel partner: {1}", model.reservationGuestName, getHotelPartenerName((int)model.idPartner));
            helper.authcode = "";

            return helper;
        }

        public static string getHotelPartenerName(int partner)
        {
            var lst = unity.PartnersRepository.Get(x => x.idPartner == partner && x.partnerActive == Globals.ActiveRecord, null, "").FirstOrDefault();

            return lst.partnerName;
        }

        public static string getHotelChainName(int partner)
        {
            var lst = unity.HotelChainsRepository.Get(x => x.idHotelChain == partner && x.hotelChainActive == Globals.ActiveRecord, null, "").FirstOrDefault();

            return lst.hotelChainName;
        }

        public static string getPaymentMethodName(int idPaymentMethod)
        {
            var lst = unity.PaymentFormsRepository.Get(x => x.idPaymentForm == idPaymentMethod && x.paymentFormActive == Globals.ActiveRecord, null, "").FirstOrDefault();

            return lst.paymentFormName;
        }

        public static string getCurrencyName(int currency)
        {
            var lst = unity.CurrencyRepository.Get(x => x.idCurrency == currency, null, "").FirstOrDefault();

            return lst.currencyAlphabeticCode;
        }

        public static decimal getPrefixAmount(string prefix, int hotelchain)
        {
            var amountPrefix = 0m;

            var amount = unity.PrefixesRepository.Get(x => x.prefixAbbrev == prefix && x.idHotelChain == hotelchain, null, "").FirstOrDefault();
            if(amount != null)
            {
                amountPrefix = amount.PartnerPrice != null ?(decimal)amount.PartnerPrice : 0m;
            }
            return amountPrefix;
        }

        public static List<bankstatements> ConvertTbltoHelper(List<tblbankstatements2> listmodel, bool sourcedata, bool financialstateitem)
        {
            List<bankstatements> listhelper = new List<bankstatements>();

            if (listmodel != null && listmodel.Count() != 0)
            {
                foreach (tblbankstatements2 model in listmodel)
                {
                    bankstatements helper = new bankstatements();
                    helper = ConvertTbltoHelper(model, sourcedata, financialstateitem);
                    listhelper.Add(helper);
                }
            }

            return listhelper;
        }


        public static bankstatements ConvertTbltoHelper(tblbankstatements2 model, bool sourcedata, bool financialstateitem)
        {
            bankstatements helper = new bankstatements();

            helper.idBankStatements2 = model.idBankStatements2;
            helper.idBAccount = model.tblbankaccount.idbaccount;
            helper.baccountName = model.tblbankaccount.baccountshortname;
            helper.currency = model.tblbankaccount.tblcurrencies.idCurrency;
            helper.currencyName = model.tblbankaccount.tblcurrencies.currencyAlphabeticCode;
            helper.idMovementType = model.idMovementType;
            helper.MovementTypeName = model.tblmovementtype.movementTypeName;
            helper.bankstatementsAplicationDateString = DateTimeUtils.ParseDatetoString(model.bankstatements2AplicationDate);
            helper.bankstatementsAplicationDate = model.bankstatements2AplicationDate;
            helper.bankstatementsCargo = model.bankstatements2Charge * -1;
            helper.bankstatementsAbono = model.bankstatements2Pay;
            helper.bankstatementsConcept = model.bankstatements2Concept;

            return helper;
        }

        public static List<bankstatements> ConvertTblToHelper(List<tblbankstatements2> listmodel, bool sourcedata, bool financialstateitem)
        {
            List<bankstatements> listhelper = new List<bankstatements>();

            if (listmodel != null && listmodel.Count() != 0)
            {
                foreach (tblbankstatements2 model in listmodel)
                {
                    bankstatements helper = new bankstatements();
                    helper = ConvertTblToHelper(model, sourcedata, financialstateitem);
                    listhelper.Add(helper);
                }
            }

            return listhelper;
        }


        public static bankstatements ConvertTblToHelper(tblbankstatements2 model, bool sourcedata, bool financialstateitem)
        {
            bankstatements helper = new bankstatements(
            model.idBankStatements2,
            //model.tblbankaccount.tblbaccounttpv.FirstOrDefault().idTPV,
            //model.tblbankaccount.tblbaccounttpv.FirstOrDefault().tbltpv.tpvIdLocation,
            model.tblbankaccount.idcompany,
            model.tblbankaccount.tblcompanies.companyname,
            model.tblbankaccount.idbaccount,
            model.tblbankaccount.baccountshortname,
            model.tblbankaccount.tblcurrencies.idCurrency,
            model.tblbankaccount.tblcurrencies.currencyAlphabeticCode,
            model.idMovementType,
            model.tblmovementtype.movementTypeName,
            DateTimeUtils.ParseDatetoString(model.bankstatements2AplicationDate),
            model.bankstatements2AplicationDate,
            model.bankstatements2Charge * -1,
            model.bankstatements2Pay,
            model.bankstatements2Concept, model.idBankStatementMethod,model.bankstatements2BankFee, sourcedata, financialstateitem
            );
            return helper;
        }


        public static List<bankreconciliationModel> ConvertTbltoHelper(List<tblbankstatements> listmodel, bool sourcedata, bool financialstateitem)
        {
            List<bankreconciliationModel> listhelper = new List<bankreconciliationModel>();

            if (listmodel != null && listmodel.Count() != 0)
            {
                foreach (tblbankstatements model in listmodel)
                {
                    bankreconciliationModel helper = new bankreconciliationModel();
                    helper = ConvertTbltoHelper(model, sourcedata, financialstateitem);
                    listhelper.Add(helper);
                }
            }

            return listhelper;
        }

        public static bankreconciliationModel ConvertTbltoHelper(tblbankstatements model, bool sourcedata, bool financialstateitem)
        {

            bankreconciliationModel helper = new bankreconciliationModel(
            model.idBankStatements,
            model.tbltpv.idtpv,
            model.tbltpv.tpvidlocation,
            model.tblcompanies.idcompany,
            model.tblcompanies.companyshortname,
            /*model.tblcompanies.tblcompanyhotel.Count() != 0 ? model.tblcompanies.tblcompanyhotel.First().idHotel :*/ 0,
            /*model.tblcompanies.tblcompanyhotel.Count() != 0 ? model.tblcompanies.tblcompanyhotel.First().tblhotels.hotelShortName :*/ "",
            model.tblbankaccount.idbaccount,
            model.tblbankaccount.baccountshortname,
            model.tblbankaccount.tblcurrencies.idCurrency,
            model.tblbankaccount.tblcurrencies.currencyAlphabeticCode,
            DateTimeUtils.ParseDatetoString(model.bankstatementAplicationDate),
            model.bankstatementAplicationDate,
            model.bankstatementAppliedAmmount,
            model.bankstatementBankFee,
            model.bankstatementAppliedAmmountFinal,
            (int)model.idBankStatementMethod,
            sourcedata, financialstateitem);

            return helper;
        }

        public static List<docpaymentparentreserv> ConvertTbltoHelper(List<tblreservationsparentpayment> list)
        {
            List<docpaymentparentreserv> listModel = new List<docpaymentparentreserv>();
            foreach (tblreservationsparentpayment model in list)
            {
                docpaymentparentreserv helper = new docpaymentparentreserv();
                helper = ConvertTbltoHelper(model);
                listModel.Add(helper);
            }
            return listModel;
        }

        public static docpaymentparentreserv ConvertTbltoHelper(tblreservationsparentpayment model)
        {

            docpaymentparentreserv helper = new docpaymentparentreserv();
            var partnerName = getHotelPartenerName((int)model.tblreservationsparent.idPartner);

            helper.ReservationPayment = model.idReservationParentPayment;
            helper.Reservation = (int)model.idReservationParent;
            helper.reservationPaymentQuantity = model.reservationPaymentCost == null ? 0 : (decimal)model.reservationPaymentCost;
            helper.reservationPaymentQuantityString = MoneyUtils.ParseDecimalToString(helper.reservationPaymentQuantity);
            helper.reservationPaymentDate = (DateTime)model.reservationPaymentDate;
            helper.reservationPaymentDateString = DateTimeUtils.ParseDatetoString(model.reservationPaymentDate);
            helper.CurrencyPay = (int)model.idCurrency;
            helper.CurrencyPayName = getCurrencyName((int)model.idCurrency);
            helper.PaymentMethod = (int)model.idPaymentType;
            helper.Hotel = (int)model.tblreservationsparent.idPartner;
            helper.HotelName = partnerName;
            helper.PaymentMethodName = getPaymentMethodName((int)model.idPaymentType);
            helper.authRef = string.Format("Datos de reserva: Cliente: {0} , Hotel partner: {1}", model.tblreservationsparent.reservationParentGuestName, partnerName);
            helper.authcode = model.authCode == null ? "0" : model.authCode;

            return helper;
        }

        public static List<docpaymentpurchase> ConvertTbltoHelper(List<tblpaymentspurchases> list)
        {
            List<docpaymentpurchase> listModel = new List<docpaymentpurchase>();
            foreach (tblpaymentspurchases model in list)
            {
                docpaymentpurchase helper = new docpaymentpurchase();
                helper = ConvertTbltoHelper(model);
                listModel.Add(helper);
            }
            return listModel;
        }

        public static List<docpaymentincome> ConvertTbltoHelper(List<tblincomemovement> list, string dummy)
        {
            List<docpaymentincome> listModel = new List<docpaymentincome>();

            foreach (tblincomemovement model in list)
            {
                docpaymentincome helper = new docpaymentincome();
                helper = ConvertTbltoHelper(model, string.Empty);
                listModel.Add(helper);
            }

            return listModel;
        }

        public static docpaymentincome ConvertTbltoHelper(tblincomemovement model, string dummy)
        {
            docpaymentincome helper = new docpaymentincome();
            helper.BankAccntType = model.idbankaccnttype;
            helper.BankAccntTypeName = model.tblbankprodttype.bankprodttypename;
            helper.BankAccount = model.idbaccount;
            helper.BankAccountName = model.tblbankaccount.baccountdescription;
            helper.Company = model.tblincome.idcompany;
            helper.CompanyName = model.tblincome.tblcompanies.companyname;
            helper.CompanyOrder = model.tblincome.tblcompanies.companyorder ?? 0;
            helper.CurrencyPay = model.tblincome.idcurrency;
            helper.CurrencyPayName = model.tblincome.tblcurrencies.currencyAlphabeticCode;
            helper.Terminal = model.idtpv ?? 0;
            helper.Income = model.idincome;
            helper.incomemovapplicationDate = model.incomemovapplicationdate;
            helper.incomemovapplicationDateString = DateTimeUtils.ParseDatetoString(model.incomemovapplicationdate);
            helper.incomemovauthCode = model.incomemovauthcode;
            helper.incomemovauthRef = model.incomemovauthref;
            helper.incomemovcard = model.incomemovcard;
            helper.incomemovchargedAmount = model.incomemovchargedamount;
            helper.incomemovchargedAmountString = MoneyUtils.ParseDecimalToString(model.incomemovchargedamount);
            helper.IncomeMovement = model.idincomeMovement;
            helper.identifier = AccountsUtils.IdentifierComplete(model.tblincome.tblcompanies.companyshortname, model.tblincome.tblusers.tblprofilesaccounts.profileaccountshortame, model.tblincome.incomenumber);

            return helper;
        }

        public static docpaymentpurchase ConvertTbltoHelper(tblpaymentspurchases model)
        {

            docpaymentpurchase helper = new docpaymentpurchase();
            var partnerName = getHotelPartenerName(model.tblpurchases.idPartner);

            helper.Payment = model.idPaymentPurchase;
            helper.Purchase = (int)model.idPurchase;
            helper.paymentAmount = model.paymentCost == null ? 0 : (decimal)model.paymentCost;
            helper.paymentAmountString = MoneyUtils.ParseDecimalToString(helper.paymentAmount);
            helper.paymentDate = (DateTime)model.paymentDate;
            helper.paymentDateString = DateTimeUtils.ParseDatetoString(model.paymentDate);
            helper.CurrencyPay = (int)model.idCurrency;
            helper.CurrencyPayName = getCurrencyName((int)model.idCurrency);
            helper.PaymentMethod = (int)model.idPaymentType;
            helper.Hotel = model.tblpurchases.idPartner;
            helper.HotelName = partnerName;
            helper.PaymentMethodName = getPaymentMethodName((int)model.idPaymentType);
            helper.authRef = string.Format("Datos de reserva: Cliente: {0}{1} , Hotel partner: {2}", model.tblpurchases.tblmemberships.tblmembers.FirstOrDefault().memberFirstName, model.tblpurchases.tblmemberships.tblmembers.FirstOrDefault().memberLastName, partnerName);
            helper.authcode = model.authCode == null ? "0" : model.authCode;

            return helper;
        }

        public static tblbankstatements PrepareToSave(bankreconciliationModel helper)
        {
            tblbankstatements model = new tblbankstatements();
            model.idTPV = helper.idTPV;
            model.idCompany = helper.idCompany;
            model.idBAccount = helper.idBAccount;
            model.bankstatementAplicationDate = helper.bankstatementAplicationDate;
            model.bankstatementAppliedAmmount = helper.bankstatementAppliedAmmount;
            model.bankstatementBankFee = helper.bankstatementBankFee;
            model.bankstatementAppliedAmmountFinal = helper.bankstatementAppliedAmmountFinal;
            model.idBankStatementMethod = helper.methodconciliation;
            return model;
        }

        public static List<invoiceitems> ConvertTbltoHelper(IEnumerable<tblinvoiceditem> listmodel)
        {
            List<invoiceitems> list = new List<invoiceitems>();
            foreach (tblinvoiceditem model in listmodel)
            {
                invoiceitems helper = new invoiceitems();
                helper = ConvertTbltoHelperItem(model);
                list.Add(helper);
            }
            return list;
        }

        public static invoiceitems ConvertTbltoHelperItem(tblinvoiceditem model)
        {
            invoiceitems helper = new invoiceitems();
            helper.identifier = AccountsUtils.IdentifierComplete(model.tblinvoice.tblcompanies.companyshortname, model.tblinvoice.tblusers.tblprofilesaccounts.profileaccountshortame, model.tblinvoice.invoicenumber);
            helper.item = model.idinvoiceitem;
            helper.id = model.idinvoice;
            helper.company = model.tblinvoice.idcompany;
            helper.companyname = model.tblinvoice.tblcompanies.companyshortname;
            helper.aplicationdate = model.tblinvoice.invoicedate;
            helper.istax = model.ditemistax;
            helper.accountl1 = model.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.idaccountl1;
            helper.accountl1name = model.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1description;
            helper.accountl1order = model.tblaccountsl4.tblaccountsl3.tblaccountsl2.tblaccountsl1.accountl1order;
            helper.accountl2 = model.tblaccountsl4.tblaccountsl3.tblaccountsl2.idaccountl2;
            helper.accountl2name = model.tblaccountsl4.tblaccountsl3.tblaccountsl2.accountl2description;
            helper.accountl2order = model.tblaccountsl4.tblaccountsl3.tblaccountsl2.accountl2order;
            helper.accountl3 = model.tblaccountsl4.tblaccountsl3.idaccountl3;
            helper.accountl3name = model.tblaccountsl4.tblaccountsl3.accountl3description;
            helper.accountl3order = model.tblaccountsl4.tblaccountsl3.accountl3order;
            helper.accountl4 = model.tblaccountsl4.idAccountl4;
            helper.accountl4name = model.tblaccountsl4.accountl4description;
            helper.category = model.tblaccountsl4.tblaccountsl3.idaccountl3;
            helper.categoryName = model.tblaccountsl4.tblaccountsl3.accountl3description;
            helper.type = model.tblaccountsl4.idAccountl4;
            helper.typename = model.tblaccountsl4.accountl4description;
            helper.accountl4order = model.tblaccountsl4.accountl4order;
            helper.ammount = model.itemsubtotal == null ? 0L : model.itemsubtotal;
            helper.taxesammount = model.itemtax;
            helper.othertaxesammount = model.itemothertax;
            helper.description = model.itemdescription;
            helper.billidentifier = model.itemidentifier == null ? "" : model.itemidentifier;
            helper.ammounttotal = ((decimal)helper.ammount + helper.taxesammount + helper.othertaxesammount);
            helper.status = model.tblinvoiceitemstatus.idinvoiceItemstatus;
            helper.statusname = model.tblinvoiceitemstatus.invoicestatusname;
            helper.isverified = model.idinvoiceitemstatus == 1 ? false : model.idinvoiceitemstatus == 2 ? true : true;
            helper.suppliername = model.tblSuppliers.idSupplier != 1 ? model.tblSuppliers.supplierName : model.itemsupplierother == null ? " " : model.itemsupplierother;
            helper.singlexibitionpayment = model.itemsinglepayment;
            helper.generateStringAmmounts();
            helper.generateStringDates();
            return helper;
        }

        public static List<docpaymentpurchasexpreport> ConvertTbltoHelper(List<tblpaymentspurchases> listmodel, IList<accountl4methodspay> listhelper)
        {
            List<docpaymentpurchasexpreport> list = new List<docpaymentpurchasexpreport>();

            if (listhelper != null && listhelper.Count() != 0)
            {
                foreach (tblpaymentspurchases model in listmodel)
                {
                    docpaymentpurchasexpreport helper = new docpaymentpurchasexpreport();
                    helper = ConvertTbltoHelper(model, listhelper);
                    list.Add(helper);

                }
            }

            return list;
        }

        public static docpaymentpurchasexpreport ConvertTbltoHelper(tblpaymentspurchases model, IList<accountl4methodspay> listhelper)
        {
            docpaymentpurchasexpreport helper = new docpaymentpurchasexpreport();
            var partnerName = getHotelPartenerName(model.tblpurchases.idPartner);

            helper.Payment = model.idPaymentPurchase;
            helper.Purchase = (int)model.idPurchase;
            helper.PaymentMethod = (int)model.idPaymentType;
            helper.PaymentMethodName = getPaymentMethodName((int)model.idPaymentType);
            helper.CurrencyPay = (int)model.idCurrency;
            helper.CurrencyPayName = getCurrencyName((int)model.idCurrency);
            helper.paymentDate = (DateTime)model.paymentDate;
            helper.paymentDateString = DateTimeUtils.ParseDatetoString(model.paymentDate);
            helper.paymentAmount = model.paymentCost == null ? 0m : (decimal)model.paymentCost;
            helper.paymentAmountString = MoneyUtils.ParseDecimalToString(helper.paymentAmount);
            helper.Hotel = model.tblpurchases.idPartner;
            helper.HotelName = model.tblpurchases.tblpartners.partnerName;
            helper.authcode = model.authCode;
            helper.exchangerate = model.exchangeRate == null ? 0m : (decimal)model.exchangeRate;
            helper.carddigit = model.cardDigits == null ? 0 : (long)model.cardDigits;
            helper.terminal = model.terminal == null ? 0 :(int)model.terminal;
            helper.authRef = string.Format("Datos de Purchase: Cliente: {0}{1} , Hotel partner: {2}", model.tblpurchases.tblmemberships.tblmembers.FirstOrDefault().memberFirstName, model.tblpurchases.tblmemberships.tblmembers.FirstOrDefault().memberLastName, partnerName);

            if (listhelper.Count != 0)
            {
                var x = listhelper.Where(t => t.paymentmethod == helper.PaymentMethod).FirstOrDefault();
                if (x != null)
                {
                    helper.accountl1 = x.accountl1;
                    helper.accountl1Name = x.accl1name;
                    helper.accountl1Order = x.accountl1order;
                    helper.accountl2 = x.accountl2;
                    helper.accountl2Name = x.accl2name;
                    helper.accountl2Order = x.accountl2order;
                    helper.accountl3 = x.accountl3;
                    helper.accountl3Name = x.accl3name;
                    helper.accountl3Order = x.accountl3order;
                    helper.accountl4 = x.accountl4;
                    helper.accountl4Name = x.accl4name;
                    helper.accountl4Order = x.accountl4order;
                }
            }
            return helper;
        }

        public static List<docpaymentpurchasexpreport> ConvertTbltoHelper(List<tblpurchases> listmodel, IList<accountl4methodspay> listhelper)
        {
            List<docpaymentpurchasexpreport> list = new List<docpaymentpurchasexpreport>();

            if (listhelper != null && listhelper.Count() != 0)
            {
                foreach (tblpurchases model in listmodel)
                {
                    docpaymentpurchasexpreport helper = new docpaymentpurchasexpreport();
                    helper = ConvertTbltoHelper(model, listhelper);
                    list.Add(helper);

                }
            }

            return list;
        }

        public static List<docpaymentpurchasexpreport> ConvertTbltoHelper(List<tblbatchdetail> listmodel, IList<accountl4methodspay> listhelper)
        {
            List<docpaymentpurchasexpreport> list = new List<docpaymentpurchasexpreport>();

            if (listhelper != null && listhelper.Count() != 0)
            {
                foreach (tblbatchdetail model in listmodel)
                {
                    docpaymentpurchasexpreport helper = new docpaymentpurchasexpreport();
                    helper = ConvertTbltoHelper(model, listhelper);
                    list.Add(helper);

                }
            }

            return list;
        }

        public static docpaymentpurchasexpreport ConvertTbltoHelper(tblbatchdetail model, IList<accountl4methodspay> listhelper)
        {
            docpaymentpurchasexpreport helper = new docpaymentpurchasexpreport();
            //var partnerName = getHotelPartenerName(model.tblpurchases.idPartner);
            var ammount = getPrefixAmount(model.tblpurchases.tblmemberships.membershipNumberPref, model.tblpurchases.tblpartners.idHotelChain);
            helper.Payment = (int)model.idBatch;
            helper.Purchase = (int)model.idPurchase;
            helper.saleType = model.tblpurchases.saleType != null ? (int)model.tblpurchases.saleType : 0;
            helper.saleTypeName = getSaleTypeName((int)model.tblpurchases.saleType);
            helper.PaymentMethod = (int)model.tblbatch.idPaymentForm;
            helper.PaymentMethodName = getPaymentMethodName((int)model.tblbatch.idPaymentForm);
            helper.CurrencyPay = (int)model.tblbatch.tblbankaccount.idcurrency;
            helper.CurrencyPayName = model.tblbatch.tblbankaccount.tblcurrencies.currencyAlphabeticCode;
            helper.paymentDate = (DateTime)model.tblbatch.datePay;
            helper.paymentDateString = DateTimeUtils.ParseDatetoString(helper.paymentDate);
            helper.paymentAmount = model.batchmonto == null ? ammount : (decimal)model.batchmonto;
            helper.paymentAmountString = MoneyUtils.ParseDecimalToString(helper.paymentAmount);
            helper.Hotel = model.tblpurchases.idPartner;
            helper.HotelName = model.tblpurchases.tblpartners.partnerName;
            helper.authcode = "";
            helper.exchangerate = 0m;
            helper.carddigit = 0;
            helper.terminal = 0;
            helper.authRef = string.Format("Post-Batch Datos de Purchase {0}: Cliente: {1}{2} , Hotel partner: {3}", helper.saleTypeName, model.tblpurchases.tblmemberships.tblmembers.FirstOrDefault().memberFirstName, model.tblpurchases.tblmemberships.tblmembers.FirstOrDefault().memberLastName, model.tblpurchases.tblpartners.partnerName);

            if (listhelper.Count != 0)
            {
                var x = listhelper.Where(t => t.paymentmethod == helper.PaymentMethod).FirstOrDefault();
                if (x != null)
                {
                    helper.accountl1 = x.accountl1;
                    helper.accountl1Name = x.accl1name;
                    helper.accountl1Order = x.accountl1order;
                    helper.accountl2 = x.accountl2;
                    helper.accountl2Name = x.accl2name;
                    helper.accountl2Order = x.accountl2order;
                    helper.accountl3 = x.accountl3;
                    helper.accountl3Name = x.accl3name;
                    helper.accountl3Order = x.accountl3order;
                    helper.accountl4 = x.accountl4;
                    helper.accountl4Name = x.accl4name;
                    helper.accountl4Order = x.accountl4order;
                }
            }
            return helper;
        }

        public static List<docpaymentpurchasexpreport> ConvertTbltoHelper(List<tblbatchdetailpre> listmodel, IList<accountl4methodspay> listhelper)
        {
            List<docpaymentpurchasexpreport> list = new List<docpaymentpurchasexpreport>();

            if (listhelper != null && listhelper.Count() != 0)
            {
                foreach (tblbatchdetailpre model in listmodel)
                {
                    docpaymentpurchasexpreport helper = new docpaymentpurchasexpreport();
                    helper = ConvertTbltoHelper(model, listhelper);
                    list.Add(helper);

                }
            }

            return list;
        }

        public static docpaymentpurchasexpreport ConvertTbltoHelper(tblbatchdetailpre model, IList<accountl4methodspay> listhelper)
        {
            docpaymentpurchasexpreport helper = new docpaymentpurchasexpreport();
            //var partnerName = getHotelPartenerName(model.tblpurchases.idPartner);
            //var ammount = getPrefixAmount(model.tblpurchases.tblmemberships.membershipNumberPref, model.tblpurchases.tblpartners.idHotelChain);
            helper.Payment = (int)model.idBatch;
            helper.Purchase = 0;
            helper.saleType = 0;
            helper.saleTypeName = "";
            helper.PaymentMethod = (int)model.tblbatch.idPaymentForm;
            helper.PaymentMethodName = getPaymentMethodName((int)model.tblbatch.idPaymentForm);
            helper.CurrencyPay = (int)model.tblbatch.tblbankaccount.idcurrency;
            helper.CurrencyPayName = model.tblbatch.tblbankaccount.tblcurrencies.currencyAlphabeticCode;
            helper.paymentDate = (DateTime)model.tblbatch.datePay;
            helper.paymentDateString = DateTimeUtils.ParseDatetoString(helper.paymentDate);
            helper.paymentAmount = model.tblbatch.batchMonto == null ? (decimal)model.totalPayment : (decimal)model.tblbatch.batchMonto;
            helper.paymentAmountString = MoneyUtils.ParseDecimalToString(helper.paymentAmount);
            helper.Hotel = model.tblpartners.idPartner;
            helper.HotelName = model.tblpartners.partnerName;
            helper.authcode = "";
            helper.exchangerate = 0m;
            helper.carddigit = 0;
            helper.terminal = 0;
            helper.authRef = string.Format("Pre-Batch Datos de Purchase : Hotel partner: {0}",  model.tblpartners.partnerName);

            if (listhelper.Count != 0)
            {
                var x = listhelper.Where(t => t.paymentmethod == helper.PaymentMethod).FirstOrDefault();
                if (x != null)
                {
                    helper.accountl1 = x.accountl1;
                    helper.accountl1Name = x.accl1name;
                    helper.accountl1Order = x.accountl1order;
                    helper.accountl2 = x.accountl2;
                    helper.accountl2Name = x.accl2name;
                    helper.accountl2Order = x.accountl2order;
                    helper.accountl3 = x.accountl3;
                    helper.accountl3Name = x.accl3name;
                    helper.accountl3Order = x.accountl3order;
                    helper.accountl4 = x.accountl4;
                    helper.accountl4Name = x.accl4name;
                    helper.accountl4Order = x.accountl4order;
                }
            }
            return helper;
        }

        public static string getSaleTypeName(int saletype)
        {
            string name = "";
            switch (saletype)
            {
                case (int)PurchaseSaleType.New:
                    name = PurchaseSaleType.New.ToString();
                    break;
                case (int)PurchaseSaleType.Renewed:
                    name = PurchaseSaleType.Renewed.ToString();
                    break;
                case (int)PurchaseSaleType.Upgrade:
                    name = PurchaseSaleType.Upgrade.ToString();
                    break;
                default:
                    break;
            }

            return name;
        }

        public static docpaymentpurchasexpreport ConvertTbltoHelper(tblpurchases model, IList<accountl4methodspay> listhelper)
        {
            docpaymentpurchasexpreport helper = new docpaymentpurchasexpreport();
            var partnerName = getHotelPartenerName(model.idPartner);
            var ammount = getPrefixAmount(model.tblmemberships.membershipNumberPref, model.tblpartners.idHotelChain);
            helper.Payment = model.idPurchase;
            helper.Purchase = (int)model.idPurchase;
            helper.saleType = model.saleType != null ?(int)model.saleType : 0;
            helper.saleTypeName = getSaleTypeName((int)model.saleType);
            helper.PaymentMethod = (int)PaymentMethods_Bank_Report.Transfer;
            helper.PaymentMethodName = "Wire transfer";
            helper.CurrencyPay = (int)Currencies.US_Dollar;
            helper.CurrencyPayName = "USD";
            helper.paymentDate = (DateTime)model.purchaseDate;
            helper.paymentDateString = DateTimeUtils.ParseDatetoString(model.purchaseDate);
            helper.paymentAmount = ammount;
            helper.paymentAmountString = MoneyUtils.ParseDecimalToString(helper.paymentAmount);
            helper.Hotel = model.idPartner;
            helper.HotelName = model.tblpartners.partnerName;
            helper.authcode = "";
            helper.exchangerate =  0m;
            helper.carddigit =  0;
            helper.terminal =  0;
            helper.authRef = string.Format("Datos de Purchase {0}: Cliente: {1}{2} , Hotel partner: {3}", helper.saleTypeName, model.tblmemberships.tblmembers.FirstOrDefault().memberFirstName, model.tblmemberships.tblmembers.FirstOrDefault().memberLastName, partnerName);

            if (listhelper.Count != 0)
            {
                var x = listhelper.Where(t => t.paymentmethod == helper.PaymentMethod).FirstOrDefault();
                if (x != null)
                {
                    helper.accountl1 = x.accountl1;
                    helper.accountl1Name = x.accl1name;
                    helper.accountl1Order = x.accountl1order;
                    helper.accountl2 = x.accountl2;
                    helper.accountl2Name = x.accl2name;
                    helper.accountl2Order = x.accountl2order;
                    helper.accountl3 = x.accountl3;
                    helper.accountl3Name = x.accl3name;
                    helper.accountl3Order = x.accountl3order;
                    helper.accountl4 = x.accountl4;
                    helper.accountl4Name = x.accl4name;
                    helper.accountl4Order = x.accountl4order;
                }
            }
            return helper;
        }

        public static List<docpaymentreservexpreport> ConvertTbltoHelper(List<tblreservationspayment> listmodel, IList<accountl4methodspay> listhelper)
        {
            List<docpaymentreservexpreport> list = new List<docpaymentreservexpreport>();

            if (listhelper != null && listhelper.Count() != 0)
            {
                foreach (tblreservationspayment model in listmodel)
                {
                    docpaymentreservexpreport helper = new docpaymentreservexpreport();
                    helper = ConvertTbltoHelper(model, listhelper);
                    list.Add(helper);
                }
            }

            return list;
        }        

        public static docpaymentreservexpreport ConvertTbltoHelper(tblreservationspayment model, IList<accountl4methodspay> listhelper)
        {
            UnitOfWork unity = new UnitOfWork();

            docpaymentreservexpreport helper = new docpaymentreservexpreport();
            helper.ReservationPayment = model.idReservationPayment;
            helper.Reservation = (int)model.idReservation;
            helper.reservationPaymentQuantity = model.reservationPaymentCost == null ? 0m : (decimal)model.reservationPaymentCost;
            helper.reservationPaymentQuantityString = MoneyUtils.ParseDecimalToString(helper.reservationPaymentQuantity);
            helper.reservationPaymentDate = (DateTime)model.reservationPaymentDate;
            helper.reservationPaymentDateString = DateTimeUtils.ParseDatetoString(model.reservationPaymentDate);
            helper.CurrencyPay = model.idCurrency == null ? 0 : (int)model.idCurrency;
            var _result = unity.CurrencyRepository.Get(v => v.idCurrency == model.idCurrency).FirstOrDefault();
            helper.CurrencyPayName = _result != null ? _result.currencyName : "";
            helper.CurrencytoExchange = (int)model.idCurrency;
            helper.CurrencytoExchangeName = getCurrencyName((int)model.idCurrency);
            helper.CurrencytoExchangeRate = model.exchangeRate == null ? 0m : (decimal)model.exchangeRate;
            helper.PaymentMethod = (int)model.idPaymentType;
            helper.PaymentMethodName = getPaymentMethodName((int)model.idPaymentType);
            helper.authRef = string.Format("Datos de reserva: Cliente: {0} , Hotel: {1}", model.tblreservations.reservationGuestName, getHotelPartenerName((int)model.tblreservations.idPartner));

            if (listhelper.Count != 0)
            {
                var x = listhelper.Where(t => t.paymentmethod == helper.PaymentMethod).FirstOrDefault();
                if (x != null)
                {
                    helper.accountl1 = x.accountl1;
                    helper.accountl1name = x.accl1name;
                    helper.accountl1order = x.accountl1order;
                    helper.accountl2 = x.accountl2;
                    helper.accountl2name = x.accl2name;
                    helper.accountl2order = x.accountl2order;
                    helper.accountl3 = x.accountl3;
                    helper.accountl3name = x.accl3name;
                    helper.accountl3order = x.accountl3order;
                    helper.accountl4 = x.accountl4;
                    helper.accountl4name = x.accl4name;
                    helper.accountl4order = x.accountl4order;
                }
            }

            return helper;
        }

        public static List<docpaymentreservexpreport> ConvertTbltoHelper(List<tblreservationsparentpayment> listmodel, IList<accountl4methodspay> listhelper)
        {
            List<docpaymentreservexpreport> list = new List<docpaymentreservexpreport>();

            if (listhelper != null && listhelper.Count() != 0)
            {
                foreach (tblreservationsparentpayment model in listmodel)
                {
                    docpaymentreservexpreport helper = new docpaymentreservexpreport();
                    helper = ConvertTbltoHelper(model, listhelper);
                    list.Add(helper);
                }
            }

            return list;
        }

        public static docpaymentreservexpreport ConvertTbltoHelper(tblreservationsparentpayment model, IList<accountl4methodspay> listhelper)
        {
            UnitOfWork unity = new UnitOfWork();

            docpaymentreservexpreport helper = new docpaymentreservexpreport();
            var chainname = model.tblreservationsparent.idPartner != null ? getHotelChainName((int)model.tblreservationsparent.idPartner) : "";
            helper.ReservationPayment = model.idReservationParentPayment;
            helper.Reservation = (int)model.idReservationParent;
            helper.reservationPaymentQuantity = model.reservationPaymentCost == null ? 0m : (decimal)model.reservationPaymentCost;
            helper.reservationPaymentQuantityString = MoneyUtils.ParseDecimalToString(helper.reservationPaymentQuantity);
            helper.reservationPaymentDate = (DateTime)model.reservationPaymentDate;
            helper.reservationPaymentDateString = DateTimeUtils.ParseDatetoString(model.reservationPaymentDate);
            helper.CurrencyPay = model.idCurrency == null ? 0 : (int)model.idCurrency;
            var _result = unity.CurrencyRepository.Get(v => v.idCurrency == model.idCurrency).FirstOrDefault();
            helper.CurrencyPayName = _result != null ? _result.currencyName : "";
            helper.CurrencytoExchange = (int)model.idCurrency;
            helper.CurrencytoExchangeName = getCurrencyName((int)model.idCurrency);
            helper.CurrencytoExchangeRate = model.exchangeRate == null ? 0m : (decimal)model.exchangeRate;
            helper.PaymentMethod = (int)model.idPaymentType;
            helper.PaymentMethodName = getPaymentMethodName((int)model.idPaymentType);
            helper.authRef = string.Format("Datos de reserva: Cliente: {0} , Hotel: {1}", model.tblreservationsparent.reservationParentGuestName, chainname);

            if (listhelper.Count != 0)
            {
                var x = listhelper.Where(t => t.paymentmethod == helper.PaymentMethod).FirstOrDefault();
                if (x != null)
                {
                    helper.accountl1 = x.accountl1;
                    helper.accountl1name = x.accl1name;
                    helper.accountl1order = x.accountl1order;
                    helper.accountl2 = x.accountl2;
                    helper.accountl2name = x.accl2name;
                    helper.accountl2order = x.accountl2order;
                    helper.accountl3 = x.accountl3;
                    helper.accountl3name = x.accl3name;
                    helper.accountl3order = x.accountl3order;
                    helper.accountl4 = x.accountl4;
                    helper.accountl4name = x.accl4name;
                    helper.accountl4order = x.accountl4order;
                }
            }

            return helper;
        }

        public static List<docpaymentreservexpreport> ConvertTbltoHelper(List<tblreservations> listmodel, IList<accountl4methodspay> listhelper)
        {
            List<docpaymentreservexpreport> list = new List<docpaymentreservexpreport>();

            if (listhelper != null && listhelper.Count() != 0)
            {
                foreach (tblreservations model in listmodel)
                {
                    docpaymentreservexpreport helper = new docpaymentreservexpreport();
                    helper = ConvertTbltoHelper(model, listhelper);
                    list.Add(helper);
                }
            }

            return list;
        }

        public static docpaymentreservexpreport ConvertTbltoHelper(tblreservations model, IList<accountl4methodspay> listhelper)
        {
            UnitOfWork unity = new UnitOfWork();

            docpaymentreservexpreport helper = new docpaymentreservexpreport();
            helper.ReservationPayment = model.idReservation;
            helper.Reservation = model.idReservation;
            helper.reservationPaymentQuantity = model.reservationSellPrice == null ? 0m : (decimal)model.reservationSellPrice;
            helper.reservationPaymentQuantityString = MoneyUtils.ParseDecimalToString(helper.reservationPaymentQuantity);
            helper.reservationPaymentDate = (DateTime)model.purchaseDate;
            helper.reservationPaymentDateString = DateTimeUtils.ParseDatetoString(model.purchaseDate);
            helper.CurrencyPay = (int)Currencies.US_Dollar; 
            helper.CurrencyPayName = "USD";
            helper.CurrencytoExchange = (int)Currencies.US_Dollar;
            helper.CurrencytoExchangeName = Currencies.US_Dollar.ToString().Replace('_', ' '); 
            helper.CurrencytoExchangeRate =  0m ;
            helper.PaymentMethod = (int)ReservaWeb.PaymentType;
            helper.PaymentMethodName = getPaymentMethodName((int)ReservaWeb.PaymentType);
            helper.authRef = string.Format("Datos de reserva web: Cliente: {0} , Hotel: {1}", model.reservationGuestName, getHotelPartenerName((int)model.idPartner));

            if (listhelper.Count != 0)
            {
                var x = listhelper.Where(t => t.paymentmethod == helper.PaymentMethod).FirstOrDefault();
                if (x != null)
                {
                    helper.accountl1 = x.accountl1;
                    helper.accountl1name = x.accl1name;
                    helper.accountl1order = x.accountl1order;
                    helper.accountl2 = x.accountl2;
                    helper.accountl2name = x.accl2name;
                    helper.accountl2order = x.accountl2order;
                    helper.accountl3 = x.accountl3;
                    helper.accountl3name = x.accl3name;
                    helper.accountl3order = x.accountl3order;
                    helper.accountl4 = x.accountl4;
                    helper.accountl4name = x.accl4name;
                    helper.accountl4order = x.accountl4order;
                }
            }

            return helper;
        }

        public static List<docfondos> ConvertTbltoHelper(List<tblfondos> list, bool positive)
        {
            List<docfondos> listModel = new List<docfondos>();

            foreach (tblfondos model in list)
            {
                docfondos helper = new docfondos();
                helper = ConvertTbltoHelper(model, positive);
                listModel.Add(helper);
            }

            return listModel;
        }

        public static docfondos ConvertTbltoHelper(tblfondos model, bool positive)
        {
            docfondos helper = new docfondos();
            helper.Fondo = model.idFondos;
            helper.baccntpaymentMethod = model.idPaymentMethod; //Destino
            helper.baccntpaymentMethodName = string.Concat(model.tblbankaccount.baccountshortname, " ", model.tblbankaccount.tblbank.bankshortname, " ", model.tblbankaccount.tblcurrencies.currencyAlphabeticCode, " ", model.tblbankaccount.tblcompanies.companyshortname);
            helper.baccntfinacialMethod = model.idFinancialMethod;//Origen
            helper.baccntfinancialMethodName = string.Concat(model.tblbankaccount1.baccountshortname, " ", model.tblbankaccount1.tblbank.bankshortname, " ", model.tblbankaccount1.tblcurrencies.currencyAlphabeticCode, " ", model.tblbankaccount1.tblcompanies.companyshortname);
            helper.fechaEntrega = model.fondofechaEntrega;
            helper.fechaEntregaString = DateTimeUtils.ParseDatetoString(model.fondofechaEntrega);
            helper.CurrencyPay = model.tblbankaccount.idcurrency; // Currency de la cuenta destino
            helper.CurrencyPayName = model.tblbankaccount.tblcurrencies.currencyAlphabeticCode;
            helper.fechaInicio = model.fondoFechaInicio;
            helper.fechaInicioString = DateTimeUtils.ParseDatetoString(model.fondoFechaInicio);
            helper.fechaFin = model.fondoFechaFin;
            helper.fechaFinString = DateTimeUtils.ParseDatetoString(model.fondoFechaFin);
            helper.fondoCreationDate = model.fondoCreationDate;
            helper.fondoCreationDateString = DateTimeUtils.ParseDatetoString(model.fondoCreationDate);
            helper.fondoMonto = positive == false ? model.fondoMonto * -1 : model.fondoMonto;
            helper.fondoMontoString = MoneyUtils.ParseDecimalToString(model.fondoMonto);
            helper.fondoComments = model.fondoComments;
            helper.fondoInvoice = model.fondoInvoice;
            helper.financeType = model.idFinanceType != null ? model.idFinanceType : null;
            helper.financeTypeName = model.idFinanceType != null ? model.tblfinancetype.financeTypeName : "";

            return helper;
        }

        public static List<docpayments> ConvertTbltoHelper(List<tblpayment> list, string dummy)
        {
            List<docpayments> listModel = new List<docpayments>();

            foreach (tblpayment model in list)
            {
                docpayments helper = new docpayments();
                helper = ConvertTbltoHelper(model, string.Empty);
                listModel.Add(helper);
            }

            return listModel;
        }

        public static docpayments ConvertTbltoHelper(tblpayment model, string dummy)
        {
            docpayments helper = new docpayments();
            helper.Payment = model.idpayment;
            helper.Invoice = model.idinvoice;
            helper.bankAccountType = model.idbankprodttype;
            helper.bankAccountTypeName = model.tblbankprodttype.bankprodttypename;
            helper.idBAccount = model.idbaccount;
            helper.baccountName = string.Concat(model.tblbankaccount.baccountshortname, " ", model.tblbankaccount.tblbank.bankshortname, " ", model.tblbankaccount.tblcurrencies.currencyAlphabeticCode, " ", model.tblbankaccount.tblcompanies.companyshortname);
            helper.Company = model.tblbankaccount.idcompany;
            helper.CompanyName = model.tblbankaccount.tblcompanies.companyname;
            helper.currency = model.tblbankaccount.idcurrency;
            helper.currencyName = model.tblbankaccount.tblcurrencies.currencyAlphabeticCode;
            helper.applicationDate = model.paymentdate;
            helper.applicationDateString = DateTimeUtils.ParseDatetoString(model.paymentdate);
            helper.chargeAmount = model.paymentamount;
            helper.chargeAmountString = MoneyUtils.ParseDecimalToString(model.paymentamount);
            helper.authReference = model.paymentauthref;
            helper.InvoiceIdentifier = AccountsUtils.IdentifierComplete(model.tblinvoice.tblcompanies.companyshortname, model.tblinvoice.tblusers.tblprofilesaccounts.profileaccountshortame, model.tblinvoice.invoicenumber);

            return helper;
        }
    }
}