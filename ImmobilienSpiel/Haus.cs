using System;
using System.Collections.Generic;
using System.Text;

namespace ImmobilienSpiel
{
    class Haus
    { 
        int anzahlZimmer;
        string farbeDesHauses;
        // public string besitzer;
        public int hauspreis;
        int hausnummer;
        public int rendite;

        public Haus(int hausnummer, int hauspreis, int zimmer, string farbe, int rendite)
        {
            this.anzahlZimmer = zimmer;
            this.farbeDesHauses = farbe;
            this.hauspreis = hauspreis;
            this.hausnummer = hausnummer;
            this.rendite = rendite;
        }

        public void HausVorstellung()
        {
            Console.WriteLine($"Hausnummer: {hausnummer}--- Zimmer: {anzahlZimmer} , Farbe: {farbeDesHauses}, Hauspreis: {hauspreis}, Rendite: {rendite}");
        }
    }
}