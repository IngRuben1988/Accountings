using System;
using System.Linq;
using System.Data;
using VTAworldpass.VTACore.GenericRepository.Repositories;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Logger;

namespace VTAworldpass.VTACore
{
    public class UnitOfWork : IDisposable
    {
        // Get de DB Context (Conextion to DB)
        protected vtclubdbEntities _context = new vtclubdbEntities();

        //----------------------------------------------------------------------------------------------------------------------
        //------------------------------- IMPLEMENTING GENERIC REPOSITORY  -----------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------

        // Creating the generic repository to use for unity of work
        private VirtualRepository<tblinvoice>           invoiceRepository;
        private VirtualRepository<tblinvoiceattach>     invoiceAttachRepository;
        private VirtualRepository<tblinvoiceditem>      invoiceditemRepository;
        private VirtualRepository<tblinvoicecomments>   invoiceCommentsRepository;
        private VirtualRepository<tblinvoiceditemLog>   invoicedItemLogRepository;
        private VirtualRepository<tblinvoiceLog>        invoiceLogRepository;

        private VirtualRepository<tblincome>            incomeRepository;
        private VirtualRepository<tblincomeitem>        incomeitemRepository;
        private VirtualRepository<tblincomecomments>    incomecommentsRepository;
        private VirtualRepository<tblincomeattach>      incomeAttachRepository;
        private VirtualRepository<tblincomemovement>    incomemovementsRepository;
        private VirtualRepository<tblincomeLog>         incomeLogRepository;
        private VirtualRepository<tblincomeitemLog>     incomeitemLogRepository;
        private VirtualRepository<tblincomemovementLog> incomemovementLogRepository;

        private VirtualRepository<tblusercompanies>      userCompaniesRepository;
        private VirtualRepository<tblcompaniescurrencies>companiesCurrenciesRepository;

        private VirtualRepository<tblaccounttypereport>  accounttypeRepository;
        private VirtualRepository<tblcurrencyexchange>   currenciesexchangeRepository;
        private VirtualRepository<tblcompanies>          companiesRepository;
        private VirtualRepository<tblcompanydevelopment> companydevelopmentRepository;
        private VirtualRepository<tblcompanygroupdevelopment> companygroupdevelopmentRepository;
        private VirtualRepository<tblcurrencies>         currencyRepository;
        private VirtualRepository<tblreservationspayment>reservationspaymentsRepository;
        private VirtualRepository<tblreservationsparentpayment> reservationsparentpaymentsRepository;
        private VirtualRepository<tblreservations>       reservationsRepository;
        private VirtualRepository<tblpurchases>          purchasesRepository;
        private VirtualRepository<tblprefixes>           prefixesRepository;
        private VirtualRepository<tblmembers>            membersRepository;
        private VirtualRepository<tblpaymentspurchases>  paymentspurchasesRepository;
        private VirtualRepository<tblaccl4paymenmethods> accl4paymenmethodsRepository;
        private VirtualRepository<tblattachments>        attachmentRepository;
        private VirtualRepository<tblcompaniesuppliers>  companiessuppliersRepository;
        private VirtualRepository<tblaccountsl2>         accountl2Repository;
        private VirtualRepository<tblaccountsl3>         accountl3Repository;
        private VirtualRepository<tblprofaccclass3>      profacccl3Repository;
        //private VirtualRepository<tblbankstatementmethod>bankstatementmethodRepository;
        private VirtualRepository<tblbankstatements>     bankstatementsRepository;
        private VirtualRepository<tblbankstatementsdet>  bankstatementsdetRepository;
        private VirtualRepository<tblbankstatements2>    bankstatements2Repository;
        private VirtualRepository<tblbankstatementsLog>  bankstatementslogRepository;
        private VirtualRepository<tblbankstatements2Log> bankstatements2logRepository;
        private VirtualRepository<tblbankstatementsgap>  bankstatementsgapRepository;
        private VirtualRepository<tblbankstatereserv>    bankstatereservRepository;
        //private VirtualRepository<tblbankstatereservweb> bankstatereservwebRepository;
        private VirtualRepository<tblbankstatepurchase>  bankstatepurchaseRepository;
        private VirtualRepository<tblbankstateincome>    bankstateincomeRepository;
        private VirtualRepository<tblbankstateparentreserv> bankstateparentreservRepository;
        private VirtualRepository<tblbankaccount>        bankaccountRepository;
        private VirtualRepository<tblbankstat2purchase>  bankstat2purchaseRepository;
        private VirtualRepository<tblbankstat2parentreserv> bankstat2parentreservRepository;
        private VirtualRepository<tblbankstat2reserv>    state2reservRepository;
        private VirtualRepository<tblbankstat2income>    state2incomeRepository;
        private VirtualRepository<tblbankstat2fondo>     state2fondoRepository;
        private VirtualRepository<tblbankstat2invoice>   state2invoiceRepository;
        private VirtualRepository<tblconciliationsegmentaccl4fee> conciliationsegmentaccl4feeRepository;
        private VirtualRepository<tblbugettype>          budgettypeRepository;
        private VirtualRepository<tbluserbacount>        userbaccountRepository;
        private VirtualRepository<tblparameters>         parametersRepository; // Parámetros [vta]
        private VirtualRepository<tblusers>              usersRepository; // [dbo]
        private VirtualRepository<tblpartners>           partnersRepository;
        private VirtualRepository<tblhotelchains>        hotelchainsRepository;
        private VirtualRepository<tblPaymentForms>       paymentformsRepository;
        private VirtualRepository<tbluseraccl4>          userAccl4Repository;
        private VirtualRepository<tblprofilesaccounts>   profilesAccountRepository;
        private VirtualRepository<tblpayment>            tblpaymentRepository;
        private VirtualRepository<tblbaccounttpv>        baccountTpvRepository;
        private VirtualRepository<tbltpv>                tpvRepository;
        private VirtualRepository<tblpaymentLog>         paymentLogRepository;
        private VirtualRepository<tblfondos>             fondosRepository;
        private VirtualRepository<tblfondosLog>          fondosLogRepository;
        private VirtualRepository<tblfondosmaxammountLog> fondosMaxAmmountLogRepository;
        private VirtualRepository<tblfondosmaxammount>   fondosMaxAmmountRepository;
        private VirtualRepository<tbluserpermissions>    userpermissionsRepository;
        private VirtualRepository<tblparameters1> parameters1Repository;  // Parámetros [vta]
        private VirtualRepository<tblpermissions>        permissionsRepository;
        private VirtualRepository<tblbstaterrors> bsErrorsRepository;
        private VirtualRepository<tblmovementtype> movementtypeRepository;
        private VirtualRepository<tblsourcedata> sourcedataRepository;
        private VirtualRepository<tbloperationtype> operationtypeRepository;
        private VirtualRepository<tblbatch> batchRepository;
        private VirtualRepository<tblbatchdetail> batchdetailRepository;
        private VirtualRepository<tblbatchdetailpre> batchdetailpreRepository;

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }

            catch (DataException DataException)
            {
                Log.Error("Commit-SaveChanges - No se peude realizr la acción:" + DataException.Message + " -> Inner ->" + DataException.InnerException, DataException);
                throw new Exception("Commit-SaveChanges No se peude realziar la acción:" + DataException.Message + " -> Inner ->" + DataException.InnerException);
            }

        }

        public void CommitAsync()
        {
            try
            {
                _context.SaveChangesAsync();
            }

            catch (DataException DataException)
            {
                Log.Error("Commit-SaveChangesAsync  - No se peude realizar la acción:" + DataException.Message + " -> Inner ->" + DataException.InnerException, DataException);
                throw new Exception("Commit-SaveChanges No se peude realizar la acción:" + DataException.Message + " -> Inner ->" + DataException.InnerException);
            }
        }

        public void Rollback()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        public VirtualRepository<tblbstaterrors> BSErrorsRepository
        {
            get
            {
                if (this.bsErrorsRepository == null)
                {
                    this.bsErrorsRepository = new VirtualRepository<tblbstaterrors>(_context);
                }

                return bsErrorsRepository;
            }
        }

        // USERS
        public VirtualRepository<tblusers> UserRepository
        {
            get
            {
                if (this.usersRepository == null)
                {
                    this.usersRepository = new VirtualRepository<tblusers>(_context);
                }
                return usersRepository;
            }
        }

        // USER ACCL4
        public VirtualRepository<tbluseraccl4> UserAccl4Repository
        {
            get
            {
                if (this.userAccl4Repository == null)
                {
                    this.userAccl4Repository = new VirtualRepository<tbluseraccl4>(_context);
                }
                return userAccl4Repository;
            }
        }

        // PROFILE-ACCOUNT
        public VirtualRepository<tblprofilesaccounts> ProfileAccountRepository
        {
            get
            {
                if (this.profilesAccountRepository == null)
                {
                    this.profilesAccountRepository = new VirtualRepository<tblprofilesaccounts>(_context);
                }
                return profilesAccountRepository;
            }
        }

        // USER COMPANIES
        public VirtualRepository<tblusercompanies> UserCompaniesRepository
        {
            get
            {
                if (this.userCompaniesRepository == null)
                {
                    this.userCompaniesRepository = new VirtualRepository<tblusercompanies>(_context);
                }
                return userCompaniesRepository;
            }
        }

        // COMPANIES CURRENCIES
        public VirtualRepository<tblcompaniescurrencies> CompaniesCurrenciesRepository
        {
            get
            {
                if (this.companiesCurrenciesRepository == null)
                {
                    this.companiesCurrenciesRepository = new VirtualRepository<tblcompaniescurrencies>(_context);
                }
                return companiesCurrenciesRepository;
            }
        }

        // COMMENTS 


        // REPORT TYPE
        public VirtualRepository<tblaccounttypereport> AccountTypeRepository
        {
            get
            {
                if (this.accounttypeRepository == null)
                {
                    this.accounttypeRepository = new VirtualRepository<tblaccounttypereport>(_context);
                }
                return accounttypeRepository;
            }
        }

        // CURRENCIES EXCHANGE
        public VirtualRepository<tblcurrencyexchange> CurrenciesExchangeRepository
        {
            get
            {
                if (this.currenciesexchangeRepository == null)
                {
                    this.currenciesexchangeRepository = new VirtualRepository<tblcurrencyexchange>(_context);
                }
                return currenciesexchangeRepository;
            }
        }

        // COMPANIES
        public VirtualRepository<tblcompanies> CompaniesRepository
        {
            get
            {
                if (this.companiesRepository == null)
                {
                    this.companiesRepository = new VirtualRepository<tblcompanies>(_context);
                }
                return companiesRepository;
            }
        }

        public VirtualRepository<tblcompanydevelopment> CompanyDevelopmentRepository
        {
            get
            {
                if (this.companydevelopmentRepository == null)
                {
                    this.companydevelopmentRepository = new VirtualRepository<tblcompanydevelopment>(_context);
                }
                return companydevelopmentRepository;
            }
        }

        public VirtualRepository<tblcompanygroupdevelopment> CompanyGroupDevelopmentRepository
        {
            get
            {
                if (this.companygroupdevelopmentRepository == null)
                {
                    this.companygroupdevelopmentRepository = new VirtualRepository<tblcompanygroupdevelopment>(_context);
                }
                return companygroupdevelopmentRepository;
            }
        }

        // CURRENCIES
        public VirtualRepository<tblcurrencies> CurrencyRepository
        {
            get
            {
                if (this.currencyRepository == null)
                {
                    this.currencyRepository = new VirtualRepository<tblcurrencies>(_context);
                }
                return currencyRepository;
            }
        }

        public VirtualRepository<tblreservations> ReservationsRepository
        {
            get
            {
                if (this.reservationsRepository == null)
                {
                    this.reservationsRepository = new VirtualRepository<tblreservations>(_context);
                }
                return reservationsRepository;
            }
        }

        // RESERVATIONS PAYMENTS
        public VirtualRepository<tblreservationspayment> ReservationsPaymentsRepository
        {
            get
            {
                if (this.reservationspaymentsRepository == null)
                {
                    this.reservationspaymentsRepository = new VirtualRepository<tblreservationspayment>(_context);
                }
                return reservationspaymentsRepository;
            }
        }

        public VirtualRepository<tblreservationsparentpayment> ReservationsParentPaymentsRepository
        {
            get
            {
                if (this.reservationsparentpaymentsRepository == null)
                {
                    this.reservationsparentpaymentsRepository = new VirtualRepository<tblreservationsparentpayment>(_context);
                }
                return reservationsparentpaymentsRepository;
            }
        }


        public VirtualRepository<tblpaymentspurchases> PaymentsPurchasesRepository
        {
            get
            {
                if (this.paymentspurchasesRepository == null)
                {
                    this.paymentspurchasesRepository = new VirtualRepository<tblpaymentspurchases>(_context);
                }
                return paymentspurchasesRepository;
            }
        }

        public VirtualRepository<tblpurchases> PurchasesRepository
        {
            get
            {
                if (this.purchasesRepository == null)
                {
                    this.purchasesRepository = new VirtualRepository<tblpurchases>(_context);
                }
                return purchasesRepository;
            }
        }

        public VirtualRepository<tblprefixes> PrefixesRepository
        {
            get
            {
                if (this.prefixesRepository == null)
                {
                    this.prefixesRepository = new VirtualRepository<tblprefixes>(_context);
                }
                return prefixesRepository;
            }
        }

        public VirtualRepository<tblmembers> MembersRepository
        {
            get
            {
                if (this.membersRepository == null)
                {
                    this.membersRepository = new VirtualRepository<tblmembers>(_context);
                }
                return membersRepository;
            }
        }

        // ACCL4-PAYMENTS
        public VirtualRepository<tblaccl4paymenmethods> Accl4PaymentsRepository
        {
            get
            {
                if (this.accl4paymenmethodsRepository == null)
                {
                    this.accl4paymenmethodsRepository = new VirtualRepository<tblaccl4paymenmethods>(_context);
                }
                return accl4paymenmethodsRepository;
            }
        }


        // VTA.ATTACHMENTS (types) 
        public VirtualRepository<tblattachments> AttachmentsVtaRepository
        {
            get
            {
                if (this.attachmentRepository == null)
                {
                    this.attachmentRepository = new VirtualRepository<tblattachments>(_context);
                }
                return attachmentRepository;
            }
        }

        //  TBLACCOUNTSL2
        public VirtualRepository<tblaccountsl2> AccountL2Repository
        {
            get
            {
                if (this.accountl2Repository == null)
                {
                    this.accountl2Repository = new VirtualRepository<tblaccountsl2>(_context);
                }
                return accountl2Repository;
            }
        }


        // INVOICE COMMENTS
        public VirtualRepository<tblinvoicecomments> InvoiceCommentsRepository
        {
            get
            {
                if (this.invoiceCommentsRepository == null)
                {
                    this.invoiceCommentsRepository = new VirtualRepository<tblinvoicecomments>(_context);
                }
                return invoiceCommentsRepository;
            }
        }

        // VTA.INVOICE ATTACHMENTS 
        public VirtualRepository<tblinvoiceattach> InvoiceAttachmentRepository
        {
            get
            {
                if (this.invoiceAttachRepository == null)
                {
                    this.invoiceAttachRepository = new VirtualRepository<tblinvoiceattach>(_context);
                }
                return invoiceAttachRepository;
            }
        }

        // INVOICE ITEM
        public VirtualRepository<tblinvoiceditem> InvoiceItemRepository
        {
            get
            {
                if (this.invoiceditemRepository == null)
                {
                    this.invoiceditemRepository = new VirtualRepository<tblinvoiceditem>(_context);
                }
                return invoiceditemRepository;
            }
        }

        // INVOICE 
        public VirtualRepository<tblinvoice> InvoiceRepository
        {
            get
            {
                if (this.invoiceRepository == null)
                {
                    this.invoiceRepository = new VirtualRepository<tblinvoice>(_context);
                }
                return invoiceRepository;
            }
        }

        // INVOICE LOG
        public VirtualRepository<tblinvoiceLog> InvoiceLogRepository
        {
            get
            {
                if (this.invoiceLogRepository == null)
                {
                    this.invoiceLogRepository = new VirtualRepository<tblinvoiceLog>(_context);
                }
                return invoiceLogRepository;
            }
        }

        // INVOICE ITEM LOG
        public VirtualRepository<tblinvoiceditemLog> InvoicedItemLogRepository
        {
            get
            {
                if (this.invoicedItemLogRepository == null)
                {
                    this.invoicedItemLogRepository = new VirtualRepository<tblinvoiceditemLog>(_context);
                }
                return invoicedItemLogRepository;
            }
        }


        // INCOME 
        public VirtualRepository<tblincome> IncomeRepository
        {
            get
            {
                if (this.incomeRepository == null)
                {
                    this.incomeRepository = new VirtualRepository<tblincome>(_context);
                }
                return incomeRepository;
            }
        }

        // STATEMENTS GAPS
        public VirtualRepository<tblconciliationsegmentaccl4fee> ConciliationsFeeAccountRepository
        {
            get
            {
                if (this.conciliationsegmentaccl4feeRepository == null)
                {
                    this.conciliationsegmentaccl4feeRepository = new VirtualRepository<tblconciliationsegmentaccl4fee>(_context);
                }
                return conciliationsegmentaccl4feeRepository;
            }
        }

        // INCOME ITEM
        public VirtualRepository<tblincomeitem> IncomeItemRepository
        {
            get
            {
                if (this.incomeitemRepository == null)
                {
                    this.incomeitemRepository = new VirtualRepository<tblincomeitem>(_context);
                }
                return incomeitemRepository;
            }
        }

        // INCOME COMMENTS
        public VirtualRepository<tblincomecomments> IncomeCommentsRepository
        {
            get
            {
                if (this.incomecommentsRepository == null)
                {
                    this.incomecommentsRepository = new VirtualRepository<tblincomecomments>(_context);
                }
                return incomecommentsRepository;
            }
        }

        // VTA.INCOME ATTACHMENTS 
        public VirtualRepository<tblincomeattach> IncomeAttachmentRepository
        {
            get
            {
                if (this.incomeAttachRepository == null)
                {
                    this.incomeAttachRepository = new VirtualRepository<tblincomeattach>(_context);
                }
                return incomeAttachRepository;
            }
        }

        // INCOME MOVEMENTS
        public VirtualRepository<tblincomemovement> IncomeMovementsRepository
        {
            get
            {
                if (this.incomemovementsRepository == null)
                {
                    this.incomemovementsRepository = new VirtualRepository<tblincomemovement>(_context);
                }
                return incomemovementsRepository;
            }
        }

        public VirtualRepository<tblincomemovementLog> IncomeMovementLogRepository
        {
            get
            {
                if (this.incomemovementLogRepository == null)
                {
                    this.incomemovementLogRepository = new VirtualRepository<tblincomemovementLog>(_context);
                }
                return incomemovementLogRepository;
            }
        }

        // INCOME LOG
        public VirtualRepository<tblincomeLog> IncomeLogRepository
        {
            get
            {
                if (this.incomeLogRepository == null)
                {
                    this.incomeLogRepository = new VirtualRepository<tblincomeLog>(_context);
                }
                return incomeLogRepository;
            }
        }

        // INCOME ITEM LOG
        public VirtualRepository<tblincomeitemLog> IncomeItemLogRepository
        {
            get
            {
                if (this.incomeitemLogRepository == null)
                {
                    this.incomeitemLogRepository = new VirtualRepository<tblincomeitemLog>(_context);
                }
                return incomeitemLogRepository;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------
        //------------------------------- D I S P O S A B L E ------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        // COMPANIES-SUPPLIERS
        public VirtualRepository<tblcompaniesuppliers> CompaniesSuppliersRepository
        {
            get
            {
                if (this.companiessuppliersRepository == null)
                {
                    this.companiessuppliersRepository = new VirtualRepository<tblcompaniesuppliers>(_context);
                }
                return companiessuppliersRepository;
            }
        }

        // BUDGETS TYPE
        public VirtualRepository<tblbugettype> BudgetTypeRepository
        {
            get
            {
                if (this.budgettypeRepository == null)
                {
                    this.budgettypeRepository = new VirtualRepository<tblbugettype>(_context);
                }
                return budgettypeRepository;
            }
        }

        // PROFILE-ACCOUNTACCOUNTL#
        public VirtualRepository<tblprofaccclass3> ProfileAccountL3Repository
        {
            get
            {
                if (this.profacccl3Repository == null)
                {
                    this.profacccl3Repository = new VirtualRepository<tblprofaccclass3>(_context);
                }
                return profacccl3Repository;
            }
        }

        public VirtualRepository<tblbankaccount> BankAccountRepository
        {
            get
            {
                if (this.bankaccountRepository == null)
                {
                    this.bankaccountRepository = new VirtualRepository<tblbankaccount>(_context);
                }
                return this.bankaccountRepository;
            }
        }

        public VirtualRepository<tblbankstatements> BankstatementsRepository
        {
            get
            {
                if (this.bankstatementsRepository == null)
                {
                    this.bankstatementsRepository = new VirtualRepository<tblbankstatements>(_context);
                }
                return this.bankstatementsRepository;
            }
        }

        public VirtualRepository<tblbankstatementsdet> BankstatementsDetRepository
        {
            get
            {
                if (this.bankstatementsdetRepository == null)
                {
                    this.bankstatementsdetRepository = new VirtualRepository<tblbankstatementsdet>(_context);
                }
                return this.bankstatementsdetRepository;
            }
        }

        public VirtualRepository<tblbankstatements2> Bankstatements2Repository
        {
            get
            {
                if (this.bankstatements2Repository == null)
                {
                    this.bankstatements2Repository = new VirtualRepository<tblbankstatements2>(_context);
                }
                return this.bankstatements2Repository;
            }
        }

        public VirtualRepository<tblbankstatementsLog> BankstatementsLogRepository
        {
            get
            {
                if (this.bankstatementslogRepository == null)
                {
                    this.bankstatementslogRepository = new VirtualRepository<tblbankstatementsLog>(_context);
                }
                return this.bankstatementslogRepository;
            }
        }

        public VirtualRepository<tblbankstatements2Log> Bankstatements2LogRepository
        {
            get
            {
                if (this.bankstatements2logRepository == null)
                {
                    this.bankstatements2logRepository = new VirtualRepository<tblbankstatements2Log>(_context);
                }
                return this.bankstatements2logRepository;
            }
        }
        public VirtualRepository<tblbankstat2purchase> Statements2PURCHASESRepository
        {
            get
            {
                if (this.bankstat2purchaseRepository == null)
                {
                    this.bankstat2purchaseRepository = new VirtualRepository<tblbankstat2purchase>(_context);
                }

                return bankstat2purchaseRepository;
            }
        }

        public VirtualRepository<tblbankstat2parentreserv> Statements2PARENTRESERVRepository
        {
            get
            {
                if (this.bankstat2parentreservRepository == null)
                {
                    this.bankstat2parentreservRepository = new VirtualRepository<tblbankstat2parentreserv>(_context);
                }

                return bankstat2parentreservRepository;
            }
        }

        public VirtualRepository<tblbankstat2reserv> Statements2RESERVRepository
        {
            get
            {
                if (this.state2reservRepository == null)
                {
                    this.state2reservRepository = new VirtualRepository<tblbankstat2reserv>(_context);
                }

                return state2reservRepository;
            }
        }

        public VirtualRepository<tblbankstat2income> Statements2INCOMERepository
        {
            get
            {
                if (this.state2incomeRepository == null)
                {
                    this.state2incomeRepository = new VirtualRepository<tblbankstat2income>(_context);
                }

                return state2incomeRepository;
            }
        }

        public VirtualRepository<tblbankstat2fondo> Statements2FONDORepository
        {
            get
            {
                if (this.state2fondoRepository == null)
                {
                    this.state2fondoRepository = new VirtualRepository<tblbankstat2fondo>(_context);
                }

                return state2fondoRepository;
            }
        }

        public VirtualRepository<tblbankstat2invoice> Statements2INVOICERepository
        {
            get
            {
                if (this.state2invoiceRepository == null)
                {
                    this.state2invoiceRepository = new VirtualRepository<tblbankstat2invoice>(_context);
                }

                return state2invoiceRepository;
            }
        }

        public VirtualRepository<tblmovementtype> MovementTypeRepository
        {
            get
            {
                if (this.movementtypeRepository == null)
                {
                    this.movementtypeRepository = new VirtualRepository<tblmovementtype>(_context);
                }

                return movementtypeRepository;
            }
        }

        public VirtualRepository<tblsourcedata> SourceDataRepository
        {
            get
            {
                if (this.sourcedataRepository == null)
                {
                    this.sourcedataRepository = new VirtualRepository<tblsourcedata>(_context);
                }

                return sourcedataRepository;
            }
        }

        public VirtualRepository<tbloperationtype> OperationTypeRepository
        {
            get
            {
                if (this.operationtypeRepository == null)
                {
                    this.operationtypeRepository = new VirtualRepository<tbloperationtype>(_context);
                }

                return operationtypeRepository;
            }
        }

        public VirtualRepository<tblbankstatementsgap> BankstatementsGapRepository
        {
            get
            {
                if(this.bankstatementsgapRepository == null)
                {
                    this.bankstatementsgapRepository = new VirtualRepository<tblbankstatementsgap>(_context);
                }
                return this.bankstatementsgapRepository;
            }
        }

        public VirtualRepository<tblbankstatereserv> BankstateReservRepository
        {
            get
            {
                if (this.bankstatereservRepository == null)
                {
                    this.bankstatereservRepository = new VirtualRepository<tblbankstatereserv>(_context);
                }
                return this.bankstatereservRepository;
            }
        }

        //public VirtualRepository<tblbankstatereservweb> BankstateReservWebRepository
        //{
        //    get
        //    {
        //        if (this.bankstatereservwebRepository == null)
        //        {
        //            this.bankstatereservwebRepository = new VirtualRepository<tblbankstatereservweb>(_context);
        //        }
        //        return this.bankstatereservwebRepository;
        //    }
        //}

        public VirtualRepository<tblbankstatepurchase> BankstatePurchaseRepository
        {
            get
            {
                if (this.bankstatepurchaseRepository == null)
                {
                    this.bankstatepurchaseRepository = new VirtualRepository<tblbankstatepurchase>(_context);
                }
                return this.bankstatepurchaseRepository;
            }
        }

        public VirtualRepository<tblbankstateparentreserv> BankstateParentReservRepository
        {
            get
            {
                if (this.bankstateparentreservRepository == null)
                {
                    this.bankstateparentreservRepository = new VirtualRepository<tblbankstateparentreserv>(_context);
                }
                return this.bankstateparentreservRepository;
            }
        }

        public VirtualRepository<tblbankstateincome> BankstateIncomeRepository
        {
            get
            {
                if (this.bankstateincomeRepository == null)
                {
                    this.bankstateincomeRepository = new VirtualRepository<tblbankstateincome>(_context);
                }
                return this.bankstateincomeRepository;
            }
        }

        public VirtualRepository<tblpartners> PartnersRepository
        {
            get
            {
                if (this.partnersRepository == null)
                {
                    this.partnersRepository = new VirtualRepository<tblpartners>(_context);
                }
                return this.partnersRepository;
            }
        }

        public VirtualRepository<tblhotelchains> HotelChainsRepository
        {
            get
            {
                if (this.hotelchainsRepository == null)
                {
                    this.hotelchainsRepository = new VirtualRepository<tblhotelchains>(_context);
                }
                return this.hotelchainsRepository;
            }
        }

        public VirtualRepository<tblPaymentForms> PaymentFormsRepository
        {
            get
            {
                if (this.paymentformsRepository == null)
                {
                    this.paymentformsRepository = new VirtualRepository<tblPaymentForms>(_context);
                }
                return this.paymentformsRepository;
            }
        }

        public VirtualRepository<tblfondos> FondosRepository
        {
            get
            {
                if (this.fondosRepository == null)
                {
                    this.fondosRepository = new VirtualRepository<tblfondos>(_context);
                }
                return this.fondosRepository;
            }
        }

        public VirtualRepository<tblfondosLog> FondosLogRepository
        {
            get
            {
                if (this.fondosLogRepository == null)
                {
                    this.fondosLogRepository = new VirtualRepository<tblfondosLog>(_context);
                }
                return this.fondosLogRepository;
            }
        }

        public VirtualRepository<tblfondosmaxammount> FondosMaxLimitRepository
        {
            get
            {
                if (this.fondosMaxAmmountRepository == null)
                {
                    this.fondosMaxAmmountRepository = new VirtualRepository<tblfondosmaxammount>(_context);
                }
                return fondosMaxAmmountRepository;
            }
        }

        public VirtualRepository<tblfondosmaxammountLog> FondosMaxLimitLogRepository
        {
            get
            {
                if (this.fondosMaxAmmountLogRepository == null)
                {
                    this.fondosMaxAmmountLogRepository = new VirtualRepository<tblfondosmaxammountLog>(_context);
                }
                return fondosMaxAmmountLogRepository;
            }
        }

        // USER-BACKACCOUNTS
        public VirtualRepository<tbluserbacount> UserBAccountsRepository
        {
            get
            {
                if (this.userbaccountRepository == null)
                {
                    this.userbaccountRepository = new VirtualRepository<tbluserbacount>(_context);
                }
                return userbaccountRepository;
            }
        }

        //  EXPENSES CATEGORY
        public VirtualRepository<tblaccountsl3> AccountL3Repository
        {
            get
            {
                if (this.accountl3Repository == null)
                {
                    this.accountl3Repository = new VirtualRepository<tblaccountsl3>(_context);
                }
                return accountl3Repository;
            }
        }

        // PARAMETERS VTA
        public VirtualRepository<tblparameters> ParametersVTARepository
        {
            get
            {
                if (this.parametersRepository == null)
                {
                    this.parametersRepository = new VirtualRepository<tblparameters>(_context);
                }
                return parametersRepository;
            }
        }

        // PARAMETERS DBO
        public VirtualRepository<tblparameters1> ParametersRepository
        {
            get
            {
                if (this.parameters1Repository == null)
                {
                    this.parameters1Repository = new VirtualRepository<tblparameters1>(_context);
                }
                return parameters1Repository;
            }
        }

        // VTA.PAYMENTS 
        public VirtualRepository<tblpayment> PaymentsVtaRepository
        {
            get
            {
                if (this.tblpaymentRepository == null)
                {
                    this.tblpaymentRepository = new VirtualRepository<tblpayment>(_context);
                }
                return tblpaymentRepository;
            }
        }

        // BANK ACCOUNT - TPV's
        public VirtualRepository<tblbaccounttpv> BankAccountTpvRepository
        {
            get
            {
                if (this.baccountTpvRepository == null)
                {
                    this.baccountTpvRepository = new VirtualRepository<tblbaccounttpv>(_context);
                }
                return baccountTpvRepository;
            }
        }

        public VirtualRepository<tbltpv> TpvRepository
        {
            get
            {
                if (this.tpvRepository == null)
                {
                    this.tpvRepository = new VirtualRepository<tbltpv>(_context);
                }
                return tpvRepository;
            }
        }

        public VirtualRepository<tblpaymentLog> PaymentsLogRepository
        {
            get
            {
                if (this.paymentLogRepository == null)
                {
                    this.paymentLogRepository = new VirtualRepository<tblpaymentLog>(_context);
                }
                return paymentLogRepository;
            }
        }


        // USERS
        public VirtualRepository<tbluserpermissions> UserPermissionsVTARepository
        {
            get
            {
                if (this.userpermissionsRepository == null)
                {
                    this.userpermissionsRepository = new VirtualRepository<tbluserpermissions>(_context);
                }
                return userpermissionsRepository;
            }
        }

        public VirtualRepository<tblpermissions> PermissionsVTARepository
        {
            get
            {
                if (this.permissionsRepository == null)
                {
                    this.permissionsRepository = new VirtualRepository<tblpermissions>(_context);
                }
                return permissionsRepository;
            }
        }

        public VirtualRepository<tblbatch> BatchRepository
        {
            get
            {
                if (this.batchRepository == null)
                {
                    this.batchRepository = new VirtualRepository<tblbatch>(_context);
                }
                return batchRepository;
            }
        }

        public VirtualRepository<tblbatchdetail> BatchDetailRepository
        {
            get
            {
                if (this.batchdetailRepository == null)
                {
                    this.batchdetailRepository = new VirtualRepository<tblbatchdetail>(_context);
                }
                return batchdetailRepository;
            }
        }

        public VirtualRepository<tblbatchdetailpre> BatchDetailPreRepository
        {
            get
            {
                if (this.batchdetailpreRepository == null)
                {
                    this.batchdetailpreRepository = new VirtualRepository<tblbatchdetailpre>(_context);
                }
                return batchdetailpreRepository;
            }
        }
    }
}