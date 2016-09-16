using ff.coffee.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ff.coffee.webapp.Models
{
    public class AreasViewModels : BaseViewModels
    {
        public List<Area> ListAreas { get; set; }
        public List<SalePoint> ListSalePoint { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SalePointId { get; set; }
        public string SalePointName { get; set; }

        private AreaRepository areaRepo; 
        private Area dtoArea { get; set; }

        public AreasViewModels()
        { }

        public override void GetDataToList()
        {
            uow = new UnitOfWork();
            areaRepo = new AreaRepository(uow);
            ListAreas = new List<Area>();

            IEnumerable<Area> lstArea = areaRepo.GetAll();

            foreach (Area area in lstArea)
            {
                this.ListAreas.Add(area);
            }
        }

        public override void GetDataToModel(int Id)
        {
            uow = new UnitOfWork();
            areaRepo = new AreaRepository(uow);
            this.dtoArea = areaRepo.SingleOrDefault(Id);
            MapDtoToModel();
        }

        public override int InsertModel()
        {
            uow = new UnitOfWork();
            areaRepo = new AreaRepository(uow);
            MapModelToDto();
            return areaRepo.Insert(this.dtoArea);
        }

        public override void UpdateModel()
        {
            uow = new UnitOfWork();
            areaRepo = new AreaRepository(uow);
            MapModelToDto();
            areaRepo.Update(this.dtoArea);
        }

        public override int DeleteModel(int Id)
        {
            uow = new UnitOfWork();
            areaRepo = new AreaRepository(uow);
            this.dtoArea = areaRepo.SingleOrDefault(Id);
            return areaRepo.Delete(this.dtoArea);
        }

        protected override void MapDtoToModel()
        {
            this.Id = dtoArea.ID;
            this.Name = dtoArea.Name;
            this.SalePointId = dtoArea.SalePoint1.ID;
            this.SalePointName = dtoArea.SalePoint1.Name;
        }

        protected override void MapModelToDto()
        {
            this.dtoArea = new Area();
            this.dtoArea.ID = this.Id;
            this.dtoArea.Name = this.Name;
            this.dtoArea.SalePoint = this.SalePointId;
        }
    }
}