using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace WeatherApp
{
    static class InternetConnectionChecker
    {
        private static Ping pinger = new Ping();

        public static void Check()
        {
            var reply = pinger.Send(IPAddress.Parse("185.39.160.19")); //google.com
            if (reply.Status != IPStatus.Success)
            {
                throw new Exception("No Internet connection!");
            }
        }


    }
}
