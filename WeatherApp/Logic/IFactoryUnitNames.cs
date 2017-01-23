using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.API
{
    interface IFactoryUnitNames
    {
        string GetHumidityUnitLabelContent();
        string GetPressureUnitLabelContent();
        string GetWindSpeedUnitLabelContent();
    }
}
