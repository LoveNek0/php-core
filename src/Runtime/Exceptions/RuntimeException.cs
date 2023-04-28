using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Runtime.Exceptions
{
    public class RuntimeException : Exception
    {
        public RuntimeException(string message) : base($"Runtime exception: {message}") { }
    }
}
