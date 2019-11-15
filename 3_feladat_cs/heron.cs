using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace heronDLL
{
    public class heron
    {
        private double heron_S(double a, double b, double c) 
        {
            return (a + b + c) / 2;
        }
        private double heron_S2(double a, double b, double c) 
        {
            return (Math.Pow(a, 2) + Math.Pow(b, 2) + Math.Pow(c, 2)) / 2;
        }
        private double heron_S(double a, double b, double c, double d)
        {
            return (a + b + c + d) / 2;
        }
       
        public double heron_Haromszog(double a, double b, double c) 
        {
            double s = heron_S(a,b,c);
            return Math.Sqrt((s*(s-a)*(s-b)*(s-c)));
        }
        public double heron_Hurnegyszog(double a, double b, double c, double d) 
        {
            double s = heron_S(a, b, c, d);
            return Math.Sqrt(((s-a)*(s-b)*(s-c)*(s-d)));
        }
        public double heron_Negyszog(double alpha, double gamma, double a, double b, double c, double d) 
        {
            double pi = (alpha + gamma) / 2;
            return Math.Sqrt(heron_Hurnegyszog(a,b,c,d)-(a*b*c*d*Math.Cos(Math.Pow(pi,2))));
        }
        public double heron_Tetraeder(double a, double b, double c) 
        {
            return (1 / 3) * Math.Sqrt(heron_S2(a, b, c) * (heron_S2(a, b, c) - Math.Pow(a, 2)) * (heron_S2(a, b, c) - Math.Pow(b, 2)) * (heron_S2(a, b, c) - Math.Pow(c, 2)));
        }
    }
}
