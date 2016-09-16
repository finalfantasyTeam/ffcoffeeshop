using ff.coffee.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ff.coffee.webapp.Models
{
    public class ProductCatViewModels : BaseViewModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enable { get; set; }
        public List<ProductCat> ListProductCat { get; set; }

        private ProductCategoryRepository catRepo;
        private ProductCat dtoCategory;

        public override void GetDataToList()
        {
            uow = new UnitOfWork();
            catRepo = new ProductCategoryRepository(uow);
            ListProductCat = new List<ProductCat>();

            IEnumerable<ProductCat> lstCat = catRepo.GetAll();

            foreach (ProductCat cat in lstCat)
            {
                if (cat.Enable)
                {
                    ListProductCat.Add(cat);
                }
            }
        }

        public override void GetDataToModel(int Id)
        {
            uow = new UnitOfWork();
            catRepo = new ProductCategoryRepository(uow);
            this.dtoCategory = catRepo.SingleOrDefault(Id);
            MapDtoToModel();
        }

        public override int InsertModel()
        {
            uow = new UnitOfWork();
            catRepo = new ProductCategoryRepository(uow);
            MapModelToDto();
            return catRepo.Insert(this.dtoCategory);
        }

        public override void UpdateModel()
        {
            uow = new UnitOfWork();
            catRepo = new ProductCategoryRepository(uow);
            MapModelToDto();
            catRepo.Update(this.dtoCategory);
        }

        public override int DeleteModel(int Id)
        {
            uow = new UnitOfWork();
            catRepo = new ProductCategoryRepository(uow);
            this.dtoCategory = catRepo.SingleOrDefault(Id);
            return catRepo.Delete(this.dtoCategory);
        }

        protected override void MapDtoToModel()
        {
            this.Id = dtoCategory.ID;
            this.Name = dtoCategory.Name;
            this.Description = dtoCategory.Description;
            this.Enable = dtoCategory.Enable;
        }

        protected override void MapModelToDto()
        {
            this.dtoCategory = new ProductCat();
            this.dtoCategory.ID = this.Id;
            this.dtoCategory.Name = this.Name;
            this.dtoCategory.Description = this.Description;
            this.dtoCategory.Enable = this.Enable;
        }
        
    }
}