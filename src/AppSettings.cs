using System;
using System.Collections.Generic;
using System.Text;

namespace AvtoNetScraper
{
    public class AppSettings
    {
        public string[] SearchFilterUrls { get; set; }
        public long RequestIntervalMs { get; set; }
    }
}
