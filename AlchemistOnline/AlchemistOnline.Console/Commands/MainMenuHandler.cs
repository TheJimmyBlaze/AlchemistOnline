using AlchemistOnline.ConsoleApp.Services;
using AlchemistOnline.ConsoleApp.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.ConsoleApp.Commands
{
    public class MainMenuHandler
    {
        public void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1: Manage Explorers");
            Console.WriteLine("2: Manage Ingredients");

            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    
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
