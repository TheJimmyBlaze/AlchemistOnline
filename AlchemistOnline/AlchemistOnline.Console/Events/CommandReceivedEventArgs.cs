using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlchemistOnline.ConsoleApp.Events
{
    public class CommandReceivedEventArgs
    {
        public readonly string RawCommand;

        public readonly string Verb;
        public readonly IEnumerable<string> Arguments;

        public CommandReceivedEventArgs(string rawCommand)
        {
            RawCommand = rawCommand;

            string[] words = rawCommand.Split(" ");
            Verb = words[0];
            Arguments = words.Skip(1);
        }
    }
}
