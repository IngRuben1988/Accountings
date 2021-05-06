using System;
using System.Collections.Generic;
using System.Web;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTAServices.Services.attachments.model;

namespace VTAworldpass.VTAServices.Services.attachments.helpers
{
    public abstract class AttachmentsHelper
    {
        public List<attachment> convertblPropAttachToModelHelper(ICollection<tblinvoiceattach> collection)
        {
            List<attachment> lst = new List<attachment>();
            int i = 1;
            foreach (tblinvoiceattach model in collection)
            {
                attachment helper = new attachment();
                helper = this.convertblPropAttachToModelHelper(model);
                helper.row = i; lst.Add(helper); i++;
            }
            return lst;
        }

        public List<attachment> convertblPropAttachToModelHelper(ICollection<tblincomeattach> collection)
        {
            List<attachment> lst = new List<attachment>();
            int i = 1;
            foreach (tblincomeattach model in collection)
            {
                attachment helper = new attachment();
                helper = this.convertblPropAttachToModelHelper(model);

                helper.row = i; lst.Add(helper); i++;
            }
            return lst;
        }

        public attachment convertblPropAttachToModelHelper(tblinvoiceattach model)
        {
            HttpRequest oRequest = HttpContext.Current.Request;
            string host = oRequest.Url.Authority;
            string path = oRequest.ApplicationPath;

            attachment helper = new attachment();
            helper.item         = model.idinvoiceattach;
            helper.parent       = (int)model.idinvoice;
            helper.typefile     = model.idattach;
            helper.typefilename = model.tblattachments.attachmentname;
            helper.filename     = model.invoiceattachname;
            helper.filenamesys  = model.invoiceattachshortname;
            helper.contentType  = model.invoiceattachcontenttype;
            helper.directory    = model.invoiceattachdirectory;
            helper.directoryfull = string.Concat(model.invoiceattachdirectory, "/", model.invoiceattachshortname);
            helper.url = string.Concat(host, "/utilsapp/downloadbinarydocumentBills/", model.idinvoiceattach);
            helper.datechange = model.invoiceattachdatelastchange == null ? null : string.Format("{0:dd/MM/yyyy}", model.invoiceattachdatelastchange);
            return helper;
        }

        public attachment convertblPropAttachToModelHelper(tblincomeattach model)
        {
            HttpRequest oRequest = HttpContext.Current.Request;
            string host = oRequest.Url.Authority;
            string path = oRequest.ApplicationPath;

            attachment helper = new attachment();
            helper.item = model.idincomeattach;
            helper.parent       = unchecked((int)model.idincome);
            helper.typefile     = model.idattach;
            helper.typefilename = model.tblattachments.attachmentname;
            helper.filename     = model.incomeattachname;
            helper.filenamesys  = model.incomeattachshortname;
            helper.contentType  = model.incomeattachcontenttype;
            helper.directory    = model.incomeattachdirectory;
            helper.directoryfull = string.Concat(model.incomeattachdirectory, "/", model.incomeattachshortname);
            helper.url = string.Concat(host, "/utilsapp/downloadbinarydocumentBills/", model.idincomeattach, "/", 2);
            helper.datechange = model.incomeattachdatelastchange == null ? null : string.Format("{0:dd/MM/yyyy}", model.incomeattachdatelastchange);
            return helper;
        }
    }
}