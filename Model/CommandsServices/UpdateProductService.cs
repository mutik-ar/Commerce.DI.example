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
    public class UpdateProductService : ICommandService<ProductParameter, Result>
    {
        private readonly IRepository<Product> repository;

        public UpdateProductService(IRepository<Product> repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            this.repository = repository;
        }

        public Result Execute(ProductParameter parameter)
        {
            int code = 1;
            try
            {
                decimal.TryParse(parameter.UnitPrice, out decimal unitPrice);
                this.repository.Update(new Product
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
