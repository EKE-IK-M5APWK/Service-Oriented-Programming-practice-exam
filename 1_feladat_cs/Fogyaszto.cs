using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _1_feladat
{
    class Fogyaszto
    {
        ConsoleColor c;
        double score;
        public Fogyaszto(ConsoleColor c,double score)
        {
            this.c = c;
            this.score = score;
        }
        public void Fogyaszt()
        {

            while (Program.counter > Program.eddigvolt)
            {
                Program.eddigvolt++;
                Monitor.Enter(Program.puffer);
                while (Program.puffer.Count == 0)
                {
                    Monitor.Wait(Program.puffer);
                }
                Console.ForegroundColor = c;
                Console.WriteLine("[-] {0} lett kivéve a raktárból.",Program.puffer[0]);
                switch (Program.puffer[0])
                {
                    case "Sör":
                        score += 0.75;
                        break;
                    case "Bor":
                        score += 1.5;
                        break;
                    case "Pálinka":
                        score += 1.0;
                        break;
                }
                Program.puffer.RemoveAt(0);
                this.FejreszKiir();
                Console.WriteLine("Az Ön pontszáma:{0}",score);
                int sleep = Program.rnd.Next(3, 8);
                Thread.Sleep(sleep);
                Monitor.Pulse(Program.puffer);
                Monitor.Exit(Program.puffer);
            }

            Console.WriteLine("{0}-es fogyasztó szál leállt." , Thread.CurrentThread.ManagedThreadId);
        }

        public void FejreszKiir()
        {
            if (Program.puffer.Count == 0)
                Console.Title = "Üres a kocsma raktára.";
            else
            {
                if (Program.puffer.Count >= 90)
                    Console.Title = Program.puffer.Count.ToString();
                else
                    Console.Title = Program.puffer.Count.ToString();
            }
        }
    }
}
