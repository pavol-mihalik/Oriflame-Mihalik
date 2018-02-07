using Oriflame_Mihalik.Models;
using System;
using System.Collections.Generic;

namespace Oriflame_Mihalik.Code
{
    public class ProductFacade
    {
        private IProductRepository _productRepository;
        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository();
                }
                return _productRepository;
            }
            set
            {
                _productRepository = value;
            }
        }

        public Product GetProduct(int id)
        {
            return this.ProductRepository.GetProductFromJSON(id);
        }

        public List<Product> GetProducts(int[] ids)
        {
            return this.ProductRepository.GetProductsFromJSON(ids);
        }

        public bool SaveProduct(Product product)
        {
            return this.ProductRepository.SaveProductToJSON(product);
        }

        public void DeleteProduct(int id)
        {
            this.ProductRepository.DeleteProductInJSON(id);
        }

        public int GetProductCount()
        {
            return this.ProductRepository.GetProductCountInJSON();
        }
    }
}