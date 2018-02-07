using Oriflame_Mihalik.Models;
using System.Collections.Generic;

namespace Oriflame_Mihalik.Code
{
    public interface IProductRepository
    {
        Product GetProductFromJSON(int id);
        List<Product> GetProductsFromJSON(int[] ids);
        bool SaveProductToJSON(Product newProduct);
        void DeleteProductInJSON(int id);
        int GetProductCountInJSON();
    }
}