using ff.coffee.repository;
using ff.coffee.webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ff.coffee.webapp.Controllers
{
    public class SettingController : BaseController
    {
        private RestaurantViewModels resVM;
        private SalePointViewModels saleVM;
        private AreasViewModels areaVM;
        private CoffeeTableViewModels coffeeVM;

        #region Restaurant
        [HttpGet]
        public ActionResult AddRestaurant()
        {
            resVM = new RestaurantViewModels();
            return View(resVM);
        }

        [HttpPost]
        public ActionResult AddRestaurant(RestaurantViewModels model)
        {
            if (ModelState.IsValid)
            {
                int result = model.InsertModel();
                
                if (result > 0)
                {
                   return RedirectToAction("EditRestaurant", new { Id = result });
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult EditRestaurant(int Id)
        {
            resVM = new RestaurantViewModels();
            resVM.GetDataToModel(Id);

            return View(resVM);
        }

        [HttpPost]
        public ActionResult EditRestaurant(RestaurantViewModels model)
        {
            if (ModelState.IsValid)
            {
                model.UpdateModel();
            }

            return RedirectToAction("EditRestaurant", new { Id = model.Id });
        }

        [HttpGet]
        public ActionResult DeleteRestaurant(int Id)
        {
            if (Id > 0)
            {
                resVM = new RestaurantViewModels();
                int result = resVM.DeleteModel(Id);

                if (result > 0)
                {
                    return RedirectToAction("Restaurant", "Home", new { Id = 0 });
                }
            }
            return RedirectToAction("EditRestaurant", new { Id = Id });
        }
        #endregion

        #region SalePoint
        [HttpGet]
        public ActionResult AddSalePoint()
        {
            saleVM = new SalePointViewModels();
            resVM = new RestaurantViewModels();

            resVM.GetDataToList();
            saleVM.ListRestaurant = resVM.ListRestaurant;

            return View(saleVM);
        }

        [HttpPost]
        public ActionResult AddSalePoint(SalePointViewModels model)
        {
            model.StartDate = DateTime.Now;
            model.IsWorking = true;

            if (ModelState.IsValid)
            {
                int result = model.InsertModel();

                if (result > 0)
                {
                    return RedirectToAction("EditSalePoint", new { Id = result });
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult EditSalePoint(int Id)
        {
            saleVM = new SalePointViewModels();
            resVM = new RestaurantViewModels();

            saleVM.GetDataToModel(Id);
            resVM.GetDataToList();
            saleVM.ListRestaurant = resVM.ListRestaurant;

            return View(saleVM);
        }

        [HttpPost]
        public ActionResult EditSalePoint(SalePointViewModels model)
        {
            if (ModelState.IsValid)
            {
                model.UpdateModel();
            }

            return RedirectToAction("EditSalePoint", new { Id = model.Id });
        }

        [HttpGet]
        public ActionResult DeleteSalePoint(int Id)
        {
            if (Id > 0)
            {
                saleVM = new SalePointViewModels();
                int result = saleVM.DeleteModel(Id);

                if (result > 0)
                {
                    return RedirectToAction("SalePoint", "Home", new { Id = 0 });
                }
            }
            return RedirectToAction("EditSalePoint", "Setting", new { Id = Id });
        }
        #endregion

        #region Area
        [HttpGet]
        public ActionResult AddArea()
        {
            areaVM = new AreasViewModels();
            saleVM = new SalePointViewModels();

            saleVM.GetDataToList();
            areaVM.ListSalePoint = saleVM.ListSalePoint;

            return View(areaVM);
        }

        [HttpPost]
        public ActionResult AddArea(AreasViewModels model)
        {
            if (ModelState.IsValid)
            {
                int result = model.InsertModel();

                if (result > 0)
                {
                    return RedirectToAction("EditArea", new { Id = result });
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult EditArea(int Id)
        {
            areaVM = new AreasViewModels();
            saleVM = new SalePointViewModels();

            areaVM.GetDataToModel(Id);

            saleVM.GetDataToList();
            areaVM.ListSalePoint = saleVM.ListSalePoint;

            return View(areaVM);
        }

        [HttpPost]
        public ActionResult EditArea(AreasViewModels model)
        {
            if (ModelState.IsValid)
            {
                model.UpdateModel();
            }

            return RedirectToAction("EditArea", new { Id = model.Id });
        }

        [HttpGet]
        public ActionResult DeleteArea(int Id)
        {
            if (Id > 0)
            {
                areaVM = new AreasViewModels();
                int result = areaVM.DeleteModel(Id);

                if (result > 0)
                {
                    return RedirectToAction("Area", "Home", new { Id = 0 });
                }
            }
            return RedirectToAction("EditArea", new { Id = Id });
        }
        #endregion

        #region CoffeeTable
        [HttpGet]
        public ActionResult AddCoffeeTable()
        {
            coffeeVM = new CoffeeTableViewModels();
            areaVM = new AreasViewModels();

            areaVM.GetDataToList();
            coffeeVM.ListAreas = areaVM.ListAreas;

            return View(coffeeVM);
        }

        [HttpPost]
        public ActionResult AddCoffeeTable(CoffeeTableViewModels model)
        {
            model.IsServe = false;

            if (ModelState.IsValid)
            {
                int result = model.InsertModel();

                if (result > 0)
                {
                    return RedirectToAction("EditCoffeeTable", new { Id = result });
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult EditCoffeeTable(int Id)
        {
            coffeeVM = new CoffeeTableViewModels();
            areaVM = new AreasViewModels();

            coffeeVM.GetDataToModel(Id);

            areaVM.GetDataToList();
            coffeeVM.ListAreas = areaVM.ListAreas;

            return View(coffeeVM);
        }

        [HttpPost]
        public ActionResult EditCoffeeTable(CoffeeTableViewModels model)
        {
            if (ModelState.IsValid)
            {
                model.UpdateModel();
            }

            return RedirectToAction("EditCoffeeTable", new { Id = model.Id });
        }

        [HttpGet]
        public ActionResult DeleteCoffeeTable(int Id)
        {
            if (Id > 0)
            {
                coffeeVM = new CoffeeTableViewModels();
                int result = coffeeVM.DeleteModel(Id);

                if (result > 0)
                {
                    return RedirectToAction("CoffeeTable", "Home", new { Id = 0 });
                }
            }
            return RedirectToAction("EditCoffeeTable", new { Id = Id });
        }

        [HttpGet]
        public ActionResult SoftDeleteCoffeeTable(int Id)
        {
            if (Id > 0)
            {
                coffeeVM = new CoffeeTableViewModels();
                coffeeVM.GetDataToModel(Id);
                coffeeVM.Enable = false;
                coffeeVM.UpdateModel();

                return RedirectToAction("CoffeeTable", "Home", new { Id = 0 });
            }
            return RedirectToAction("EditCoffeeTable", new { Id = Id });
        }
        #endregion
    }
}
