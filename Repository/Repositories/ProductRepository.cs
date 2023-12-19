using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Interfaces;
using Repository.EF;
using System.Collections.ObjectModel;

namespace Repository.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private CommerceContext db;

        public ProductRepository(CommerceContext context)
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
            db.Entry(item).State = EntityState.Modified;
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
