using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemistOnline.ConsoleApp.Util
{
    public class ConsoleOutput
    {
        public static readonly ConsoleColor InformationMessageColour = ConsoleColor.Green;

        public void WriteColouredLine(string message, ConsoleColor foregroundColour, ConsoleColor backgroundColour = ConsoleColor.Black)
        {
            WriteColoured(message + "\n", foregroundColour, backgroundColour);
        }

        public void WriteColoured(string message, ConsoleColor foregroundColour, ConsoleColor backgroundColour = ConsoleColor.Black)
        {
            ConsoleColor originalForeground = Console.ForegroundColor;
            ConsoleColor originalBackground = Console.BackgroundColor;

            Console.ForegroundColor = foregroundColour;
            Console.BackgroundColor = backgroundColour;
            Console.WriteLine(message);

            Console.ForegroundColor = originalForeground;
            Console.BackgroundColor = originalBackground;
        }
    }
}
