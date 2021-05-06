using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTAworldpass.VTACore.Helpers;

namespace VTAworldpass.VTAServices.Services.bankreconciliation.helpers
{
    public class XLSBookObj
    {
        public int book_sheetcount { get; set; }
        public string book_filename { get; set; }
        public List<XLSSheetObj> book_sheets { get; set; }

        public XLSBookObj() { }
    }
    public class XLSSheetObj
    {
        public int sheet_idx { get; set; }
        public int sheet_rowcount { get; set; }
        public string sheet_id { get; set; }
        public string sheet_name { get; set; }
        public List<XLSRowObj> sheet_rows { get; set; }

        public XLSSheetObj() { }
    }

    public class XLSRowObj
    {
        public int row_idx { get; set; }
        public int row_cellcount { get; set; }
        public int row_groupby { get; set; }
        public List<XLSCellObj> row_cells { get; set; }

        public XLSRowObj() { }
    }

    public class XLSCellObj
    {
        public int cell_idx { get; set; }
        public int cell_row { get; set; }
        public string cell_col { get; set; }
        public string cell_name { get; set; }
        public string cell_val { get; set; }
        public bool cell_strval { get; set; }

        public XLSCellObj() { }
    }

    public class XLSImportingOutput
    {
        public int error_count { get; set; }
        public string oper_uuid { get; set; }
        public bool has_errors { get; set; }
        public bool was_halted { get; set; }
        public List<JSErrorRecord> error_report { get; set; }

        public XLSImportingOutput() { }
    }
}