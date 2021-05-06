using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTACore.Cores.Globales
{
    public class Variables
    {
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

        // Week Days
        public static double WeeksToExtemporal = 0.28;
        public static double WeekDays = 7;
        public static int    OneInt   = 1;
        // Max Result EntotyFramework Searchs
        public static int    EntityMax150PredefinedResult  = 150;
        public static int    EntityMax1000PredefinedResult = 1000;
        // Predeterimnado
        public static decimal vta_TAX = 15.00m;

        public static decimal ZeroDecimal = 0m;
    }
}