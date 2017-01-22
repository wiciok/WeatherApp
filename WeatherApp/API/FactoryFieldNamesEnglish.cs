using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace WeatherApp.API
{
    class FactoryFieldNamesEnglish : IFactoryFieldNames
    {
        public string GetTemperatureLabelContent() { return "Temperature"; }
        public string GetCloudsLabelContent(){return "Clouds"; }
        public string GetHumidityLabelContent() { return "Humidity";}
        public string GetPressureLabelContent() {return "Pressure";}
        public string GetWindLabelContent() { return "Wind";}
        public string GetWindspeedLabelContent() {return "Wind speed";}
        public string GetLastUpdateLabelContent() {return "Last Update";}

        public string GetCityLabelContent() {return "City";}
        public string GetCountryLabelContent() {return "Country";}

        public string GetCheckweatherButtonContent() {return "Check Weather!";}
        public string GetCityInputTextBoxText() { return "Type city name";}
        public string GetCountryInputTextBoxText() {return "Type country name"; }

    }
}
