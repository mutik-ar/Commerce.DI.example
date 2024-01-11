using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Parameters;
using Core.Results;
using Core.Ports.Driving;
using Core.Ports.Driven;
using Core.Utils;
using Core.DTO;

namespace Core.CommandsServices
{
    public class GetAllProductService : ICommandService<NullParameter, GetAllProductsResult>
    {
        private readonly IRepository<Product> repository;

        public GetAllProductService(IRepository<Product> repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            this.repository = repository;
        }

        public GetAllProductsResult Execute(NullParameter parameter)
        {
            int  code = 1;
            IEnumerable<ProductDTO>? productDTOs = null;
            try
            {
                IEnumerable<Product>?  products = this.repository.GetAll();
                code = 0;
                productDTOs = products?.ToList().Select(i =>
                {
                    return ProductConvert.ProductToDto(i);
                }).AsEnumerable();
            }
            catch
            {
                
            }

            return new GetAllProductsResult(code, productDTOs);
        }
    }
}
