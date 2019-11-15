using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;

namespace _2_feladat_kliens
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetLocalIPAddress());
            string ipCim = ConfigurationManager.AppSettings[GetLocalIPAddress()];
            string portSzam = ConfigurationManager.AppSettings["80"];
            TcpClient client = new TcpClient(ipCim, int.Parse(portSzam));
            StreamReader read = new StreamReader(client.GetStream(), Encoding.UTF8);
            StreamWriter write = new StreamWriter(client.GetStream(), Encoding.UTF8);
            string message = read.ReadLine();
            write.WriteLine("Egy kliens csatlakozott");
            write.Flush();
            string[] submesagges = message.Split('<');
            Console.WriteLine("Szerver üzenete:");
            for (int i = 0; i < submesagges.Length; i++)
            {
                Console.WriteLine(submesagges[i]);
            }
            bool end = false;
            while (!end)
            {
                string command = Console.ReadLine();
                write.WriteLine(command); write.Flush();
                string answer = read.ReadLine();
                if (answer.Equals("Eladott tárgyak listázása"))
                {
                    Console.WriteLine(answer);
                    while (!answer.Equals("Listázás vége"))
                    {
                        answer = read.ReadLine();
                        Console.WriteLine(answer);
                    }
                }
                else
                    Console.WriteLine(answer);
                if (answer.Equals("KILEP"))
                    end = true;
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
