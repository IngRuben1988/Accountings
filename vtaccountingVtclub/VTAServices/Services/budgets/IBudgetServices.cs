using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTAServices.Services.budgets.model;
using VTAworldpass.VTAServices.Services.invoices.model;
using VTAworldpass.VTAServices.Services.Models;

namespace VTAworldpass.VTAServices.Services.budgets
{
    public interface IBudgetServices
    {
        tblfondos getBudget(int idHotel, string date, int idpaymentMethod);
        financialstateModel getBudgetAvailable(int idHotel, DateTime date, int idpaymentMethod);
        financialstateModel getBudgetAvailable(int Company, DateTime date, int idBankAccount, int BankAccntType);
        Task<List<fondoModel>> getWeeklyBudgets(int? Type, int? id, int? idPaymentMethod, int? idCurrency, DateTime? fondofechaEntrega, DateTime? fondoFechaInicio, DateTime? fondoFechaFin, decimal? fondoMontoInicio, decimal? fondoMontoFin, int? idHotel);
        fondoModel GetFondo(int idFondo);
        Task<fondoModel> AddFondo(fondoModel helper);
        Task<fondoModel> UpdateFondoAsync(fondoModel helper);
        Task<fondoModel> DeleteFondo(fondoModel helper);
        fondoModel CalculateBudgetFinalDate(int? PaymentMethod, int? Hotel, int? idCurrency, string fondoFechaInicio);


        /************************************************/

        financialstateModel getTotalFinanceState(int idPaymentMethod, DateTime? startDate, DateTime endDate);

        /*********** BUDGET DELIMITER*************/

        IEnumerable<fondosmaxammountModel> searchBudgetLimiter(fondosmaxammountModel model);
        void saveBudgetLimiter(fondosmaxammountModel helper);
        void updateBudgetLimiter(fondosmaxammountModel helper);
        void deleteBudgetLimiter(int id);

        /***********************************************/

        List<fondoModel> convertToModelHelper(List<tblfondos> list);
        List<invoicepayment> convertToModelHelper(List<tblpayment> list);
    }
}
