using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AvtoNetScraper
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
    }
}
