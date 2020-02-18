using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Exceptions
{
    public class ExpeditionInProgressException: Exception
    {
        public ExpeditionInProgressException() { }
        public ExpeditionInProgressException(string message) : base(message) { }
    }
}
