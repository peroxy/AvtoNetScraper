using AvtoNetScraper.Database;
using AvtoNetScraper.Scrapers;
using AvtoNetScraper.Settings;
using AvtoNetScraper.Utilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace AvtoNetScraper
{
    internal class Program
    {
        private static IConfiguration Configuration => new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("Settings\\appsettings.json", optional: false)
    .Build();

        private static CarsDbHelper _dbHelper = new CarsDbHelper();
        private static void Main(string[] args)
        {
            var settings = Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            
            if (args.Length > 2)
            {
                Colorful.Console.WriteLine("Invalid arguments supplied, only -urls and -cars allowed.", Color.Red);
                return;
            }

            // registers windows-1250 encoding 
            EncodingProvider provider = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);

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
                
                Colorful.Console.WriteLine($"Merging them into database...", Color.LightSkyBlue);
                _dbHelper.MergeUrls(urls);
            }
        }

        private static void ScrapeCarInfo(AppSettings settings)
        {

            var nonScrapedUrls = _dbHelper.GetNonScrapedUrls();

            //TODO: remove after getting all attributes for model, we just use this to get a lot of random attributes fast
            nonScrapedUrls.Shuffle(new Random());

            Colorful.Console.WriteLine($"Downloading non-scraped car data for {nonScrapedUrls.Count} remaining urls.", Color.Orange);

            var interval = TimeSpan.FromMilliseconds(settings.RequestIntervalMs);
            var allAttributes = new HashSet<string>();
            foreach (var url in nonScrapedUrls.Take(1000))
            {
                var scraper = new CarScraper(url.Address, interval);
                var attributes = scraper.GetCarAttributes();
                allAttributes.UnionWith(attributes);
            }

            File.WriteAllText("attributes.txt", string.Join(Environment.NewLine, allAttributes));
        }
    }
}
