using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.API
{
    class SingletonApiParserOWM
    {
        private static SingletonApiParserOWM instance;
        private static APIParserOpenWeatherMap parser;

        private SingletonApiParserOWM() { }

        public static SingletonApiParserOWM Instance
        {
            get 
            {
                if (instance == null)
                    instance = new SingletonApiParserOWM();

                return instance;
            }
        }

        public APIParserOpenWeatherMap Parser
        {
            get
            {
                if (parser == null)
                    parser = new APIParserOpenWeatherMap();

                return parser;
            }
        }

    }
}
