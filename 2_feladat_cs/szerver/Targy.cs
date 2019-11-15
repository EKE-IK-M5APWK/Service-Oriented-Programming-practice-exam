using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_feladat_szerver
{
    class Targy
    {
        static private int _id = 1;
        public Targy(string nev, int ar, string felrako, string vasarlo) 
        {
            this.Id = _id;
            _id++;
            this.Ar = ar;
            this.Felrako = felrako;
            this.Vasarlo = vasarlo;
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string  nev;

        public string  Nev
        {
            get { return nev; }
            set { nev = value; }
        }
        private int ar;

        public int Ar
        {
            get { return ar; }
            set { ar = value; }
        }
        private string felrako;

        public string Felrako
        {
            get { return felrako; }
            set { felrako = value; }
        }
        private string vasarlo;

        public string Vasarlo
        {
            get { return vasarlo; }
            set { vasarlo = value; }
        }
        
        
        
    }
}
