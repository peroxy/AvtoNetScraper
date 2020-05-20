using System;
using System.Collections.Generic;
using System.IO;
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
                var alreadyScrapedUrls = new HashSet<int>(db.Cars.Select(x => x.UrlId));
                var nonScrapedUrls = db.Urls.Where(x => !alreadyScrapedUrls.Contains(x.Id)).ToList();
                return nonScrapedUrls;
            }
        }

        public void InsertCars(IList<Car> cars)
        {
            using (var db = new CarsContext())
            {
                db.Cars.AddRange(cars);
                db.SaveChanges();
            }
        }

        public IList<Car> GetCarsWithoutDownloadedImage()
        {
            using (var db = new CarsContext())
            {
                var carsWithoutImages = db.Cars.Where(x => x.LocalPicturePath == null && x.PictureUrl != null).ToList();
                var carsWithImages = db.Cars.Where(x => x.LocalPicturePath != null && x.PictureUrl != null).ToList();

                // check if local files might have been deleted or moved, download for those again
                foreach (var car in carsWithImages)
                {
                    if (!File.Exists(car.LocalPicturePath))
                    {
                        carsWithoutImages.Add(car);
                    }
                }
                
                return carsWithoutImages;
            }
        }

        public void UpdateCarImagePath(Car car, string path)
        {
            using (var db = new CarsContext())
            {
                car.LocalPicturePath = path;
                db.Attach(car);
                db.Entry(car).Property(x => x.LocalPicturePath).IsModified = true;
                db.SaveChanges();
            }

        }
    }
}
