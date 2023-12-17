using Model.Entities;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Model.Interfaces;
using Repository.EF;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private CommerceContext db;
        private ProductRepository productRepository;

        public UnitOfWork(string connectionString)
        {
            db = new CommerceContext(connectionString);
            var test = db.Database;
        }
        public IRepository<Product> Products
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(db);
                return (IRepository<Product>)productRepository;
            }
        }


        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
