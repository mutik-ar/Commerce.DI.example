using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Attributes;
using Core.Enums;

namespace Core.Parameters
{
    [PermittedRole(Role.Administrator)]
    public class ProductParameter: IdProductParameter
    {
        [Required, StringLength(50)]
        public string? Name { get; set; }
        [Required]
        public string? UnitPrice { get; set; }
        public string? Description { get; set; }
    }
}
