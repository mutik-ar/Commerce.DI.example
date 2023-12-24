using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Results
{
    public class Result
    {
        readonly int _code;
        public Result(int code)
        {
            this._code = code;
        }

        public int Code { get { return _code;} }
    }
}
