using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if(items == null)
            {
                items = new List<T>();
            }
        }
        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }
        public void Update(T t)
        {
            T tToUpdate = items.Find(x => x.Id == t.Id);
            if(tToUpdate == null)
            {
                throw new Exception(className + " Not found");
            }
            else
            {
                tToUpdate = t;
            }
        }
        public T Find(string Id)
        {
            T toToFind = items.Find(x => x.Id == Id);
            if (toToFind != null)
            {
                return toToFind;
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }
        public void Delete(string Id)
        {
            T toToDelete = items.Find(x => x.Id == Id);
            if (toToDelete != null)
            {
                items.Remove(toToDelete);
            }
            else
            {
                throw new Exception(className + " not found");
            }
        }



    }
}
