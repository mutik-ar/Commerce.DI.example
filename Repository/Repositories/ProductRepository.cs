﻿using Microsoft.EntityFrameworkCore;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.EF;
using System.Collections.ObjectModel;
using Core.Ports.Driven;

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
