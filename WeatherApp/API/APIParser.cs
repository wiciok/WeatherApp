using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

        private XmlReader reader;

        public void Parse(string link)
        {
            reader = XmlReader.Create(link);

            ParseCity();
            ParseCityCoords();
            ParseSun();
            ParseTemperature();
            ParseHumidity();
            ParsePressure();
            ParseWind();
            ParseClouds();
            ParseLastUpdate();
        }


        private void ParseCity()
        {
            reader.ReadToFollowing("city");
            reader.MoveToFirstAttribute();
            this.CityId = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            this.CityName = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
        }
        private void ParseCityCoords()
        {
            reader.ReadToFollowing("coord");
            reader.MoveToFirstAttribute();
            this.CityCoordY = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            this.CityCoordX = reader.Value.ToUpper();
        }
        private void ParseSun()
        {
            reader.ReadToFollowing("sun");
            reader.MoveToFirstAttribute();
            this.Sunrise = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            this.Sunset = reader.Value.ToUpper();
        }
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
        private void ParseHumidity()
        {
            reader.ReadToFollowing("humidity");
            reader.MoveToFirstAttribute();
            this.Humidity = reader.Value.ToUpper();
        }
        private void ParsePressure()
        {
            reader.ReadToFollowing("pressure");
            reader.MoveToFirstAttribute();
            this.Pressure = reader.Value.ToUpper();
        }
        private void ParseWind()
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
        }
        private void ParseClouds()
        {
            reader.ReadToFollowing("clouds");
            reader.MoveToFirstAttribute();
            reader.MoveToNextAttribute();
            this.CloudsName = reader.Value.ToUpper();
        }
        private void ParseLastUpdate()
        {
            reader.ReadToFollowing("lastupdate");
            reader.MoveToFirstAttribute();
            this.LastUpdate = reader.Value.ToUpper();
        }
    }
}
