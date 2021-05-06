using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTAServices.Services.comments.model;

namespace VTAworldpass.VTAServices.Services.comments
{
    public interface ICommentServices
    {
        void SaveInc(Comments comments);
        void SaveInv(Comments comments);
        tblinvoicecomments getbyId(int id);
        ICollection<tblinvoicecomments> GetCommentsInv(int idInvoice);
        ICollection<tblincomecomments>  GetCommentsInc(int idIncome);
        ICollection<Comments> GetCommentsByInv(int idInvoice);
        ICollection<Comments> GetCommentsByInc(int idIncome);
    }
}