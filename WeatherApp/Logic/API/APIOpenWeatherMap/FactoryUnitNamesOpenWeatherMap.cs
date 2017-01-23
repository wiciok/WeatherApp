using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.API
{
    class FactoryUnitNamesOpenWeatherMap: IFactoryUnitNames
    {
        public string GetHumidityUnitLabelContent()
        {
            return "%";
        }

        public string GetPressureUnitLabelContent()
        {
            return "hPa";
        }

        public string GetWindSpeedUnitLabelContent()
        {
            return "m/s";
        }
    }
}
