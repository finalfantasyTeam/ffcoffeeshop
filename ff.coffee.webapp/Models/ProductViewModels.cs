using ff.coffee.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ff.coffee.webapp.Models
{
    public class ProductViewModels : BaseViewModels
    {
        private ProductRepository productRepo;
        private Product dtoProduct { get; set; }

        public List<Product> ListProduct { get; set; }
        public List<ProductCat> ListProductCat { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreate { get; set; }
        public bool Enable { get; set; }

        public ProductViewModels()
        { }

        public override void GetDataToList()
        {
            uow = new UnitOfWork();
            productRepo = new ProductRepository(uow);
            ListProduct = new List<Product>();

            IEnumerable<Product> lstProduct = productRepo.GetAll();

            foreach (Product p in lstProduct)
            {
                if (p.Enable)
                {
                    ListProduct.Add(p);
                }
            }
        }

        public override void GetDataToModel(int Id)
        {
            uow = new UnitOfWork();
            productRepo = new ProductRepository(uow);
            this.dtoProduct = productRepo.SingleOrDefault(Id);
            MapDtoToModel();
        }

        public override int InsertModel()
        {
            uow = new UnitOfWork();
            productRepo = new ProductRepository(uow);
            MapModelToDto();
            return productRepo.Insert(this.dtoProduct);
        }

        public override void UpdateModel()
        {
            uow = new UnitOfWork();
            productRepo = new ProductRepository(uow);
            MapModelToDto();
            productRepo.Update(this.dtoProduct);
        }

        public override int DeleteModel(int Id)
        {
            uow = new UnitOfWork();
            productRepo = new ProductRepository(uow);
            this.dtoProduct = productRepo.SingleOrDefault(Id);
            return productRepo.Delete(this.dtoProduct);
        }

        protected override void MapDtoToModel()
        {
            this.Id = dtoProduct.ID;
            this.Name = dtoProduct.Name;
            this.Price = dtoProduct.Price.Value;
            this.Unit = dtoProduct.Unit;
            this.CategoryId = dtoProduct.Category;
            this.DateCreate = dtoProduct.DateCreate;
            this.Enable = dtoProduct.Enable;
        }

        protected override void MapModelToDto()
        {
            this.dtoProduct = new Product();
            this.dtoProduct.ID = this.Id;
            this.dtoProduct.Name = this.Name;
            this.dtoProduct.Price = this.Price;
            this.dtoProduct.Unit = this.Unit;
            this.dtoProduct.Category = this.CategoryId;
            this.dtoProduct.DateCreate = this.DateCreate;
            this.dtoProduct.Enable = this.Enable;
        }
    }
}