using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.API
{
    interface IFactoryFieldNames
    {
        string GetTemperatureLabelContent();
        string GetCloudsLabelContent();
        string GetHumidityLabelContent();
        string GetPressureLabelContent();
        string GetWindLabelContent();
        string GetWindspeedLabelContent();
        string GetLastUpdateLabelContent();

        string GetCityLabelContent();
        string GetCountryLabelContent();

        string GetCheckweatherButtonContent();

        string GetCityInputTextBoxText();
        string GetCountryInputTextBoxText();

    }
}
