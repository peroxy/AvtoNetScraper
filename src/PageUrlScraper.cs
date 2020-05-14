using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace AvtoNetScraper
{
    /// <summary>
    /// Scrapes all car urls from all pages
    /// </summary>
    public class PageUrlScraper : Scraper
    {
        public PageUrlScraper(string url, TimeSpan requestInterval) : base(url, requestInterval)
        {
        }

        /// <summary>
        /// Scrapes initial URL and all following pages and builds a list of urls for all cars found.
        /// </summary>
        public IList<string> GetCarUrls()
        {
            var carUrls = new List<string>();
            //initial page download
            var doc = GetHtmlDocument(Url);

            //try to find "Zadetki 433 - 480 od skupno 486" text
            var resultsNode = doc.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("ResultsSelectedCriteriaLeft"));
            if (resultsNode == null)
            {
                // couldn't find the text and can't proceed, we don't know how many pages to crawl
                return new string[0];
            }

            var resultsText = resultsNode.InnerText.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
            if (!int.TryParse(resultsText, out int resultsCount))
            {
                // couldn't find number of results on page
                return new string[0];
            }

            // each avto.net page has max 48 results
            int endPage = (int)Math.Ceiling((double)resultsCount / 48);

            Colorful.Console.WriteLine($"Found {resultsCount} cars on {endPage} pages. Crawling all pages to find all car urls now...", Color.Green);

            var initialPageUrls = FindUrlNodes(doc);
            carUrls.AddRange(initialPageUrls);

            for (int i = 2; i <= endPage; i++)
            {
                string currentPageUrl = Url.Replace("&stran=1", $"&stran={i}");
                var currentPageDoc = GetHtmlDocument(currentPageUrl);
                var currentPageUrls = FindUrlNodes(currentPageDoc);
                carUrls.AddRange(currentPageUrls);
            }

            return carUrls;
        }

        private IList<string> FindUrlNodes(HtmlDocument document)
        {
            var urlNodes = document.DocumentNode.Descendants().Where(x => x.HasClass("Adlink"))?.Select(x => x.Attributes["href"]?.Value);
            if (urlNodes == null || urlNodes.Count() == 0)
            {
                return new string[0];
            }

            return urlNodes.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => $"https://www.avto.net{x.GetUntilOrEmpty("&display=").Substring(2)}").ToArray();
        }

    }
}
