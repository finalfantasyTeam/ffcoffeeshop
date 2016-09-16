using ff.coffee.repository;
using ff.coffee.webapp.Languages.vi;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ff.coffee.webapp.Models
{
    public class RestaurantViewModels : BaseViewModels
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string PrintSize { get; set; }

        public List<Restaurant> ListRestaurant { get; set; }

        private RestaurantRepository resRepo;
        private Restaurant dtoRestaurant { get; set; }

        public RestaurantViewModels()
        { }

        public override void GetDataToList()
        {
            uow = new UnitOfWork();
            resRepo = new RestaurantRepository(uow);
            ListRestaurant = new List<Restaurant>();

            IEnumerable<Restaurant> listRes = resRepo.GetAll();

            foreach (var res in listRes)
            {
                this.ListRestaurant.Add(res);
            }
        }

        public override void GetDataToModel(int Id)
        {
            uow = new UnitOfWork();
            resRepo = new RestaurantRepository(uow);
            this.dtoRestaurant = resRepo.SingleOrDefault(Id);
            MapDtoToModel();
        }

        public override int InsertModel()
        {
            uow = new UnitOfWork();
            resRepo = new RestaurantRepository(uow);
            MapModelToDto();
            return resRepo.Insert(this.dtoRestaurant);
        }

        public override void UpdateModel()
        {
            uow = new UnitOfWork();
            resRepo = new RestaurantRepository(uow);
            MapModelToDto();
            resRepo.Update(this.dtoRestaurant);
        }

        public override int DeleteModel(int Id)
        {
            uow = new UnitOfWork();
            resRepo = new RestaurantRepository(uow);
            this.dtoRestaurant = resRepo.SingleOrDefault(Id);
            return resRepo.Delete(this.dtoRestaurant);
        }

        protected override void MapDtoToModel()
        {
            this.Id = dtoRestaurant.ID;
            this.Name = dtoRestaurant.Name;
            this.Address = dtoRestaurant.Address;
            this.Phone = dtoRestaurant.Phone;
            this.Fax = dtoRestaurant.Fax;
            this.Email = dtoRestaurant.Email;
            this.Website = dtoRestaurant.Website;
            this.PrintSize = dtoRestaurant.PrintSize;
        }

        protected override void MapModelToDto()
        {
            this.dtoRestaurant = new Restaurant();
            this.dtoRestaurant.ID = this.Id;
            this.dtoRestaurant.Name = this.Name;
            this.dtoRestaurant.Address = this.Address;
            this.dtoRestaurant.Phone = this.Phone;
            this.dtoRestaurant.Fax = this.Fax;
            this.dtoRestaurant.Email = this.Email;
            this.dtoRestaurant.Website = this.Website;
            this.dtoRestaurant.PrintSize = this.PrintSize;
        }
    }
}