using ff.coffee.webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ff.coffee.webapp.Controllers
{
    public class ReportController : BaseController
    {
        private ReportViewModels reportVM;
        public ActionResult ReportSales()
        {
            reportVM = new ReportViewModels();
            reportVM.ReportDateFrom = DateTime.Now;
            reportVM.ReportDateTo = DateTime.Now;
            return View(reportVM);
        }

        public ActionResult ReportSalesDetail(int invoiceId)
        {
            reportVM = new ReportViewModels();
            reportVM.GetSalesReportDetail(invoiceId);

            return View(reportVM);
        }

        [HttpPost]
        public ActionResult ReportSales(ReportViewModels model)
        {
            model.GetSalesReports(model.ReportDateFrom, model.ReportDateTo);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportIncomeStatement(ReportViewModels model)
        {
            reportVM = new ReportViewModels();
            reportVM.GetIncomeStatementReport(model.ReportDateFrom, model.ReportDateTo);

            return View(reportVM);
        }

    }
}
