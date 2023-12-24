using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Ports.Driven
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        void Save();
    }
}
