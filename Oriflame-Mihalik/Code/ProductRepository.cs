using Newtonsoft.Json;
using Oriflame_Mihalik.Models;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Oriflame_Mihalik.Code
{
    public class ProductRepository : IProductRepository
    {
        public Product GetProductFromJSON(int id)
        {
            List<Product> productList = LoadProductsFromJSON();
            return productList.Where(product => product.Id == id).FirstOrDefault();
        }

        public List<Product> GetProductsFromJSON(int[] productIds)
        {
            List<Product> productList = LoadProductsFromJSON();
            return productList.Where(product => productIds.Contains(product.Id)).ToList();
        }

        public bool SaveProductToJSON(Product newProduct)
        {
            if (!IsProductCompliant(newProduct))
            {
                return false;
            }

            List<Product> productList = LoadProductsFromJSON();

            // Pokud produkt se stejným Id už existuje, nepřidá se do seznamu.
            if (!productList.Any(product => product.Id == newProduct.Id))
            {
                productList.Add(newProduct);
                SaveProductsToJSON(productList);
                return true;
            }

            return false;
        }

        public void DeleteProductInJSON(int id)
        {
            List<Product> productList = LoadProductsFromJSON();
            Product productToDelete = productList.Where(product => product.Id == id).FirstOrDefault();

            // Pokud ostranění ze seznamu proběhne úspěšně, změny se uloží.
            if (productList.Remove(productToDelete))
            {
                SaveProductsToJSON(productList);
            }
        }

        public int GetProductCountInJSON()
        {
            return LoadProductsFromJSON().Count();
        }

        private List<Product> LoadProductsFromJSON()
        {
            // Nikdy jsem nemockoval, takže nevím, jak přesně to vyřešit pro unit testy, proto cesta natvrdo.
            string path = ConfigurationManager.AppSettings["JSONPath"];
            string jsonData = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(path) ??
                @"C:\Users\summ\Source\Repos\Oriflame-Mihalik\Oriflame-Mihalik\" + path.Substring(2, path.Length - 2));

            return JsonConvert.DeserializeObject<ProductList>(jsonData,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }
            ).Products;
        }

        private void SaveProductsToJSON(List<Product> productList)
        {
            ProductList products = new ProductList()
            {
                Products = productList
            };
            //string jsonData = JsonConvert.SerializeObject(products);
            string jsonData = JsonConvert.SerializeObject(products, Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            string path = ConfigurationManager.AppSettings["JSONPath"];
            File.WriteAllText(System.Web.Hosting.HostingEnvironment.MapPath(path) ??
                @"C:\Users\summ\Source\Repos\Oriflame-Mihalik\Oriflame-Mihalik\" + path.Substring(2, path.Length - 2)
                , jsonData);
        }

        /// <summary>
        /// Ověření, jestli produkt splňuje povinné požadavky.
        /// </summary>
        private bool IsProductCompliant(Product newProduct)
        {
            if (newProduct.Id == 0 || string.IsNullOrWhiteSpace(newProduct.Name) || string.IsNullOrWhiteSpace(newProduct.Code))
            {
                return false;
            }

            return true;
        }
    }
}