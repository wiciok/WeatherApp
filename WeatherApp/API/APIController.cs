using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.API
{
    abstract class APIController
    {
        protected string link;
        protected string key;
        protected string format;

        protected abstract void Initialize(string city, string country);
        public abstract void Parse();
    }
}
