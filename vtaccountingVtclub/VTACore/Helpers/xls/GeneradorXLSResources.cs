using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTACore.Helpers.xls
{
    public class GeneradorXLSResources
    {
        public static SLStyle Arial10Bold()
        {
            SLStyle Arial10Bold = new SLStyle();
            Arial10Bold.SetFontBold(true);
            Arial10Bold.SetFont("Arial", 10);
            return Arial10Bold;
        }


        public static SLStyle Head_Style()
        {

            SLStyle Style = new SLStyle();

            // Border Top
            Style.Border.TopBorder.BorderStyle = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            Style.Border.TopBorder.Color = System.Drawing.Color.FromArgb(0, 44, 62, 80);
            // Border Right
            Style.Border.RightBorder.BorderStyle = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            Style.Border.RightBorder.Color = System.Drawing.Color.FromArgb(0, 44, 62, 80);
            // Border Bottom
            Style.Border.BottomBorder.BorderStyle = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            Style.Border.BottomBorder.Color = System.Drawing.Color.FromArgb(0, 44, 62, 80);
            // Border Left
            Style.Border.LeftBorder.BorderStyle = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            Style.Border.LeftBorder.Color = System.Drawing.Color.FromArgb(0, 44, 62, 80);
            // Back Ground Color
            Style.Fill.SetPattern(DocumentFormat.OpenXml.Spreadsheet.PatternValues.Solid, System.Drawing.Color.FromArgb(0, 44, 62, 80), System.Drawing.Color.FromArgb(0, 44, 62, 80));
            // Font 
            Style.SetFontBold(true);
            Style.SetFont("Arial", 10);
            Style.SetFontColor(System.Drawing.Color.White);
            // Font Aligment
            Style.Alignment.Horizontal = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;

            return Style;

        }

        /****************************************************************************************************************************************************************************************/

        public static SLStyle Top_Cell_Data_Style()
        {
            SLStyle Style = new SLStyle();
            Style.Border.TopBorder.BorderStyle = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            Style.Border.TopBorder.Color = System.Drawing.Color.FromArgb(0, 44, 62, 80);
            return Style;
        }

        public static SLStyle Right_Cell_Data_Style()
        {
            SLStyle Style = new SLStyle();
            Style.Border.RightBorder.BorderStyle = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            Style.Border.RightBorder.Color = System.Drawing.Color.FromArgb(0, 44, 62, 80);
            return Style;
        }

        public static SLStyle Bottom_Cell_Data_Style()
        {
            SLStyle Style = new SLStyle();
            Style.Border.BottomBorder.BorderStyle = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            Style.Border.BottomBorder.Color = System.Drawing.Color.FromArgb(0, 44, 62, 80);
            return Style;
        }

        public static SLStyle Left_Cell_Data_Style()
        {
            SLStyle Style = new SLStyle();
            Style.Border.LeftBorder.Color = System.Drawing.Color.FromArgb(0, 44, 62, 80);
            Style.Border.LeftBorder.BorderStyle = DocumentFormat.OpenXml.Spreadsheet.BorderStyleValues.Thin;
            return Style;
        }

        public static SLStyle Empty_Cell_Data_Style()
        {
            SLStyle Style = new SLStyle();
            return Style;
        }



        /****************************************************************************************************************************************************************************************/
        public static SLStyle BackGround_Cell_Data_Green_Style()
        {
            SLStyle Style = new SLStyle();
            Style.Fill.SetPattern(DocumentFormat.OpenXml.Spreadsheet.PatternValues.Solid, System.Drawing.Color.FromArgb(1, 52, 168, 83), System.Drawing.Color.FromArgb(1, 52, 168, 83));
            return Style;
        }

        public static SLStyle BackGround_Cell_Data_Yellow_Style()
        {
            SLStyle Style = new SLStyle();
            Style.Fill.SetPattern(DocumentFormat.OpenXml.Spreadsheet.PatternValues.Solid, System.Drawing.Color.FromArgb(1, 251, 235, 189), System.Drawing.Color.FromArgb(1, 251, 235, 189));
            return Style;
        }

        public static SLStyle BackGround_Cell_Data_Red_Style()
        {
            SLStyle Style = new SLStyle();
            Style.Fill.SetPattern(DocumentFormat.OpenXml.Spreadsheet.PatternValues.Solid, System.Drawing.Color.FromArgb(1, 234, 67, 53), System.Drawing.Color.FromArgb(1, 234, 67, 53));
            return Style;
        }

        public static SLStyle BackGround_Cell_Data_Black_Style()
        {
            SLStyle Style = new SLStyle();
            Style.Fill.SetPattern(DocumentFormat.OpenXml.Spreadsheet.PatternValues.Solid, System.Drawing.Color.FromArgb(1, 44, 62, 80), System.Drawing.Color.FromArgb(1, 44, 62, 80));
            return Style;
        }
    }
}