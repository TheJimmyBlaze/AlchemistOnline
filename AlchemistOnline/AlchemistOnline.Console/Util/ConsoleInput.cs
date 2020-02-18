using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.ConsoleApp.Util
{
    public class ConsoleInput
    {
        public string GetHiddenConsoleInput()
        {
            StringBuilder input = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) 
                    break;

                if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove(input.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    input.Append(key.KeyChar);
                    Console.Write("*");
                }
            }

            Console.WriteLine();
            return input.ToString();
        }
    }
}
