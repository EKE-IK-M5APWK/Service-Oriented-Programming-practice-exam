using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using heronDLL;

namespace _3_feladat
{
    class Program
    {
        static void Main(string[] args)
        {
            heron szamitas = new heron();
            Console.WriteLine("Geomtria Héron Képlet hármoszög: a={0} b={1},c={2} | Eredmény: {3}", 5, 6, 7,szamitas.heron_Haromszog(5,6,7));
            Console.WriteLine(" Héron Képlet húrnégyszög: a={0} b={1},c={2}, d={3} | Eredmény: {4}", 5, 6, 7, 8, szamitas.heron_Hurnegyszog(5,6,7,8));
            Console.WriteLine(" Héron Képlet konvex négyszög: a={0} b={1},c={2}, d={3} | Eredmény: {4}", 5, 6, 7, 8, szamitas.heron_Negyszog(50,10,5,6,7,8));

            Console.ReadLine();
            
        }
    }
}
