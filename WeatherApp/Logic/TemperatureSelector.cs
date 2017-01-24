using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.API;

namespace WeatherApp
{
    public enum TemperatureUnits
    {
        Kelvin = 0, Celsius = 1, Fahrenheit = 2
    }

    static class TemperatureSelector
    {
        public static string GetTemperatureValue()
        {
            switch (Settings.tempUnit)
            {
                case TemperatureUnits.Kelvin:
                    return SingletonApiParser.Instance.Parser.TemperatureValue;
                    break;

                case TemperatureUnits.Celsius:
                    return ConvertFromKelvinToCelsius(SingletonApiParser.Instance.Parser.TemperatureValue);
                    break;
                default:
                    return "";
            }
        }

        public static string GetTemperatureUnit()
        {
            switch (Settings.tempUnit)
            {
                case TemperatureUnits.Kelvin:
                    return "K";
                    break;

                case TemperatureUnits.Celsius:
                    return "˚C";
                    break;
                default:
                    return "";
            }
        }

        private static string ConvertFromKelvinToCelsius(string temp)
        {
            var kelvTemp = (float) Convert.ToDouble(temp, CultureInfo.InvariantCulture.NumberFormat);
            var celsTemp = kelvTemp - 272.15;
            
            string tmp= celsTemp.ToString("0.00");

            return tmp;
        }

    }
}
