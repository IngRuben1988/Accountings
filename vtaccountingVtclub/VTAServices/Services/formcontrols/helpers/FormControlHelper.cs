using System.Collections.Generic;
using System.Linq;
using VTAworldpass.VTAServices.Services.formcontrols.model;
using VTAworldpass.VTACore.Database;

namespace VTAworldpass.VTAServices.Services.formcontrols.helpers
{
    public abstract class FormControlHelper
    {
        public List<formselectmodel> ConvertToSelectModel(List<tblsegment> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tblsegment _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idsegment.ToString();
                select.valueint = _tbl.idsegment;
                select.text = _tbl.segmentname;
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }

        public List<formselectmodel> ConvertToSelectModel(List<tblcompanies> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tblcompanies model in list)
            {
                listToSend.Add(ConvertToSelectModel(model));
            }
            this.evaluateselected(listToSend);

            return listToSend;
        }

        public List<formselectmodel> ConvertToSelectModelFill(List<tblcompanies> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();

            foreach (tblcompanies model in list)
            {
                listToSend.Add(ConvertToSelectModel(model));
            }
            this.evaluateselected(listToSend);

            return listToSend;
        }

        public List<formselectmodel> ConvertToSelectModel(List<tblcurrencies> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tblcurrencies _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idCurrency.ToString();
                select.valueint = _tbl.idCurrency;
                select.text = _tbl.currencyName;
                select.shorttext = _tbl.currencyAlphabeticCode;
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }

        public List<formselectmodel> ConvertToSelectModel(List<tblaccountsl1> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tblaccountsl1 _tbl in list.OrderBy(c => c.accountl1order))
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idaccountl1.ToString();
                select.text = _tbl.accountl1name;
                select.valueint = _tbl.idaccountl1;
                select.shorttext = _tbl.accountl1name;
                listToSend.Add(select);
            }

            this.evaluateselected(listToSend);
            return listToSend;
        }

        public List<formselectmodel> ConvertToSelectModel(List<tblaccountsl2> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tblaccountsl2 _tbl in list.OrderBy(c => c.accountl2order).ToList())
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idaccountl2.ToString();
                select.text = _tbl.accountl2description;
                select.valueint = _tbl.idaccountl2;
                select.shorttext = _tbl.accountl2name;
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }

        public List<formselectmodel> ConvertToSelectModel(List<tblaccountsl3> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tblaccountsl3 _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idaccountl3.ToString();
                select.text = _tbl.accountl3description;
                select.valueint = _tbl.idaccountl3;
                select.shorttext = _tbl.accountl3name;
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }

        public List<formselectmodel> ConvertToSelectModel(List<tblaccountsl4> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tblaccountsl4 _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idAccountl4.ToString();
                select.valueint = _tbl.idAccountl4;
                select.text = _tbl.accountl4description;
                select.shorttext = _tbl.accountl4name;
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }

        public List<formselectmodel> ConvertToSelectModel(List<tblaccounttypereport> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tblaccounttypereport _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idaccounttypereport.ToString();
                select.valueint = _tbl.idaccounttypereport;
                select.text = _tbl.accounttypereportname;
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }

        public List<formselectmodel> ConvertToSelectModel(List<int> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            foreach (int _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.ToString();
                select.valueint = _tbl;
                select.text = _tbl.ToString();
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }


        protected List<formselectmodel> ConvertToSelectModel(List<tblusercompanies> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend = ConvertToSelectModel(list.Select(c=> c.tblcompanies).OrderBy(x => x.companyname).Distinct().ToList());
            this.evaluateselected(listToSend);
            return listToSend;
        }


        protected List<formselectmodel> convertToSelectModel(List<tblcurrencies> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());
            foreach (tblcurrencies _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idCurrency.ToString();
                select.text = string.Concat("[", _tbl.currencyAlphabeticCode, "]", "-", _tbl.currencyName.ToString());
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }

        protected List<formselectmodel> convertToSelectModel(List<tblexternalgroup> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tblexternalgroup _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select = this.convertToSelectModel(_tbl);
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }

        protected formselectmodel convertToSelectModel(tblexternalgroup model)
        {
            formselectmodel select = new formselectmodel();
            select.value = model.IdExternalGroup.ToString();
            select.text = model.externalgroupShortName.ToString();
            select.valueint = model.IdExternalGroup;
            return select;
        }

        /*****************************************************************************/

        protected formselectmodel ConvertToSelectModel(tblcompanies model)
        {
            formselectmodel select = new formselectmodel();
            select.valueint = model.idcompany;
            select.value = model.idcompany.ToString();
            select.text = model.companyname.ToString();
            select.shorttext = model.companyshortname;
            return select;
        }

        protected List<formselectmodel> ConvertToSelectModel(List<tblattachments> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());
            foreach (tblattachments _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idattachment.ToString();
                select.text = _tbl.attachmentname;
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }


        protected List<formselectmodel> ConvertToSelectModel(List<tblSuppliers> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());
            foreach (tblSuppliers _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idSupplier.ToString();
                select.valueint = _tbl.idSupplier;
                select.text = _tbl.supplierName;
                select.shorttext = _tbl.supplierName;
                if (listToSend.Contains(select) == false) { listToSend.Add(select); }
            }
            return listToSend;
        }

        protected List<formselectmodel> ConvertToSelectModel(List<tblbugettype> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tblbugettype _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idbudgettype.ToString();
                select.valueint = _tbl.idbudgettype;
                select.text = _tbl.budgettypename;
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }

        protected List<formselectmodel> ConvertToSelectModel(List<tblbankaccount> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tblbankaccount _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idbaccount.ToString();
                select.valueint = _tbl.idbaccount;
                select.text = string.Concat(_tbl.baccountshortname, " ", _tbl.tblbank.bankshortname, " ", _tbl.tblcurrencies.currencyAlphabeticCode, " ", _tbl.tblcompanies.companyshortname);
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }


        protected List<formselectmodel> convertToSelectModel(List<tblbaccounprodttype> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tblbaccounprodttype _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.tblbankprodttype.idbankprodttype.ToString();
                select.valueint = _tbl.tblbankprodttype.idbankprodttype;
                select.text = _tbl.tblbankprodttype.bankprodttypename;
                var x = listToSend.Find(y => y.valueint == select.valueint);
                if (x == null) listToSend.Add(select); ;
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }

        protected List<formselectmodel> ConvertToSelectModel(List<tblbaccounprodttype> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tblbaccounprodttype _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.tblbankprodttype.idbankprodttype.ToString();
                select.valueint = _tbl.tblbankprodttype.idbankprodttype;
                select.text = _tbl.tblbankprodttype.bankprodttypename;
                var x = listToSend.Find(y => y.valueint == select.valueint);
                if (x == null) listToSend.Add(select); ;
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }

        protected List<formselectmodel> ConvertToSelectModel(List<tblbankaccntclasification> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tblbankaccntclasification _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idbankaccntclasification.ToString();
                select.valueint = _tbl.idbankaccntclasification;
                select.text = _tbl.bankaccntclasname;
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }

        protected List<formselectmodel> ConvertToSelectModel(List<tbltpv> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());
            foreach (tbltpv _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select = this.ConvertToSelectModel(_tbl);
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }

        protected formselectmodel ConvertToSelectModel(tbltpv model)
        {
            formselectmodel select = new formselectmodel();
            select.value = model.idtpv.ToString();
            select.text = model.tpvidlocation.ToString();
            select.valueint = model.idtpv;
            return select;
        }

        protected List<formselectmodel> ConvertToSelectModel(List<tblsourcedata> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tblsourcedata _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idsourcedata.ToString();
                select.valueint = _tbl.idsourcedata;
                select.text = _tbl.sourcedataname.ToString();
                select.shorttext = _tbl.sourcedataname;
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);

            return listToSend;
        }

        protected List<formselectmodel> ConvertToSelectModel(List<tbloperationtype> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tbloperationtype _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idOperationType.ToString();
                select.valueint = _tbl.idOperationType;
                select.text = _tbl.operationTypeName.ToString();
                select.shorttext = _tbl.operationTypeName;
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);

            return listToSend;
        }


        /************** Evaluate Selected *******************************************/
        protected void evaluateselected(List<formselectmodel> list)
        {
            if (list.Count() == 2) list[1].selected = list[1].value;
        }

        protected List<formselectmodel> convertToSelectModel(List<tblusers> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());
            foreach (tblusers _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idUser.ToString();
                select.text = _tbl.userPersonName.ToString() + " " + _tbl.userPersonLastName.ToString();
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);
            return listToSend;
        }

        protected List<formselectmodel> convertToSelectModel(List<tblprofilesaccounts> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            listToSend.Add(new formselectmodel().initialize());

            foreach (tblprofilesaccounts _tbl in list)
            {
                formselectmodel select = new formselectmodel();
                select.value = _tbl.idprofileaccount.ToString();
                select.text = _tbl.profileaccountname.ToString();
                listToSend.Add(select);
            }
            this.evaluateselected(listToSend);

            return listToSend;
        }

        protected List<accountmodel> convertToSelectModelAccount(List<tblbankaccount> list)
        {
            List<formselectmodel> listToSend = new List<formselectmodel>();
            List<accountmodel> lst = new List<accountmodel>();

            foreach (tblbankaccount _tbl in list)
            {
                bool result = lst.Any(x => x.idCompany == _tbl.idcompany);
                if (result == false)
                {
                    accountmodel account = new accountmodel();
                    account.idCompany = _tbl.idcompany;
                    account.companyName = _tbl.tblcompanies.companyname;
                    lst.Add(account);
                    var baccount = list.Where(x => x.idcompany == _tbl.idcompany).ToList();
                    foreach (var tbl in baccount)
                    {
                        formselectmodel select = new formselectmodel();
                        select.value = tbl.idbaccount.ToString();
                        select.valueint = tbl.idbaccount;
                        select.text = string.Concat(tbl.baccountshortname, " ", tbl.tblbank.bankshortname, " ", tbl.tblcurrencies.currencyAlphabeticCode, " ", tbl.tblcompanies.companyshortname);
                        account.formSelectModel.Add(select);
                    }
                }
            }
            return lst;
        }

        protected List<permissionsmodel> convertToSelectModelPermissions(List<tblpermissions> list)
        {
            var menupermission = this.GeneratePermissions(list);

            return menupermission;
        }

        private List<permissionsmodel> GeneratePermissions(List<tblpermissions> sourcepermission)
        {
            var menuList = new List<permissionsmodel>();
            foreach (var modelPermission in sourcepermission)
            {
                //0 indica que se trata de un nodo padre
                if (modelPermission.permissionParentId == 0)
                {
                    // uri para accesar una view bajo el patron mvc

                    permissionsmodel model = new permissionsmodel(modelPermission.idPermission, modelPermission.permissionTitle, modelPermission.permissionDescription, GenerarSubPermissions(sourcepermission, modelPermission));

                    menuList.Add(model);
                }
            }
            return menuList;
        }

        private List<permissionsmodel> GenerarSubPermissions(List<tblpermissions> sourcepermissions, tblpermissions modelpermission)
        {
            var menuList = new List<permissionsmodel>();
            foreach (var objeto in sourcepermissions)
            {
                //la igualdad indica que objeto es hijo de sObj
                if (objeto.permissionParentId == modelpermission.idPermission)
                {

                    permissionsmodel model = new permissionsmodel(objeto.idPermission, objeto.permissionTitle, objeto.permissionDescription, GenerarSubPermissions(sourcepermissions, objeto));
                    menuList.Add(model);

                }
            }
            return menuList;
        }
    }
}
