using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results
{
    public class GetAllProductsResult: Result
    {
        private IEnumerable<Product>? _products;
        public GetAllProductsResult(int code, IEnumerable<Product>? products) : base(code)
        {
            _products = products;
        }
        public IEnumerable<Product>? Products { get { return _products; } }

    }
}
