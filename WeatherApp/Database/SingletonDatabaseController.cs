using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Database
{
    class SingletonDatabaseController
    {
        private static SingletonDatabaseController instance;
        private DatabaseController dbController = new WeatherDatabaseController();

        private SingletonDatabaseController() { }

        public static SingletonDatabaseController Instance
        {
            get 
            {
                if (instance == null)
                    instance = new SingletonDatabaseController();

                return instance;
            }
        }

        public DatabaseController DbController
        {
            get
            {
                return dbController;
            }
        }

    }
}
