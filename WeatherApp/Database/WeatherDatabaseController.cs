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
            server = "mn27.webd.pl";
            database = "qhoros_weatherApp";
            uid = "qhoros_wiciok";
            password = "123aplikacjaTO";

            connection = new MySqlConnection(CreateConnectionString());
        }

        protected override string CreateConnectionString()
        {
            StringBuilder dBConnectionStringBuilder = new StringBuilder();
            dBConnectionStringBuilder.Append("SERVER=" + server + ";");
            dBConnectionStringBuilder.Append("DATABASE=" + database + ";");
            dBConnectionStringBuilder.Append("UID=" + uid + ";");
            dBConnectionStringBuilder.Append("PASSWORD=" + password + ";");


            return dBConnectionStringBuilder.ToString();
        }

        protected override bool OpenConnection()
        {
            //todo: rethrow wyjątków
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                // Switch catches most common errors
                // code error 0 - cannot connect to server
                // code error 1045 - invalid username and/ord passowrd

                switch (ex.Number)
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
            //todo: rethrow wyjątków
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void QueryExecute(string query, string queryType)
        {
            if (!query.ToLower().Contains(queryType))
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

        public override void Insert(string query)
        {
            QueryExecute(query,"insert into");
        }

        public override void Update(string query)
        {
            QueryExecute(query,"update");
        }

        public override void Delete(string query)
        {
            QueryExecute(query, "delete from");
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
