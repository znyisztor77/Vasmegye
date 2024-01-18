using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Vasmegye
{
    internal class Program
    {
        struct Adatok
        {
            public int nem { get; set; }
            public string szuldatum { get; set; }
            public int azonosito { get; set; }
            public string teljessor {  get; set; }
        }


        static void Main(string[] args)
        {
            string fajlnev = "vas.txt";
            List<Adatok> vasmegye = new List<Adatok>();
                    
            vasmegye  = adatBeolvas(fajlnev);
            szemelyiSzam(vasmegye);
                       
            Console.WriteLine($"5.feladat: Vasmegyében a vizsgált évek alatt {vasmegye.Count} csecsemő született ");

            Console.Write("6.feladat: ");
            fiukSzamol(vasmegye);
            Console.ReadKey();

        }

        private static void fiukSzamol(List<Adatok> vasmegye)
        {
            int szamol = 0;
            for(int i = 0; i< vasmegye.Count; i++)
            {
                if (vasmegye[i].nem == 1 || vasmegye[i].nem == 3)
                    szamol++;
            }
            Console.WriteLine($"A fiúk száma: {szamol}");
        }

        private static void szemelyiSzam(List<Adatok> vasmegye)
        {           
                         
            Console.WriteLine("1 sor");
            string szemszamadat1 = vasmegye[2].nem.ToString() + vasmegye[2].szuldatum.ToString() + vasmegye[2].azonosito.ToString();
            Console.WriteLine(szemszamadat1);

            int osszeg = 0;
            int leptek = 10;
            for(int i = 0;i< szemszamadat1.Length; i++)
            {
                osszeg += szemszamadat1[i] * leptek;
                leptek--;
            }
            osszeg = osszeg % 11;
            Console.Write("Az eredmény:  ");
            Console.WriteLine(osszeg);
        }

        private static List<Adatok> adatBeolvas(string fajlnev)
        {
            List<Adatok> vasAdatok = new List<Adatok>(); 

            if (!File.Exists(fajlnev))
            {
                Console.WriteLine("Nem sikerült az adatok olvasása, hibás fájl, vagy fájlnév!");
            }
            else
            {
                Console.WriteLine("2. feladat: ");
                Console.WriteLine("Adatok olvasása....");
                StreamReader sr = new StreamReader(fajlnev);
                        
                    while(!sr.EndOfStream) 
                    {
                       Adatok vasAdat = new Adatok();
                        string sor = sr.ReadLine();
                         string[] sorok = sor.Split('-');

                       vasAdat.nem = int.Parse(sorok[0]);
                        vasAdat.szuldatum = sorok[1];
                        vasAdat.azonosito = int.Parse(sorok[2]);
                        //vasAdat.teljessor = sor.Split('-');
                        vasAdatok.Add(vasAdat);
                    }
                Console.WriteLine("Sikeres fájl beolvasás!");
            }           

            return vasAdatok;
        }
    }
}
