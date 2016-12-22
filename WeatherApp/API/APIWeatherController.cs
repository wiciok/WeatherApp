using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;

namespace WeatherApp.API
{
    class APIWeatherController : APIController
    {
        public APIWeatherController(string city, string country)
        {
            Initialize(city, country);
        }

        protected override void Initialize(string city, string coutry)
        {
            key = "9382f55d22f029e00303d1134d77dd9f";
            format = "xml";

            link = "http://api.openweathermap.org/data/2.5/weather?q="+city+","+coutry+"&appid="+key+"&mode="+format;
        }

        public override void Parse()
        {
            SingletonApiParser instance = SingletonApiParser.Instance;
            XmlReader reader = XmlReader.Create(link);

            ParseCity(instance, reader);
            ParseCityCoords(instance, reader);
            ParseCountry(instance, reader);
            ParseSun(instance, reader);
            ParseTemperature(instance, reader);
            ParseHumidity(instance, reader);
            ParsePressure(instance, reader);
            ParseWind(instance, reader);
            ParseClouds(instance, reader);
            ParseLastUpdate(instance, reader);
        }

        private void ParseCity(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("city");
            reader.MoveToFirstAttribute();
            instance.Parser.cityId = reader.Value;
            reader.MoveToNextAttribute();
            instance.Parser.cityName = reader.Value;
            reader.MoveToNextAttribute();
        }

        private void ParseCityCoords(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("coord");
            reader.MoveToFirstAttribute();
            instance.Parser.cityCoordX = reader.Value;
            reader.MoveToNextAttribute();
            instance.Parser.cityCoordY = reader.Value;
        }

        private void ParseCountry(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("country");
            reader.MoveToFirstAttribute();
            instance.Parser.countryTag = reader.Value;
        }

        private void ParseSun(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("sun");
            reader.MoveToFirstAttribute();
            instance.Parser.sunrise = reader.Value;
            reader.MoveToNextAttribute();
            instance.Parser.sunset = reader.Value;
        }

        private void ParseTemperature(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("temperature");
            reader.MoveToFirstAttribute();
            instance.Parser.temperatureValue = reader.Value;
            reader.MoveToNextAttribute();
            reader.MoveToNextAttribute();
            reader.MoveToNextAttribute();
            instance.Parser.unitName = reader.Value;
        }

        private void ParseHumidity(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("humidity");
            reader.MoveToFirstAttribute();
            instance.Parser.humidity = reader.Value;
        }

        private void ParsePressure(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("pressure");
            reader.MoveToFirstAttribute();
            instance.Parser.pressure = reader.Value;
        }

        private void ParseWind(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("speed");
            reader.MoveToFirstAttribute();
            instance.Parser.windSpeed = reader.Value;
            reader.MoveToNextAttribute();
            instance.Parser.windName = reader.Value;

            reader.ReadToFollowing("direction");
            reader.MoveToFirstAttribute();
            instance.Parser.windDirection = reader.Value;
            reader.MoveToNextAttribute();
            instance.Parser.windDirectionCode = reader.Value;
            reader.MoveToNextAttribute();
            instance.Parser.windDirectionName = reader.Value;
        }

        private void ParseClouds(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("clouds");
            reader.MoveToFirstAttribute();
            reader.MoveToNextAttribute();
            instance.Parser.cloudsName = reader.Value;
        }

        private void ParseLastUpdate(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("lastupdate");
            reader.MoveToFirstAttribute();
            instance.Parser.lastUpdate = reader.Value;
        }
    }
}
