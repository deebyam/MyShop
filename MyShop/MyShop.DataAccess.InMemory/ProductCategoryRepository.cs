using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;
        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }
        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }
        public void Update(ProductCategory productCat)
        {
            ProductCategory prodUpdate = productCategories.Find(p => p.Id == productCat.Id);
            if (prodUpdate != null)
            {
                prodUpdate = productCat;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }
        public ProductCategory Find(string Id)
        {
            ProductCategory prodFind = productCategories.Find(p => p.Id == Id);
            if (prodFind != null)
            {
                return prodFind;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }
        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }
        public void Delete(string Id)
        {
            ProductCategory prodDelete = productCategories.Find(p => p.Id == Id);
            if (prodDelete != null)
            {
                productCategories.Remove(prodDelete);
            }
            else
            {
                throw new Exception("No Product to delete.");
            }
        }

    }
}

