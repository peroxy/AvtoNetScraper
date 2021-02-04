using AvtoNetScraper.Database;
using System;
using System.Globalization;
using System.Linq;
using System.Drawing;
using System.Text.RegularExpressions;

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
            
            var priceNode = document.DocumentNode.SelectSingleNode("//comment()[contains(., '-- PRICE --')]/following-sibling::div");
            var priceText =  priceNode.InnerText.Trim();

            var price = ReadPrice(priceText);
            if(price > 0)
            {
                car.Price = price;
            }
            
            var photoUrl = document.GetElementbyId("BigPhoto")?.Attributes["src"]?.Value;
            car.PictureUrl = photoUrl;

            var dataNode = document.DocumentNode.SelectSingleNode("//comment()[contains(., '-- DATA --')]/following-sibling::table");
            var attributeNodes = document.DocumentNode.Descendants().Where(x => x.Name == "tr");
            foreach (var oglasData in attributeNodes)
            {
                var oglasDataNodes = oglasData.ChildNodes;
                var attributeName = oglasDataNodes.FirstOrDefault(x => x.Name == "th");
                var attributeValue = oglasDataNodes.FirstOrDefault(x => x.Name == "td");

                if (attributeName == null || attributeValue == null)
                {
                    continue;
                }

                SetCarAttribute(car, attributeName.InnerText, attributeValue.InnerText);
            }

            return car;

        }

        private decimal ReadPrice(string priceText)
        {
            var priceRegex = new Regex(@"\d+\.?\d* €");
            var prices = priceRegex.Matches(priceText);

            try
            {
                var parsed = prices.Select(x => decimal.Parse(x.Value, NumberStyles.Currency, new CultureInfo("de-DE")));

                if (parsed.Count() == 1)
                {
                    return parsed.First();
                }
                else if(prices.Count == 2)
                {
                    if(priceText.Contains("export", StringComparison.CurrentCultureIgnoreCase))
                        return parsed.Max();
                    
                    if(priceText.Contains("akcijska cena", StringComparison.CurrentCultureIgnoreCase))
                        return parsed.Min();
                }
                else
                {
                   throw new Exception("Unknown price text");
                }
            }
            catch(Exception)
            {
                Colorful.Console.WriteLine($"Unable to read price: {priceText}",Color.Red);
            }

            return 0;
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
                case "Gorivo:":
                    car.EngineType = value;
                    break;
                case "Menjalnik:":
                    car.Transmission = value;
                    break;
                case "Oblika:":
                    car.BodyShape = value;
                    break;
                case "Št.vrat:":
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
            if (DateTime.TryParseExact(value.Trim(), new[] { "M / yyyy", "yyyy / M", "M/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
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
