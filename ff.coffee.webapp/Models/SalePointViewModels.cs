using ff.coffee.repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ff.coffee.webapp.Models
{
    public class SalePointViewModels : BaseViewModels
    {     
        public List<SalePoint> ListSalePoint { get; set; }
        public List<Restaurant> ListRestaurant { get; set; }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsWorking { get; set; }

        private SalePointRepository saleRepo;
        private SalePoint dtoSalePoint { get; set; }

        public SalePointViewModels()
        { }

        public override void GetDataToList()
        {
            uow = new UnitOfWork();
            saleRepo = new SalePointRepository(uow);
            ListSalePoint = new List<SalePoint>();
            IEnumerable<SalePoint> listRes = saleRepo.GetAll();

            foreach (var res in listRes)
            {
                this.ListSalePoint.Add(res);
            }
        }

        public override void GetDataToModel(int Id)
        {
            uow = new UnitOfWork();
            saleRepo = new SalePointRepository(uow);
            this.dtoSalePoint = saleRepo.SingleOrDefault(Id);
            MapDtoToModel();
        }

        public override int InsertModel()
        {
            uow = new UnitOfWork();
            saleRepo = new SalePointRepository(uow);
            MapModelToDto();
            return saleRepo.Insert(this.dtoSalePoint);
        }

        public override void UpdateModel()
        {
            uow = new UnitOfWork();
            saleRepo = new SalePointRepository(uow);
            MapModelToDto();
            saleRepo.Update(this.dtoSalePoint);
        }

        public override int DeleteModel(int Id)
        {
            uow = new UnitOfWork();
            saleRepo = new SalePointRepository(uow);
            this.dtoSalePoint = saleRepo.SingleOrDefault(Id);
            return saleRepo.Delete(this.dtoSalePoint);
        }

        protected override void MapDtoToModel()
        {
            this.Id = dtoSalePoint.ID;
            this.Name = dtoSalePoint.Name;
            this.Address = dtoSalePoint.Address;
            this.Phone = dtoSalePoint.Phone;
            this.Fax = dtoSalePoint.Fax;
            this.Email = dtoSalePoint.Email;
            this.Website = dtoSalePoint.Website;
            this.RestaurantId = dtoSalePoint.RestaurantID.Value;
            this.RestaurantName = dtoSalePoint.Restaurant.Name;
            this.StartDate = dtoSalePoint.StartDate.Value;
            this.IsWorking = dtoSalePoint.IsWorking.Value;
        }

        protected override void MapModelToDto()
        {
            this.dtoSalePoint = new SalePoint();
            this.dtoSalePoint.ID = this.Id;
            this.dtoSalePoint.Name = this.Name;
            this.dtoSalePoint.Address = this.Address;
            this.dtoSalePoint.Phone = this.Phone;
            this.dtoSalePoint.Fax = this.Fax;
            this.dtoSalePoint.Email = this.Email;
            this.dtoSalePoint.Website = this.Website;
            this.dtoSalePoint.RestaurantID = this.RestaurantId;
            this.dtoSalePoint.StartDate = this.StartDate;
            this.dtoSalePoint.IsWorking = this.IsWorking;
        }
    }
}