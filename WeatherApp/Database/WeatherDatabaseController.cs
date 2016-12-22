using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WeatherApp.Database
{
    sealed class WeatherDatabaseController : DatabaseController
    {
        
        public WeatherDatabaseController()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            server = "sql7.freemysqlhosting.net";
            database = "sql7150398";
            uid = "sql7150398";
            password = "cLhk8zryBn";

            connection = new MySqlConnection(CreateConnectionString());
        }

        protected override string CreateConnectionString()
        {
            string returnDatabaseConnectionString;

            returnDatabaseConnectionString = "SERVER=" + server + ";";
            returnDatabaseConnectionString += "DATABASE=" + database + ";";
            returnDatabaseConnectionString += "UID=" + uid + ";";
            returnDatabaseConnectionString += "PASSWORD=" + password + ";";

            return returnDatabaseConnectionString;
        }

        protected override bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch(MySqlException ex)
            {
                // Switch catches most common errors
                // code error 0 - cannot connect to server
                // code error 1045 - invalid username and/ord passowrd

                switch(ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server");
                        break;
                    case 1045:
                        Console.WriteLine("Invalid username and/or password");
                        break;
                    default:
                        Console.WriteLine("Unknown error");
                        break;
                }

                return false;
            }
        }

        protected override bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public override void Insert(string query)
        {
            if (!query.ToLower().Contains("insert into"))
            {
                Console.WriteLine("Invalid format");
                return;
            }

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }

        }

        public override void Update(string query)
        {
            if (!query.ToLower().Contains("update"))
            {
                Console.WriteLine("Invalid format");
                return;
            }

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }

        public override void Delete(string query)
        {
            if (!query.ToLower().Contains("delete from"))
            {
                Console.WriteLine("Invalid format");
                return;
            }

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }

        public override List<string>[] Select(string query)
        {
            if (!query.ToLower().Contains("select"))
            {
                Console.WriteLine("Invalid format");
                return null;
            }

            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    list[0].Add(dataReader["TEMPERATURE_ID"] + "");
                    list[1].Add(dataReader["TEMPERATURE_VALUE"] + "");
                    list[2].Add(dataReader["UNIT_ID"] + "");
                }

                dataReader.Close();
                CloseConnection();

                return list;
            }
            else
                return list;
        }

        public override List<string>[] SelectCountryId(string query)
        {
            if (!query.ToLower().Contains("select"))
            {
                Console.WriteLine("Invalid format");
                return null;
            }

            List<string>[] list = new List<string>[1];
            list[0] = new List<string>();

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                    list[0].Add(dataReader["COUNTRY_ID"] + "");

                dataReader.Close();
                CloseConnection();

                return list;
            }
            else
                return list;
        }

        public override List<string>[] SelectUnitId(string query)
        {
            if (!query.ToLower().Contains("select"))
            {
                Console.WriteLine("Invalid format");
                return null;
            }

            List<string>[] list = new List<string>[1];
            list[0] = new List<string>();

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                    list[0].Add(dataReader["UNIT_ID"] + "");

                dataReader.Close();
                CloseConnection();

                return list;
            }
            else
                return list;
        }

        public override List<string>[] SelectTemperatureId(string query)
        {
            if (!query.ToLower().Contains("select"))
            {
                Console.WriteLine("Invalid format");
                return null;
            }

            List<string>[] list = new List<string>[1];
            list[0] = new List<string>();

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                    list[0].Add(dataReader["TEMPERATURE_ID"] + "");

                dataReader.Close();
                CloseConnection();

                return list;
            }
            else
                return list;
        }

        public override List<string>[] SelectWindId(string query)
        {
            if (!query.ToLower().Contains("select"))
            {
                Console.WriteLine("Invalid format");
                return null;
            }

            List<string>[] list = new List<string>[1];
            list[0] = new List<string>();

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                    list[0].Add(dataReader["WIND_ID"] + "");

                dataReader.Close();
                CloseConnection();

                return list;
            }
            else
                return list;
        }

        public override int Count(string query)
        {
            int count = -1;

            if (!query.ToLower().Contains("select"))
            {
                Console.WriteLine("Invalid format");
                return count;
            }

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                count = Int32.Parse(cmd.ExecuteScalar() + "");
                CloseConnection();

                return count;
            }
            else
                return count;
        }
    }
}
