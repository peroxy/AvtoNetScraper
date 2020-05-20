using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;

namespace AvtoNetScraper.Scrapers
{
    public class ImageScraper : Scraper
    {
        public ImageScraper(string url, TimeSpan requestInterval) : base(url, requestInterval)
        {
        }

        public string DownloadImage(string directory)
        {
            var path = Path.Combine(directory, $"{DateTime.UtcNow.Ticks}.jpg");

            var client = PrepareClient(Url);
            try
            {
                client.DownloadFile(Url, path);
                return path;
            }
            catch (WebException)
            {
                // probably 404 or image has been deleted or similar
                return null;
            }
        }
    }
}
