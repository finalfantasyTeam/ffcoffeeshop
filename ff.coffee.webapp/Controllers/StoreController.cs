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
    [CoffeeAuthorize("Admin")]
    public class StoreController : BaseController
    {
        private GoodsViewModels goodsVM;
        private ProductViewModels productVM;
        private ProductCatViewModels catVM;
        private CoffeeTableViewModels tableVM;

        #region Coffee Table

        public ActionResult CoffeeTables(int Id = 0)
        {
            tableVM = new CoffeeTableViewModels();
            tableVM.GetDataToList();

            return View(tableVM);
        }

        #endregion

        #region Goods
        public ActionResult Goods(int Id = 0)
        {
            goodsVM = new GoodsViewModels();
            goodsVM.GetDataToList();
            ViewBag.Title = StorePageResource.GoodsListHeader;

            return View(goodsVM);
        }

        public ActionResult AddGoods()
        {
            goodsVM = new GoodsViewModels();
            productVM = new ProductViewModels();

            productVM.GetDataToList();
            goodsVM.ListProduct = productVM.ListProduct;

            return View(goodsVM);
        }

        [HttpPost]
        public ActionResult AddGoods(GoodsViewModels model)
        {
            model.DateImport = DateTime.Now;
            model.Enable = true;

            if (ModelState.IsValid)
            {
                int result = model.InsertModel();

                if (result > 0)
                {
                    return RedirectToAction("Goods", new { Id = result });
                }
            }

            return View(model);
        }

        public ActionResult EditGoods(int Id)
        {
            goodsVM = new GoodsViewModels();
            goodsVM.GetDataToModel(Id);

            return View(goodsVM);
        }

        [HttpPost]
        public ActionResult EditGoods(GoodsViewModels model)
        {
            if (ModelState.IsValid)
            {
                model.UpdateModel();
            }

            return RedirectToAction("EditGoods", new { Id = model.Id });
        }

        public ActionResult DeleteGoods(int Id)
        {
            if (Id > 0)
            {
                goodsVM = new GoodsViewModels();
                int result = goodsVM.DeleteModel(Id);

                if (result > 0)
                {
                    return RedirectToAction("Goods", new { Id = 0 });
                }
            }
            return RedirectToAction("EditGoods", new { Id = Id });
        }
        #endregion

        #region Product
        public ActionResult Product()
        {
            productVM = new ProductViewModels();
            productVM.GetDataToList();

            ViewBag.Title = StorePageResource.ProductListHeader;

            return View(productVM);
        }

        public ActionResult AddProduct()
        {
            productVM = new ProductViewModels();
            catVM = new ProductCatViewModels();

            catVM.GetDataToList();
            productVM.ListProductCat = catVM.ListProductCat;

            return View(productVM);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModels model)
        {
            model.DateCreate = DateTime.Now;
            model.Enable = true;

            if (ModelState.IsValid)
            {
               int result = model.InsertModel();

               if (result > 0)
               {
                   return RedirectToAction("Product", new { Id = result });
               }
            }

            return View(model);
        }

        public ActionResult EditProduct(int Id)
        {
            productVM = new ProductViewModels();
            catVM = new ProductCatViewModels();

            productVM.GetDataToModel(Id);
            catVM.GetDataToList();
            productVM.ListProductCat = catVM.ListProductCat;

            return View(productVM);
        }

        [HttpPost]
        public ActionResult EditProduct(ProductViewModels model)
        {
            if (ModelState.IsValid)
            {
                model.UpdateModel();  
            }

            return RedirectToAction("EditProduct", new { Id = model.Id });
        }

        public ActionResult DeleteProduct(int Id)
        {
            if (Id > 0)
            {
                productVM = new ProductViewModels();
                int result = productVM.DeleteModel(Id);

                if (result > 0)
                {
                    return RedirectToAction("Product", new { Id = 0 });
                }
            }
            return RedirectToAction("EditProduct", new { Id = Id });
        }

        [HttpGet]
        public ActionResult SoftDeleteProduct(int Id)
        {
            if (Id > 0)
            {
                productVM = new ProductViewModels();
                productVM.GetDataToModel(Id);
                productVM.Enable = false;
                productVM.UpdateModel();

                return RedirectToAction("Product", new { Id = 0 });
            }
            return RedirectToAction("EditProduct", new { Id = Id });
        }
        #endregion

        #region Product Category
        public ActionResult ProductCat()
        {
            catVM = new ProductCatViewModels();
            catVM.GetDataToList();

            ViewBag.Title = StorePageResource.ProductCatListHeader;

            return View(catVM);
        }

        public ActionResult AddProductCat()
        {
            catVM = new ProductCatViewModels();

            return View(catVM);
        }

        [HttpPost]
        public ActionResult AddProductCat(ProductCatViewModels model)
        {
            model.Enable = true;

            if (ModelState.IsValid)
            {
                int result = model.InsertModel();

                if (result > 0)
                {
                    return RedirectToAction("ProductCat", new { Id = result });
                }
            }

            return View(model);
        }

        public ActionResult EditProductCat(int Id)
        {
            catVM = new ProductCatViewModels();
            catVM.GetDataToModel(Id);

            return View(catVM);
        }

        [HttpPost]
        public ActionResult EditProductCat(ProductCatViewModels model)
        {
            if (ModelState.IsValid)
            {
                model.UpdateModel();
            }

            return RedirectToAction("ProductCat", new { Id = model.Id });
        }

        [HttpGet]
        public ActionResult DeleteProductCat(int Id)
        {
            if (Id > 0)
            {
                catVM = new ProductCatViewModels();
                int result = catVM.DeleteModel(Id);

                if (result > 0)
                {
                    return RedirectToAction("ProductCat", new { Id = 0 });
                }
            }
            return RedirectToAction("EditProductCat", new { Id = Id });
        }

        [HttpGet]
        public ActionResult SoftDeleteProductCat(int Id)
        {
            if (Id > 0)
            {
                catVM = new ProductCatViewModels();
                catVM.GetDataToModel(Id);
                catVM.Enable = false;
                catVM.UpdateModel();

                return RedirectToAction("ProductCat", new { Id = 0 });
            }
            return RedirectToAction("EditProductCat", new { Id = Id });
        }
        #endregion
    }
}
