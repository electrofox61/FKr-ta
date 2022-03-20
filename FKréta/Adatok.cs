using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKréta
{
    internal class Adatok
    {
        public int Sorszam { get; set; }
        public string Nev { get; set; }
        public string Osztaly { get; set; }
        public double Atlag { get; set; } 
        public Adatok(string sor)
        {
            string[] resz = sor.Split(';');
            Sorszam = Convert.ToInt32(resz[0]);
            Nev = resz[1];
            Osztaly = resz[2];
        }
    }
}
