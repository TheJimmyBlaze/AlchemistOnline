using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Exceptions
{
    public class ExpeditionNotFoundException: Exception
    {
        public ExpeditionNotFoundException() { }
        public ExpeditionNotFoundException(string message) : base(message) { }
    }
}
