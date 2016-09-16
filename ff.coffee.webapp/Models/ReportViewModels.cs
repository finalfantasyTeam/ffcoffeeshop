using ff.coffee.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ff.coffee.webapp.Models
{
    public class ReportViewModels
    {
        public int InvoiceId { get; set; }
        public DateTime ReportDateFrom { get; set; }
        public DateTime ReportDateTo { get; set; }
        public string TableName { get; set; }
        public List<Invoice> ListInvoice { get; set; }
        public List<InvoiceDetail> ListInvoiceDetails { get; set; }
        public List<ImportDetail> ListImportDetails { get; set; }

        private IUnitOfWork uow;
        private InvoiceRepository invoiceRepo;
        private InvoiceDetailRepository invDetailRepo;
        private ImportDetailRepository impDetailsRepo;

        public void GetSalesReports(DateTime dateFrom, DateTime dateTo)
        {
            uow = new UnitOfWork();
            invoiceRepo = new InvoiceRepository(uow);

            ListInvoice = invoiceRepo.GetAll()
                    .Where(i => i.DateCreate.CompareTo(dateFrom) >= 0 
                            && i.DateCreate.CompareTo(dateTo) < 0).ToList();
        }

        public void GetSalesReportDetail(int invoiceId)
        {
            uow = new UnitOfWork();
            invDetailRepo = new InvoiceDetailRepository(uow);

            ListInvoiceDetails = invDetailRepo.GetAll().Where(d => d.Invoice == invoiceId).ToList();
        }

        public void GetIncomeStatementReport(DateTime dateFrom, DateTime dateTo)
        {
            uow = new UnitOfWork();
            impDetailsRepo = new ImportDetailRepository(uow);
            invoiceRepo = new InvoiceRepository(uow);

            ListInvoice = invoiceRepo.GetAll()
                .Where(i => i.DateCreate.CompareTo(dateFrom) >= 0 
                        && i.DateCreate.CompareTo(dateTo) < 0).ToList();

            ListImportDetails = impDetailsRepo.GetAll()
                .Where(im => im.DateImport.Value.CompareTo(dateFrom) >= 0 
                        && im.DateImport.Value.CompareTo(dateTo) < 0).ToList();

        }
    }
}