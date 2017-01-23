using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WeatherApp.Logic.API
{
    abstract class APIParser
    {
        public string cityName;
        public string countryTag;
        public string humidity;
        public string pressure;
        public string cloudsName;
        public string lastUpdate;
        public string temperatureValue;
        public string windSpeed;
        public string windName;
        public string windDirectionCode;

        private XmlReader reader;

        public abstract void Parse(string link);
    }
}
