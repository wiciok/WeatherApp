using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.API;

namespace WeatherApp.Logic
{
    class FactoryFieldNamesPolish : IFactoryFieldNames
    {
        public string GetTemperatureLabelContent() { return "Temperatura"; }
        public string GetCloudsLabelContent() { return "Zachmurzenie"; }
        public string GetHumidityLabelContent() { return "Wilgotność"; }
        public string GetPressureLabelContent() { return "Ciśnienie"; }
        public string GetWindLabelContent() { return "Wiatr"; }
        public string GetWindspeedLabelContent() { return "Prędkość wiatru"; }
        public string GetLastUpdateLabelContent() { return "Ostatnia zmiana"; }

        public string GetCityLabelContent() { return "Miasto"; }
        public string GetCountryLabelContent() { return "Kraj"; }

        public string GetCheckweatherButtonContent() { return "Sprawdź pogodę!"; }
        public string GetCityInputTextBoxText() { return "Wpisz nazwę miasta"; }
        public string GetCountryInputTextBoxText() { return "Wpisz nazwę kraju"; }

    }
}
