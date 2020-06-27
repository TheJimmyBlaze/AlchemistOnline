using AlchemistOnline.ConsoleApp.Services;
using AlchemistOnline.ConsoleApp.Services.Explorers;
using AlchemistOnline.ConsoleApp.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlchemistOnline.ConsoleApp.Commands
{
    public class MainMenuHandler
    {
        private readonly IExplorerService explorer;
        public MainMenuHandler(IExplorerService explorer) => this.explorer = explorer;

        public async Task DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1: Manage Explorers");
            Console.WriteLine("2: Manage Ingredients");

            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine(await explorer.ExplorerCount());
                    Console.WriteLine(await explorer.IdleExplorerCount());
                    break;
                case "2":

                    break;
                default:
                    Console.WriteLine("Option: {0} not recognized", option);
                    break;
            }
        }
    }
}
