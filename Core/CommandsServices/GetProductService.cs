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

namespace Core.CommandsServices
{
    public class GetProductService : ICommandService<IdProductParameter, GetProductsResult>
    {
        private readonly IRepository<Product> repository;

        public GetProductService(IRepository<Product> repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            this.repository = repository;
        }

        public GetProductsResult Execute(IdProductParameter parameter)
        {
            int  code = 1;
            Product? product = null;
            try
            {
                product = this.repository.Get(parameter.ProductId);
                code = 0;
            }
            catch
            {
                
            }

            return new GetProductsResult(code, product);
        }
    }
}
