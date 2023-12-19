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
    public class IdProductParameter
    {
        [RequiredGuid]
        public Guid ProductId { get; set; }
    }
}
