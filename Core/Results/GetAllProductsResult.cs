using Core.DTO;
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
        private IEnumerable<ProductDTO>? _products;
        public GetAllProductsResult(int code, IEnumerable<ProductDTO>? products) : base(code)
        {
            _products = products;
        }
        public IEnumerable<ProductDTO>? Products { get { return _products; } }

    }
}
