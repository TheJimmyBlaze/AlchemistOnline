using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Exceptions
{
    public class ExplorerLevelException : Exception
    {
        public ExplorerLevelException() { }
        public ExplorerLevelException(string message) : base(message) { }
    }
}
