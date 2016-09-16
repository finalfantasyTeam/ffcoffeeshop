using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ff.coffee.webapp
{
    public abstract class BaseViewPage : WebViewPage
    {
        public virtual new CoffeePrincipal User
        {
            get 
            {
                return base.User as CoffeePrincipal;
            }
        }
    }

    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual new CoffeePrincipal User
        {
            get { return base.User as CoffeePrincipal; }
        }
    }
}