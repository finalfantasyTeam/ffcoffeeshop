using ff.coffee.repository;
using ff.coffee.webapp.Languages.vi;
using ff.coffee.webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ff.coffee.webapp.Controllers
{
    public class HomeController : BaseController
    {
        private RestaurantViewModels resVM;
        private SalePointViewModels saleVM;
        private AreasViewModels areaVM;
        private CoffeeTableViewModels coffeeVM;

        [CoffeeAuthorize]
        public ActionResult Index()
        {
            switch (User.UserRole)
            {
                case (int)StaffRoles.Admin:
                    return RedirectToAction("Restaurant", new { Id = 0 });
                case (int)StaffRoles.ViceManager:
                    return RedirectToAction("SalePoint", new { Id = 0 });
                case (int)StaffRoles.Manager:
                    return RedirectToAction("Areas", new { Id = 0 });
            }

            return RedirectToAction("CoffeeTable", new { Id = 0 });
        }

        [CoffeeAuthorize("Admin")]
        public ActionResult StoreLanding()
        {
            return View();
        }

        [CoffeeAuthorize("Admin,ViceManager,Manager,Chef")]
        public ActionResult Kitchen()
        {
            return View();
        }

        [CoffeeAuthorize("Admin")]
        public ActionResult Restaurant(int Id)
        {
            resVM = new RestaurantViewModels();
            resVM.GetDataToList();
            ViewBag.Title = HomePageResource.RestListHeader;

            return View(resVM);
        }

        [CoffeeAuthorize("Admin,ViceManager,Manager")]
        public ActionResult Areas(int Id)
        {
            areaVM = new AreasViewModels();
            areaVM.GetDataToList();
            ViewBag.Title = HomePageResource.AreaListHeader;

            return View(areaVM);
        }

        [CoffeeAuthorize("Admin,ViceManager")]
        public ActionResult SalePoint(int Id)
        {
            saleVM = new SalePointViewModels();
            saleVM.GetDataToList();
            ViewBag.Title = HomePageResource.SalePointListHeader;

            return View(saleVM);
        }

        [CoffeeAuthorize]
        public ActionResult CoffeeTable(int Id)
        {
            coffeeVM = new CoffeeTableViewModels();
            coffeeVM.AreaId = Id;
            coffeeVM.GetDataToList();
            ViewBag.Title = HomePageResource.CoffeeTblListHeader;

            return View(coffeeVM);
        }
    }
}
