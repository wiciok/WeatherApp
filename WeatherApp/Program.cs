using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Database;

namespace WeatherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SingletonDatabaseController.Instance.DbController.Insert("INSERT INTO TEMPERATURES (TEMPERATURE_VALUE, UNIT_ID) VALUES (1, 4);");

            Console.ReadLine();
        }
    }
}
