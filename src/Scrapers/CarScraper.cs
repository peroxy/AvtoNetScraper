using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AvtoNetScraper.Scrapers
{
    public class CarScraper : Scraper
    {
        public CarScraper(string url, TimeSpan requestInterval) : base(url, requestInterval)
        {
        }

        /// <summary>
        /// TEMP so we can know how the model will look like
        /// </summary>
        /// <returns></returns>
        public HashSet<string> GetCarAttributes()
        {
            var document = GetHtmlDocument(Url);

            if (document == null)
            {
                //ad has been removed;
                return new HashSet<string>();
            }

            var attributes = document.DocumentNode.Descendants().Where(x => x.HasClass("OglasDataLeft")).Select(x => x.InnerText).ToHashSet();

            return attributes;
        }
    }
}
