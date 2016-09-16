using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ff.coffee.webapp
{
    public class CoffeeAuthorizeAttribute : AuthorizeAttribute
    {
        public string UsersConfigKey { get; set; }
        public string RolesConfigKey { get; set; }

        public CoffeeAuthorizeAttribute(string roles)
            : base()
        {
            Roles = roles;
        }

        public CoffeeAuthorizeAttribute()
            : base()
        {
        }

        protected virtual CoffeePrincipal CurrrentUser
        {
            get { return HttpContext.Current.User as CoffeePrincipal; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrrentUser != null)
            {
                if (filterContext.HttpContext.Request.IsAuthenticated)
                {
                    var authorizedUsers = ConfigurationManager.AppSettings[UsersConfigKey];
                    var authorizeRoles = ConfigurationManager.AppSettings[RolesConfigKey];

                    Users = String.IsNullOrEmpty(Users) ? authorizedUsers : Users;
                    Roles = String.IsNullOrEmpty(Roles) ? authorizeRoles : Roles;
                }

                if (!String.IsNullOrEmpty(Roles))
                {
                    if (!CurrrentUser.IsInRole(Roles))
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Error",
                            action = "AccessDenied"
                        }));
                    }
                }

                if (!String.IsNullOrEmpty(Users))
                {
                    if (!Users.Contains(CurrrentUser.StaffUserName))
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Error",
                            action = "AccessDenied"
                        }));
                    }
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Account",
                    action = "Login"
                }));
            }
        }
    }
}