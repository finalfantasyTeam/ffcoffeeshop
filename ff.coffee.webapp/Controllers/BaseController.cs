using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ff.coffee.webapp.Controllers
{
    [CoffeeAuthorize]
    public class BaseController : Controller
    {
        public readonly string DATE_TIME_FORMAT = "dd/MM/yyyy HH:mm";

        protected virtual new CoffeePrincipal User
        {
            get
            {
                return HttpContext.User as CoffeePrincipal;
            }
        }
    }
}
