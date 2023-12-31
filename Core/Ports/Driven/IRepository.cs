﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Ports.Driven
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T? Get(Guid id);
        List<T> Find(Func<T, bool> predicate);
        void Create(T item);
        void Update(T item);
        bool Delete(Guid id);
        void DeleteAll();
    }
}
