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
            DatabaseController dbController = new WeatherDatabaseController();

            dbController.Insert("INSERT INTO TEMPERATURES (TEMPERATURE_VALUE, UNIT_ID) VALUES (1, 999);");

            foreach (List<string> list in dbController.Select("SELECT * FROM TEMPERATURES;"))
            {
                Console.WriteLine(list[0]);
            }

            dbController.Delete("DELETE FROM TEMPERATURES WHERE TEMPERATURE_VALUE=1");

            Console.ReadLine();
        }
    }
}
