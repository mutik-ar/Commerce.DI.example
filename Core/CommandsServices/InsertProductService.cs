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
    public class InsertProductService : ICommandService<ProductParameter, Result>
    {
        private readonly IRepository<Product> repository;

        public InsertProductService(IRepository<Product> repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            this.repository = repository;
        }

        public Result Execute(ProductParameter parameter)
        {
            int  code = 1;
            try
            {
                decimal.TryParse(parameter.UnitPrice, out decimal unitPrice);
                this.repository.Create(new Product
                {
                    Id = parameter.ProductId,
                    Name = parameter.Name,
                    UnitPrice = unitPrice,
                    Description = parameter.Description ?? string.Empty,
                });
                code = 0;
            }
            catch 
            {
            }
            return new Result(code);
        }
    }
}
