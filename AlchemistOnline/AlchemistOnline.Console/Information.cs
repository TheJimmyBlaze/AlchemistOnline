using AlchemistOnline.ConsoleApp.Util;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AlchemistOnline.ConsoleApp
{
    public static class Information
    {
        private const string APP_NAME = "Alchemist Online: Console";

        public static void PrintTitle(ConsoleOutput output)
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            string name = string.Format("{0} - v{1}", APP_NAME, version);
            output.WriteColouredLine(name, ConsoleOutput.InformationMessageColour);
        }
    }
}
