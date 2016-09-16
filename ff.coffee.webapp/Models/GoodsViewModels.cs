using ff.coffee.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ff.coffee.webapp.Models
{
    public class GoodsViewModels : BaseViewModels
    {
        public int Id { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string Unit { get; set; }
        public DateTime DateImport { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string UserImport { get; set; }
        public bool Enable { get; set; }
    

        public List<Store> ListGoods { get; set; }
        public IEnumerable<Product> ListProduct { get; set; }

        private StoreRepository goodsRepo;
        private ImportDetailRepository ticketRepo;
        private Store dtoGoods { get; set; }
        private ImportDetail dtoImportTicket { get; set; }

        public GoodsViewModels()
        {
        }

        public override void GetDataToList()
        {
            uow = new UnitOfWork();
            goodsRepo = new StoreRepository(uow);
            ListGoods = new List<Store>();

            IEnumerable<Store> lstGoods = goodsRepo.GetAll();

            foreach (Store goods in lstGoods)
            {
                ListGoods.Add(goods);
            }
        }

        public override void GetDataToModel(int Id)
        {
            uow = new UnitOfWork();
            goodsRepo = new StoreRepository(uow);
            this.dtoGoods = goodsRepo.SingleOrDefault(Id);
            MapDtoToModel();
        }

        public override int InsertModel()
        {
            uow = new UnitOfWork();
            goodsRepo = new StoreRepository(uow);
            ticketRepo = new ImportDetailRepository(uow);
            MapModelToDto();

            // Import goods to store
            this.dtoGoods = FindGoodsInStore(this.ProductID.Value, this.Unit);

            if (this.dtoGoods != null)
            {
                this.dtoGoods.LastImport = this.dtoImportTicket.DateImport;
                this.dtoGoods.OriginPrice = this.dtoImportTicket.Price;
                this.dtoGoods.QuantityImport += this.dtoImportTicket.Quantity;
                this.dtoGoods.QuantityInStock += this.dtoImportTicket.Quantity;
                this.dtoGoods.UnitImport = this.dtoImportTicket.Unit;

                goodsRepo.Update(this.dtoGoods);
            }
            else 
            {
                this.dtoGoods = new Store();
                this.dtoGoods.ProductID = this.dtoImportTicket.ProductID;
                this.dtoGoods.LastImport = this.dtoImportTicket.DateImport;
                this.dtoGoods.OriginPrice = this.dtoImportTicket.Price;
                this.dtoGoods.QuantityImport = this.dtoImportTicket.Quantity.Value;
                this.dtoGoods.QuantityInStock = this.dtoImportTicket.Quantity.Value;
                this.dtoGoods.UnitImport = this.dtoImportTicket.Unit;
                this.dtoGoods.QuantitySold = 0;
                this.dtoGoods.Enable = true;

                goodsRepo.Insert(this.dtoGoods);
            }

            return ticketRepo.Insert(this.dtoImportTicket);
        }

        public override void UpdateModel()
        {
            uow = new UnitOfWork();
            goodsRepo = new StoreRepository(uow);
            MapModelToDto();
            goodsRepo.Update(this.dtoGoods);
        }

        public override int DeleteModel(int Id)
        {
            uow = new UnitOfWork();
            goodsRepo = new StoreRepository(uow);
            this.dtoGoods = goodsRepo.SingleOrDefault(Id);
            return goodsRepo.Delete(this.dtoGoods);
        }

        private Store FindGoodsInStore(int productId, string productUnit)
        {
            uow = new UnitOfWork();
            goodsRepo = new StoreRepository(uow);

            IEnumerable<Store> lstGoods = goodsRepo.GetAll();
            Store goodsInStore = lstGoods.FirstOrDefault(g => g.ProductID == productId && g.UnitImport.Contains(productUnit));

            return goodsInStore;
        }

        protected override void MapDtoToModel()
        {
            this.Id = dtoImportTicket.ID;
            this.ProductID = dtoImportTicket.ProductID.Value;
            this.Unit = dtoImportTicket.Unit;
            this.Price = dtoImportTicket.Price.Value;
            this.Quantity = dtoImportTicket.Quantity.Value;
            this.DateImport = dtoImportTicket.DateImport.Value;
            this.UserImport = dtoImportTicket.UserImport;
            this.Enable = dtoImportTicket.Enable.Value;
        }

        protected override void MapModelToDto()
        {
            this.dtoImportTicket = new ImportDetail();
            this.dtoImportTicket.ID = this.Id;
            this.dtoImportTicket.ProductID = this.ProductID;
            this.dtoImportTicket.Unit = this.Unit;
            this.dtoImportTicket.Price = this.Price;
            this.dtoImportTicket.Quantity = this.Quantity;
            this.dtoImportTicket.UserImport = this.UserImport;
            this.dtoImportTicket.DateImport = this.DateImport;
            this.dtoImportTicket.Enable = this.Enable;
        }
    }
}