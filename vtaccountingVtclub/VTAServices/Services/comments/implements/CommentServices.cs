using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTAServices.Services.accounts;
using VTAworldpass.VTAServices.Services.comments.helpers;
using VTAworldpass.VTAServices.Services.comments.model;

namespace VTAworldpass.VTAServices.Services.comments.implements
{
    public class CommentServices : CommentHelper, ICommentServices
    {
        private readonly UnitOfWork unity = new UnitOfWork();
        private readonly IAccountServices accountServices;

        public CommentServices(IAccountServices _accountServices)
        {
            this.accountServices = _accountServices;
        }


        public tblinvoicecomments getbyId(int id)
        {
            return this.unity.InvoiceCommentsRepository.Get(t => t.idinvoicecomment == id, null, "tblusers").FirstOrDefault();
        }

        public ICollection<Comments> GetCommentsByInc(int idIncome)
        {
            var result = this.unity.IncomeCommentsRepository.Get(t => t.idincome == idIncome, t => t.OrderByDescending(c => c.incomeCommentcreactiondate), "tblusers").ToList();
            if (result != null)
            {
                return this.ConvertModelToHelper(result);
            }
            return null;
        }

        public ICollection<Comments> GetCommentsByInv(int idInvoice)
        {
            var result = this.unity.InvoiceCommentsRepository.Get(t => t.idinvoice == idInvoice, t => t.OrderByDescending(c => c.invoicecommentcreactiondate), "tblusers").ToList();
            if (result != null)
            {
                return this.ConvertModelToHelper(result);
            }
            return null;
        }

        public ICollection<tblincomecomments> GetCommentsInc(int idIncome)
        {
            return this.unity.IncomeCommentsRepository.Get(t => t.idincome == idIncome, null, "tblusers").ToList();
        }

        public ICollection<tblinvoicecomments> GetCommentsInv(int idInvoice)
        {
            return this.unity.InvoiceCommentsRepository.Get(t => t.idinvoice == idInvoice, null, "tblusers").ToList();
        }

        public void SaveInv(Comments comments)
        {
            tblinvoicecomments model = new tblinvoicecomments();
            model.iduser = this.accountServices.AccountIdentity();
            this.PrepareToSave(comments, model);
            this.unity.InvoiceCommentsRepository.Insert(model);
            this.unity.Commit();
            comments.IdComment = model.idinvoicecomment;
        }

        public void SaveInc(Comments comments)
        {
            tblincomecomments model = new tblincomecomments();
            model.iduser = this.accountServices.AccountIdentity();
            this.PrepareToSave(comments, model);
            this.unity.IncomeCommentsRepository.Insert(model);
            this.unity.Commit();
            comments.IdComment = model.idincomecomment;
        }
    }
}