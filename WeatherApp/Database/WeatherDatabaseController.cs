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

        private string CreateConnectionString()
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
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                string errString;

                // Switch catches most common errors
                // code error 0 - cannot connect to server
                // code error 1045 - invalid username and/ord passowrd

                switch (ex.Number)
                {
                    case 0:;
                        errString = "Cannot connect to server";
                        break;
                    case 1045:
                        errString = "Invalid username and/or password";
                        break;
                    default:
                        errString = "Unknown DB connection error";
                        break;
                }
                throw new ApplicationException(errString, ex);
                
            }
        }
        protected override bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException(ex.Message, ex);
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
            QueryExecute(query, "insert into");
        }
        public override void Update(string query)
        {
            QueryExecute(query, "update");
        }
        public override void Delete(string query)
        {
            QueryExecute(query, "delete from");
        }


        public override List<string>[] SelectMultipleAttributesRecords(string query, params string[] attribute)
        {
            if (!query.ToLower().Contains("select"))
            {
                Console.WriteLine("Invalid format");
                return null;
            }

            List<string>[] list = new List<string>[attribute.Length];
            for (var i=0; i < list.Length; i++)
                list[i]=new List<string>();

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    for (var i = 0; i < list.Length; i++)
                        list[i].Add(dataReader[attribute[i]] + "");
                }

                dataReader.Close();
                CloseConnection();
            }
            return list;
        }
        public override string SelectSingleAttributeRecord(string query, string attributeName)
        {
            if (!query.ToLower().Contains("select"))
            {
                Console.WriteLine("Invalid format");
                return null;
            }

            string retString = null;

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Read();
                retString = dataReader[attributeName] + "";

                dataReader.Close();
                CloseConnection();
            }
            return retString;
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
