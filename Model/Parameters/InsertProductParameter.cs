using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Attributes;
using Model.Enums;

namespace Model.Parameters
{
    [PermittedRole(Role.Administrator)]
    public class InsertProductParameter
    {
        [RequiredGuid]
        public Guid ProductId { get; set; }
        [Required, StringLength(50)]
        public string? Name { get; set; }
        [Required]
        public string? UnitPrice { get; set; }
        public string? Description { get; set; }
    }
}
