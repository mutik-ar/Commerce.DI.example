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
    public class InsertProductService : ICommandService<InsertProductParameter, Result>
    {
        private readonly IRepository<Product> repository;

        public InsertProductService(IRepository<Product> repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            this.repository = repository;
        }

        public Result Execute(InsertProductParameter parameter)
        {
            int  code = 0;
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
            }
            catch (Exception ex)
            {
                code = 1;
            }
            return new Result(code);
        }
    }
}
