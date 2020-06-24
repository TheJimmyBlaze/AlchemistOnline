using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlchemistOnline.ConsoleApp.Services.Explorers
{
    public interface IExplorerService
    {
        public Task<int> ExplorerCount();
        public Task<int> IdleExplorerCount();
    }
}
