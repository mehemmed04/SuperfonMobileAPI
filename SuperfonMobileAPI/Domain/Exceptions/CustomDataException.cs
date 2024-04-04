using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Domain.Exceptions
{
    public class CustomDataException : Exception
    {
        public CustomDataException(string errmsg) : base(errmsg) { }
    }
}
