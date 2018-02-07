using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oriflame_Mihalik.Code;
using Oriflame_Mihalik.Models;
using System.Collections.Generic;
using System.Linq;

namespace Oriflame_Mihalik_Test
{
    [TestClass]
    public class ProductTest
    {
        [TestInitialize()]
        public void Initialize()
        {
            var facade = new ProductFacade();
            for (int i = 1; i <= 5; i++)
            {
                facade.DeleteProduct(i);
            }

            List<Product> productList = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Code = "AB12345",
                    Name = "Abcdef",
                    Img = "img0001.jpg"
                }, new Product()
                {
                    Id = 2,
                    Code = "CD12345",
                    Name = "YXEDLS",
                    Img = "img0002.png"
                }, new Product()
                {
                    Id = 3,
                    Code = "EF55555",
                    Name = "Zfdfs",
                    Img = "img0003.tiff"
                }, new Product()
                {
                    Id = 4,
                    Code = "GH5645",
                    Name = "Dawefd",
                }
            };
            foreach (var product in productList)
            {
                facade.SaveProduct(product);
            }
        }

        [TestMethod]
        public void GetProductTest()
        {
            var facade = new ProductFacade();
            Assert.AreEqual(facade.GetProduct(2).Name, "YXEDLS");
        }

        [TestMethod]
        public void GetProductsTest()
        {
            var facade = new ProductFacade();
            var products = facade.GetProducts(new int[4] { 3, 4, 5, 6 });
            Assert.AreEqual(products.Count, 2);
            Assert.AreEqual(products.Last().Code, "GH5645");
        }

        [TestMethod]
        public void SaveProductTest()
        {
            var facade = new ProductFacade();
            Product newProduct = new Product()
            {
                Id = 5,
                Code = "OS56sad",
                Name = "Wpesda",
                Img = "img0005.png"
            };
            var initialProductCount = facade.GetProductCount();
            var result = facade.SaveProduct(newProduct);
            Assert.IsTrue(result);
            Assert.AreEqual(initialProductCount + 1, facade.GetProductCount());
        }

        [TestMethod]
        public void DeleteProductTest()
        {
            var facade = new ProductFacade();
            facade.DeleteProduct(4);
            Assert.AreEqual(facade.GetProductCount(), 3);
            Assert.AreEqual(facade.GetProduct(4), null);
        }
    }
}
