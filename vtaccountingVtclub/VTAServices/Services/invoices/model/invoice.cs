using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VTAworldpass.VTAServices.Services.invoices.model
{
    public class invoice
    {
        [Key]
        public long     id                      { get; set; }
        public int      parent                  { get; set; }
        public string   identifier              { get; set; }
        public int      currency                { get; set; }
        public string   currencyname            { get; set; }
        public int      company                 { get; set; }
        public string   companyname             { get; set; }
        public int      segment                 { get; set; }
        public string   segmentname             { get; set; }
        public int      paymentstatus           { get; set; }
        public string   paymentstatusstring     { get; set; }
        public decimal  cost                    { get; set; }
        public string   costString              { get; set; }
        public int      bankaccntType           { get; set; }
        public decimal  payment                 { get; set; }
        public string   paymentsstring          { get; set; }
        public string   description             { get; set; }
        public int      number                  { get; set; }
        public bool     istax                   { get; set; }
        public int      createdby               { get; set; }
        public DateTime creactiondate           { get; set; }
        public string   creactiondatestring     { get; set; }
        public DateTime applicationdate         { get; set; }
        public string   applicationdatestring   { get; set; }
        public string   applicationdatestring0  { get; set; }
        public int      updatedby               { get; set; }
        public DateTime updateon                { get; set; }
        public string   updateonstring          { get; set; }
        public int      statusexpenses          { get; set; }
        public bool     editable                { get; set; }
        public virtual  IEnumerable<invoiceitems> invoiceitems { get; set; }
    }
}