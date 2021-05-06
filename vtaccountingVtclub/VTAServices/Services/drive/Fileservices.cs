using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTACore.Logger;
using VTAworldpass.VTAServices.Services.drive.model;

namespace VTAworldpass.VTAServices.Services.drive
{
    public class Fileservices
    {

        public static string SubirArchivo(string destiny,HttpPostedFileBase parameter)
        {
            var Resuloperation="";
            filemodel archivo = new filemodel();
            if (parameter != null && parameter.ContentLength > 0)
            {
                try
                {
                    var _contenido = new byte[parameter.ContentLength];
                    archivo.UploadFile(destiny+DateTime.Now.ToString("yyyy"), _contenido);
                    Resuloperation = Globals.SussesExecution;
                }
                catch (Exception e)
                {
                    Log.Error(Globals.FailExecution+" subir archivo", e);
                    Resuloperation = Globals.FailExecution;
                }
            }
            return Resuloperation;
        }

        public IEnumerable<file> parseFileBasetoFile(ICollection<HttpPostedFileBase> files)
        {
            if (files.Count == 0) { return null; }

            else
            {
                List<file> list = new List<file>();

                foreach (HttpPostedFileBase model in files)
                {
                    file helper = new file(model.FileName, model.ContentType, model.ContentLength, model.InputStream);
                    list.Add(helper);
                }

                return new List<file>();
            }
        }

        public IEnumerable<file> parseFilesBasetoFile(HttpFileCollectionBase files, ref XLSFileImportErrors errorReport)
        {
            if (files.Count == 0)
            { errorReport.AddCriticalStop("No se proporcionaron archivos para procesar.", null);  return null; }
            else
            {
                List<file> list = new List<file>();
                for (int indexfile = 0; indexfile <= files.Count - 1; indexfile++)
                {
                    try
                    {
                        FileInfo _fileInfo = new FileInfo(files[indexfile].FileName);
                        file helper = new file(_fileInfo.Name, files[indexfile].ContentType, _fileInfo.Extension, 0, files[indexfile].InputStream);
                        list.Add(helper);
                    }
                    catch (Exception ex)
                    {
                        errorReport.AddFileUploadError("No se pudo leer el archivo proporcionado.", indexfile, files[indexfile].FileName, ex);
                        continue;
                    }
                }
                return list;
            }
        }
    }
}