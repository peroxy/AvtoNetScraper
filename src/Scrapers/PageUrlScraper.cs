using AvtoNetScraper.Utilities;
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

namespace AvtoNetScraper.Scrapers
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

            if (doc == null)
            {
                //url has been removed.. nothing exists
                return new string[0];
            }

            var currentPageDoc = doc;
            int pageNum = 1;
            do
            {
                Colorful.Console.WriteLine($"Parsing page {pageNum}...", Color.Green);
                
                string currentPageUrl = Url.Replace("&stran=1", $"&stran={pageNum}");
                currentPageDoc = GetHtmlDocument(currentPageUrl);
                
                if (currentPageDoc == null)
                {
                    //url has been removed.. nothing exists
                    return new string[0];
                }

                var currentPageUrls = FindUrlNodes(currentPageDoc);

                Colorful.Console.WriteLine($"Found {currentPageUrls.Count} ads.", Color.Green);

                carUrls.AddRange(currentPageUrls);
                pageNum++;

            }while(!IsLastPage(currentPageDoc));

            Colorful.Console.WriteLine($"Found {carUrls.Count} cars on {pageNum - 1} pages.", Color.Green);


            return carUrls;
        }

        private bool IsLastPage(HtmlDocument doc){
            var nextButton = doc.DocumentNode.Descendants()
                .FirstOrDefault(x => x.Name == "span" && x.InnerText == "Naprej");

            //No 'Next' button probably means that all results are listed on one page.
            if(nextButton == null)
                return true;
            
            //We are on last page when 'next' button is disabled
            return nextButton
                .ParentNode
                .ParentNode
                .HasClass("disabled");
        }

        private IList<string> FindUrlNodes(HtmlDocument document)
        {
            var urlNodes = document.DocumentNode.Descendants().Where(x => x.HasClass("stretched-link"))?.Select(x => x.Attributes["href"]?.Value);
            if (urlNodes == null || urlNodes.Count() == 0)
            {
                return new string[0];
            }

            return urlNodes.Where(x => !string.IsNullOrWhiteSpace(x))
                .Where(x => x.Contains("&display="))
                .Select(x => $"https://www.avto.net{x.GetUntilOrEmpty("&display=").Substring(2)}").ToList();
        }

    }
}
