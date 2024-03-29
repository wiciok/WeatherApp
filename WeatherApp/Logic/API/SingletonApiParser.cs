﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Logic.API;

namespace WeatherApp.API
{
    class SingletonApiParser
    {
        private static SingletonApiParser instance;
        private static APIParser parser;

        private SingletonApiParser() { }

        public static SingletonApiParser Instance
        {
            get 
            {
                if (instance == null)
                    instance = new SingletonApiParser();

                return instance;
            }
        }

        public APIParser Parser
        {
            get
            {
                if (parser == null)
                    parser = new APIParserOpenWeatherMap(); //tutaj strategia jesli beda inne api

                return parser;
            }
        }

    }
}
