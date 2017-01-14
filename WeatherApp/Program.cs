using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Database;
using WeatherApp.API;

namespace WeatherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //SingletonDatabaseController.Instance.DbController.Insert("INSERT INTO TEMPERATURE (TEMPERATURE_VALUE, UNIT_ID) VALUES (1, 4);");
            try
            {
                string city = Console.ReadLine();
                string country = Console.ReadLine();

                APIController api = new APIWeatherController(city, country);
                api.Parse();
                api.Insert();


                Console.WriteLine(SingletonApiParser.Instance.Parser.CityId);
                Console.WriteLine(SingletonApiParser.Instance.Parser.CityName);
                Console.WriteLine(SingletonApiParser.Instance.Parser.CityCoordX);
                Console.WriteLine(SingletonApiParser.Instance.Parser.CityCoordY);
                Console.WriteLine(SingletonApiParser.Instance.Parser.CountryTag);
                Console.WriteLine(SingletonApiParser.Instance.Parser.Sunrise);
                Console.WriteLine(SingletonApiParser.Instance.Parser.Sunset);
                Console.WriteLine(SingletonApiParser.Instance.Parser.TemperatureValue);
                Console.WriteLine(SingletonApiParser.Instance.Parser.UnitName);
                Console.WriteLine(SingletonApiParser.Instance.Parser.Humidity);
                Console.WriteLine(SingletonApiParser.Instance.Parser.Pressure);
                Console.WriteLine(SingletonApiParser.Instance.Parser.WindSpeed);
                Console.WriteLine(SingletonApiParser.Instance.Parser.WindName);
                Console.WriteLine(SingletonApiParser.Instance.Parser.WindDirection);
                Console.WriteLine(SingletonApiParser.Instance.Parser.WindDirectionCode);
                Console.WriteLine(SingletonApiParser.Instance.Parser.WindDirectionName);
                Console.WriteLine(SingletonApiParser.Instance.Parser.CloudsName);
                Console.WriteLine(SingletonApiParser.Instance.Parser.LastUpdate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
