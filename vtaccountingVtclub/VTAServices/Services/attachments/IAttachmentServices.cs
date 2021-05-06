using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTAServices.Services.accounts.model;
using VTAworldpass.VTAServices.Services.attachments.model;

namespace VTAworldpass.VTAServices.Services.attachments
{
    public interface IAttachmentServices
    {
        tblinvoiceattach ObtainAttachmentInv(HttpFileCollectionBase file, int indexfile, int idAttach, int idInvoice);
        tblincomeattach  ObtainAttachmentInc(HttpFileCollectionBase file, int indexfile, int idAttach, int idIncome);
        tblinvoiceattach SaveAttachmentInv(tblinvoiceattach attach);
        tblincomeattach  SaveAttachmentInc(tblincomeattach attach);
        attachment GetAttachmentInvSimple(int id);
        attachment GetAttachemntIncSimple(int id);
        List<attachment> GetAttachmentInv(int id);
        List<attachment> GetAttachemntInc(int id);
        bool DeleteAttachmentInv(int id);
        bool DeleteAttachmentInc(int id);
    }
}