using System;
using VTAworldpass.VTACore.Helpers;
using VTAworldpass.VTAServices.Services.accounts;
using VTAworldpass.VTAServices.Services.utilsapp;
using VTAworldpass.VTAServices.Services.utilsapp.helper;

namespace VTAworldpass.Business.Services.Implementations
{
    public class UtilsappServices : utilsappHelper,IUtilsappServices
    {
        private IAccountServices accountServices;

        public UtilsappServices(IAccountServices _accountServices)
        {
            this.accountServices = _accountServices;
        }


        public DateTime getStartDateCalendardocuments(DateTime? date, double? weeksBefore)
        {
            DateTime final = DateTime.Now;
            if (date != null) final = (DateTime)date;
            var permission = this.accountServices.isInPermission("outtimestart");
            if (permission == false)
            {
                if (weeksBefore == null) weeksBefore = Globals.WeeksToExtemporal;
                final = final.AddDays(-1 * ((double)weeksBefore * Globals.WeekDays));
                return final;
            }
            else
            {
                var year = DateTime.Now.Year - 1;
                return final = new DateTime(year, 1, 1);
            }
        }

        public DateTime getFinalDateCalendardocuments()
        {
            DateTime final = DateTime.Now;
            var permission = this.accountServices.isInPermission("outtimeend");
            if (permission == false)
            { return final; }
            else
            { return final = new DateTime(DateTime.Now.Year, 12, 31); }
        }

        public DateTime getMondayCurrentWeek(DateTime? date)
        {
            DateTime calculate = DateTime.Now;
            if (date != null) calculate = (DateTime)date;
            return this.getMondaybyDate(calculate);
        }

        public DateTime getSundayCurrentWeek(DateTime? date)
        {
            DateTime calculate = DateTime.Now;
            if (date != null) calculate = (DateTime)date;
            return this.getSundaybyDate(calculate);
        }

        public int diferenceDays(DateTime promise, DateTime start)
        {
            return this.daysDifference(promise, start);
        }

        //public List<MenuList> GenerarMenu(List<tblpermissions1> modelList)
        //{
        //    var menuList = new List<MenuList>();
        //    foreach (var model in modelList)
        //    {
        //        //0 indica que se trata de un nodo padre
        //        if (model.permissionParentId == 0)
        //        {
        //            //uri para accesar una view bajo el patron mvc
        //            var urlAction = model.permissionController + "/" + model.permissionAction;
        //            menuList.Add(new MenuList(urlAction, model.permissionTitle, GenerarSubMenu(modelList, model)));
        //        }
        //    }
        //    return menuList;
        //}

        //private List<MenuList> GenerarSubMenu(List<tblpermissions1> modelList, tblpermissions1 model)
        //{
        //    var menuList = new List<MenuList>();
        //    foreach (var objeto in modelList)
        //    {
        //        //la igualdad indica que objeto es hijo de sObj
        //        if (objeto.permissionParentId == model.idPermission)
        //        {
        //            var urlAction = "/" + objeto.permissionController + "/" + objeto.permissionAction;
        //            menuList.Add(new MenuList(urlAction, objeto.permissionTitle, GenerarSubMenu(modelList, objeto)));
        //        }
        //    }
        //    return menuList;
        //}

    }
}