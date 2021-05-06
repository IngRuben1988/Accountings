using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.comments.model
{
    public class Comments
    {
        public int      row           { get; set; }
        public int      IdComment     { get; set; }
        public int      Invoice       { get; set; }
        public long     Income        { get; set; }
        public int      IdUser        { get; set; }
        public string   UserComment   { get; set; }
        public string   Description   { get; set; }
        public DateTime CreactionDate { get; set; }
        public string   CreactionDateString { get; set; }
    }
}