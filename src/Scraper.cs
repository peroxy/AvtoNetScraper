using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;
using System.Threading;

namespace AvtoNetScraper
{
    public abstract class Scraper
    {
        protected string Url;
        private TimeSpan _requestInterval;

        public Scraper(string url, TimeSpan requestInterval)
        {
            Url = url;
            _requestInterval = requestInterval;
        }

        /// <summary>
        /// Downloads HTML as string via WebClient and loads it up into HtmlAgilityPack's document.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected HtmlDocument GetHtmlDocument(string url)
        {
            var interval = GetRandomRequestInterval();
            Colorful.Console.WriteLine($"Sleeping for {interval.TotalMilliseconds} ms...", Color.Red);
            Thread.Sleep(interval);

            using var client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");
            client.Encoding = Encoding.UTF8;

            Colorful.Console.WriteLine($"Downloading {url}...", Color.Orange);
            var html = client.DownloadString(url);
            var document = new HtmlDocument();
            document.LoadHtml(html);
            return document;
        }

        /// <summary>
        /// Returns a request interval that can vary from config value +- 10%. Try to avoid looking like a robot.
        /// </summary>
        /// <returns></returns>
        protected TimeSpan GetRandomRequestInterval()
        {
            var random = new Random();
            var tenPercent = (int)(0.1 * _requestInterval.TotalMilliseconds);
            return _requestInterval.Add(TimeSpan.FromMilliseconds(random.Next(-tenPercent, tenPercent)));
        }
    }
}
