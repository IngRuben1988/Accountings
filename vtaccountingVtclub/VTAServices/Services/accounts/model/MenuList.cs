using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.accounts.model
{
    public class MenuList
    {
        public String UrlView { get; set; }
        public String Name { get; set; }
        public List<MenuList> SubMenu { get; set; }

        public MenuList(string urlview, string name, List<MenuList> submenu)
        {
            UrlView = urlview;
            Name = name;
            SubMenu = submenu;
        }
    }
}