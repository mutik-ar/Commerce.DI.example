using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results
{
    public class GetProductsResult: Result
    {
        private Product? _product;
        public GetProductsResult(int code, Product? product) : base(code)
        {
            _product = product;
        }
        public Product? Product { get { return _product; } }

    }
}
