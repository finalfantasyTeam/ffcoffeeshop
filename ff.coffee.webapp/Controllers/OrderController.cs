using ff.coffee.webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ff.coffee.webapp.Controllers
{
    public class OrderController : BaseController
    {
        private OrderViewModels orderVM;
        private CoffeeTableViewModels tableVM;
        private ProductCatViewModels catVM;
        private ProductViewModels productVM;
        private InvoiceViewModels invoiceVM;
        private InvoiceDetailViewModels invDetailVM;

        public ActionResult Index(int tableId, string tableName)
        {
            orderVM = new OrderViewModels();
            orderVM.TableID = tableId;
            orderVM.TableName = tableName;

            orderVM.GetDataToList();
            orderVM.ListOrder = orderVM.ListOrder.
                                            Where(o => o.TableID == tableId 
                                                        && o.IsNew == true 
                                                        && o.Paid == false).ToList();
            if (orderVM.ListOrder.Count > 0)
            {
                orderVM.TimeIn = orderVM.ListOrder.FirstOrDefault().TimeIn.Value;
            }

            tableVM = new CoffeeTableViewModels();
            tableVM.GetDataToModel(tableId);

            orderVM.TableNotes = tableVM.OrderNotes;

            catVM = new ProductCatViewModels();
            catVM.GetDataToList();
            orderVM.ListProductCategory = catVM.ListProductCat;

            return View(orderVM);
        }

        public ActionResult SearchProduct(string keyword)
        {
            productVM = new ProductViewModels();
            productVM.GetDataToList();
            keyword = keyword.ToLower();
            productVM.ListProduct = productVM.ListProduct.Where(p => p.Name.ToLower().Contains(keyword)).ToList();

            return View("_SearchProductResultPartial", productVM);
        }

        public ActionResult SearchProductCat(int category)
        {
            productVM = new ProductViewModels();
            productVM.GetDataToList();
            productVM.ListProduct = productVM.ListProduct.Where(p => p.Category == category).ToList();

            return View("_SearchProductResultPartial", productVM);
        }

        [HttpPost]
        public ActionResult AddOrder(OrderViewModels model)
        {
            string[] strProductOrders = Request.Form["productId[]"].Split(',');
            string[] strQuantityOrders = Request.Form["productQuatity[]"].Split(',');
            string[] strNoteOrders = Request.Form["orderNotes[]"].Split(',');

            int[] productOrders = new int[strProductOrders.Length];
            int[] quantityOrders = new int[strQuantityOrders.Length];

            for (int i = 0; i < strProductOrders.Length; i++)
            {
                productOrders[i] = int.Parse(strProductOrders[i]);
                quantityOrders[i] = int.Parse(strQuantityOrders[i]);
            }

            model.ProductID = 1;
            model.StaffOrder = User.StaffUserName;
            model.StaffConfirm = User.StaffUserName;
            model.StaffDone = User.StaffUserName;

            if (ModelState.IsValid)
            {
                int result = model.InsertListOrder(productOrders, quantityOrders, strNoteOrders);
                if (result > 0)
                {
                    // Set current table to servicing status
                    tableVM = new CoffeeTableViewModels();
                    tableVM.GetDataToModel(model.TableID);
                    tableVM.IsServe = true;
                    tableVM.UpdateModel();

                    return RedirectToAction("Index", new { tableId = model.TableID, 
                                                           tableName = model.TableName });
                }
            }

            return View();
        }

        [HttpGet]
        public object UpdateOrderNotes(int tableId, int productId, string orderNote, int orderQuantity)
        {
            orderVM = new OrderViewModels();
            int result = orderVM.UpdateOrderNotes(tableId, productId, orderNote, orderQuantity);

            return new { data = result };
        }

        public ActionResult Payment(DateTime timeIn, int tableId, string tableName)
        {
            orderVM = new OrderViewModels();
            orderVM.TableID = tableId;
            orderVM.TableName = tableName;
            orderVM.TimeIn = timeIn;
            orderVM.GetDataToList();
            DateTime lastTimeIn = orderVM.ListOrder.LastOrDefault().TimeIn.Value;
            orderVM.ListOrder = orderVM.ListOrder.Where(o => o.TableID == tableId
                                                        && o.IsNew == true 
                                                        && o.Paid == false).ToList();

            return View(orderVM);
        }

        [HttpPost]
        public ActionResult Paid(OrderViewModels model)
        {
            string[] lstOrder = Request.Form.Get("txtOrderID[]").Split(',');

            foreach (string strOrder in lstOrder)
            {
                int orderId = int.Parse(strOrder);
                model.GetDataToModel(orderId);
                model.IsNew = false;
                model.Paid = true;
                model.UpdateModel();
            }

            
            model.GetDataToList();
            model.ListOrder = model.ListOrder.Where(o => o.TableID == model.TableID
                                                        && o.TimeIn.Value.ToString(this.DATE_TIME_FORMAT).Equals(model.TimeIn.ToString(this.DATE_TIME_FORMAT)))
                                                        .ToList();  

            decimal orderTotal = decimal.Parse(Request.Form.Get("txtTotal"));

            if (model.ListOrder.Count > 0)
            {

                invoiceVM = new InvoiceViewModels();

                invoiceVM.TableId = model.TableID;
                invoiceVM.Total = orderTotal;
                invoiceVM.Discount = model.Discount;
                invoiceVM.Enable = true;
                invoiceVM.DateCreate = DateTime.Now;
                invoiceVM.Cashier = User.StaffUserName;
                invoiceVM.CustomerName = model.TableName;

                int resultInvoice = invoiceVM.InsertModel();

                if (resultInvoice > 0)
                {
                    invDetailVM = new InvoiceDetailViewModels();
                    int resultDetail;

                    foreach (var order in model.ListOrder)
                    {
                        invDetailVM.InvoiceId = resultInvoice;
                        invDetailVM.ProductId = order.ProductID.Value;
                        invDetailVM.Price = order.Product.Price.Value;
                        invDetailVM.Quantity = order.Quantity.Value;
                        invDetailVM.Cost = order.Product.Price.Value * order.Quantity.Value;
                        invDetailVM.UserOrder = order.StaffOrder;
                        invDetailVM.UserKitchenDone = order.StaffDone;
                        invDetailVM.UserKitchenConfirm = order.StaffConfirm;
                        invDetailVM.TimeConfirm = order.ConfirmTime.Value;
                        invDetailVM.TimeDone = order.DoneTime.Value;

                        resultDetail = invDetailVM.InsertModel();
                    }

                    tableVM = new CoffeeTableViewModels();
                    tableVM.GetDataToModel(model.TableID);
                    tableVM.IsServe = false;
                    tableVM.OrderNotes = "";
                    tableVM.UpdateModel();
                }
            }

            return RedirectToAction("Payment", new
            {
                timeIn = model.TimeIn,
                tableId = model.TableID,
                tableName = model.TableName
            });
        }

        public ActionResult ChangeTable()
        {
            return View();
        }
    }
}
