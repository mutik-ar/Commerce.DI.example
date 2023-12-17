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
            int  code = 0;
            IEnumerable<Product>? products = null;
            try
            {
                products = this.repository.GetAll();
            }
            catch (Exception ex)
            {
                code = 1;
            }

            return new GetAllProductsResult(code, products);
        }
    }
}
