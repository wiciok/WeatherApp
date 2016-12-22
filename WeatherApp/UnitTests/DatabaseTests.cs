using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WeatherApp.Database;

namespace WeatherApp.Tests
{
    [TestFixture]
    class UnitTests
    {
        [Test]
        public static void CheckDatabaseSelectMethod()
        {
            Assert.IsNull(new WeatherDatabaseController().Select("to nie jest poprawne zapytanie"));
            Assert.IsNotNull(new WeatherDatabaseController().Select("SELECT * FROM TEMPERATURES"));
        }

        [Test]
        public static void CheckDatabaseCountMethod()
        {
            Assert.AreEqual(-1, new WeatherDatabaseController().Count("to jest niepoprawne zapytanie"));
            Assert.AreEqual(4, new WeatherDatabaseController().Count("SELECT COUNT(*) FROM UNITS"));
        }
    }
}
