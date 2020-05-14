using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AvtoNetScraper
{
    internal class Program
    {
        private static IConfiguration Configuration => new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

        private static void Main(string[] args)
        {
            var settings = Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            foreach (var url in settings.SearchFilterUrls)
            {
                var scraper = new PageUrlScraper(url, TimeSpan.FromMilliseconds(settings.RequestIntervalMs));
                var urls = scraper.GetCarUrls();


            }
        }
    }
}
