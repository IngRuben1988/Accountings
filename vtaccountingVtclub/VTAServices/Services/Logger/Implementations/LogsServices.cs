using System;
using System.Threading.Tasks;
using VTAworldpass.VTACore;
using VTAworldpass.VTAServices.Services.accounts;
using VTAworldpass.VTAServices.Services.Logger;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTAServices.Services.Logger.helpers;
using System.Collections.Generic;

namespace VTAworldpass.Business.Services.Logger.Implementations
{
    public class LogsServices : LogsHelper, ILogsServices
    {
        private readonly UnitOfWork unity;
        private readonly IAccountServices accountServices;


        public LogsServices(IAccountServices _accountServices)
        {
            this.accountServices = _accountServices;
            this.unity = new UnitOfWork();
        }

        public void addTblLog(tblfondos model, string message)
        {
            tblfondosLog log = new tblfondosLog();

            log.LogUser = this.accountServices.AccountIdentity();
            log.LogObs = message;
            prepareToSaveLog(model, log);
            unity.FondosLogRepository.Insert(log);
            unity.Commit();
        }

        public void addTblLog(tblfondosmaxammount model, string message)
        {

            tblfondosmaxammountLog log = new tblfondosmaxammountLog();
            log.LogUser = this.accountServices.AccountIdentity();
            log.LogObs = message;
            prepareToSaveLog(model, log);
            unity.FondosMaxLimitLogRepository.Insert(log);
            unity.Commit();

        }

        public async Task<int> addTblLog(tblincome model, string message)
        {
            tblincomeLog log = new tblincomeLog();
            log.LogDate = DateTime.Now;
            log.LogUser = this.accountServices.AccountIdentity();
            log.LogObs = message;
            prepareToSaveLog(model, log);

            try
            {
                var result = await unity.IncomeLogRepository.InsertAsync(log);
                unity.CommitAsync();
                return 1;
            }
            catch (Exception e)
            {
                Log.Error("NO se puede realizar el guardado de registro LOG.", e);
                return 0;
            }
        }

        public async Task<int> addTblLog(tblincomeitem model, string message)
        {
            tblincomeitemLog log = new tblincomeitemLog();
            log.LogDate = DateTime.Now;
            log.LogUser = this.accountServices.AccountIdentity();
            log.LogObs = message;
            prepareToSaveLog(model, log);
            try
            {
                var result = await unity.IncomeItemLogRepository.InsertAsync(log);
                unity.CommitAsync();
                return 1;
            }
            catch (Exception e)
            {
                Log.Error("NO se puede realizar el guardado de registro LOG.", e);
                return 0;
            }
        }

        public async Task<int> addTblLog(tblincomemovement model, string message)
        {
            tblincomemovementLog log = new tblincomemovementLog();
            log.LogDate = DateTime.Now;
            log.LogUser = this.accountServices.AccountIdentity();
            log.LogObs = message;
            prepareToSaveLog(model, log);

            try
            {
                var result = await unity.IncomeMovementLogRepository.InsertAsync(log);
                unity.CommitAsync();
                return 1;
            }
            catch (Exception e)
            {
                Log.Error("NO se puede realizar el guardado de registro LOG.", e);
                return 0;
            }
        }



        public async Task<int> addTblLog(tblinvoice model, string message)
        {
            tblinvoiceLog log = new tblinvoiceLog();
            log.LogUser = this.accountServices.AccountIdentity();
            log.LogObs = message;
            prepareToSaveLog(model, log);

            try
            {
                var result = await unity.InvoiceLogRepository.InsertAsync(log);
                unity.CommitAsync();
                return 1;
            }
            catch (Exception e)
            {
                Log.Error("NO se puede realizar el guardado de registro LOG.", e);
                return 0;
            }
        }

        public async Task<int> addTblLog(tblinvoiceditem model, string message)
        {
            tblinvoiceditemLog log = new tblinvoiceditemLog();
            log.LogUser = this.accountServices.AccountIdentity();
            log.LogObs = message;
            prepareToSaveLog(model, log);

            try
            {
                var result = await unity.InvoicedItemLogRepository.InsertAsync(log);
                unity.CommitAsync();
                return 1;
            }
            catch (Exception e)
            {
                Log.Error("NO se puede realizar el guardado de registro LOG.", e);
                return 0;
            }
        }

        public async Task<int> addTblLog(tblpayment model, string message)
        {
            tblpaymentLog log = new tblpaymentLog();
            log.LogUser = this.accountServices.AccountIdentity();
            log.LogObs = message;
            prepareToSaveLog(model, log);

            try
            {
                var result = await this.unity.PaymentsLogRepository.InsertAsync(log);
                unity.CommitAsync();
                return 1;
            }
            catch (Exception e)
            {
                Log.Error("NO se puede realizar el guardado de registro LOG.", e);
                return 0;
            }
        }

        public void addTblLog(List<tblbankstatements> model, string message)
        {
            foreach (tblbankstatements item in model)
            {
                tblbankstatementsLog log = new tblbankstatementsLog();

                log.Log_User = this.accountServices.AccountIdentity();
                log.Log_Obs = message;
                prepareToSaveLog(item, log);
                unity.BankstatementsLogRepository.Insert(log);
                unity.Commit();
            }

        }

        public void addTblLog(List<tblbankstatements2> model, string message)
        {
            foreach (tblbankstatements2 item in model)
            {
                tblbankstatements2Log log = new tblbankstatements2Log();

                log.Log_User = this.accountServices.AccountIdentity();
                log.Log_Obs = message;
                prepareToSaveLog(item, log);
                unity.Bankstatements2LogRepository.Insert(log);
                unity.Commit();
            }

        }
    }
}