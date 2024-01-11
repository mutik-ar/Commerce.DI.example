using Core.Entities;
using Core.Ports.Driven;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tests
{
    internal class RepositoryProductInMemory : IRepository<Product>
    {
        private WritingDbContext db;

        public RepositoryProductInMemory(WritingDbContext context)
        {
            this.db = context;
        }

        public List<Product> GetAll()
        {
            return db.Products.ToList();
        }

        public Product? Get(Guid Id)
        {
            return db.Products.Find(Id);
        }

        public void Create(Product item)
        {
            db.Products.Add(item);
        }

        public void Update(Product item)
        {
            Product product = db.Products.Find(item.Id);
            if (product != null)
            {
                product.Name = item.Name;
                product.Description = item.Description;
                product.UnitPrice = item.UnitPrice;
                db.Entry(product).State = EntityState.Modified;
            }
        }
        public List<Product> Find(Func<Product, Boolean> predicate)
        {
            return db.Products.Where(predicate).ToList();
        }
        public bool Delete(Guid Id)
        {
            Product? item = db.Products.Find(Id);
            if (item != null)
            {
                db.Products.Remove(item);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DeleteAll()
        {
            db.Products.RemoveRange(db.Products);
        }

    }
}
