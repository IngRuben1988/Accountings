using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using VTAworldpass.Web.VTACore.Utils;
using VTAworldpass.VTACore;
using VTAworldpass.VTACore.Utils;
using VTAworldpass.VTAServices.Services.accounts;
using VTAworldpass.VTAServices.Services.attachments.model;
using VTAworldpass.VTACore.Database;
using VTAworldpass.VTAServices.Services.attachments.helpers;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Logger;

namespace VTAworldpass.VTAServices.Services.attachments.implements
{
    public class AttachmentServices : AttachmentsHelper, IAttachmentServices
    {
        private readonly IAccountServices accountServices;
        private readonly UnitOfWork unity = new UnitOfWork();

        public AttachmentServices(IAccountServices _accountServices)
        {
            this.accountServices = _accountServices;
        }

        public List<attachment> GetAttachemntInc(int id)
        {
            try
            {
                List<attachment> lst = new List<attachment>();
                var data = unity.IncomeAttachmentRepository.Get(x => x.tblincome.idincome == id, null, "tblattachments,tblincome").ToList();
                if (data != null)
                {
                    return lst = convertblPropAttachToModelHelper(data);
                } else
                {
                    return lst;
                }
            }
            catch (Exception e)
            {
                throw new Exception("No es posible obtener la lista de archivos para esta propiedad: " + e.Message + "[ ---- Starck Trace] ----" + e.StackTrace);
            }
        }

        public List<attachment> GetAttachmentInv(int id)
        {
            try
            {
                List<attachment> lst = new List<attachment>();
                var data = unity.InvoiceAttachmentRepository.Get(x => x.tblinvoice.idinvoice == id, null, "tblattachments,tblinvoice").ToList();
                if (data != null)
                {
                    return lst = convertblPropAttachToModelHelper(data);
                } else
                {
                    return lst;
                }
            }
            catch (Exception e)
            {
                throw new Exception("No es posible obtener la lista de archivos para esta propiedad: " + e.Message + "[ ---- Starck Trace] ----" + e.StackTrace);
            }
        }

        public tblincomeattach SaveAttachmentInc(tblincomeattach attach)
        {
            try
            {
                this.unity.IncomeAttachmentRepository.Insert(attach);
                this.unity.Commit();
                return this.unity.IncomeAttachmentRepository.Get(x => x.idincomeattach == attach.idincomeattach, null, "").FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception("No es posible guadar el registro del archivo: " + e.Message + "[ ---- Starck Trace] ----" + e.StackTrace);
            }
        }

        public tblinvoiceattach SaveAttachmentInv(tblinvoiceattach attach)
        {
            try
            {
                this.unity.InvoiceAttachmentRepository.Insert(attach);
                this.unity.Commit();
                return this.unity.InvoiceAttachmentRepository.Get(x => x.idinvoiceattach == attach.idinvoiceattach, null, "").FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception("No es posible guadar el registro del archivo: " + e.Message + "[ ---- Starck Trace] ----" + e.StackTrace);
            }
        }

        public tblincomeattach ObtainAttachmentInc(HttpFileCollectionBase file, int indexfile, int idAttach, int idIncome)
        {
            tblincomeattach attach = new tblincomeattach();
            FileInfo fi = new FileInfo(file[indexfile].FileName);
            var FIleNameLenght  = file[indexfile].FileName.Length;
            var FileName        = file[indexfile].FileName.Substring(0, FIleNameLenght - 5);
            var FileExtension   = fi.Extension;
            var FileContentType = file[indexfile].ContentType;
            var FIleSize = file[indexfile].ContentLength;
            Guid g = Guid.NewGuid();

            attach.idattach = idAttach;
            attach.idincome = idIncome;
            attach.incomeattachcontenttype    = FileContentType;
            attach.incomeattachdatelastchange = DateTime.Now;
            attach.incomeattachname           = string.Concat(StringUtils.CutLenghtString120Characters(StringUtils.ReplaceSpecialCharacters(FileName)), FileExtension);
            attach.incomeattachshortname      = string.Concat(StringUtils.CutLenghtString120Characters(StringUtils.ReplaceSpecialCharacters(string.Concat(DateTimeUtils.ParseDatetoStringFull(DateTime.Now), "-", g, "-", FileName))), FileExtension);
            attach.incomeattachuserlastchange = this.accountServices.AccountIdentity();
            attach.incomeattachactive         = Globals.activeRecord;
            return attach;
        }

        public tblinvoiceattach ObtainAttachmentInv(HttpFileCollectionBase file, int indexfile, int idAttach, int idInvoice)
        {
            tblinvoiceattach attach = new tblinvoiceattach();
            FileInfo fi = new FileInfo(file[indexfile].FileName);
            var FIleNameLenght  = file[indexfile].FileName.Length;
            var FileName        = file[indexfile].FileName.Substring(0, FIleNameLenght - 5);
            var FileExtension   = fi.Extension;
            var FileContentType = file[indexfile].ContentType;
            var FIleSize = file[indexfile].ContentLength;
            Guid g = Guid.NewGuid();

            attach.idattach = idAttach;
            attach.idinvoice = idInvoice;
            attach.invoiceattachcontenttype     = FileContentType;
            attach.invoiceattachdatelastchange  = DateTime.Now;
            attach.invoiceattachname            = string.Concat(StringUtils.CutLenghtString120Characters(StringUtils.ReplaceSpecialCharacters(FileName)), FileExtension);
            attach.invoiceattachshortname       = string.Concat(StringUtils.CutLenghtString120Characters(StringUtils.ReplaceSpecialCharacters(string.Concat(DateTimeUtils.ParseDatetoStringFull(DateTime.Now), "-", g, "-", FileName))), FileExtension);
            attach.invoiceattachuserlastchange  = this.accountServices.AccountIdentity();
            attach.invoiceattachactive          = Globals.activeRecord;
            return attach;
        }

        public bool DeleteAttachmentInv(int id)
        {
            var model = unity.InvoiceAttachmentRepository.Get(x => x.idinvoiceattach == id).FirstOrDefault();
            if (model != null)
            {
                try
                {
                    this.unity.InvoiceAttachmentRepository.Delete(model);
                    this.unity.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    Log.Error("NO se puede borrar registro de Invoice Attachments.", e);
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public bool DeleteAttachmentInc(int id)
        {
            var model = unity.IncomeAttachmentRepository.Get(x => x.idincomeattach == id).FirstOrDefault();
            if (model != null)
            {
                try
                {
                    this.unity.IncomeAttachmentRepository.Delete(model);
                    this.unity.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    Log.Error("NO se puede borrar registro de Invoice Attachments.", e);
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public attachment GetAttachmentInvSimple(int id)
        {
            return convertblPropAttachToModelHelper(this.unity.InvoiceAttachmentRepository.Get(x => x.idinvoiceattach == id, null, "").FirstOrDefault());
        }

        public attachment GetAttachemntIncSimple(int id)
        {
            return convertblPropAttachToModelHelper(this.unity.IncomeAttachmentRepository.Get(x => x.idincomeattach == id, null, "").FirstOrDefault());
        }
    }
}