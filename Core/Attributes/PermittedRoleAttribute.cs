using Core.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    // ---- Start code Listing 10.17 ---- 
    public class PermittedRoleAttribute : Attribute
    {
        public readonly Role Role;

        public PermittedRoleAttribute(Role role)
        {
            this.Role = role;
        }
    }
}
