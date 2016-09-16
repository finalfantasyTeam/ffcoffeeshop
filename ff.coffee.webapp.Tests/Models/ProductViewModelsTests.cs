using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ff.coffee.webapp.Models;
using ff.coffee.repository;
using System.Linq;
using System.Collections.Generic;

namespace ff.coffee.webapp.Tests.Models
{
    [TestClass]
    public class ProductViewModelsTests
    {
        private ProductCat fkCategory;
        private Product testDto;

        [TestInitialize]
        public void InitTest()
        {
            using (CoffeeShopEntities db = new CoffeeShopEntities())
            {
                fkCategory = new ProductCat()
                {
                    Name = "cat_dto_test_" + Guid.NewGuid().ToString(),
                    Description = "Test Category",
                    Enable = true
                };

                db.ProductCats.Add(fkCategory);
                db.SaveChanges();

                testDto = new Product()
                {
                    Name = "product_vm_test_" + Guid.NewGuid().ToString(),
                    Unit = "Test",
                    Category = fkCategory.ID,
                    Price = 1000,
                    DateCreate = DateTime.Now,
                    Enable = true
                };
                
                db.Products.Add(testDto);
                db.SaveChanges();
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            using (CoffeeShopEntities db = new CoffeeShopEntities())
            {
                db.Products.Attach(testDto);
                db.ProductCats.Attach(fkCategory);

                db.Products.RemoveRange(db.Products.Where(r => r.Name.StartsWith("product_vm_test_")));
                db.ProductCats.Remove(fkCategory);
                db.SaveChanges();
            }
        }

        private bool CompareModelToDto(ProductViewModels testModel)
        {
            if (testModel.Id != testDto.ID) { return false; }
            if (testModel.Name != testDto.Name) { return false; }
            if (testModel.Price != testDto.Price.Value) { return false; }
            if (testModel.Unit != testDto.Unit) { return false; }
            if (testModel.CategoryId != testDto.Category) { return false; }
            if (testModel.DateCreate.ToString() != testDto.DateCreate.ToString()) { return false; }
            if (testModel.Enable != testDto.Enable) { return false; }

            return true;
        }

        [TestMethod]
        public void GetDataToList()
        {
            ProductViewModels testModel = new ProductViewModels();
            testModel.GetDataToList();
            Assert.IsNotNull(testModel.ListProduct);
            Assert.IsTrue(testModel.ListProduct.Count > 0);
        }

        [TestMethod]
        public void GetDataToModel()
        {
            ProductViewModels testModel = new ProductViewModels();
            testModel.GetDataToModel(testDto.ID);

            Assert.IsTrue(CompareModelToDto(testModel));
        }

        [TestMethod]
        public void InsertModel()
        {
            ProductViewModels testModel = new ProductViewModels();

            //  Insert new Product from Models
            testModel.Name = "product_vm_test_" + Guid.NewGuid().ToString();
            testModel.Unit = "Test Insert";
            testModel.CategoryId = fkCategory.ID;
            testModel.Price = 2000;
            testModel.DateCreate = DateTime.Now;
            testModel.Enable = true;

            int actualId = testModel.InsertModel();
            ProductViewModels actualModel = new ProductViewModels();
            actualModel.GetDataToModel(actualId);

            Assert.AreEqual(testModel.Name, actualModel.Name);
            Assert.AreEqual(testModel.Unit, actualModel.Unit);
            Assert.AreEqual(testModel.CategoryId, actualModel.CategoryId);
            Assert.AreEqual(testModel.Price, actualModel.Price);
            Assert.AreEqual(testModel.DateCreate.ToString(), actualModel.DateCreate.ToString());
            Assert.AreEqual(testModel.Enable, actualModel.Enable);
        }
    }
}
