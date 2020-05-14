using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AvtoNetScraper
{
    internal class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
