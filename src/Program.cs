using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

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
            
            if (args.Length > 2)
            {
                Colorful.Console.WriteLine("Invalid arguments supplied, only -urls and -cars allowed.", Color.Red);
                return;
            }
            if (args.Length == 0)
            {
                Colorful.Console.WriteLine("Default configuration will be used, scraping car urls first and then car info.", Color.Yellow);
                ScrapeCarUrls(settings);
                ScrapeCarInfo(settings);
            }
            
            if (args.Contains("-urls"))
            {
                Colorful.Console.WriteLine("-urls option chosen, will scrape car urls from appsettings url values.", Color.Yellow);
                ScrapeCarUrls(settings);
            }
            if (args.Contains("-cars"))
            {
                Colorful.Console.WriteLine("-cars option chosen, will scrape car info for all urls in database.", Color.Yellow);
                ScrapeCarInfo(settings);
            }
        }

        private static void ScrapeCarUrls(AppSettings settings)
        {
            foreach (var url in settings.SearchFilterUrls)
            {
                var scraper = new PageUrlScraper(url, TimeSpan.FromMilliseconds(settings.RequestIntervalMs));
                var urls = scraper.GetCarUrls();
                Colorful.Console.WriteLine($"Finished scraping car urls, got {urls.Count} urls.", Color.GreenYellow);
                var helper = new CarsDbHelper();
                Colorful.Console.WriteLine($"Merging them into database...", Color.LightSkyBlue);
                helper.MergeUrls(urls);
            }
        }

        private static void ScrapeCarInfo(AppSettings settings)
        {
            //todo: read from db and scrape
            return;
        }
    }
}
