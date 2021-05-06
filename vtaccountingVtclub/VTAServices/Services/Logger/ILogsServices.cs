using System.Collections.Generic;
using System.Threading.Tasks;
using VTAworldpass.VTACore.Database;

namespace VTAworldpass.VTAServices.Services.Logger
{
    public interface ILogsServices
    {
        Task<int> addTblLog(tblincome model, string message);
        Task<int> addTblLog(tblincomeitem model, string message);
        Task<int> addTblLog(tblincomemovement model, string message);

        Task<int> addTblLog(tblinvoice model, string message);
        Task<int> addTblLog(tblinvoiceditem model, string message);
        Task<int> addTblLog(tblpayment model, string message);
        void addTblLog(tblfondosmaxammount model, string message);
        void addTblLog(tblfondos model, string message);
        void addTblLog(List<tblbankstatements> model, string message);
        void addTblLog(List<tblbankstatements2> model, string message);
    }
}
