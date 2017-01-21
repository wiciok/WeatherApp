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

        public void Init(string city, string country)
        {
            this.Initialize(city, country);
        }
        public abstract void Parse();
        public abstract void Insert();
    }
}
