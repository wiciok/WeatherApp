using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace WeatherApp.API
{
    class APIParser
    {
        private string cityId;
        private string cityName;
        private string cityCoordX;
        private string cityCoordY;
        private string countryTag;
        private string sunrise;
        private string sunset;
        private string humidity;
        private string pressure;
        private string cloudsName;
        private string lastUpdate;
        private string temperatureValue;
        private string unitName;
        private string windSpeed;
        private string windName;
        private string windDirection;
        private string windDirectionCode;
        private string windDirectionName;



        private XmlReader reader;

        public string CityId
        {
            get { return cityId; }
            set { cityId = value; }
        }

        public string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }

        public string CityCoordX
        {
            get { return cityCoordX; }
            set { cityCoordX = value; }
        }

        public string CityCoordY
        {
            get { return cityCoordY; }
            set { cityCoordY = value; }
        }

        public string CountryTag
        {
            get { return countryTag; }
            set { countryTag = value; }
        }

        public string Sunrise
        {
            get { return sunrise; }
            set { sunrise = value; }
        }

        public string Sunset
        {
            get { return sunset; }
            set { sunset = value; }
        }

        public string Humidity
        {
            get { return humidity; }
            set { humidity = value; }
        }

        public string Pressure
        {
            get { return pressure; }
            set { pressure = value; }
        }

        public string CloudsName
        {
            get { return cloudsName; }
            set { cloudsName = value; }
        }

        public string LastUpdate
        {
            get { return lastUpdate; }
            set { lastUpdate = value; }
        }

        public string TemperatureValue
        {
            get { return temperatureValue; }
            set { temperatureValue = value; }
        }

        public string UnitName
        {
            get { return unitName; }
            set { unitName = value; }
        }

        public string WindSpeed
        {
            get { return windSpeed; }
            set { windSpeed = value; }
        }

        public string WindName
        {
            get { return windName; }
            set { windName = value; }
        }

        public string WindDirection
        {
            get { return windDirection; }
            set { windDirection = value; }
        }

        public string WindDirectionCode
        {
            get { return windDirectionCode; }
            set { windDirectionCode = value; }
        }

        public string WindDirectionName
        {
            get { return windDirectionName; }
            set { windDirectionName = value; }
        }

        private void ParsingAdapter(string xmlRecord, ref string field1)
        {
            var cityArr = new[] { field1 };
            ParserTemplate doubleParser = new SingleParser();
            doubleParser.Parse(reader, xmlRecord, ref cityArr);
            field1 = cityArr[0];
        }
        private void ParsingAdapter(string xmlRecord, ref string field1, ref string field2)
        {
            var cityArr = new[] { field1, field2 };
            ParserTemplate doubleParser = new MultipleParser();
            doubleParser.Parse(reader, xmlRecord, ref cityArr);
            field1 = cityArr[0];
            field2 = cityArr[1];
        }
        private void ParsingAdapter(string xmlRecord, ref string field1, ref string field2, ref string field3)
        {
            var cityArr = new[] { field1, field2, field3 };
            ParserTemplate doubleParser = new MultipleParser();
            doubleParser.Parse(reader, xmlRecord, ref cityArr);
            field1 = cityArr[0];
            field2 = cityArr[1];
            field3 = cityArr[2];
        }

        public void Parse(string link)
        {
            reader = XmlReader.Create(link);

            ParsingAdapter("city", ref cityId, ref cityName);
            ParsingAdapter("coord", ref cityCoordY, ref cityCoordX);
            ParsingAdapter("sun", ref sunrise, ref sunset);

            //itd. jak wyżej, plus dla temperature i cloud unikalne metody
            new TemperatureParser().Parse(reader, "temperature", TemperatureValue, UnitName);

            singleParser.Parse(reader, "humidity", Humidity);
            singleParser.Parse(reader, "pressure", Pressure);
            doubleParser.Parse(reader, "speed", WindSpeed, WindName);
            doubleParser.Parse(reader, "direction", WindDirection, WindDirectionCode, WindDirectionName);
            new CloudParser().Parse(reader, "clouds", CloudsName);
            singleParser.Parse(reader, "lastupdate", LastUpdate);
        }


        private abstract class ParserTemplate
        {
            protected void StartParsing(XmlReader reader, string xmlRecord, ref string field)
            {
                reader.ReadToFollowing(xmlRecord);
                reader.MoveToFirstAttribute();
                field = reader.Value.ToUpper();
            }

            protected void ParseNext(XmlReader reader, string xmlRecord, ref string field)
            {
                reader.MoveToNextAttribute();
                field = reader.Value.ToUpper();
            }

            public abstract void Parse(XmlReader reader, string xmlRecord, ref string[] field);
        }


        private sealed class SingleParser : ParserTemplate
        {
            public override void Parse(XmlReader reader, string xmlRecord, ref string[] field)
            {
                this.StartParsing(reader, xmlRecord, ref field[0]);
            }
        }

        private sealed class MultipleParser : ParserTemplate
        {
            public override void Parse(XmlReader reader, string xmlRecord, ref string[] field)
            {
                this.StartParsing(reader, xmlRecord, ref field[0]);
                for (var i = 1; i < field.Length; i++)
                    this.ParseNext(reader, xmlRecord, ref field[i]);
            }
        }

        private sealed class TemperatureParser : ParserTemplate
        {
            public override void Parse(XmlReader reader, string xmlRecord, ref string[] field)
            {
                this.StartParsing(reader, xmlRecord, ref field[0]);
                reader.MoveToNextAttribute();
                reader.MoveToNextAttribute();
                this.ParseNext(reader, xmlRecord, ref field[1]);
            }
        }

        private sealed class CloudParser : ParserTemplate
        {
            public override void Parse(XmlReader reader, string xmlRecord, ref string[] field)
            {
                reader.ReadToFollowing(xmlRecord);
                reader.MoveToFirstAttribute();
                this.ParseNext(reader, xmlRecord, ref field[0]);
            }
        }
    }
}