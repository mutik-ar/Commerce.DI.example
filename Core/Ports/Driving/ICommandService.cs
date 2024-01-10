using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Ports.Driving
{
    public interface ICommandService<in T, out P>
        where T : class
        where P : class
    {
        public P Execute(T parameter);
    }
}
