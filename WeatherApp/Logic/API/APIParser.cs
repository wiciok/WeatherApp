using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WeatherApp.Logic.API
{
    //shows what parser elements are necessary due to being used in GUI parts of application
    abstract class APIParser
    {
        public abstract string CityName { get; set; }
        public abstract string CountryTag { get; set; }
        public abstract string Humidity { get; set; }
        public abstract string Pressure { get; set; }
        public abstract string CloudsName { get; set; }
        public abstract string LastUpdate { get; set; }
        public abstract string TemperatureValue { get; set; }
        public abstract string WindSpeed { get; set; }
        public abstract string WindName { get; set; }
        public abstract string WindDirectionCode { get; set; }

        private XmlReader reader;

        public abstract void Parse(string link);
    }
}
