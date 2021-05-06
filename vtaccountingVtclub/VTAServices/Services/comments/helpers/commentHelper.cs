using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.comments.model;

namespace VTAworldpass.VTAServices.Services.comments.helpers
{
    public abstract class CommentHelper
    {
        protected ICollection<Comments> ConvertModelToHelper(IEnumerable<tblinvoicecomments> list)
        {
            List<Comments> listHelper = new List<Comments>();
            int row = 1;
            foreach (tblinvoicecomments model in list)
            {
                Comments helper = new Comments();
                helper.row           = row;
                helper.IdComment     = model.idinvoicecomment;
                helper.Invoice       = (int)model.idinvoice;
                helper.IdUser        = model.iduser;
                helper.UserComment   = string.Concat(model.tblusers.userPersonName, " ", model.tblusers.userPersonLastName);
                helper.Description   = model.invoicecommentdescription == null ? " " : model.invoicecommentdescription;
                helper.CreactionDate = model.invoicecommentcreactiondate;
                helper.CreactionDateString = DateTimeUtils.ParseDatetoString(model.invoicecommentcreactiondate);
                listHelper.Add(helper); row++;
            }
            return listHelper;
        }

        protected ICollection<Comments> ConvertModelToHelper(IEnumerable<tblincomecomments> list)
        {
            List<Comments> listHelper = new List<Comments>();
            int row = 1;
            foreach (tblincomecomments model in list)
            {
                Comments helper = new Comments();
                helper.row       = row;
                helper.IdComment = model.idincomecomment;
                helper.Income    = model.idincome;
                helper.IdUser    = model.iduser;
                helper.UserComment = string.Concat(model.tblusers.userPersonName, " ", model.tblusers.userPersonLastName);
                helper.Description = model.invcomeCommentdescription == null ? " " : model.invcomeCommentdescription;
                helper.CreactionDate       = model.incomeCommentcreactiondate;
                helper.CreactionDateString = DateTimeUtils.ParseDatetoString(model.incomeCommentcreactiondate);
                listHelper.Add(helper); row++;
            }
            return listHelper;
        }

        protected void PrepareToSave(Comments helper, tblinvoicecomments model)
        {
            model.idinvoice = helper.Invoice;
            model.invoicecommentdescription   = helper.Description;
            model.invoicecommentcreactiondate = DateTime.Now;
        }

        protected void PrepareToSave(Comments helper, tblincomecomments model)
        {
            model.idincome = helper.Income;
            model.invcomeCommentdescription = helper.Description;
            model.incomeCommentcreactiondate = DateTime.Now;
        }
    }
}