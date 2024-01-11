using Core.DTO;
using Core.Entities;
using Core.Ports.Driven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    internal static class ProductConvert
    {
        internal static Product? ProductFromDto(ProductDTO? productDTO)
        {
            if (productDTO == null)
            {
                throw new ArgumentNullException(nameof(productDTO));
            }

            Product product  = new();
            product.Id = productDTO.Id;
            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.UnitPrice = productDTO.UnitPrice;

            return product;
        }
        internal static ProductDTO ProductToDto(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            ProductDTO productDTO = new();
            productDTO.Id = product.Id;
            productDTO.Name = product.Name;
            productDTO.Description = product.Description;
            productDTO.UnitPrice = product.UnitPrice;

            return productDTO;
        }
    }
}
