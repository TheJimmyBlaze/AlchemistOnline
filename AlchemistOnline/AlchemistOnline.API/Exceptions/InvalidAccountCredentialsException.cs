using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlchemistOnline.API.Exceptions
{
    public class InvalidAccountCredentialsException: Exception
    {
        public InvalidAccountCredentialsException() { }
        public InvalidAccountCredentialsException(string message) : base(message) { }
    }
}
