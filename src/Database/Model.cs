using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvtoNetScraper.Database
{
    public class CarsContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }

        public DbSet<Car> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=Database\\cars.db");
    }

    public class Url
    {
        public int Id { get; set; }
        public string Address { get; set; }
    }

    public class Car
    {
        /// <summary>
        /// PK
        /// </summary>
        public int Id { get; set; }

        public int UrlId { get; set; }
        public string Age { get; set; }
        public DateTime? FirstRegistration { get; set; }
        public DateTime? TechnicalInspectionValidUntil { get; set; }
        public int? ProductionYear { get; set; }
        public int? MileageInKm { get; set; }
        public string Engine { get; set; }
        public string EngineType { get; set; }
        public decimal? Price { get; set; }
        public string Transmission { get; set; }
        public string BodyShape { get; set; }
        public int? DoorNumber { get; set; }
        public string Color { get; set; }
        public string Interior { get; set; }
        public string ChassisNumber { get; set; }
        public string AdLocation { get; set; }
        public string CombinedConsumption { get; set; }
        public string OutOfTownConsumption { get; set; }
        public string CityConsumption { get; set; }
        public string EmissionClass { get; set; }
        public string CO2Emissions { get; set; }
        public string InternalNumber { get; set; }
        public string StockStatus { get; set; }
        public string PictureUrl { get; set; }
        public string LocalPicturePath { get; set; }
    }

}
