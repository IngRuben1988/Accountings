using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.attachments.model
{
    public class attachment
    {
            public int    row           { get; set; }
            public int    item          { get; set; }
            public int    parent        { get; set; }
            public int    clasification { get; set; }
            public int    typefile      { get; set; }
            public string typefilename  { get; set; }
            public string filename      { get; set; }
            public string filenamesys   { get; set; }
            public string directory     { get; set; }
            public string directoryfull { get; set; }
            public string contentType   { get; set; }
            public string url           { get; set; }
            public string datechange    { get; set; }
    }
}