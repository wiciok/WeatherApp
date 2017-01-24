﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using WeatherApp.Database;
using WeatherApp.Logic;
using WeatherApp.Logic.API;
using WeatherApp.Logic.Langs;

namespace WeatherApp.API
{
    class APIOpenWeatherMapController : APIController
    {
        private APIParserOpenWeatherMap parser;
        public APIOpenWeatherMapController(string city, string country)
        {
            parser = new APIParserOpenWeatherMap();
            Initialize(city, country);
        }

        public APIOpenWeatherMapController()
        {
            parser = new APIParserOpenWeatherMap();
            SetStringsFactory();
        }

        public override void SetLangFactory()
        {
            SetStringsFactory();
        }

        private void SetStringsFactory()
        {
            switch (Settings.lang)
            {
                case Langs.English:
                    StringsFactory = new FactoryFieldNamesEnglish();
                    break;

                case Langs.Polish:
                    StringsFactory = new FactoryFieldNamesPolish();
                    break;
                default:
                    throw new Exception("Wrong lang type");
                    break;
            }
             
            UnitStringsFactory=new FactoryUnitNamesOpenWeatherMap();
        }

        protected override void Initialize(string city, string coutry)
        {
            key = "9382f55d22f029e00303d1134d77dd9f";
            format = "xml";

            link = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "," + coutry + "&appid=" + key +
                   "&mode=" + format;

            // państwo się nie parsuje
            parser.countryTag = coutry.ToUpper();
            SetStringsFactory();
        }


        public override void Parse()
        {
            parser.Parse(link);

            //unfortunately it's necessary, we cannot just do that:
            //APIParserOpenWeatherMap parser = (APIParserOpenWeatherMap)SingletonApiParser.Instance.Parser;
            //because it doesn't work - derived class properties don't 'translate' automatically to base class

            SingletonApiParser.Instance.Parser.cityName = parser.cityName;
            SingletonApiParser.Instance.Parser.countryTag = parser.countryTag;
            SingletonApiParser.Instance.Parser.humidity = parser.humidity;
            SingletonApiParser.Instance.Parser.pressure = parser.pressure;
            SingletonApiParser.Instance.Parser.cloudsName = parser.cloudsName;
            SingletonApiParser.Instance.Parser.lastUpdate = parser.lastUpdate;
            SingletonApiParser.Instance.Parser.temperatureValue = parser.temperatureValue;
            SingletonApiParser.Instance.Parser.windSpeed = parser.windSpeed;
            SingletonApiParser.Instance.Parser.windName = parser.windName;
            SingletonApiParser.Instance.Parser.windDirectionCode = parser.windDirectionCode;
        }


        public override void InsertToDB()
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
            if (dbInstance.DbController.Count("SELECT COUNT(*) " +
                                              "FROM COUNTRIES " +
                                              "WHERE COUNTRY_TAG='" +
                                              parser.countryTag + "'") == 0)

                dbInstance.DbController.Insert("INSERT INTO " +
                                               "COUNTRIES (COUNTRY_TAG) " +
                                               "VALUES ('" +
                                               parser.countryTag + "')");
        }

        private void InsertCity(SingletonDatabaseController dbInstance, SingletonApiParser apiInstance)
        {
            string country_id = dbInstance.DbController.SelectSingleAttributeRecord("SELECT COUNTRY_ID " +
                                                                                    "FROM COUNTRIES " +
                                                                                    "WHERE COUNTRY_TAG='" +
                                                                                    parser.countryTag + "'",
                "COUNTRY_ID");

            if (country_id == null)
                throw new ApplicationException("No DB connection or null returned from query");
            
            string query = String.Format("INSERT INTO CITIES " +
                                         "(CITY_ID, CITY_NAME, CITY_COORD_X, CITY_COORD_Y, COUNTRY_ID) " +
                                         "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
                parser.cityId,
                parser.cityName,
                parser.cityCoordX,
                parser.cityCoordY,
                country_id);

            if (
                dbInstance.DbController.Count("SELECT COUNT(*) FROM CITIES WHERE CITY_NAME='" +
                                              parser.cityName + "'") == 0)
                dbInstance.DbController.Insert(query);
        }

        private void InsertTemperature(SingletonDatabaseController dbInstance, SingletonApiParser apiInstance)
        {
            string unit_id = dbInstance.DbController.SelectSingleAttributeRecord("SELECT UNIT_ID " +
                                                                                 "FROM UNITS " +
                                                                                 "WHERE UNIT_NAME='" +
                                                                                 parser.unitName + "'",
                "UNIT_ID");

            if (unit_id == null)
                throw new ApplicationException("No DB connection or null returned from query");

            string query = String.Format("INSERT INTO TEMPERATURE " +
                                         "(TEMPERATURE_VALUE, UNIT_ID) " +
                                         "VALUES ('{0}', '{1}')",
                parser.temperatureValue, unit_id);

            if (dbInstance.DbController.Count("SELECT COUNT(*) " +
                                              "FROM TEMPERATURE " +
                                              "WHERE TEMPERATURE_VALUE='" +
                                              parser.temperatureValue + "'") == 0)

                dbInstance.DbController.Insert(query);
        }

        private void InsertWind(SingletonDatabaseController dbInstance, SingletonApiParser apiInstance)
        {
            string query = String.Format("INSERT INTO WINDS " +
                                         "(WIND_SPEED, " +
                                         "WIND_NAME, " +
                                         "WIND_DIRECTION, " +
                                         "WIND_DIRECTION_CODE, " +
                                         "WIND_DIRECTION_NAME) " +
                                         "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
                parser.windSpeed,
                parser.windName,
                parser.windDirection,
                parser.windDirectionCode,
                parser.windDirectionName);

            string count_query = String.Format("SELECT COUNT(*) " +
                                               "FROM WINDS " +
                                               "WHERE WIND_SPEED='{0}' " +
                                               "AND WIND_NAME='{1}' " +
                                               "AND WIND_DIRECTION='{2}'",
                parser.windSpeed,
                parser.windName,
                parser.windDirection);

            if (dbInstance.DbController.Count(count_query) == 0)
                dbInstance.DbController.Insert(query);
        }

        private void InsertMerge(SingletonDatabaseController dbInstance, SingletonApiParser apiInstance)
        {
            string wind_query = String.Format("SELECT WIND_ID " +
                                              "FROM WINDS " +
                                              "WHERE WIND_SPEED='{0}' " +
                                              "AND WIND_NAME='{1}' " +
                                              "AND WIND_DIRECTION='{2}'",
                parser.windSpeed,
                parser.windName,
                parser.windDirection);
            string wind_id = dbInstance.DbController.SelectSingleAttributeRecord(wind_query, "WIND_ID");

            string temp_query = "SELECT TEMPERATURE_ID " +
                                "FROM TEMPERATURE " +
                                "WHERE TEMPERATURE_VALUE='" +
                                parser.temperatureValue + "'";
            string temperature_id = dbInstance.DbController.SelectSingleAttributeRecord(temp_query, "TEMPERATURE_ID");


            string query = String.Format("INSERT INTO MERGE_PARAMS " +
                                         "(MERGE_PARAM_SUNRISE, " +
                                         "MERGE_PARAM_SUNSET, " +
                                         "MERGE_PARAM_HUMIDITY, " +
                                         "MERGE_PARAM_PRESSURE, " +
                                         "MERGE_PARAM_CLOUDS_NAME," +
                                         " MERGE_PARAM_VISIBILITY, " +
                                         "MERGE_PARAM_LAST_UPDATE, " +
                                         "WIND_ID, " +
                                         "TEMPERATURE_ID, " +
                                         "CITY_ID) " +
                                         "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}')",
                parser.sunrise,
                parser.sunset,
                parser.humidity,
                parser.pressure,
                parser.cloudsName,
                0,
                parser.lastUpdate,
                wind_id,
                temperature_id,
                parser.cityId);


            string count_query = String.Format("SELECT COUNT(*) " +
                                               "FROM MERGE_PARAMS " +
                                               "WHERE MERGE_PARAM_SUNRISE='{0}' " +
                                               "AND MERGE_PARAM_SUNSET='{1}' " +
                                               "AND MERGE_PARAM_HUMIDITY='{2}' " +
                                               "AND MERGE_PARAM_PRESSURE='{3}' " +
                                               "AND MERGE_PARAM_CLOUDS_NAME='{4}' " +
                                               "AND MERGE_PARAM_VISIBILITY='{5}' " +
                                               "AND MERGE_PARAM_LAST_UPDATE='{6}' " +
                                               "AND WIND_ID='{7}' " +
                                               "AND TEMPERATURE_ID='{8}' " +
                                               "AND CITY_ID='{9}'",
                parser.sunrise,
                parser.sunset,
                parser.humidity,
                parser.pressure,
                parser.cloudsName,
                0,
                parser.lastUpdate,
                wind_id,
                temperature_id,
                parser.cityId);

            if (dbInstance.DbController.Count(count_query) == 0)
                dbInstance.DbController.Insert(query);
        }

    }

}