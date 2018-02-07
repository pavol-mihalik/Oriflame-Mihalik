using Oriflame_Mihalik.Code;
using Oriflame_Mihalik.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Oriflame_Mihalik.Controllers
{
    public class ProductsController : ApiController
    {
        // GET api/products/5
        public Product GetProduct(int id)
        {
            ProductFacade facade = new ProductFacade();
            Product product = facade.GetProduct(id);
            return product;
        }

        // GET api/products
        public IEnumerable<Product> GetProducts([FromUri] int[] ids)
        {
            ProductFacade facade = new ProductFacade();
            List<Product> products = facade.GetProducts(ids);
            return products;
        }

        // POST api/products
        public void AddProduct([FromBody]Product product)
        {
            ProductFacade facade = new ProductFacade();
            facade.SaveProduct(product);
        }

        // DELETE api/products/5
        public void DeleteProduct(int id)
        {
            ProductFacade facade = new ProductFacade();
            facade.DeleteProduct(id);
        }
    }
}
