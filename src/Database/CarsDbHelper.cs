using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AvtoNetScraper.Database
{
    public class CarsDbHelper
    {

        /// <summary>
        /// Inserts only missing urls into database.
        /// </summary>
        /// <param name="urls"></param>
        public void MergeUrls(IList<string> urls)
        {
            using (var db = new CarsContext())
            {
                var dbUrls = db.Urls.Select(x => x.Address);
                var missingUrls = urls.Except(dbUrls).Select(x => new Url { Address = x });
                db.Urls.AddRange(missingUrls);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Returns all urls that have not been yet scraped and inserted into cars table.
        /// </summary>
        /// <returns></returns>
        public IList<Url> GetNonScrapedUrls()
        {
            using (var db = new CarsContext())
            {
                var alreadyScrapedUrls = new HashSet<int>(db.Cars.Select(x => x.Url.Id));
                var nonScrapedUrls = db.Urls.Where(x => !alreadyScrapedUrls.Contains(x.Id)).ToList();
                return nonScrapedUrls;
            }
        }
    }
}
