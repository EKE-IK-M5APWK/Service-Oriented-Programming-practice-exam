using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1_feladat
{
    class Termelo
    {
        public int db;
        public string mint;

        public Termelo(int db,string mit) 
        {
            this.db = db;
            this.mint = mit;
        }

        public void Termel() 
        {
             for (int i = 0; i < db; i++)
            {
                Monitor.Enter(Program.puffer);
                while (Program.puffer.Count >= 90)
                {
                    Monitor.Wait(Program.puffer);
                }
                 switch(mint)
                {
                     case "Sör":
                         Console.WriteLine("[+] 1 Sőr bekerült a raktárba.");
                         break;
                     case "Bor":
                         Console.WriteLine("[+] 1 Bor bekerült a raktárba.");
                         break;
                     case "Pálinka":
                         Console.WriteLine("[+] 1 Pálinka bekerült a raktárba.");
                         break;
                }
                int sleep = Program.rnd.Next(1, 5);
                Thread.Sleep(sleep);
                Program.puffer.Add(mint);
                Monitor.Pulse(Program.puffer);
                Monitor.Exit(Program.puffer);

            }
            Console.WriteLine("A {0}-as szál leállt.", Thread.CurrentThread.ManagedThreadId);
        }

     }
}

