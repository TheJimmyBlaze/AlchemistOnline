using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlchemistOnline.ConsoleApp.Commands
{
    public interface ILoginHandler
    {
        public Task Login();
    }
}
