using System;
using System.Configuration;
using System.IO;
using System.Web;
using VTAworldpass.VTACore.Logger;

namespace VTAworldpass.VTAServices.Services.drive.model
{
    public class filemodel
    {
        public filemodel(){}


        public string systemFileName {
            get {
                Guid newID = Guid.NewGuid();
                return DateTime.Now.ToString("yyyymmddhhMMss") + newID.ToString();
            }
        }

        public string pathRelative
        {
            get
            {
                return ConfigurationManager.AppSettings["VTCloudControler"];
            }
        }

        public Stream streamFile { get; set; }


        public void UploadFile(string complemento,byte[] archivo)
        {
            File.WriteAllBytes(this.pathRelative + complemento, archivo);
        }

        public byte[] DownloadFile(string complemento)
        {
            return File.ReadAllBytes(this.pathRelative + complemento);
        }

        public void DeleteFile(string complemento)
        {
            File.Delete(this.pathRelative + complemento);
        }

    }

    public class file
    {
        public int row { get; set; }
        public int id { get; set; }
        public int idParent { get; set; }
        public string fileName { get; set; }
        public string systemFileName { get; set; }
        public string relativeDirecotry { get; set; }
        public string directory { get; set; }
        public string url { get; set; }
        public string contentType { get; set; }
        public string date { get; set; }
        public string extensionFile { get; set; }
        public long size { get; set; }
        public Stream stream { get; set; }



        public file()
        { }

        public file(string fileName, string contentType, long size, Stream stream)
        {
            this.fileName = fileName;
            this.contentType = contentType;
            this.extensionFile = this.getExtension(fileName);
            this.size = size;
            this.stream = stream;
        }

        public file(string fileName, string contentType, string extensionFile, long size, Stream stream)
        {
            this.fileName = fileName;
            this.contentType = contentType;
            this.extensionFile = extensionFile;
            this.size = size;
            this.stream = stream;
        }

        private string getExtension(string fileName)
        {
            try
            {
                return fileName.Substring(fileName.Length - 4, 3);
            }

            catch (Exception e)
            {
                Log.Error("No se puede obtener extensión de archivo. file -> getExtension", e);
                return "";
            }

        }
    }
}