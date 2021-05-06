
namespace VTAworldpass.VTACore.Helpers
{
    public class Globals
    {
        public const string NotPermitAction = "Carece de los permisos para acceder";
        public const string NotWorkIn       = "S0001";
        public const string FailExecution   = "Proceso fallido:";
        public const string SussesExecution = "proceso exitoso:";
        public const string DriveAttachment = "Attachments";
        public const string DriveReconciliations = "Reconciliations";
        public const long   FilzeSize      = 2097153L;
        public const string FileType       = "txt,pdf,docx,doc,xls,xlsx,ppt,pptx,jpg,bmp,png,gif";
        public const bool   ActiveRecord   = true;
        public const bool   UnactiveRecord = false;

        public static bool  activeRecord   = true;
        public static bool  unactiveRecord = true;

        public static int   MaxQuickDescriptionCharacters = 250;
        public static int   MaxQuickDescriptionFileCharacters = 120;
        public static int   QuickDescriptionCharacters128 = 128;
        public static int   MaxQuickDescriptionCharactersTooltip = 50;

        public static int   invoicedItemStatus_sinEstatus = 0;
        public static int   invoicedItemStatus_SinRevisar = 1;
        public static int   invoicedItemStatus_Aprobado   = 2;
        public static int   invoicedItemStatus_Rechazado  = 3;
        public static int   invoicedItemStatus_Extemporal = 4;

        public static string ApplicationSession = "VTAccountSession";
        public static string ApplicationCookies = "VTAccountingCookie";
        public static string AccesVTCPermission = "AccesVTCPermission";

        // budget DB
        public static int tipoCalculoFechaFinal = 1; // Typo 1: semana DL // tipo 2: días //tipo 3: Libre 
        public static int diasCalculoFechaFinal = 7; // day to calculate

        // day to add and substract
        public static int Oneday = 1;
        public static int OnedayNegative = -1;

        // Días de la semana por numeros
        public static int Domingo = 0;
        public static int Lunes = 1;
        public static int Martes = 2;
        public static int Miercoles = 3;
        public static int Jueves = 4;
        public static int Viernes = 5;
        public static int Sabado = 6;

        // Week Days
        public static double WeeksToExtemporal = 0.28;
        public static double WeekDays = 7;
        public static int    OneInt   = 1;
        // Max Result EntotyFramework Searchs
        public static int    EntityMax150PredefinedResult  = 150;
        public static int    EntityMax1000PredefinedResult = 1000;

        // primer dia del mes
        public static int PrimerdiaMes = 1;

        // Moths before AnnualChange
        public static int MaxMounthsBeforeEndYear2 = 2;

        //Moths to end StartFinancial Year
        public static int[] MothsEndtartFinancialYear = { 12, 1 };

        public static int menosDiaAnteriorSemana = -1;

        // Meses para buscar fondos predeter,inados
        public static int MonthstoSearchTblFondos = 2;

        // Predeterimnado
        public static decimal vta_TAX = 15.00m;
    }

    public class SystemControl
    {
        public const string VTADenied = "No tiene permiso o acceso a este recurso";
        public const string VTAConfirm = "La accion se ha ejercido";
        public const string VTAFail = "Proceso fallido:";
        public const string VTASusses = "proceso exitoso:";
        public const string VTAUserNOAccess = "No existe el usuario o esta desactivado.";
        public const string VTAModuleNOAccess = "No tiene permisos para acceder al modulo.";

        public static string[] VTAControllers =
            new string[]
            {
                "AccountController",    //0
                "BudgetController",     //1
                "ComplementController", //2
                "ConfigController",     //3
                "FormcontrolController",//4
                "HomeController",       //5
                "IncomeController",     //6
                "InvoiceController",    //7
                "ReportController",     //8
                "UtilsappController"    //9
            };

        public static string[] VTAActions =
            new string[]
            {
                "Save",     //0
                "Update",   //1
                "Delete",   //2
                "Select",   //3
            };

        public static string[] VTAMessages =
            new string[]
            {
                "Registro fue guardado",    //0
                "Registro fue actualizado", //1
                "Registro fue Eliminado",   //2
                "Consulta exitosa"          //3
            };

    }

    //public enum Currencies
    //{
    //    Canadian_Dollar = 1,
    //    Euro            = 2,
    //    Mexican_Peso    = 3,
    //    US_Dollar       = 4,
    //    Dominican_Republic_Peso = 5,
    //    Reais_Brasil    = 6
    //};

    public enum Method_Exchangue_Currencies
    {
        No_method = 1,
        Self      = 2,
        Currency_Exchangue_Dictionary = 3
    };

    public enum InvoicedItemStatus
    {
        SinEstatus = 0,
        SinRevisar = 1,
        Aprobado   = 2,
        Rechazado  = 3,
        Extemporal = 4
    };

    public class Enum
    {
        //public enum InvoiceIncomeStatus
        //{
        //    invoicedItemStatus_sinEstatus = 0,
        //    invoicedItemStatus_SinRevisar = 1,
        //    invoicedItemStatus_Aprobado = 2,
        //    invoicedItemStatus_Rechazado = 3,
        //    invoicedItemStatus_Extemporal = 4
        //}

        //public enum ExpenseReport
        //{
        //    Expenses = 1,
        //    ExpensesConliationsIn = 2
        //}

        //public enum Currencies
        //{
        //    Canadian_Dollar = 1,
        //    Euro = 2,
        //    Mexican_Peso = 3,
        //    US_Dollar = 4,
        //    Dominican_Republic_Peso = 5,
        //    Reais_Brasil = 6
        //};

    }

    //public enum FinancialStateReport
    //{
    //    Balance = 1,
    //    AccountHistory = 2,
    //    MaxBalance = 3,
    //    AccountHistoryConciliationsIn = 4,
    //    AccountHistoryOnlyConciliationsIn = 5,
    //    MaxBalanceConciliationIn = 6,
    //    BalanceConciliationIn = 7,
    //};

    //public enum BankAccountReconcilitionStatus
    //{
    //    Sin_Estatus = -1,
    //    Sin_conciliar = 0,
    //    Completo = 1,
    //    Parcial = 2,
    //    Error = 3,
    //};

    //public enum BankAccountReconciliationMethod
    //{
    //    SinConciliar = 1,
    //    Manual = 2,
    //    Sistema = 3,
    //};

    //public enum PaymentMethods_Bank_Report
    //{
    //    Transfer = 9,
    //    Deposit = 4,
    //};

    //public enum Record
    //{
    //    Deactivated = 0,
    //    Activeted = 1
    //};

    //public enum BankAccounType
    //{ // Se debe modificar segun la tabla
    //    SinTipo = 1,
    //    Débito = 2,
    //    Credito = 3,
    //    Cheques = 4,
    //    Efectivo = 5,
    //    Transferencias = 6
    //};

    //public enum ExpenseReport
    //{
    //    Expenses = 1,
    //    ExpensesConliationsIn = 2
    //}

    public enum Segment
    {
        Hotel = 1,
        Proveedor = 2,
        Construccion = 3,
        Excellent = 4,
        Fundacion = 5,
        Worldpass = 6,
        Vacationtime = 7,
        Nextpropertyadvisor = 8,
        Corporativo = 9
    };
}