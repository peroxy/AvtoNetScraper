using AvtoNetScraper.Database;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AvtoNetScraper.Scrapers
{
    public class CarScraper : Scraper
    {
        private Url _url;

        public CarScraper(Url url, TimeSpan requestInterval) : base(url.Address, requestInterval)
        {
            _url = url;
        }

        /// <summary>
        /// Scrapes the specified URL and creates a car entity ready for database.
        /// </summary>
        /// <returns></returns>
        public Car ScrapeCarInformation()
        {
            var document = GetHtmlDocument(Url);

            if (document == null)
            {
                //ad has been removed;
                return null;
            }

            var car = new Car
            {
                UrlId = _url.Id
            };

            var priceText = document.GetElementbyId("CenaFinanciranja")?.InnerText;
            if (decimal.TryParse(priceText, NumberStyles.Currency, new CultureInfo("de-DE"), out var price))
            {
                car.Price = price;
            }

            var photoUrl = document.DocumentNode.Descendants().FirstOrDefault(x => x.HasClass("OglasPhoto"))?.Attributes["data-src"]?.Value;
            car.PictureUrl = photoUrl;

            var attributeNodes = document.DocumentNode.Descendants().Where(x => x.HasClass("OglasData"));
            foreach (var oglasData in attributeNodes)
            {
                var oglasDataNodes = oglasData.Descendants();
                var attributeName = oglasDataNodes.FirstOrDefault(x => x.HasClass("OglasDataLeft"));
                var attributeValue = oglasDataNodes.FirstOrDefault(x => x.HasClass("OglasDataRight"));

                if (attributeName == null || attributeValue == null)
                {
                    continue;
                }

                SetCarAttribute(car, attributeName.InnerText, attributeValue.InnerText);
            }

            return car;

        }

        private void SetCarAttribute(Car car, string name, string value)
        {
            name = name.Trim();
            
            //cleanup html garbage
            value = value.Trim().Replace("&nbsp;", " ").Replace("\t", "").Replace(Environment.NewLine, "");
            switch (name)
            {
                case "Starost:":
                    car.Age = value;
                    break;
                case "Prva registracija:":
                    car.FirstRegistration = GetDateTime(value);
                    break;
                case "Leto proizvodnje:":
                    car.ProductionYear = GetInteger(value);
                    break;
                case "Prevoženi km:":
                    car.MileageInKm = GetInteger(value);
                    break;
                case "Tehnični pregled:":
                    car.TechnicalInspectionValidUntil = GetDateTime(value);
                    break;
                case "Motor:":
                    car.Engine = value;
                    break;
                case "Vrsta goriva:":
                    car.EngineType = value;
                    break;
                case "Menjalnik:":
                    car.Transmission = value;
                    break;
                case "Oblika karoserije:":
                    car.BodyShape = value;
                    break;
                case "Število vrat:":
                    car.DoorNumber = GetInteger(value.Substring(0, 1));
                    break;
                case "Barva:":
                    car.Color = value;
                    break;
                case "Notranjost:":
                    car.Interior = value;
                    break;
                case "VIN / številka šasije:":
                    car.ChassisNumber = value;
                    break;
                case "Kraj ogleda:":
                    car.AdLocation = value;
                    break;
                case "Kombinirana vožnja:":
                    car.CombinedConsumption = value;
                    break;
                case "Izvenmestna vožnja:":
                    car.OutOfTownConsumption = value;
                    break;
                case "Mestna vožnja:":
                    car.CityConsumption = value;
                    break;
                case "Emisijski razred:":
                    car.EmissionClass = value;
                    break;
                case "Emisija CO2:":
                    car.CO2Emissions = value;
                    break;
                case "Interna številka:":
                    car.InternalNumber = value;
                    break;
                case "Status zaloge:":
                    car.StockStatus = value;
                    break;
                default:
                    break;
            }
        }

        private DateTime? GetDateTime(string value)
        {
            if (DateTime.TryParseExact(value, new[] { "M / yyyy", "M/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
            {
                return result;
            }
            
            return null;
        }

        private int? GetInteger(string value)
        {
            if (int.TryParse(value, out var result))
            {
                return result;
            }

            return null;
        }

    }
}
