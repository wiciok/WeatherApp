using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using WeatherApp.Database;

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
        
            // państwo się nie parsuje
            SingletonApiParser.Instance.Parser.countryTag = coutry.ToUpper();
        }



        public override void Parse()
        {
            SingletonApiParser instance = SingletonApiParser.Instance;
            XmlReader reader = XmlReader.Create(link);

            ParseCity(instance, reader);
            ParseCityCoords(instance, reader);
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
            instance.Parser.cityId = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            instance.Parser.cityName = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
        }

        private void ParseCityCoords(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("coord");
            reader.MoveToFirstAttribute();
            instance.Parser.cityCoordY = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            instance.Parser.cityCoordX = reader.Value.ToUpper();
        }

        private void ParseSun(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("sun");
            reader.MoveToFirstAttribute();
            instance.Parser.sunrise = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            instance.Parser.sunset = reader.Value.ToUpper();
        }

        private void ParseTemperature(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("temperature");
            reader.MoveToFirstAttribute();
            instance.Parser.temperatureValue = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            reader.MoveToNextAttribute();
            reader.MoveToNextAttribute();
            instance.Parser.unitName = reader.Value.ToUpper();
        }

        private void ParseHumidity(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("humidity");
            reader.MoveToFirstAttribute();
            instance.Parser.humidity = reader.Value.ToUpper();
        }

        private void ParsePressure(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("pressure");
            reader.MoveToFirstAttribute();
            instance.Parser.pressure = reader.Value.ToUpper();
        }

        private void ParseWind(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("speed");
            reader.MoveToFirstAttribute();
            instance.Parser.windSpeed = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            instance.Parser.windName = reader.Value.ToUpper();

            reader.ReadToFollowing("direction");
            reader.MoveToFirstAttribute();
            instance.Parser.windDirection = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            instance.Parser.windDirectionCode = reader.Value.ToUpper();
            reader.MoveToNextAttribute();
            instance.Parser.windDirectionName = reader.Value.ToUpper();
        }

        private void ParseClouds(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("clouds");
            reader.MoveToFirstAttribute();
            reader.MoveToNextAttribute();
            instance.Parser.cloudsName = reader.Value.ToUpper();
        }

        private void ParseLastUpdate(SingletonApiParser instance, XmlReader reader)
        {
            reader.ReadToFollowing("lastupdate");
            reader.MoveToFirstAttribute();
            instance.Parser.lastUpdate = reader.Value.ToUpper();
        }

        public override void Insert()
        {
            SingletonDatabaseController dbInstance = SingletonDatabaseController.Instance;
            SingletonApiParser apiInstance = SingletonApiParser.Instance;

            InsertCountry(dbInstance, apiInstance);
            InsertCity(dbInstance, apiInstance);
            InsertTemperature(dbInstance, apiInstance);
            InsertWind(dbInstance, apiInstance);
            InsertMerge(dbInstance, apiInstance);
        }

        private void InsertCountry(SingletonDatabaseController dbInstance, SingletonApiParser apiInstance)
        {
            if (dbInstance.DbController.Count("SELECT COUNT(*) FROM COUNTRIES WHERE COUNTRY_TAG='" + apiInstance.Parser.countryTag + "'") == 0)
                dbInstance.DbController.Insert("INSERT INTO COUNTRIES (COUNTRY_TAG) VALUES ('" + apiInstance.Parser.countryTag + "')");
        }

        private void InsertCity(SingletonDatabaseController dbInstance, SingletonApiParser apiInstance)
        {
            List<string>[] country_id = dbInstance.DbController.SelectCountryId("SELECT COUNTRY_ID FROM COUNTRIES WHERE COUNTRY_TAG='" + apiInstance.Parser.countryTag + "'");

            string query = String.Format("INSERT INTO CITIES (CITY_ID, CITY_NAME, CITY_COORD_X, CITY_COORD_Y, COUNTRY_ID) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')", 
                apiInstance.Parser.cityId, apiInstance.Parser.cityName, apiInstance.Parser.cityCoordX, apiInstance.Parser.cityCoordY, country_id[0][0]);
            
            if (dbInstance.DbController.Count("SELECT COUNT(*) FROM CITIES WHERE CITY_NAME='" + apiInstance.Parser.cityName + "'") == 0)
               dbInstance.DbController.Insert(query);
        }

        private void InsertTemperature(SingletonDatabaseController dbInstance, SingletonApiParser apiInstance)
        {
            List<string>[] unit_id = dbInstance.DbController.SelectUnitId("SELECT UNIT_ID FROM UNITS WHERE UNIT_NAME='" + apiInstance.Parser.unitName + "'");

            string query = String.Format("INSERT INTO TEMPERATURES (TEMPERATURE_VALUE, UNIT_ID) VALUES ('{0}', '{1}')",
                apiInstance.Parser.temperatureValue, unit_id[0][0]);

            if (dbInstance.DbController.Count("SELECT COUNT(*) FROM TEMPERATURES WHERE TEMPERATURE_VALUE='" + apiInstance.Parser.temperatureValue +"'") == 0)
                dbInstance.DbController.Insert(query);
        }

        private void InsertWind(SingletonDatabaseController dbInstance, SingletonApiParser apiInstance)
        {
            string query = String.Format("INSERT INTO WINDS (WIND_SPEED, WIND_NAME, WIND_DIRECTION, WIND_DIRECTION_CODE, WIND_DIRECTION_NAME) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
                apiInstance.Parser.windSpeed, apiInstance.Parser.windName, apiInstance.Parser.windDirection, apiInstance.Parser.windDirectionCode, apiInstance.Parser.windDirectionName);

            string count_query = String.Format("SELECT COUNT(*) FROM WINDS WHERE WIND_SPEED='{0}' AND WIND_NAME='{1}' AND WIND_DIRECTION='{2}'",
                apiInstance.Parser.windSpeed, apiInstance.Parser.windName, apiInstance.Parser.windDirection);

            if (dbInstance.DbController.Count(count_query) == 0)
                dbInstance.DbController.Insert(query);
        }

        private void InsertMerge(SingletonDatabaseController dbInstance, SingletonApiParser apiInstance)
        {
            string wind_query = String.Format("SELECT WIND_ID FROM WINDS WHERE WIND_SPEED='{0}' AND WIND_NAME='{1}' AND WIND_DIRECTION='{2}'",
                apiInstance.Parser.windSpeed, apiInstance.Parser.windName, apiInstance.Parser.windDirection);

            List<string>[] wind_id = dbInstance.DbController.SelectWindId(wind_query);
            List<string>[] temperature_id = dbInstance.DbController.SelectTemperatureId("SELECT TEMPERATURE_ID FROM TEMPERATURES WHERE TEMPERATURE_VALUE='" + apiInstance.Parser.temperatureValue + "'");

            string query = String.Format("INSERT INTO MERGE_PARAMS (MERGE_PARAM_SUNRISE, MERGE_PARAM_SUNSET, MERGE_PARAM_HUMIDITY, MERGE_PARAM_PRESSURE, MERGE_PARAM_CLOUDS_NAME, MERGE_PARAM_VISIBILITY, MERGE_PARAM_LAST_UPDATE, WIND_ID, TEMPERATURE_ID, CITY_ID) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')",
                apiInstance.Parser.sunrise, apiInstance.Parser.sunset, apiInstance.Parser.humidity, apiInstance.Parser.pressure, apiInstance.Parser.cloudsName, 0, apiInstance.Parser.lastUpdate, wind_id[0][0], temperature_id[0][0], apiInstance.Parser.cityId);

            string count_query = String.Format("SELECT COUNT(*) FROM MERGE_PARAMS WHERE MERGE_PARAM_SUNRISE='{0}' AND MERGE_PARAM_SUNSET='{1}' AND MERGE_PARAM_HUMIDITY='{2}' AND MERGE_PARAM_PRESSURE='{3}' AND MERGE_PARAM_CLOUDS_NAME='{4}' AND MERGE_PARAM_VISIBILITY='{5}' AND MERGE_PARAM_LAST_UPDATE='{6}' AND WIND_ID='{7}' AND TEMPERATURE_ID='{8}' AND CITY_ID='{9}'",
                apiInstance.Parser.sunrise, apiInstance.Parser.sunset, apiInstance.Parser.humidity, apiInstance.Parser.pressure, apiInstance.Parser.cloudsName, 0, apiInstance.Parser.lastUpdate, wind_id[0][0], temperature_id[0][0], apiInstance.Parser.cityId);

            if (dbInstance.DbController.Count(count_query) == 0)
                dbInstance.DbController.Insert(query);
        }
    }
}
