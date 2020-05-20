using System;
using System.Collections.Generic;
using System.Text;

namespace AvtoNetScraper.Settings
{
    public class AppSettings
    {
        public string[] SearchFilterUrls { get; set; }
        public long RequestIntervalMs { get; set; }
        public string ImagesDirectory { get; set; }
    }
}
