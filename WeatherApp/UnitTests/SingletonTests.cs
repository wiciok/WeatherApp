using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WeatherApp.API;
using WeatherApp.Database;

namespace WeatherApp.UnitTests
{
    [TestFixture]
    class SingletonTests
    {
        [Test]
        public static void CheckDatabaseSingleton()
        {
            Assert.IsNotNull(SingletonDatabaseController.Instance);
        }

        [Test]
        public static void CheckAPISingleton()
        {
            Assert.IsNotNull(SingletonApiParser.Instance);
        }
    }
}
