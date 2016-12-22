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
            //SingletonDatabaseController.Instance.DbController.Insert("INSERT INTO TEMPERATURES (TEMPERATURE_VALUE, UNIT_ID) VALUES (1, 4);");
            APIController api = new APIWeatherController("Kwidzyn", "PL");
            api.Parse();

            Console.WriteLine(SingletonApiParser.Instance.Parser.cityId);
            Console.WriteLine(SingletonApiParser.Instance.Parser.cityName);
            Console.WriteLine(SingletonApiParser.Instance.Parser.cityCoordX);
            Console.WriteLine(SingletonApiParser.Instance.Parser.cityCoordY);
            Console.WriteLine(SingletonApiParser.Instance.Parser.countryTag);
            Console.WriteLine(SingletonApiParser.Instance.Parser.sunrise);
            Console.WriteLine(SingletonApiParser.Instance.Parser.sunset);
            Console.WriteLine(SingletonApiParser.Instance.Parser.temperatureValue);
            Console.WriteLine(SingletonApiParser.Instance.Parser.unitName);
            Console.WriteLine(SingletonApiParser.Instance.Parser.humidity);
            Console.WriteLine(SingletonApiParser.Instance.Parser.pressure);
            Console.WriteLine(SingletonApiParser.Instance.Parser.windSpeed);
            Console.WriteLine(SingletonApiParser.Instance.Parser.windName);
            Console.WriteLine(SingletonApiParser.Instance.Parser.windDirection);
            Console.WriteLine(SingletonApiParser.Instance.Parser.windDirectionCode);
            Console.WriteLine(SingletonApiParser.Instance.Parser.windDirectionName);
            Console.WriteLine(SingletonApiParser.Instance.Parser.cloudsName);
            Console.WriteLine(SingletonApiParser.Instance.Parser.lastUpdate);

            Console.ReadLine();
        }
    }
}
