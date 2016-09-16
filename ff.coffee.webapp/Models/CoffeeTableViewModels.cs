using ff.coffee.repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ff.coffee.webapp.Models
{
    public class CoffeeTableViewModels : BaseViewModels
    {
        
        public List<CoffeeTable> ListCoffeeTables { get; set; }
        public List<Area> ListAreas { get; set; }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public bool IsServe { get; set; }
        public string Notes { get; set; }
        public string OrderNotes { get; set; }
        public bool Enable { get; set; }

        private CoffeeTableRepository tableRepo;
        private CoffeeTable dtoCoffeeTable { get; set; }

        public CoffeeTableViewModels()
        { }

        public override void GetDataToList()
        {
            uow = new UnitOfWork();
            tableRepo = new CoffeeTableRepository(uow);
            ListCoffeeTables = new List<CoffeeTable>();

            IEnumerable<CoffeeTable> lstTable = tableRepo.GetAll();

            foreach (CoffeeTable coffee in lstTable)
            {
                if (coffee.Enable.Value)
                {
                    this.ListCoffeeTables.Add(coffee);
                }
            }

            if (this.AreaId > 0)
            {
                this.ListCoffeeTables = this.ListCoffeeTables.Where(coffee => coffee.Areas == this.AreaId).ToList();
                this.AreaName = this.ListCoffeeTables[0].Area.Name;
            }
        }

        public override void GetDataToModel(int Id)
        {
            uow = new UnitOfWork();
            tableRepo = new CoffeeTableRepository(uow);
            this.dtoCoffeeTable = tableRepo.SingleOrDefault(Id);
            MapDtoToModel();
        }

        public override int InsertModel()
        {
            uow = new UnitOfWork();
            tableRepo = new CoffeeTableRepository(uow);
            MapModelToDto();
            return tableRepo.Insert(this.dtoCoffeeTable);
        }

        public override void UpdateModel()
        {
            uow = new UnitOfWork();
            tableRepo = new CoffeeTableRepository(uow);
            MapModelToDto();
            tableRepo.Update(this.dtoCoffeeTable);
        }

        public override int DeleteModel(int Id)
        {
            uow = new UnitOfWork();
            tableRepo = new CoffeeTableRepository(uow);
            this.dtoCoffeeTable = tableRepo.SingleOrDefault(Id);
            return tableRepo.Delete(this.dtoCoffeeTable);
        }

        protected override void MapDtoToModel()
        {
            this.Id = dtoCoffeeTable.ID;
            this.Name = dtoCoffeeTable.Name;
            this.AreaId = dtoCoffeeTable.Areas.Value;
            this.AreaName = dtoCoffeeTable.Area.Name;
            this.OrderNotes = dtoCoffeeTable.OrderNotes;
            this.Notes = dtoCoffeeTable.Notes;
            this.IsServe = dtoCoffeeTable.IsServe.Value;
            this.Enable = dtoCoffeeTable.Enable.Value;
        }

        protected override void MapModelToDto()
        {
            this.dtoCoffeeTable = new CoffeeTable();
            this.dtoCoffeeTable.ID = this.Id;
            this.dtoCoffeeTable.Name = this.Name;
            this.dtoCoffeeTable.Areas = this.AreaId;
            this.dtoCoffeeTable.OrderNotes = this.OrderNotes;
            this.dtoCoffeeTable.Notes = this.Notes;
            this.dtoCoffeeTable.IsServe = this.IsServe;
            this.dtoCoffeeTable.Enable = this.Enable;
        }
    }
}