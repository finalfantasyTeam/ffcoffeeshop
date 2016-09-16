using ff.coffee.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ff.coffee.webapp.Models
{
    public class InvoiceDetailViewModels : BaseViewModels
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string UserOrder { get; set; }
        public string UserKitchenConfirm { get; set; }
        public string UserKitchenDone { get; set; }
        public DateTime TimeConfirm { get; set; }
        public DateTime TimeDone { get; set; }

        public InvoiceDetail dtoInvoiceDetail { get; set; }
        public List<InvoiceDetail> ListInvoiceDetail { get; set; }

        private InvoiceDetailRepository idetailRepo;

        public InvoiceDetailViewModels() 
        { }

        public override void GetDataToList()
        {
            uow = new UnitOfWork();
            idetailRepo = new InvoiceDetailRepository(uow);
            ListInvoiceDetail = new List<InvoiceDetail>();

            IEnumerable<InvoiceDetail> lstInvoiceDetail = idetailRepo.GetAll();

            foreach (InvoiceDetail indetail in lstInvoiceDetail)
            {
                ListInvoiceDetail.Add(indetail);
            }
        }

        public override void GetDataToModel(int Id)
        {
            uow = new UnitOfWork();
            idetailRepo = new InvoiceDetailRepository(uow);
            this.dtoInvoiceDetail = idetailRepo.SingleOrDefault(Id);
            MapDtoToModel();
        }

        public override int InsertModel()
        {
            uow = new UnitOfWork();
            idetailRepo = new InvoiceDetailRepository(uow);
            MapModelToDto();
            return idetailRepo.Insert(this.dtoInvoiceDetail);
        }

        public override void UpdateModel()
        {
            uow = new UnitOfWork();
            idetailRepo = new InvoiceDetailRepository(uow);
            MapModelToDto();
            idetailRepo.Update(this.dtoInvoiceDetail);
        }

        public override int DeleteModel(int Id)
        {
            uow = new UnitOfWork();
            idetailRepo = new InvoiceDetailRepository(uow);
            this.dtoInvoiceDetail = idetailRepo.SingleOrDefault(Id);
            return idetailRepo.Delete(this.dtoInvoiceDetail);
        }

        protected override void MapDtoToModel()
        {
            this.Id = this.dtoInvoiceDetail.ID;
            this.InvoiceId = this.dtoInvoiceDetail.Invoice;
            this.ProductId = this.dtoInvoiceDetail.ProductID;
            this.Quantity = this.dtoInvoiceDetail.Quantity;
            this.Price = this.dtoInvoiceDetail.Price;
            this.Cost = this.dtoInvoiceDetail.Cost;
            this.UserKitchenConfirm = this.dtoInvoiceDetail.UserKitchenConfirm;
            this.UserKitchenDone = this.dtoInvoiceDetail.UserKitchenDone;
            this.UserOrder = this.dtoInvoiceDetail.UserOrder;
            this.TimeDone = this.dtoInvoiceDetail.TimeDone.Value;
            this.TimeConfirm = this.dtoInvoiceDetail.TimeConfirm.Value;
        }

        protected override void MapModelToDto()
        {
            this.dtoInvoiceDetail = new InvoiceDetail();
            this.dtoInvoiceDetail.ID = this.Id;
            this.dtoInvoiceDetail.Invoice = this.InvoiceId;
            this.dtoInvoiceDetail.ProductID = this.ProductId;
            this.dtoInvoiceDetail.Quantity = this.Quantity;
            this.dtoInvoiceDetail.Price = this.Price;
            this.dtoInvoiceDetail.Cost = this.Cost;
            this.dtoInvoiceDetail.UserKitchenConfirm = this.UserKitchenConfirm;
            this.dtoInvoiceDetail.UserKitchenDone = this.UserKitchenDone;
            this.dtoInvoiceDetail.UserOrder = this.UserOrder;
            this.dtoInvoiceDetail.TimeDone = this.TimeDone;
            this.dtoInvoiceDetail.TimeConfirm = this.TimeConfirm;
        }
    
    }
}