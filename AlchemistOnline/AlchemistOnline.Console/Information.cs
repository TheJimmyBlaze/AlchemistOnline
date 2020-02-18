using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AlchemistOnline.ConsoleApp
{
    static class Information
    {
        private const string APP_NAME = "Alchemist Online: Console";

        private static readonly ConsoleColor titleConsoleColour = ConsoleColor.Green;

        public static void PrintTitle()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            string name = string.Format("{0} - v{1}", APP_NAME, version);

            ConsoleColor preForground = Console.ForegroundColor;
            Console.ForegroundColor = titleConsoleColour;
            Console.WriteLine(name);
            Console.ForegroundColor = preForground;
        }

        public static void PrintInstructions()
        {
            Console.WriteLine("Todo: print instructions...");
        }
    }
}
