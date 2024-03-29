﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
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
            CdvEll(vasmegye);
                       
            Console.WriteLine($"5.feladat: Vasmegyében a vizsgált évek alatt {vasmegye.Count} csecsemő született ");

            Console.Write("6.feladat: ");
            // 6. feladat
            fiukSzamol(vasmegye);
            // 7-8. feladat
            szuletesEvekStatisztika(vasmegye);
            Console.ReadKey();

        }

         

        private static void szuletesEvekStatisztika(List<Adatok> vasmegye)
        {
            int[] evekszamol = new int[vasmegye.Count];
            for(int i = 0; i<vasmegye.Count; i++)
            {                
                int ev =Convert.ToInt32( vasmegye[i].szuldatum.Substring(0,1));
                
                if (ev != 9)
                {
                   string szam1 = "20" + vasmegye[i].szuldatum.Substring(0, 2);
                    evekszamol[i] = Convert.ToInt32(szam1) ;
                }
                else
                {
                    string szam2 = "19" + vasmegye[i].szuldatum.Substring(0, 2);
                    evekszamol[i] = Convert.ToInt32(szam2);
                }
            }

            int min = evekszamol[0];
            int max = evekszamol[0];
            int db = 0;
            
            for(int i = 0; i< evekszamol.Length; i++)
            {
                if (min > evekszamol[i])
                {
                    min = evekszamol[i];
                    
                }
                else if(max < evekszamol[i])
                { 
                    max = evekszamol[i];
                }
            }
            int elso = min;

            for(int i = 0;i< evekszamol.Length; i++)
            {
                if (evekszamol[i] == min)
                {
                    db++;
                }
            }
            Console.WriteLine($"7. feladat: A vizsgált időszak: {min} - {max}");

            int[] evszamok = { 1998, 1999, 2000, 2001 };
            int evszam_db = 0;

            Console.WriteLine($"8. feladat: Statisztika");

            for (int i = 0; i < evszamok.Length; i++)
            {
                for (int j = 0; j < evekszamol.Length; j++)
                {
                    if (evszamok[i] == evekszamol[j])
                    {
                        evszam_db++;
                    }
                }

                Console.WriteLine($"\t {evszamok[i]} - {evszam_db}");
                evszam_db = 0;
            }           
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

        private static void CdvEll(List<Adatok> vasmegye)
        {           
                         
            Console.WriteLine("Egy sor ellenörzése: ");
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
            Console.WriteLine($"Az eredmény: {osszeg}");
            Console.WriteLine("Ellenörzés OK!");
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
