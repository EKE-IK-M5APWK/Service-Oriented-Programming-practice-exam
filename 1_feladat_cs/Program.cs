using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace _1_feladat
{
   
    class Program
    {
        static public List<string> puffer = new List<string>();
        static public int counter = 90;
        static public int eddigvolt = 0;
        static public Random rnd = new Random();
        static void Main(string[] args)
        {
            Termelo t1 = new Termelo(15,"Pálinka");
            Termelo t2 = new Termelo(20,"Sör");
            Termelo t3 = new Termelo(15,"Bor");
            Termelo t4 = new Termelo(15, "Pálinka");

            Thread szal1 = new Thread(t1.Termel);
            Thread szal2 = new Thread(t2.Termel);
            Thread szal3 = new Thread(t3.Termel);
            Thread szal4 = new Thread(t4.Termel);
            szal1.Start();
            szal2.Start();
            szal3.Start();
            szal4.Start();
            double f1_score = 0.0;
            double f2_score = 0.0;
            double f3_score = 0.0;
            double f4_score = 0.0;
            double f5_score = 0.0;
            Fogyaszto f1 = new Fogyaszto(ConsoleColor.Red,f1_score);
            Fogyaszto f2 = new Fogyaszto(ConsoleColor.Yellow,f2_score);
            Fogyaszto f3 = new Fogyaszto(ConsoleColor.Green,f3_score);
            Fogyaszto f4 = new Fogyaszto(ConsoleColor.White, f4_score);
            Fogyaszto f5 = new Fogyaszto(ConsoleColor.Blue, f5_score);

            Thread szal5 = new Thread(f1.Fogyaszt);
            Thread szal6 = new Thread(f2.Fogyaszt);
            Thread szal7 = new Thread(f3.Fogyaszt);
            Thread szal8 = new Thread(f4.Fogyaszt);
            Thread szal9 = new Thread(f5.Fogyaszt);
            szal5.Start();
            szal6.Start();
            szal7.Start();
            szal8.Start();
            szal9.Start();

            szal5.Join();
            szal6.Join();
            szal7.Join();
            szal8.Join();
            szal9.Join();
            double[] anArray = { f1_score, f2_score, f3_score, f4_score, f5_score };
            double maxValue = anArray.Max();
            int maxIndex = anArray.ToList().IndexOf(maxValue);
            Console.WriteLine("A győztes: {0} fogyasztó. Eredmény: {1}",maxIndex,maxValue);
            Console.ReadLine();
        }
    }

   
}
