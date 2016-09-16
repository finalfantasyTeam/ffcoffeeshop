using ff.coffee.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ff.coffee.webapp.Models
{
    public class InvoiceViewModels : BaseViewModels
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public DateTime DateCreate { get; set; }
        public string CustomerName { get; set; }
        public string Cashier { get; set; }
        public decimal Total { get; set; }
        public double Discount { get; set; }
        public bool Enable { get; set; }

        public List<Invoice> ListInvoices { get; set; }
        public List<InvoiceDetail> ListDetails { get; set; }
        public Invoice dtoInvoice { get; set; }

        private InvoiceRepository invoiceRepo;

        public InvoiceViewModels() { }

        public override void GetDataToList()
        {
            uow = new UnitOfWork();
            invoiceRepo = new InvoiceRepository(uow);
            ListInvoices = new List<Invoice>();

            IEnumerable<Invoice> lstInvoice = invoiceRepo.GetAll();

            foreach (Invoice invoice in lstInvoice)
            {
                ListInvoices.Add(invoice);
            }
        }

        public override void GetDataToModel(int Id)
        {
            uow = new UnitOfWork();
            invoiceRepo = new InvoiceRepository(uow);
            this.dtoInvoice = invoiceRepo.SingleOrDefault(Id);
            MapDtoToModel();
        }

        public override int InsertModel()
        {
            uow = new UnitOfWork();
            invoiceRepo = new InvoiceRepository(uow);
            MapModelToDto();
            return invoiceRepo.Insert(this.dtoInvoice);
        }

        public override void UpdateModel()
        {
            uow = new UnitOfWork();
            invoiceRepo = new InvoiceRepository(uow);
            MapModelToDto();
            invoiceRepo.Update(this.dtoInvoice);
        }

        public override int DeleteModel(int Id)
        {
            uow = new UnitOfWork();
            invoiceRepo = new InvoiceRepository(uow);
            this.dtoInvoice = invoiceRepo.SingleOrDefault(Id);
            return invoiceRepo.Delete(this.dtoInvoice);
        }

        protected override void MapDtoToModel()
        {
            this.Id = this.dtoInvoice.ID;
            this.TableId = this.dtoInvoice.TableID.Value;
            this.DateCreate = this.dtoInvoice.DateCreate;
            this.CustomerName = this.dtoInvoice.Customer;
            this.Cashier = this.dtoInvoice.Cashier;
            this.Total = this.dtoInvoice.Total;
            this.Discount = this.dtoInvoice.Discount.Value;
            this.Enable = this.dtoInvoice.Enable;
        }

        protected override void MapModelToDto()
        {
            this.dtoInvoice = new Invoice();
            this.dtoInvoice.ID = this.Id;
            this.dtoInvoice.TableID = this.TableId;
            this.dtoInvoice.DateCreate = this.DateCreate;
            this.dtoInvoice.Customer = this.CustomerName;
            this.dtoInvoice.Cashier = this.Cashier;
            this.dtoInvoice.Total = this.Total;
            this.dtoInvoice.Discount = this.Discount;
            this.dtoInvoice.Enable = this.Enable;
        }
    }
}