using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvtoNetScraper.Database
{
    public class CarsContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=cars.db");
    }

    public class Url
    {
        public int Id { get; set; }
        public string Address { get; set; }
    }

}
