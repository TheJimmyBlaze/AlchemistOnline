using AlchemistOnline.ConsoleApp.Commands;
using AlchemistOnline.ConsoleApp.Events;
using AlchemistOnline.ConsoleApp.Services;
using AlchemistOnline.ConsoleApp.Util;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace AlchemistOnline.ConsoleApp
{
    class Program
    {
        #region events
        public static EventHandler<CommandReceivedEventArgs> CommandReceived;
        private static void OnCommandReceived(string rawCommand) => CommandReceived?.Invoke(null, new CommandReceivedEventArgs(rawCommand));

        public static EventHandler Closing;
        private static void OnClosing() => Closing?.Invoke(null, null);
        #endregion

        #region args
        private const string API_ADDRESS_ARG = "-apiadr";
        private static readonly Dictionary<string, string> arguments = new Dictionary<string, string>();
        #endregion

        public static string ApiAddress { get { return arguments[API_ADDRESS_ARG]; } }

        private static bool Running = true;
        private static ServiceProvider provider;

        static async Task Main(string[] args)
        {
            BuildArguments(args);
            provider = ConfigureServices(new ServiceCollection());

            await Start();
        }

        private static void BuildArguments(string[] args)
        {
            for (int i = 0; i < args.Length; i += 2)
                arguments.Add(args[i], args[i + 1]);
        }

        private static ServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
            services.AddTransient<ConsoleInput>();

            services.AddSingleton(new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            //Domain Services
            services.AddSingleton<IAccountService, AccountService>();

            //Command Services
            services.AddTransient<ILoginHandler, LoginHandler>();

            return services.BuildServiceProvider();
        }

        private static async Task Start()
        {
            Information.PrintTitle();

            await provider.GetService<ILoginHandler>().Login();
            ReadCommands();
        }

        private static void ReadCommands()
        {
            while (Running)
            {
                string command = Console.ReadLine();
                OnCommandReceived(command);
            }
        }

        public static void Close()
        {
            OnClosing();
            Running = false;
        }
    }
}
