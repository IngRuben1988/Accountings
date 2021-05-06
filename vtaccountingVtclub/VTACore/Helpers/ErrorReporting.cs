using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTAServices.Services.accounts;
using VTAworldpass.VTAServices.Services.accounts.implements;
using static VTAworldpass.VTACore.Cores.Globales.Enumerables;

namespace VTAworldpass.VTACore.Helpers
{
    public enum ErrorAction
    {
        Ignored = 1,
        Fixed = 2,
        Skipped = 3,
        Halted = 4
    }

    public enum ErrorSeverity
    {
        Information = 1,
        Warning = 2,
        Critical = 3
    }

    public enum XLSFileErrorDepth
    {
        File = 1,
        Sheet = 2,
        Row = 3,
        Cell = 4
    }

    public class ErrorMessages
    {
        public static string defErrMsg = "Unespecified error encountered.";
        public static string noExGiven = "No exception was raised.";
        public static string[] severities = { "unk", "info", "warn", "crit" };
        public static string[] actions = { "unk", "Ignorado.", "Corregido.", "Saltado.", "Operación detenida." };
    }

    public class ErrorReportBase
    {
        private Guid _oper_uuid { get; set; }
        private IAccountServices accountServices = new AccountServices();
        public static readonly UnitOfWork unity = new UnitOfWork();

        public int error_count { get; set; }
        public long logged_userid { get; set; }
        public string logged_username { get; set; }
        public bool has_errors { get; set; }
        public bool was_halted = false;
        public List<ErrorRecordBase> generic_errors { get; set; }

        public Guid oper_uuid
        {
            get
            {
                if (_oper_uuid == null) _oper_uuid = Guid.NewGuid();

                return _oper_uuid;
            }
        }

        public ErrorReportBase()
        {
            _oper_uuid = Guid.NewGuid();
            error_count = 0;
            has_errors = false;
            logged_userid = accountServices.AccountIdentity();
            logged_username = accountServices.getUserName();
            generic_errors = new List<ErrorRecordBase>();
        }

        public static string GetTpvLocation(int tpvId)
        {
            return unity.TpvRepository.Get(t => t.idtpv == tpvId).FirstOrDefault().tpvidlocation ?? string.Empty;
        }

        public void AddCriticalStop(string errorMsg = null, Exception ex = null)
        {
            error_count++;
            has_errors = true;
            was_halted = true;

            generic_errors.Add(new ErrorRecordBase()
            {
                error_consec = error_count,
                error_msg = errorMsg ?? ErrorMessages.defErrMsg,
                exception_msg = ((ex != null) && !(string.IsNullOrWhiteSpace(ex.Message))) ? ex.Message : ErrorMessages.noExGiven,
                exception_stacktrace = ((ex != null) && !(string.IsNullOrWhiteSpace(ex.StackTrace))) ? ex.StackTrace : ErrorMessages.noExGiven,
                with_exception = (ex != null),
                error_timestamp = DateTime.Now,
                error_severity = ErrorSeverity.Critical,
                action_taken = ErrorAction.Halted
            });
        }
    }

    public class ErrorRecordBase
    {
        public int error_consec { get; set; }
        public string error_msg { get; set; }
        public string exception_msg { get; set; }
        public string exception_stacktrace { get; set; }
        public bool with_exception { get; set; }
        public DateTime error_timestamp { get; set; }
        public ErrorSeverity error_severity { get; set; }
        public ErrorAction action_taken { get; set; }

        public ErrorRecordBase() { }
    }

    public class XLSFileImportErrors : ErrorReportBase
    {
        public List<XLSFileError> file_parsing_errors { get; set; }
        public List<BankStatementError> bank_statement_errors { get; set; }
        public List<BankConciliationError> bank_conciliation_errors { get; set; }

        public XLSFileImportErrors()
        {
            file_parsing_errors = new List<XLSFileError>();
            bank_statement_errors = new List<BankStatementError>();
            bank_conciliation_errors = new List<BankConciliationError>();
        }

        public void AddFileUploadError(string errorMsg, int fileIdx, string fileName, Exception ex = null)
        {
            error_count++;
            has_errors = true;

            file_parsing_errors.Add(new XLSFileError()
            {
                error_consec = error_count,
                error_msg = string.IsNullOrWhiteSpace(errorMsg) ? ErrorMessages.defErrMsg : errorMsg,
                exception_msg = ((ex != null) && !(string.IsNullOrWhiteSpace(ex.Message))) ? ex.Message : ErrorMessages.noExGiven,
                exception_stacktrace = ((ex != null) && !(string.IsNullOrWhiteSpace(ex.StackTrace))) ? ex.StackTrace : ErrorMessages.noExGiven,
                with_exception = (ex != null),
                error_timestamp = DateTime.Now,
                error_severity = ErrorSeverity.Critical,
                action_taken = ErrorAction.Skipped,
                file_idx = fileIdx,
                file_name = fileName,
                error_depth = XLSFileErrorDepth.File
            });
        }

        public void AddXlsSheetSkipped(string errorMsg, int fileIdx, string fileName, int sheetIdx, string sheetName, Exception ex = null)
        {
            error_count++;
            has_errors = true;

            file_parsing_errors.Add(new XLSFileError()
            {
                error_consec = error_count,
                error_msg = string.IsNullOrWhiteSpace(errorMsg) ? ErrorMessages.defErrMsg : errorMsg,
                exception_msg = ((ex != null) && !(string.IsNullOrWhiteSpace(ex.Message))) ? ex.Message : ErrorMessages.noExGiven,
                exception_stacktrace = ((ex != null) && !(string.IsNullOrWhiteSpace(ex.StackTrace))) ? ex.StackTrace : ErrorMessages.noExGiven,
                with_exception = (ex != null),
                error_timestamp = DateTime.Now,
                error_severity = ErrorSeverity.Warning,
                action_taken = ErrorAction.Skipped,
                file_idx = fileIdx,
                file_name = fileName,
                sheet_idx = sheetIdx,
                sheet_name = sheetName,
                error_depth = XLSFileErrorDepth.Sheet
            });
        }

        public void AddXlsRowSkipped(string errorMsg, int fileIdx, string fileName, int sheetIdx, string sheetName, int rowIdx, Exception ex = null)
        {
            error_count++;
            has_errors = true;

            file_parsing_errors.Add(new XLSFileError()
            {
                error_consec = error_count,
                error_msg = string.IsNullOrWhiteSpace(errorMsg) ? ErrorMessages.defErrMsg : errorMsg,
                exception_msg = ((ex != null) && !(string.IsNullOrWhiteSpace(ex.Message))) ? ex.Message : ErrorMessages.noExGiven,
                exception_stacktrace = ((ex != null) && !(string.IsNullOrWhiteSpace(ex.StackTrace))) ? ex.StackTrace : ErrorMessages.noExGiven,
                with_exception = (ex != null),
                error_timestamp = DateTime.Now,
                error_severity = ErrorSeverity.Warning,
                action_taken = ErrorAction.Skipped,
                file_idx = fileIdx,
                file_name = fileName,
                sheet_idx = sheetIdx,
                sheet_name = sheetName,
                row_idx = rowIdx,
                error_depth = XLSFileErrorDepth.Row
            });

        }

        public void AddXlsCellSkipped(string errorMsg, int fileIdx, string fileName, int sheetIdx, string sheetName, int rowIdx, int cellIdx, string cellValue, Exception ex = null)
        {
            error_count++;
            has_errors = true;

            file_parsing_errors.Add(new XLSFileError()
            {
                error_consec = error_count,
                error_msg = string.IsNullOrWhiteSpace(errorMsg) ? ErrorMessages.defErrMsg : errorMsg,
                exception_msg = ((ex != null) && !(string.IsNullOrWhiteSpace(ex.Message))) ? ex.Message : ErrorMessages.noExGiven,
                exception_stacktrace = ((ex != null) && !(string.IsNullOrWhiteSpace(ex.StackTrace))) ? ex.StackTrace : ErrorMessages.noExGiven,
                with_exception = (ex != null),
                error_timestamp = DateTime.Now,
                error_severity = ErrorSeverity.Warning,
                action_taken = ErrorAction.Skipped,
                file_idx = fileIdx,
                file_name = fileName,
                sheet_idx = sheetIdx,
                sheet_name = sheetName,
                row_idx = rowIdx,
                cell_idx = cellIdx,
                cell_value = cellValue,
                error_depth = XLSFileErrorDepth.Cell
            });
        }

        public void AddStatRecordFailure(string errorMsg, string sheetName, int rowIdx, BankTypeClasification recordType, Exception ex = null)
        {
            error_count++;
            has_errors = true;

            bank_statement_errors.Add(new BankStatementError()
            {
                error_consec = error_count,
                error_msg = string.IsNullOrWhiteSpace(errorMsg) ? ErrorMessages.defErrMsg : errorMsg,
                exception_msg = ((ex != null) && !(string.IsNullOrWhiteSpace(ex.Message))) ? ex.Message : ErrorMessages.noExGiven,
                exception_stacktrace = ((ex != null) && !(string.IsNullOrWhiteSpace(ex.StackTrace))) ? ex.StackTrace : ErrorMessages.noExGiven,
                with_exception = (ex != null),
                error_timestamp = DateTime.Now,
                error_severity = ErrorSeverity.Warning,
                action_taken = ErrorAction.Skipped,
                record_type = recordType,
                sheet_name = sheetName,
                row_idx = rowIdx
            });
        }

        public void AddStatConciliationError(string errorMsg, tblbankstatements bankStatement, Exception ex = null)
        {
            error_count++;
            has_errors = true;

            bank_conciliation_errors.Add(new BankConciliationError()
            {
                error_consec = error_count,
                error_msg = string.IsNullOrWhiteSpace(errorMsg) ? ErrorMessages.defErrMsg : errorMsg,
                exception_msg = ((ex != null) && !(string.IsNullOrWhiteSpace(ex.Message))) ? ex.Message : ErrorMessages.noExGiven,
                exception_stacktrace = ((ex != null) && !(string.IsNullOrWhiteSpace(ex.StackTrace))) ? ex.StackTrace : ErrorMessages.noExGiven,
                with_exception = (ex != null),
                error_timestamp = DateTime.Now,
                error_severity = ErrorSeverity.Warning,
                action_taken = ErrorAction.Skipped,
                bstat_id = bankStatement.idBankStatements,
                bstat_date = bankStatement.bankstatementAplicationDate,
                bstat_tpv = GetTpvLocation(bankStatement.idTPV),
                bstat_amount = bankStatement.bankstatementAppliedAmmount,
                bstat_fee = bankStatement.bankstatementBankFee
            });
        }

        public void AddStatConciliationError2(string errorMsg, tblbankstatements2 bankStatement, Exception ex = null)
        {
            error_count++;
            has_errors = true;

            bank_conciliation_errors.Add(new BankConciliationError()
            {
                error_consec = error_count,
                error_msg = string.IsNullOrWhiteSpace(errorMsg) ? ErrorMessages.defErrMsg : errorMsg,
                exception_msg = ((ex != null) && !(string.IsNullOrWhiteSpace(ex.Message))) ? ex.Message : ErrorMessages.noExGiven,
                exception_stacktrace = ((ex != null) && !(string.IsNullOrWhiteSpace(ex.StackTrace))) ? ex.StackTrace : ErrorMessages.noExGiven,
                with_exception = (ex != null),
                error_timestamp = DateTime.Now,
                error_severity = ErrorSeverity.Warning,
                action_taken = ErrorAction.Skipped,
                bstat_id = bankStatement.idBankStatements2,
                bstat_date = bankStatement.bankstatements2AplicationDate,
                bstat_tpv = GetTpvLocation(bankStatement.tblbankaccount.tblbaccounttpv.First().idtpv),
                bstat_amount = (decimal)bankStatement.bankstatements2Pay,
                bstat_fee = bankStatement.bankstatements2Charge
            });
        }

        public List<JSErrorRecord> GrindErrorReport()
        {
            List<JSErrorRecord> la_resp = new List<JSErrorRecord>();
            List<tblbstaterrors> errors_tolog = new List<tblbstaterrors>();
            string error_head = "Error {0} de {1}:";
            string joiner = "\n\r";
            string[] err_dets;

            if (has_errors)
            {
                foreach (XLSFileError xf_error in file_parsing_errors)
                { 
                    err_dets = new string[]
                    {
                        ("Archivo: " + (xf_error.file_idx + 1).ToString() + " - «" + xf_error.file_name + "»."),
                        ((xf_error.sheet_idx != null) ? "Hoja: " + (xf_error.sheet_idx + 1).ToString() + " - «" + xf_error.sheet_name + "»." : string.Empty),
                        ((xf_error.row_idx != null) ? "Fila #" + (xf_error.row_idx + 1).ToString() + "." : string.Empty),
                        ((xf_error.cell_idx != null) ? "Celda #" + (xf_error.cell_idx + 1) + "." : string.Empty),
                        ((xf_error.cell_value != null) ? "Texto: '" + xf_error.cell_value + "'." : string.Empty)
                    };

                    la_resp.Add(new JSErrorRecord()
                    {
                        error_consec = xf_error.error_consec,
                        error_header = string.Format(error_head, xf_error.error_consec.ToString(), error_count.ToString()),
                        error_description = "\"" + xf_error.error_msg + "\" procesando archivo.",
                        error_severity_str = ErrorMessages.severities[(int)xf_error.error_severity],
                        action_taken_str = ErrorMessages.actions[(int)xf_error.action_taken],
                        exception_msg = xf_error.exception_msg,
                        exception_stack = xf_error.exception_stacktrace,
                        has_exception = xf_error.with_exception,
                        error_details = err_dets
                    });

                    errors_tolog.Add(new tblbstaterrors()
                    {
                        bStatErrorOperUuid = oper_uuid.ToString(),
                        bStatErrorDatetime = xf_error.error_timestamp,
                        bStatErrorConsec = xf_error.error_consec,
                        bStatErrorTotal = error_count,
                        bStatErrorMessage = xf_error.error_msg,
                        bStatErrorDetails = string.Join(joiner, err_dets),
                        idErrorSeverity = (int)xf_error.error_severity,
                        idErrorAction = (int)xf_error.action_taken,
                        bStatErrorHasExcep = xf_error.with_exception,
                        bStatErrorExcepMsg = xf_error.exception_msg,
                        bStatErrorExcepStack = xf_error.exception_stacktrace,
                        idUser = (int)logged_userid
                    });
                }

                foreach (BankStatementError bs_error in bank_statement_errors)
                {
                    err_dets = new string[]
                    {
                        ("Hoja «" + bs_error.sheet_name + "»."),
                        ("Fila #" + (bs_error.row_idx + 1).ToString() + ".")
                    };

                    la_resp.Add(new JSErrorRecord()
                    {
                        error_consec = bs_error.error_consec,
                        error_header = string.Format(error_head, bs_error.error_consec.ToString(), error_count.ToString()),
                        error_description = "\"" + bs_error.error_msg + "\" procesando registro.",
                        error_severity_str = ErrorMessages.severities[(int)bs_error.error_severity],
                        action_taken_str = ErrorMessages.actions[(int)bs_error.action_taken],
                        exception_msg = bs_error.exception_msg,
                        exception_stack = bs_error.exception_stacktrace,
                        has_exception = bs_error.with_exception,
                        error_details = err_dets
                    });

                    errors_tolog.Add(new tblbstaterrors()
                    {
                        bStatErrorOperUuid = oper_uuid.ToString(),
                        bStatErrorDatetime = bs_error.error_timestamp,
                        bStatErrorConsec = bs_error.error_consec,
                        bStatErrorTotal = error_count,
                        bStatErrorMessage = bs_error.error_msg,
                        bStatErrorDetails = string.Join(joiner, err_dets),
                        idErrorSeverity = (int)bs_error.error_severity,
                        idErrorAction = (int)bs_error.action_taken,
                        bStatErrorHasExcep = bs_error.with_exception,
                        bStatErrorExcepMsg = bs_error.exception_msg,
                        bStatErrorExcepStack = bs_error.exception_stacktrace,
                        idUser = (int)logged_userid
                    });
                }

                foreach (BankConciliationError bc_error in bank_conciliation_errors)
                {
                    err_dets = new string[]
                    {
                        ("ID de conciliation: " + bc_error.bstat_id.ToString() + "."),
                        ("Terminal: " + bc_error.bstat_tpv + "."),
                        ("Fecha: "+ bc_error.bstat_date.ToString("yyyy/MM/dd") +"."),
                        ("Monto: " + bc_error.bstat_amount.ToString("C2") + "."),
                        ("Comisión: " + ((decimal)(bc_error.bstat_fee)).ToString("C2") + ".")
                    };

                    la_resp.Add(new JSErrorRecord()
                    {
                        error_consec = bc_error.error_consec,
                        error_header = string.Format(error_head, bc_error.error_consec.ToString(), error_count.ToString()),
                        error_description = "\"" + bc_error.error_msg + "\" procesando conciliación.",
                        error_severity_str = ErrorMessages.severities[(int)bc_error.error_severity],
                        action_taken_str = ErrorMessages.actions[(int)bc_error.action_taken],
                        exception_msg = bc_error.exception_msg,
                        exception_stack = bc_error.exception_stacktrace,
                        has_exception = bc_error.with_exception,
                        error_details = err_dets
                    });

                    errors_tolog.Add(new tblbstaterrors()
                    {
                        bStatErrorOperUuid = oper_uuid.ToString(),
                        bStatErrorDatetime = bc_error.error_timestamp,
                        bStatErrorConsec = bc_error.error_consec,
                        bStatErrorTotal = error_count,
                        bStatErrorMessage = bc_error.error_msg,
                        bStatErrorDetails = string.Join(joiner, err_dets),
                        idErrorSeverity = (int)bc_error.error_severity,
                        idErrorAction = (int)bc_error.action_taken,
                        bStatErrorHasExcep = bc_error.with_exception,
                        bStatErrorExcepMsg = bc_error.exception_msg,
                        bStatErrorExcepStack = bc_error.exception_stacktrace,
                        idUser = (int)logged_userid
                    });
                }

                la_resp = la_resp.OrderBy(e => e.error_consec).ToList();
                errors_tolog = errors_tolog.OrderBy(l => l.bStatErrorConsec).ToList();

                unity.BSErrorsRepository.InsertMulti(errors_tolog);
                unity.Commit();
            }

            return la_resp;
        }
    }

    public class XLSFileError : ErrorRecordBase
    {
        public int file_idx { get; set; }
        public int? sheet_idx { get; set; }
        public int? row_idx { get; set; }
        public int? cell_idx { get; set; }
        public string file_name { get; set; }
        public string sheet_name { get; set; }
        public string cell_value { get; set; }
        public XLSFileErrorDepth error_depth { get; set; }

        public XLSFileError() { }
    }

    public class BankStatementError : ErrorRecordBase
    {
        public int row_idx { get; set; }
        public string sheet_name { get; set; }
        public BankTypeClasification? record_type { get; set; }

        public BankStatementError() { }
    }

    public class BankConciliationError : ErrorRecordBase
    {
        public long bstat_id { get; set; }
        public string bstat_tpv { get; set; }
        public decimal bstat_amount { get; set; }
        public decimal? bstat_fee { get; set; }
        public DateTime bstat_date { get; set; }

        public BankConciliationError() { }
    }

    public class JSErrorRecord
    {
        public int error_consec { get; set; }
        public string error_header { get; set; }
        public string error_description { get; set; }
        public string[] error_details { get; set; }
        public string error_severity_str { get; set; }
        public string action_taken_str { get; set; }
        public string exception_msg { get; set; }
        public string exception_stack { get; set; }
        public bool has_exception { get; set; }

        public JSErrorRecord() { }
    }
}