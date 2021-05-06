using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using VTAworldpass.VTAServices.Services.bankreconciliation.model;
using VTAworldpass.VTAServices.Services.Models;
using static VTAworldpass.VTACore.Cores.Globales.Enumerables;

namespace VTAworldpass.VTACore.Helpers.xls
{
    public class GeneradorXLS
    {
        private string rutaArchivoCompleta = "";
        private string rutaArchivoRelativa = "";

        public string GetFileDir()
        {
            return this.rutaArchivoCompleta;
        }
        public string GetFileDirRelative()
        {
            return this.rutaArchivoRelativa;
        }

        public GeneradorXLS(string directory, string nombreArchivo)
        {
            //obtenemos la ruta de nuestro programa y concatenamos el nombre del archivo a crear
            var _route = "/" + directory + "/" + nombreArchivo;
            rutaArchivoCompleta = AppDomain.CurrentDomain.BaseDirectory + _route;
            rutaArchivoRelativa = _route;
        }

        public void GeneradorXLSFinancialStateComplete(financialstateModel financialstate)
        {
            try
            {
                //creamos el objeto documentDocument el cual creara el excel
                SLDocument document = new SLDocument();
                document.RenameWorksheet(SLDocument.DefaultFirstSheetName, " Cuenta- " + financialstate.CompanyName);

                // string[] hotels = new string[] { "LM", "Santosi", "Xoxula", "Las Escaleras", "Gran Guadalupe" };

                int _rowIndexStart = 6;
                int _columnIndexStart = 2;
                int arrayValuesCounter = 0;



                string[] columnNames = new string[] { "Fecha", "Origen", "Hotel/Cía.", "Tipo de Pago", "Identificador", "Monto", "Saldo" };
                int columnNamesCounter = 0;

                for (int rowIndex = _rowIndexStart - 2; rowIndex <= _rowIndexStart - 2; rowIndex++)
                // foreach(financialstateitem model  in financialstate.financialstateitemlist)
                {
                    for (int columnIndex = _columnIndexStart; columnIndex <= _columnIndexStart + columnNames.Count() - 1; columnIndex++)
                    {
                        document.SetCellValue(rowIndex, columnIndex, columnNames[columnNamesCounter]);
                        document.SetCellStyle(rowIndex, columnIndex, GeneradorXLSResources.Arial10Bold());
                        columnNamesCounter++;

                        if (rowIndex == _rowIndexStart - 2 && columnIndex == _columnIndexStart + columnNames.Count() - 1)
                        {
                            document.SetCellValue(rowIndex + 1, columnIndex, (decimal)financialstate.balanceBefore);
                        }
                    }
                }

                if (financialstate.financialstateitemlist.Count() != 0)
                {
                    financialstateitemModel[] itemlist = financialstate.financialstateitemlist.ToArray();
                    for (int rowIndex = _rowIndexStart; rowIndex <= financialstate.financialstateitemlist.Count() + _rowIndexStart - 1; rowIndex++)
                    {
                        for (int columnIndex = _columnIndexStart; columnIndex <= _columnIndexStart + columnNames.Count() - 1; columnIndex++)
                        {

                            switch (columnIndex)
                            {
                                case 2:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].aplicationDateString);
                                    document.SetColumnWidth(columnIndex, 12.00);
                                    break;
                                case 3:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].SourceDataName);
                                    document.SetColumnWidth(columnIndex, 12.00);
                                    break;
                                case 4:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].HotelName);
                                    document.SetColumnWidth(columnIndex, 18.00);
                                    break;
                                case 5:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].PaymentMethodName);
                                    document.SetColumnWidth(columnIndex, 18.00);
                                    break;
                                case 6:
                                    document.SetCellValue(rowIndex, columnIndex, (long)itemlist[arrayValuesCounter].Reference);
                                    document.SetColumnWidth(columnIndex, 12.00);
                                    break;
                                case 7:
                                    document.SetCellValue(rowIndex, columnIndex, (decimal)itemlist[arrayValuesCounter].appliedAmmount);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    break;
                                case 8:
                                    document.SetCellValue(rowIndex, columnIndex, (decimal)itemlist[arrayValuesCounter].balance);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    break;
                                default:
                                    document.SetCellValue(rowIndex, columnIndex, "");
                                    document.SetColumnWidth(columnIndex, 10.00);
                                    break;
                            }
                        }
                        arrayValuesCounter++;
                    }
                }

                if (File.Exists(rutaArchivoCompleta))
                {
                    File.Delete(rutaArchivoCompleta);
                    document.SaveAs(rutaArchivoCompleta);
                }
                else
                {
                    document.SaveAs(rutaArchivoCompleta);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio una Excepción en GeneradorXLS-GeneradorXLSFinancialState al generar archivo: " + ex.Message);
            }

        }

        public void GeneradorXLSFinancialStateBankModal(financialstateModel financialstate)
        {
            try
            {
                //creamos el objeto documentDocument el cual creara el excel
                SLDocument document = new SLDocument();
                document.RenameWorksheet(SLDocument.DefaultFirstSheetName, " Cuenta- " + financialstate.CompanyName);

                // string[] hotels = new string[] { "LM", "Santosi", "Xoxula", "Las Escaleras", "Gran Guadalupe" };

                int _rowIndexStart = 6;
                int _columnIndexStart = 2;
                int arrayValuesCounter = 0;



                string[] columnNames = new string[] { "Fecha", "Transacción", "Cargos", "Abonos", "Saldo" };
                int columnNamesCounter = 0;

                for (int rowIndex = _rowIndexStart - 2; rowIndex <= _rowIndexStart - 2; rowIndex++)
                // foreach(financialstateitem model  in financialstate.financialstateitemlist)
                {
                    for (int columnIndex = _columnIndexStart; columnIndex <= _columnIndexStart + columnNames.Count() - 1; columnIndex++)
                    {
                        document.SetCellValue(rowIndex, columnIndex, columnNames[columnNamesCounter]);
                        document.SetCellStyle(rowIndex, columnIndex, GeneradorXLSResources.Arial10Bold());
                        columnNamesCounter++;

                        if (rowIndex == _rowIndexStart - 2 && columnIndex == _columnIndexStart + columnNames.Count() - 1)
                        {
                            document.SetCellValue(rowIndex + 1, columnIndex, (decimal)financialstate.balanceBefore);
                        }
                    }
                }

                if (financialstate.financialstateitemlist.Count() != 0)
                {
                    financialstateitemModel[] itemlist = financialstate.financialstateitemlist.ToArray();
                    for (int rowIndex = _rowIndexStart; rowIndex <= financialstate.financialstateitemlist.Count() + _rowIndexStart - 1; rowIndex++)
                    {
                        for (int columnIndex = _columnIndexStart; columnIndex <= _columnIndexStart + columnNames.Count() - 1; columnIndex++)
                        {

                            switch (columnIndex)
                            {
                                case 2:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].aplicationDateString);
                                    document.SetColumnWidth(columnIndex, 12.00);
                                    break;
                                case 3:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].description);
                                    document.SetColumnWidth(columnIndex, 60.00);
                                    break;
                                case 4:
                                    {
                                        document.SetColumnWidth(columnIndex, 15.00);
                                        if (itemlist[arrayValuesCounter].accounttype == 1)
                                        { document.SetCellValue(rowIndex, columnIndex, (decimal)itemlist[arrayValuesCounter].appliedAmmount); }
                                        else { document.SetCellValue(rowIndex, columnIndex, ""); }
                                    }
                                    break;
                                case 5:
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    if (itemlist[arrayValuesCounter].accounttype == 2)
                                    { document.SetCellValue(rowIndex, columnIndex, (decimal)itemlist[arrayValuesCounter].appliedAmmount); }
                                    else { document.SetCellValue(rowIndex, columnIndex, ""); }
                                    break;
                                case 6:
                                    document.SetCellValue(rowIndex, columnIndex, (decimal)itemlist[arrayValuesCounter].balance);
                                    document.SetColumnWidth(columnIndex, 12.00);
                                    break;
                                default:
                                    document.SetCellValue(rowIndex, columnIndex, "");
                                    document.SetColumnWidth(columnIndex, 10.00);
                                    break;
                            }
                        }
                        arrayValuesCounter++;
                    }
                }

                if (File.Exists(rutaArchivoCompleta))
                {
                    File.Delete(rutaArchivoCompleta);
                    document.SaveAs(rutaArchivoCompleta);
                }
                else
                {
                    document.SaveAs(rutaArchivoCompleta);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio una Excepción en GeneradorXLS-GeneradorXLSFinancialState al generar archivo: " + ex.Message);
            }

        }

        public void GeneradorXLSbankReconciliation(List<bankreconciliationModel> list)
        {
            try
            {
                SLDocument document = new SLDocument();
                int _rowIndexStart = 6; // Fila de Inicio
                int _columnIndexStart = 2; // Columna Inicio
                int arrayValuesCounter = 0; // Arreglo a utilizar


                // Columnas/Cabecera del Reporte
                string[] columnNames = new string[] { "Terminal", "Hotel", "Moneda", "Cuenta", "Fecha depósito", "Ventas totales", "Ventas Ajuste", "Cargos por intercambio", "Comisiones", "Detalles de cargos/abonos", "Devoluciones", "Depósito", "Estado" }; // Columnas para el reporte
                int columnNamesCounter = 0;

                // Creando Cabeceras
                for (int rowIndex = _rowIndexStart - 1; rowIndex <= _rowIndexStart - 1; rowIndex++)
                {
                    for (int columnIndex = _columnIndexStart; columnIndex <= _columnIndexStart + columnNames.Count() - 1; columnIndex++)
                    {
                        document.SetCellValue(rowIndex, columnIndex, columnNames[columnNamesCounter]);
                        document.SetCellStyle(rowIndex, columnIndex, GeneradorXLSResources.Head_Style());
                        columnNamesCounter++;

                        /*
                        if (rowIndex == _rowIndexStart - 2 && columnIndex == _columnIndexStart + columnNames.Count() - 1)
                        {
                            document.SetCellValue(rowIndex + 1, columnIndex, financialstate.balanceBefore);
                        } */
                    }
                }

                if (list.Count() != 0)
                {
                    bankreconciliationModel[] itemlist = list.ToArray();
                    for (int rowIndex = _rowIndexStart; rowIndex <= list.Count() + _rowIndexStart - 1; rowIndex++)
                    {


                        var _backGround = this.ResolveStatusConciliationBackGroundColor(itemlist[arrayValuesCounter].statusconciliation);
                        SLStyle _BorderInorLast = new SLStyle();
                        if (rowIndex == (list.Count() + _rowIndexStart - 1)) { _BorderInorLast = GeneradorXLSResources.Bottom_Cell_Data_Style(); } else { _BorderInorLast = GeneradorXLSResources.Empty_Cell_Data_Style(); }
                        for (int columnIndex = _columnIndexStart; columnIndex <= _columnIndexStart + columnNames.Count() - 1; columnIndex++)
                        {

                            switch (columnIndex)
                            {
                                case 2:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].tpvname);
                                    document.SetColumnWidth(columnIndex, 12.00);
                                    document.SetCellStyle(rowIndex, columnIndex, GeneradorXLSResources.Left_Cell_Data_Style());
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 3:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].companyname);
                                    document.SetColumnWidth(columnIndex, 12.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 4:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].currencyname);
                                    document.SetColumnWidth(columnIndex, 12.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 5:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].baccountname);
                                    document.SetColumnWidth(columnIndex, 12.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 6:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].bankstatementaplicationdatestring);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 7:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].bankstatementappliedammountstring);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 8:
                                    document.SetCellValue(rowIndex, columnIndex, string.Empty);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 9:
                                    document.SetCellValue(rowIndex, columnIndex, string.Empty);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 10:
                                    document.SetCellValue(rowIndex, columnIndex, (decimal)itemlist[arrayValuesCounter].bankstatementBankFee);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 11:
                                    document.SetCellValue(rowIndex, columnIndex, string.Empty);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;

                                case 12:
                                    document.SetCellValue(rowIndex, columnIndex, string.Empty);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;

                                case 13:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].bankstatementappliedammountfinalstring);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;

                                case 14:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].statusconciliationname);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    document.SetCellStyle(rowIndex, columnIndex, GeneradorXLSResources.Right_Cell_Data_Style());
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                default:
                                    document.SetCellValue(rowIndex, columnIndex, "");
                                    document.SetColumnWidth(columnIndex, 10.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    break;
                            }
                        }
                        arrayValuesCounter++;
                    }
                }

                if (File.Exists(rutaArchivoCompleta))
                {
                    File.Delete(rutaArchivoCompleta);
                    document.SaveAs(rutaArchivoCompleta);
                }
                else
                {
                    document.SaveAs(rutaArchivoCompleta);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio una Excepción en GeneradorXLS-GeneradorXLSbankReconciliation al generar archivo: " + ex.Message);
                throw new Exception("No se puede generar el archivo de conciliación.", ex.InnerException);
            }
        }

        public void GeneradorXLSbankReconciliation(List<bankstatements> list)
        {
            try
            {
                SLDocument document = new SLDocument();
                int _rowIndexStart = 6; // Fila de Inicio
                int _columnIndexStart = 2; // Columna Inicio
                int arrayValuesCounter = 0; // Arreglo a utilizar


                // Columnas/Cabecera del Reporte
                string[] columnNames = new string[] { "Compañia", "Moneda", "Cuenta", "Fecha depósito", "Abonos", "Cargos", "Detalles de cargos/abonos", "Total", "Estado" }; // Columnas para el reporte
                int columnNamesCounter = 0;

                // Creando Cabeceras
                for (int rowIndex = _rowIndexStart - 1; rowIndex <= _rowIndexStart - 1; rowIndex++)
                {
                    for (int columnIndex = _columnIndexStart; columnIndex <= _columnIndexStart + columnNames.Count() - 1; columnIndex++)
                    {
                        document.SetCellValue(rowIndex, columnIndex, columnNames[columnNamesCounter]);
                        document.SetCellStyle(rowIndex, columnIndex, GeneradorXLSResources.Head_Style());
                        columnNamesCounter++;

                        /*
                        if (rowIndex == _rowIndexStart - 2 && columnIndex == _columnIndexStart + columnNames.Count() - 1)
                        {
                            document.SetCellValue(rowIndex + 1, columnIndex, financialstate.balanceBefore);
                        } */
                    }
                }

                if (list.Count() != 0)
                {
                    bankstatements[] itemlist = list.ToArray();
                    for (int rowIndex = _rowIndexStart; rowIndex <= list.Count() + _rowIndexStart - 1; rowIndex++)
                    {


                        var _backGround = this.ResolveStatusConciliationBackGroundColor(itemlist[arrayValuesCounter].statusconciliation);
                        SLStyle _BorderInorLast = new SLStyle();
                        if (rowIndex == (list.Count() + _rowIndexStart - 1)) { _BorderInorLast = GeneradorXLSResources.Bottom_Cell_Data_Style(); } else { _BorderInorLast = GeneradorXLSResources.Empty_Cell_Data_Style(); }
                        for (int columnIndex = _columnIndexStart; columnIndex <= _columnIndexStart + columnNames.Count() - 1; columnIndex++)
                        {

                            switch (columnIndex)
                            {
                                case 2:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].companyname);
                                    document.SetColumnWidth(columnIndex, 12.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 3:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].currencyName);
                                    document.SetColumnWidth(columnIndex, 12.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 4:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].baccountName);
                                    document.SetColumnWidth(columnIndex, 12.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 5:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].bankstatementsAplicationDateString);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 6:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].bankstatementsAbonoString);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 7:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].bankstatementsCargoString);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 8:
                                    document.SetCellValue(rowIndex, columnIndex, string.Empty);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 9:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].bankstatementsamountfinalstring);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                case 10:
                                    document.SetCellValue(rowIndex, columnIndex, itemlist[arrayValuesCounter].statusconciliationname);
                                    document.SetColumnWidth(columnIndex, 15.00);
                                    document.SetCellStyle(rowIndex, columnIndex, GeneradorXLSResources.Right_Cell_Data_Style());
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    document.SetCellStyle(rowIndex, columnIndex, _BorderInorLast);
                                    break;
                                default:
                                    document.SetCellValue(rowIndex, columnIndex, "");
                                    document.SetColumnWidth(columnIndex, 10.00);
                                    // document.SetCellStyle(rowIndex, columnIndex, _backGround);
                                    break;
                            }
                        }
                        arrayValuesCounter++;
                    }
                }

                if (File.Exists(rutaArchivoCompleta))
                {
                    File.Delete(rutaArchivoCompleta);
                    document.SaveAs(rutaArchivoCompleta);
                }
                else
                {
                    document.SaveAs(rutaArchivoCompleta);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio una Excepción en GeneradorXLS-GeneradorXLSbankReconciliation al generar archivo: " + ex.Message);
                throw new Exception("No se puede generar el archivo de conciliación.", ex.InnerException);
            }
        }

        private SLStyle ResolveStatusConciliationBackGroundColor(int Status)
        {

            switch (Status)
            {
                case (int)BankAccountReconciliationStatus.Sin_conciliar:
                    {
                        return GeneradorXLSResources.BackGround_Cell_Data_Black_Style();
                    }
                case (int)BankAccountReconciliationStatus.Completo:
                    {
                        return GeneradorXLSResources.BackGround_Cell_Data_Green_Style();
                    }
                case (int)BankAccountReconciliationStatus.Parcial:
                    {
                        return GeneradorXLSResources.BackGround_Cell_Data_Yellow_Style();
                    }
                case (int)BankAccountReconciliationStatus.Error:
                    {
                        return GeneradorXLSResources.BackGround_Cell_Data_Red_Style();
                    }
                default:
                    {
                        return GeneradorXLSResources.BackGround_Cell_Data_Black_Style();
                    }
            }
        }
    }
}