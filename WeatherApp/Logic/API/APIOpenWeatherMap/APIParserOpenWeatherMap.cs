using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using WeatherApp.Logic.API;

namespace WeatherApp.API
{
    class APIParserOpenWeatherMap : APIParser
    {
        public string cityId;
        private string cityName;
        public string cityCoordX;
        public string cityCoordY;
        private string countryTag;
        public string sunrise;
        public string sunset;
        private string humidity;
        private string pressure;
        private string cloudsName;
        private string lastUpdate;
        private string temperatureValue;
        public string unitName;
        private string windSpeed;
        private string windName;
        public string windDirection;
        private string windDirectionCode;
        public string windDirectionName;

        private XmlReader reader;

        public override string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }

        public override string CountryTag
        {
            get { return countryTag; }

            set { countryTag = value; }
        }

        public override string Humidity
        {
            get { return humidity; }

            set { humidity = value; }
        }

        public override string Pressure
        {
            get { return pressure; }
            set { pressure = value; }
        }

        public override string CloudsName
        {
            get { return cloudsName; }
            set { cloudsName = value; }
        }

        public override string LastUpdate
        {
            get { return lastUpdate; }
            set { lastUpdate = value; }
        }

        public override string TemperatureValue
        {
            get { return temperatureValue; }
            set { temperatureValue = value; }
        }

        public override string WindSpeed
        {
            get { return windSpeed; }
            set { windSpeed = value; }
        }

        public override string WindName
        {
            get { return windName; }
            set { windName = value; }
        }

        public override string WindDirectionCode
        {
            get { return windDirectionCode; }
            set { windDirectionCode = value; }
        }

        private void ParsingAdapter(string xmlRecord, ref string field1)
        {
            var cityArr = new[] {field1};
            ParserTemplate doubleParser = new SingleParser();
            doubleParser.Parse(reader, xmlRecord, ref cityArr);
            field1 = cityArr[0];
        }

        private void ParsingAdapter(string xmlRecord, ref string field1, ref string field2)
        {
            var cityArr = new[] {field1, field2};
            ParserTemplate doubleParser = new MultipleParser();
            doubleParser.Parse(reader, xmlRecord, ref cityArr);
            field1 = cityArr[0];
            field2 = cityArr[1];
        }

        private void ParsingAdapter(string xmlRecord, ref string field1, ref string field2, ref string field3)
        {
            var cityArr = new[] {field1, field2, field3};
            ParserTemplate doubleParser = new MultipleParser();
            doubleParser.Parse(reader, xmlRecord, ref cityArr);
            field1 = cityArr[0];
            field2 = cityArr[1];
            field3 = cityArr[2];
        }

        public override void Parse(string link)
        {
            reader = XmlReader.Create(link);

            ParsingAdapter("city", ref cityId, ref cityName);
            ParsingAdapter("coord", ref cityCoordY, ref cityCoordX);
            ParsingAdapter("sun", ref sunrise, ref sunset);

            var temperatureTempArr = new[] {temperatureValue, unitName};
            ParserTemplate temperatureParser = new TemperatureParser();
            temperatureParser.Parse(reader, "temperature", ref temperatureTempArr);
            temperatureValue = temperatureTempArr[0];
            unitName = temperatureTempArr[1];

            ParsingAdapter("humidity", ref humidity);
            ParsingAdapter("pressure", ref pressure);
            ParsingAdapter("speed", ref windSpeed, ref windName);
            ParsingAdapter("direction", ref windDirection, ref windDirectionCode, ref windDirectionName);

            var cloudTempArr = new[] {cloudsName};
            ParserTemplate cloudParser = new CloudParser();
            cloudParser.Parse(reader, "clouds", ref cloudTempArr);
            cloudsName = cloudTempArr[0];

            ParsingAdapter("lastupdate", ref lastUpdate);
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