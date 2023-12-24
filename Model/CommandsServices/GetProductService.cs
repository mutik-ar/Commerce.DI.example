using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Parameters;
using Model.Results;
using Model.Interfaces;

namespace Model.CommandsServices
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
