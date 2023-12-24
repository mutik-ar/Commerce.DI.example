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
            IEnumerable<Product>? products = null;
            try
            {
                products = this.repository.GetAll();
                code = 0;
            }
            catch
            {
                
            }

            return new GetAllProductsResult(code, products);
        }
    }
}
