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
    public class DeleteProductService : ICommandService<IdProductParameter, Result>
    {
        private readonly IRepository<Product> repository;

        public DeleteProductService(IRepository<Product> repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            this.repository = repository;
        }

        public Result Execute(IdProductParameter parameter)
        {
            int  code = 1;
            try
            {
                bool result = this.repository.Delete(parameter.ProductId);
                if (result)
                {
                    code = 0;
                }
            }
            catch 
            {
            }
            return new Result(code);
        }
    }
}
