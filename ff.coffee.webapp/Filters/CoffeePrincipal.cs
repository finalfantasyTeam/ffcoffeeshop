using ff.coffee.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ff.coffee.webapp
{
    public class CoffeePrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public string FullName { get; set; }
        public string StaffUserName { get; set; }
        public int UserRole { get; set; }

        public CoffeePrincipal(string userName)
        {
            this.Identity = new GenericIdentity(userName);
        }

        public bool IsInRole(string roles)
        {
            if (Identity == null || !Identity.IsAuthenticated)
            {
                return false;
            }

            string[] lstRole = roles.Split(',');
            bool result = false;

            foreach (string role in lstRole)
            {
                switch (role)
                {
                    case "Admin":
                        result = this.UserRole == (int)StaffRoles.Admin;
                        break;
                    case "ViceManager":
                        result = this.UserRole == (int)StaffRoles.ViceManager;
                        break;
                    case "Manager":
                        result = this.UserRole == (int)StaffRoles.Manager;
                        break;
                    case "GroupLeader":
                        result = this.UserRole == (int)StaffRoles.GroupLeader;
                        break;
                    case "Cashier":
                        result = this.UserRole == (int)StaffRoles.Cashier;
                        break;
                    case "Order":
                        result = this.UserRole == (int)StaffRoles.Order;
                        break;
                    case "Chef":
                        result = this.UserRole == (int)StaffRoles.Chef;
                        break;
                }

                if (result == true)
                {
                    return result;
                }
            }

            return result;
        }
    }
}