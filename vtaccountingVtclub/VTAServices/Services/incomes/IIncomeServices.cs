using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTAworldpass.VTAServices.Services.incomes.model;

namespace VTAworldpass.VTAServices.Services.incomes
{
    public interface IIncomeServices
    {
        Task<IEnumerable<income>> getIncomesSearchAsync(int? number, int? company, decimal? ammountIni, decimal? ammountEnd, DateTime? applicationDateIni, DateTime? applicationDateFin, DateTime? creationDateIni, DateTime? creationDateFin);
        // Task<Income> GetIncome(long idIncome);
        Task<income> SaveIncome(income item);
        Task<income> UpdateIncome(income item);
        Task<int> DeleteIncome(long id);

        Task<incomeitem> GetIncomeItem(long id);
        Task<List<incomeitem>> GetIncomeItemList(long id);
        Task<incomeitem> SaveIncomeItem(incomeitem item);
        Task<incomeitem> UpdateIncomeItem(incomeitem item);
        Task<int> DeleteIncomeItem(long id);

        Task<incomepayment> GetIncomeMovement(long id);
        Task<List<incomepayment>> GetIncomeMovementsList(long id);
        Task<incomepayment> SaveIncomeMovement(incomepayment item);
        Task<int> DeleteIncomeMovement(long id);

    }
}
