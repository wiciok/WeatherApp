using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WeatherApp.Database
{
    abstract class DatabaseController
    {
        protected MySqlConnection connection;
        protected string server;
        protected string database;
        protected string uid;
        protected string password;

        protected abstract void Initialize();
        protected abstract string CreateConnectionString();
        protected abstract bool OpenConnection();
        protected abstract bool CloseConnection();

        public abstract void Insert(string query);
        public abstract void Update(string query);
        public abstract void Delete(string query);
        public abstract List<string>[] Select(string query);
        public abstract int Count(string query);
    }
}
