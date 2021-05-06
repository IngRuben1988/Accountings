using System.Web;
using System.Web.Optimization;

namespace VTAworldpass
{
    public class BundleConfig
    {
        public static string[] WebPaths = { "~/Scripts/", "~/Content/", "" };
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {                
            bundles.Add(new ScriptBundle("~/bundles/fontawesome").Include(
                        WebPaths[1] + "/fontawesome/js/all.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(                        
                        WebPaths[1] + "/datatable/datatables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryconfirm").Include(
                        WebPaths[1] + "/jqueryconfirm/dist/jquery-confirm.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         WebPaths[0] + "/jquery-{version}.js",
                         WebPaths[0] + "/jquery-ui-1.12.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                         WebPaths[0] + "/jquery.validate.js",
                         WebPaths[0] + "/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                        WebPaths[0] + "/moment/min/moment-with-locales.min.js",
                        WebPaths[0] + "/moment/locale/es-us.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        WebPaths[0] + "/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                        WebPaths[0] + "/popper.js"));

            bundles.Add(new ScriptBundle("~/bundles/tempus_datetime").Include(
                        WebPaths[0] + "/tempus_dominuts_datetime/tempusdominus-bootstrap-4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/notify").Include(
                        WebPaths[0] + "/notify.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/axios").Include(
                        WebPaths[0] + "/axios.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        WebPaths[0] + "/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/chartJS").Include(
                        WebPaths[0] + "/Chart.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jsgrid").Include(
                        WebPaths[1] + "jsgrid-1.5.3/dist/jsgrid.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/account").Include(
                        WebPaths[0] + "account/commons/attachments.js",
                        WebPaths[0] + "account/commons/comments.js",
                        WebPaths[0] + "account/commons/urls.js",
                        WebPaths[0] + "account/commons/components.js",
                        WebPaths[0] + "account/account.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                        WebPaths[0] + "tempus_dominuts_datetime/tempusdominus-bootstrap-4.min.css",
                        WebPaths[1] + "jqueryconfirm/dist/jquery-confirm.min.css",
                        WebPaths[1] + "jsgrid-1.5.3/dist/jsgrid-theme.min.css",
                        WebPaths[1] + "jsgrid-1.5.3/dist/jsgrid.min.css",
                        WebPaths[1] + "fontawesome/css/all.min.css",
                        WebPaths[1] + "vtacss/IndexView.css",
                        WebPaths[1] + "bootstrap.min.css",
                        WebPaths[1] + "site.css",
                        WebPaths[1] + "vtacss/common/ButtonView.css",
                        WebPaths[1] + "vtacss/common/FontView.css",
                        WebPaths[1] + "vtacss/common/FormView.css",
                        WebPaths[1] + "vtacss/common/GridView.css",
                        WebPaths[1] + "vtacss/common/ImageView.css",
                        WebPaths[1] + "vtacss/common/InputView.css",
                        WebPaths[1] + "vtacss/common/PanelView.css",
                        WebPaths[1] + "vtacss/common/VtaMenu.css",
                        WebPaths[1] + "vtacss/common/VtaNotification.css"));

            //---------------- INCOME AND INVOICE  ---------------------------------
            bundles.Add(new ScriptBundle("~/bundles/utils").Include(
                         WebPaths[0] + "/utils/utils.js"));

            bundles.Add(new ScriptBundle("~/bundles/utils_datetime").Include(
                         WebPaths[0] + "/utils/datetime/datetime.js"));

            bundles.Add(new ScriptBundle("~/bundles/business").Include(
                         WebPaths[0] + "/utils/commos/commonsbusiness.js"));

            bundles.Add(new ScriptBundle("~/bundles/invoice").Include(
                         WebPaths[0] + "/invoice/methods/components.js",
                         WebPaths[0] + "/invoice/methods/modulefunctions.js",
                         WebPaths[0] + "/invoice/variables/urls.js",
                         WebPaths[0] + "/utils/datetime/datetime.js",
                         WebPaths[0] + "/utils/utils.js"));

            bundles.Add(new ScriptBundle("~/bundles/invoiceapp").Include(
                         WebPaths[0] + "/invoice/invoice.js"));

            bundles.Add(new ScriptBundle("~/bundles/search").Include(
                         WebPaths[0] + "/invoice/search.js"));
   

            bundles.Add(new ScriptBundle("~/bundles/commonincomeinvoice").Include(
                        WebPaths[0] + "/utils/commos/commonsincomeinvoice.js"));

            bundles.Add(new ScriptBundle("~/bundles/income").Include(
                        WebPaths[0] + "/income/methods/components.js",
                        WebPaths[0] + "/income/methods/modulefunctions.js",
                        WebPaths[0] + "/income/variables/urls.js",
                        WebPaths[0] + "/utils/utils.js",
                        WebPaths[0] + "/income/income.js",
                        WebPaths[0] + "/income/search.js"));

            bundles.Add(new ScriptBundle("~/bundles/expensedetails").Include(
                         WebPaths[0] + "/report/reporexpensesdetails.js"));

            bundles.Add(new ScriptBundle("~/bundles/expensemonthly").Include(
                         WebPaths[0] + "/report/reportexpenses.js",
                         WebPaths[0] + "/utils/utils.js",
                         WebPaths[0] + "/excelexportjs.js",
                         WebPaths[0] + "/tableToExcel.js",
                         WebPaths[0] + "/utils/commos/commonsbusiness.js",
                         WebPaths[0] + "/utils/commos/commonsincomeinvoice.js",
                         WebPaths[0] + "/account/commons/urls.js"));

            bundles.Add(new ScriptBundle("~/bundles/expensemonthlydetails").Include(
                         WebPaths[0] + "/report/reportexpensesdetails.js",
                         WebPaths[0] + "/utils/utils.js",
                         WebPaths[0] + "/excelexportjs.js",
                         WebPaths[0] + "/tableToExcel.js",
                         WebPaths[0] + "/utils/commos/commonsbusiness.js",
                         WebPaths[0] + "/utils/commos/commonsincomeinvoice.js",
                         WebPaths[0] + "/account/commons/urls.js"));

            bundles.Add(new ScriptBundle("~/bundles/cashclosing").Include(
                         WebPaths[0] + "/report/cashclosing.js",
                         WebPaths[0] + "/utils/datetime/datetime.js",
                         WebPaths[0] + "/utils/utils.js",
                         WebPaths[0] + "/excelexportjs.js",
                         WebPaths[0] + "/tableToExcel.js",
                         WebPaths[0] + "/utils/commos/commonsbusiness.js",
                         WebPaths[0] + "/utils/commos/commonsincomeinvoice.js",
                         WebPaths[0] + "/account/commons/urls.js"));

            bundles.Add(new ScriptBundle("~/bundles/cashbaccount").Include(
                         WebPaths[0] + "/report/cashbaccount.js",
                         WebPaths[0] + "/utils/datetime/datetime.js",
                         WebPaths[0] + "/utils/utils.js",
                         WebPaths[0] + "/excelexportjs.js",
                         WebPaths[0] + "/tableToExcel.js",
                         WebPaths[0] + "/utils/commos/commonsbusiness.js",
                         WebPaths[0] + "/utils/commos/commonsincomeinvoice.js",
                         WebPaths[0] + "/account/commons/urls.js"));


            bundles.Add(new ScriptBundle("~/bundles/user").Include(
                         WebPaths[0] + "/account/commons/components.js",
                         WebPaths[0] + "/account/commons/urls.js",
                         WebPaths[0] + "/config/users.js",
                         WebPaths[0] + "/excelexportjs.js",
                         WebPaths[0] + "/utils/datetime/datetime.js",
                         WebPaths[0] + "/utils/utils.js"));

#if DEBUG
            BundleTable.EnableOptimizations = false;
            #else
                BundleTable.EnableOptimizations = true;
            #endif
        }
    }
}
