using ff.coffee.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ff.coffee.webapp.Models
{
    public class OrderViewModels : BaseViewModels
    {
        public int Id { get; set; }
        public DateTime TimeIn { get; set; }
        public int TableID { get; set; }
        public string TableName { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public bool ChefConfirm { get; set; }
        public bool ChefDone { get; set; }
        public bool IsNew { get; set; }
        public bool IsEdit { get; set; }
        public string StaffOrder { get; set; }
        public string StaffConfirm { get; set; }
        public string StaffDone { get; set; }
        public DateTime ConfirmTime { get; set; }
        public DateTime DoneTime { get; set; }
        public bool Paid { get; set; }
        public string Notes { get; set; }

        public string TableNotes { get; set; }
        public List<Ordering> ListOrder { get; set; }
        public List<ProductCat> ListProductCategory { get; set; }
        public Ordering dtoOrder { get; set; }

        public float Discount { get; set; }

        private OrderingRepository orderRepo;

        public OrderViewModels() 
        { }

        public override void GetDataToList()
        {
            uow = new UnitOfWork();
            orderRepo = new OrderingRepository(uow);
            ListOrder = new List<Ordering>();

            IEnumerable<Ordering> lstOrder = orderRepo.GetAll();

            foreach (Ordering order in lstOrder)
            {
                ListOrder.Add(order);
            }
        }

        public int UpdateOrderNotes(int tableId, int productId, string orderNotes, int orderQuantity)
        {
            uow = new UnitOfWork();
            orderRepo = new OrderingRepository(uow);

            Ordering orderUpdate = orderRepo.GetAll().
                Where(o => o.ProductID == productId
                        && o.TableID == tableId).Single();

            if (orderUpdate != null)
            {
                if (orderQuantity != 0)
                {
                    orderUpdate.Notes = orderNotes;
                    orderUpdate.Quantity = orderQuantity;
                    orderRepo.Update(orderUpdate);
                    return 1;
                }
                else
                {
                    return orderRepo.Delete(orderUpdate);
                }
            }
            
            return 0;
        }

        public int InsertListOrder(int[] orderProducts, int[] orderQuatity, string[] orderNotes)
        {
            int result = 0;

            this.ChefConfirm = true;
            this.ChefDone = true;
            this.IsNew = true;
            this.IsEdit = false;
            this.Paid = false;
            this.ConfirmTime = DateTime.Now;
            this.DoneTime = DateTime.Now;
            this.TimeIn = DateTime.Now;

            for (int i = 0; i < orderProducts.Length; i++)
            {
                if (orderQuatity[i] != 0)
                {
                    this.ProductID = orderProducts[i];
                    this.Quantity = orderQuatity[i];
                    this.Notes = orderNotes[i];

                    result = this.InsertModel();
                }
            }

            return result;
        }

        public override int InsertModel()
        {
            uow = new UnitOfWork();
            orderRepo = new OrderingRepository(uow);
            MapModelToDto();
            return orderRepo.Insert(this.dtoOrder);
        }

        public override void GetDataToModel(int Id)
        {
            uow = new UnitOfWork();
            orderRepo = new OrderingRepository(uow);
            this.dtoOrder = orderRepo.SingleOrDefault(Id);
            MapDtoToModel();
        }

        public override void UpdateModel()
        {
            uow = new UnitOfWork();
            orderRepo = new OrderingRepository(uow);
            MapModelToDto();
            orderRepo.Update(this.dtoOrder);
        }

        public override int DeleteModel(int Id)
        {
            uow = new UnitOfWork();
            orderRepo = new OrderingRepository(uow);
            this.dtoOrder = orderRepo.SingleOrDefault(Id);
            return orderRepo.Delete(this.dtoOrder);
        }

        protected override void MapDtoToModel()
        {
            this.Id = this.dtoOrder.ID;
            this.TimeIn = this.dtoOrder.TimeIn.Value;
            this.TableID = this.dtoOrder.TableID.Value;
            this.ProductID = this.dtoOrder.ProductID.Value;
            this.Quantity = this.dtoOrder.Quantity.Value;
            this.ChefConfirm = this.dtoOrder.ChefConfirm.Value;
            this.ChefDone = this.dtoOrder.ChefDone.Value;
            this.IsNew = this.dtoOrder.IsNew.Value;
            this.IsEdit = this.dtoOrder.IsEdit.Value;
            this.StaffOrder = this.dtoOrder.StaffOrder;
            this.StaffConfirm = this.dtoOrder.StaffConfirm;
            this.StaffDone = this.dtoOrder.StaffDone;
            this.ConfirmTime = this.dtoOrder.ConfirmTime.Value;
            this.DoneTime = this.dtoOrder.DoneTime.Value;
            this.Paid = this.dtoOrder.Paid.Value;
            this.Notes = this.dtoOrder.Notes;
        }

        protected override void MapModelToDto()
        {
            this.dtoOrder = new Ordering();
            this.dtoOrder.ID = this.Id;
            this.dtoOrder.TimeIn = this.TimeIn;
            this.dtoOrder.TableID = this.TableID;
            this.dtoOrder.ProductID = this.ProductID;
            this.dtoOrder.Quantity = this.Quantity;
            this.dtoOrder.ChefConfirm = this.ChefConfirm;
            this.dtoOrder.ChefDone = this.ChefDone;
            this.dtoOrder.IsNew = this.IsNew;
            this.dtoOrder.IsEdit = this.IsEdit;
            this.dtoOrder.StaffOrder = this.StaffOrder;
            this.dtoOrder.StaffConfirm = this.StaffConfirm;
            this.dtoOrder.StaffDone = this.StaffDone;
            this.dtoOrder.ConfirmTime = this.ConfirmTime;
            this.dtoOrder.DoneTime = this.DoneTime;
            this.dtoOrder.Paid = this.Paid;
            this.dtoOrder.Notes = this.Notes;
        }
    }
}