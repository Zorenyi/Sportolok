using System;

namespace WpfSportolok
{
    public class Sportolo
    {
        public string Nev { get; set; }
        public int Pontszam { get; set; }
        public string Orszag { get; set; }

        public Sportolo(string sor)
        {
            string[] t = sor.Split(';');

            Nev = t[0];
            Pontszam = int.Parse(t[1]);
            Orszag = t[2];
        }

        public override string ToString()
        {
            return $"{Nev} ({Orszag}) - {Pontszam} pont";
        }
    }
}