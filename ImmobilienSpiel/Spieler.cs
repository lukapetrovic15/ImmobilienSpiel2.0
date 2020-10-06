using System;
using System.Collections.Generic;
using System.Text;

namespace ImmobilienSpiel
{
    class Spieler
    {
        public int kontostand;
        public string name;
        // public int anzahlHaeuser; 

        public Spieler(string name, int kontostand)
        {
            this.name = name;
            this.kontostand = kontostand;
        }

        public Spieler(string name)
        {
            this.name = name;
        }

        public void SpielerStatus()
        {
            Console.WriteLine($"Name Spieler: {name}, Kontostand: {kontostand}");
        }
    }
}