using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace _2_feladat_szerver
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener figyelo = null;
            try
            {
                string ipCim = ConfigurationManager.AppSettings[GetLocalIPAddress()];
                string portSzam = ConfigurationManager.AppSettings["80"];
                IPAddress ip2 = IPAddress.Parse(ipCim);
                int port = int.Parse(portSzam);
                figyelo = new TcpListener(ip2, port);
                figyelo.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                figyelo = null;
            }
            bool vege = false;
            while (!vege)
            {
                Console.WriteLine("A szerver vár egy kliens csatlakozására...");
                TcpClient client = figyelo.AcceptTcpClient();
                Protocol protokoll = new Protocol(client);
                Thread th = new Thread(protokoll.StartKomm);
                th.Start();
            }
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
