using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ff.coffee.webapp.Models
{
    public class CoffeePrincipalSerializeModel
    {
        public int UserId { get; set; }
        public string StaffUserName { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public int StaffRole { get; set; }
        public string Notes { get; set; }
    }
}