using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace WeatherApp.API
{
    class APIParser
    {
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string CityCoordX { get; set; }
        public string CityCoordY { get; set; }
        public string CountryTag { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public string Humidity { get; set; }
        public string Pressure { get; set; }
        public string CloudsName { get; set; }
        public string LastUpdate { get; set; }
        public string TemperatureValue { get; set; }
        public string UnitName { get; set; }
        public string WindSpeed { get; set; }
        public string WindName { get; set; }
        public string WindDirection { get; set; }
        public string WindDirectionCode { get; set; }
        public string WindDirectionName { get; set; }


        //todo: sprawdzic czy argumenty xmlrecord nie muszą byc przekazywane przez ref

        private XmlReader reader;

        public void Parse(string link)
        {
            reader = XmlReader.Create(link);

            ParserTemplate singleParser = new SingleParser();
            ParserTemplate doubleParser = new MultipleParser();

            doubleParser.Parse(reader, "city", CityId, CityName);
            doubleParser.Parse(reader, "coord", CityCoordY, CityCoordX);
            doubleParser.Parse(reader, "sun", Sunrise, Sunset);


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

            protected void ParseNext(XmlReader reader, string xmlRecord,ref string field)
            {
                reader.MoveToNextAttribute();
                field = reader.Value.ToUpper();
            }

            public abstract void Parse(XmlReader reader, string xmlRecord, params string[] field);
        }


        private sealed class SingleParser : ParserTemplate
        {
            public override void Parse(XmlReader reader, string xmlRecord, params string[] field)
            {
                this.StartParsing(reader, xmlRecord,ref field[0]);
            }
        }

        private sealed class MultipleParser : ParserTemplate
        {
            public override void Parse(XmlReader reader, string xmlRecord, params string[] field)
            {
                this.StartParsing(reader, xmlRecord,ref field[0]);
                for (var i = 1; i < field.Length; i++)
                    this.ParseNext(reader, xmlRecord,ref field[i]);
            }
        }

        private sealed class TemperatureParser : ParserTemplate
        {
            public override void Parse(XmlReader reader, string xmlRecord, params string[] field)
            {
                this.StartParsing(reader, xmlRecord,ref field[0]);
                reader.MoveToNextAttribute();
                reader.MoveToNextAttribute();
                this.ParseNext(reader, xmlRecord,ref field[1]);
            }
        }

        private sealed class CloudParser : ParserTemplate
        {
            public override void Parse(XmlReader reader, string xmlRecord, params string[] field)
            {
                reader.ReadToFollowing(xmlRecord);
                reader.MoveToFirstAttribute();
                this.ParseNext(reader, xmlRecord,ref field[0]);
            }
        }


        /*private void ParseCity()
        {
            reader.ReadToFollowing("city");
            reader.MoveToFirstAttribute();
            this.CityId = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            this.CityName = reader.Value.ToUpper();
        }*/

        /*private void ParseCityCoords()
        {
            reader.ReadToFollowing("coord");
            reader.MoveToFirstAttribute();
            this.CityCoordY = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            this.CityCoordX = reader.Value.ToUpper();
        }*/
        /*private void ParseSun()
        {
            reader.ReadToFollowing("sun");
            reader.MoveToFirstAttribute();
            this.Sunrise = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            this.Sunset = reader.Value.ToUpper();
        }*/
        private void ParseTemperature()
        {
            reader.ReadToFollowing("temperature");
            reader.MoveToFirstAttribute();
            this.TemperatureValue = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            reader.MoveToNextAttribute();
            reader.MoveToNextAttribute();
            this.UnitName = reader.Value.ToUpper();
        }
        /*private void ParseHumidity()
        {
            reader.ReadToFollowing("humidity");
            reader.MoveToFirstAttribute();
            this.Humidity = reader.Value.ToUpper();
        }*/
        /*private void ParsePressure()
        {
            reader.ReadToFollowing("pressure");
            reader.MoveToFirstAttribute();
            this.Pressure = reader.Value.ToUpper();
        }*/
        /*private void ParseWind()
        {
            reader.ReadToFollowing("speed");
            reader.MoveToFirstAttribute();
            this.WindSpeed = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            this.WindName = reader.Value.ToUpper();

            reader.ReadToFollowing("direction");
            reader.MoveToFirstAttribute();
            this.WindDirection = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            this.WindDirectionCode = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            this.WindDirectionName = reader.Value.ToUpper();
        }*/
        /*private void ParseClouds()
        {
            reader.ReadToFollowing("clouds");
            reader.MoveToFirstAttribute();
            reader.MoveToNextAttribute();
            this.CloudsName = reader.Value.ToUpper();
        }*/
        /*private void ParseLastUpdate()
        {
            reader.ReadToFollowing("lastupdate");
            reader.MoveToFirstAttribute();
            this.LastUpdate = reader.Value.ToUpper();
        }*/
    }
}
