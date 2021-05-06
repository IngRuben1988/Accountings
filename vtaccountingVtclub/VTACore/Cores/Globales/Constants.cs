using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTACore.Cores.Globales
{
    public class Constants
    {
        public const string NotPermitAction = "Carece de los permisos para acceder";
        public const string NotWorkIn       = "S0001";
        public const string FailExecution   = "Proceso fallido:";
        public const string SussesExecution = "proceso exitoso:";
        public const string DriveAttachment = "Attachments";
        public const string DriveReconciliations = "Reconciliations";
        public const long   FilzeSize       = 2097153L;
        public const string FileType        = "txt,pdf,docx,doc,xls,xlsx,ppt,pptx,jpg,bmp,png,gif";
        public const bool   ActiveRecord    = true;
        public const bool   UnactiveRecord  = false;
        public static int[] methodPayment = new int[] { 5, 11 };//Deposit, Transfer
        public static int[] methodPaymentVta = new int[] { 4, 6 };//Cheques,Transferencias
        public static int[] sourceData = new int[] { 1, 4, 5 };//Ingresos, Pagos, Fondos
    }
}