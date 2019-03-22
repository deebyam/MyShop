using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if(products == null){
                products = new List<Product>();
            }
        }
        public void Commit()
        {
            cache["products"] = products;
        }
        public void Insert(Product p)
        {
            products.Add(p);
        }
        public void Update(Product product)
        {
            Product prodUpdate = products.Find(p=>p.Id == product.Id);
            if (prodUpdate != null)
            {
                prodUpdate = product;
            }
            else
            {
                throw new Exception("Product not found!");
            }      
        }
        public Product Find(string Id)
        {
            Product prodFind = products.Find(p => p.Id == Id);
            if(prodFind != null)
            {
                return prodFind;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
        public void Delete(string Id)
        {
            Product prodDelete = products.Find(p => p.Id == Id);
            if (prodDelete != null)
            {
                products.Remove(prodDelete);
            }
            else
            {
                throw new Exception("No Product to delete.");
            }
        }

    }
}
