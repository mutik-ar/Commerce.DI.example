using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RequiredGuidAttribute : RequiredAttribute
    {
        public override bool IsValid(object value) => value != null && value is Guid Id && Id != Guid.Empty;
    }
}
