using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VTAworldpass.VTAServices.Services.accounts.model
{
    public class SessionModel
    {
        private SessionModel sessionmodel;

        [Key]
        public int     idUser             { get; set; }
        public int     idProfile          { get; set; }
        public string  userLoginName      { get; set; }
        public string  userPassword       { get; set; }
        public string  userPersonName     { get; set; }
        public string  userPersonLastName { get; set; }
        public string  FullName           { get; set; }
        public string  userEmail          { get; set; }
        public bool    userActive         { get; set; }
        public Nullable<int>     idProfileAccount   { get; set; }
        public string  passwordHash       { get; set; }
        public virtual ICollection<string> permissions { get; set; }
        public virtual ICollection<int>    companies   { get; set; }
        public virtual ICollection<int>    profaccountsl3  { get; set; }

        public SessionModel()
        { }

        public SessionModel(SessionModel sessionModel)
        {
            this.sessionmodel = sessionModel;
        }
    }
}