using AlchemistOnline.ConsoleApp.Commands;
using AlchemistOnline.ConsoleApp.Events;
using AlchemistOnline.ConsoleApp.Services.Accounts;
using AlchemistOnline.ConsoleApp.Util;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace AlchemistOnline.ConsoleApp
{
    public class Program
    {
        #region args
        private const string API_ADDRESS_ARG = "-apiadr";
        private static readonly Dictionary<string, string> arguments = new Dictionary<string, string>();
        #endregion

        public static string ApiAddress { get { return arguments[API_ADDRESS_ARG]; } }

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
            services.AddTransient<ConsoleOutput>();

            services.AddSingleton(new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            //Domain Services
            services.AddSingleton<IAccountService, AccountService>();

            //Command Services
            services.AddTransient<LoginHandler>();
            services.AddTransient<MainMenuHandler>();

            return services.BuildServiceProvider();
        }

        private static async Task Start()
        {
            Information.PrintTitle(provider.GetService<ConsoleOutput>());

            await provider.GetService<LoginHandler>().Login();
            provider.GetService<MainMenuHandler>().DisplayMenu();
        }
    }
}
