using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGameCatalog.Exceptions
{
    public class LoginErrorException : Exception
    {
        public LoginErrorException()
            : base("login or password invalid")
        { }
    }
}
