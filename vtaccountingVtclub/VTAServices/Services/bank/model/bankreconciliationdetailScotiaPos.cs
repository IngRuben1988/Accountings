using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTAworldpass.VTAServices.Services.bank.model
{
    public class bankreconciliationdetailScotiaPos
    {
        public long     idscotiastatement               { get; set; }
        public long     scotiastatementtpv              { get; set; }
        public string   scotiastatementtpvname          { get; set; }
        public int      scotiastatementcurrency         { get; set; }
        public string   scotiastatementcurrencyname     { get; set; }
        public string   scotiastatementidterminal       { get; set; }
        public string   scotiastatementlot              { get; set; }
        public DateTime scotiastatementprocessingdate   { get; set; }
        public string   scotiastatementprocessingdatestring { get; set; }
        public string   scotiastatementcardtype         { get; set; }
        public string   scotiastatementcardnumber       { get; set; }
        public decimal  scotiastatementammount          { get; set; }
        public string   scotiastatementammountstring    { get; set; }
        public string   scotiastatementtranstype        { get; set; }
        public DateTime scotiastatementtransdate        { get; set; }
        public string   scotiastatementtransdatestring  { get; set; }
        public string   scotiastatementstatus           { get; set; }
        public string   scotiastatementclasification    { get; set; }
        public string   scotiastatementauthorizationcode { get; set; }
        public int      scotiastatementcurrencysent     { get; set; }
        public string   scotiastatementcurrencysentname { get; set; }
        public decimal  scotiastatemensammountsent      { get; set; }
        public string   scotiastatemensammountsentstring  { get; set; }
        public int      scotiastatementstatusconciliation { get; set; }
        public string   scotiastatementstatusconciliationname { get; set; }
        public long     scotiastatementsourcedata       { get; set; }
        public string   scotiastatementsourcedataname   { get; set; }
        public long     scotiastatementsiditem          { get; set; }

        public bool xlscorrectlyformed { get; set; }
        public int statusconciliation  { get; set; }
        public int methodconciliation  { get; set; }
    }
}