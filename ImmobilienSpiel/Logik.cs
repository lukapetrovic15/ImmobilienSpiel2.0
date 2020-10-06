using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

namespace ImmobilienSpiel
{
    class Logik
    {
        public DBA db;

        public List<Haus> haeuser;
        public Spieler spieler;

        public Logik()
        {
            db = new DBA();
            haeuser = db.GetHaeuser();
        }

        public void AlleHaeuserAnzeigen()
        {
            foreach (Haus haus in haeuser)
            {
                haus.HausVorstellung();
            }
        }
        // Methode für den Spielanfang usw.
        public void Spiel()
        {
            Console.WriteLine("Hallo, bitte gebe deinen Spielnamen ein.");
            string name = Console.ReadLine();
            spieler = db.GetPlayer(name);


            if (spieler == null)
            {
                Console.WriteLine("Spieler nicht gefunden!");
                Console.WriteLine($"Möchtest du einen Spieler namens {name} erstellen?");
                string neuenSpielerErstellen = Console.ReadLine();
                if (neuenSpielerErstellen == "y")
                {
                    Console.WriteLine("Wähle den Schwierigkeitsgrad von 1-3");
                    int schwierigkeitsgrad = Convert.ToInt32(Console.ReadLine());
                    spieler = db.MakeNewPlayer(name, schwierigkeitsgrad);
                    spieler = db.GetPlayer(name);
                }
                else if (neuenSpielerErstellen == "n")
                {
                    Console.WriteLine("Hast du dich vertippt?");
                    if (Console.ReadLine() == "y")
                    {
                        Spiel();
                    }
                    else
                    {
                        Console.WriteLine("Erstelle neuen Spieler: ");
                        Console.WriteLine("Gebe deinen Spielernamen ein!");
                        string nName = Console.ReadLine();
                        Console.WriteLine("Wähle den Schwierigkeitsgrad von 1-3");
                        int schwierigkeitsgrad = Convert.ToInt32(Console.ReadLine());
                        spieler = db.MakeNewPlayer(nName, schwierigkeitsgrad);
                        db.GetPlayer(nName);
                    }
                }
            }

            Console.WriteLine($"Hallo {spieler.name}, in diesem Spiel kannst du Häuser kaufen und verkaufen, und so immer reicher werden! Dein Startkapital ist: {spieler.kontostand}");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------");

            while (spieler.kontostand <= 1500000)
            {
                //HausBauen();
                spieler.SpielerStatus();
                Console.WriteLine("Möchtest du ein Haus kaufen, verkaufen, bauen oder renovieren? (k/v/b/r)");
                string kaufenVerkaufenBauenRenovieren = Console.ReadLine().ToLower();
                switch (kaufenVerkaufenBauenRenovieren)
                {
                    case "k":
                        Kaufen();
                        break;
                    // case "v":
                    //  Verkaufen();
                    //  break;
                    case "b":
                        HausBauen();
                        break;
                        // case "r":
                        //  HausRenovieren();
                        //  break;
                }
            }
        }
        // Methoden für den Kauf und Verkauf
        public void Kaufen()
        {
            try
            {
                Console.WriteLine("Welches Haus möchtest du kaufen? Hausnummer angeben!");
                AlleHaeuserAnzeigen();
                int hausnummerAuswahl = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine($"Du hast das {hausnummerAuswahl}. Haus gekauft");
                Console.WriteLine("-------------------------------------------------------");
                spieler.SpielerStatus();
                db.HausKauf(hausnummerAuswahl, spieler.name);
            }
            catch (Exception)
            {
                Console.WriteLine("Du musst die Hausnummer angeben!");
            }
        }

        /*public void Verkaufen()
        {
            //HausAnzahlUeberpruefung();

            try
            {
                Console.WriteLine("Welches Haus möchtest du verkaufen?");
                AlleHaeuserAnzeigen();

                string welchesHausVerkaufen = Console.ReadLine();
                int hausindex = Convert.ToInt32(welchesHausVerkaufen) - 1;

                //bool inbesitz = HausTypImBesitzUeberpruefung(hausindex);

                //if (inbesitz)
                //{
                //    Console.WriteLine($"Du hast das {welchesHausVerkaufen}. Haus verkauft");
                //    Console.WriteLine("-------------------------------------------------------");
                //    //spieler.anzahlHaeuser--;
                //    spieler.Kontostand += haeuser[hausindex].Hauspreis;
                //    spieler.Kontostand += haeuser[hausindex].rendite;
                //    spieler.KaeuferStatus();
                //    WeiterVerkaufen();
                //}
                //else
                //{
                //    Console.WriteLine("Das Haus gehört dir nicht!");
                //}
            }
            catch (Exception)
            {
                Console.WriteLine("Du musst die Hausnummer angeben!");
            }
        }*/

        // Methoden für die Häuser
        public void HausBauen()
        {
            try
            {
                Console.WriteLine("Du kannst ein neues Haus bauen lassen!");

                Console.WriteLine("Welche Hausnummer sollte das Haus haben?");
                int hausnummer = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Wie viel sollte das Haus kosten?");
                int hauspreis = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Wie viele Zimmer sollte das Haus haben?");
                int zimmer = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Welche Farbe sollte das Haus haben?");
                string farbe = Console.ReadLine();

                int rendite = spieler.kontostand / 3;

                haeuser.Add(db.HausBau(hausnummer, hauspreis, zimmer, farbe, rendite));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // verbessern!!!
        public void HausRenovieren()
        {
            try
            {
                Console.WriteLine("Wie möchtest du dein Haus renovieren?");
                Console.WriteLine("Welche Farbe sollte dein Haus haben?");
                string hausfarbeNeu = Console.ReadLine();
                Console.WriteLine("Möchtest du neue Zimmer bauen lassen?");
                string zimmerBauenLassen = Console.ReadLine();
                if (zimmerBauenLassen == "y")
                {
                    Console.WriteLine("Wie viele Zimmer möchtest du bauen lassen?");
                    int zimmerNeu = Convert.ToInt32(Console.ReadLine());

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //public void HausAnzahlUeberpruefung()
        //{
        //    if (spieler.anzahlHaeuser == 0)
        //    {
        //        Console.WriteLine("-------------------------------------------------------");
        //        Console.WriteLine("Verkaufen nicht möglich! Du hast keine Häuser im Besitz!");
        //        Console.WriteLine("Du musst zuerst ein Haus kaufen.");
        //        Console.WriteLine("-------------------------------------------------------");
        //        Kaufen();
        //    }
        //}

        //public bool HausTypImBesitzUeberpruefung(int hausindex)
        //{
        //    if (haeuser[hausindex].besitzer == spieler.name)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public void AuswahlSchwierigkeitsgrad()
        //{
        //    try
        //    {
        //        Console.WriteLine($"Welchen Schwierigkeitsgrad möchtest du {Convert.ToInt32(spieler.Name)}? 1, 2 oder 3? (1 einfach, 2 mittel, 3 schwierig");
        //        string schwierigkeitsGrad = Console.ReadLine();
        //        int schwierigkeitsGradInt = int.Parse(schwierigkeitsGrad);

        //        if (schwierigkeitsGradInt == 1)
        //        {

        //        }
        //        else if (schwierigkeitsGradInt == 2)
        //        {
        //            spieler.Kontostand = 50000;
        //        }
        //        else if (schwierigkeitsGradInt == 3)
        //        {
        //            spieler.Kontostand = 30000;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("Du must zwischen 1, 2 & 3 entscheiden!");
        //        AuswahlSchwierigkeitsgrad();
        //    }
        //}

        /* public void WeiterEinkaufen()
       {
           Console.WriteLine("Möchtest du ein weiteres Haus kaufen?");
           string kaufenJaNein = Console.ReadLine().ToLower();
           if (kaufenJaNein == "y")
           {
               Kaufen();
           }
           else if (kaufenJaNein == "n")
           {
               Console.WriteLine("Möchtest du dein Haus verkaufen?");
               string verkaufenJaNein = Console.ReadLine();
               if (verkaufenJaNein == "y")
               {
                   Verkaufen();
               }
           }
       }

       public void WeiterVerkaufen()
       {
           Console.WriteLine("Möchtest du ein weiteres Haus verkaufen?");
           string verkaufenJaNein = Console.ReadLine().ToLower();
           if (verkaufenJaNein == "y")
           {
               Verkaufen();
           }
           else if (verkaufenJaNein == "n")
           {
               Console.WriteLine("Möchtest du ein Haus kaufen?");
               string kaufenJaNein = Console.ReadLine().ToLower();
               if (kaufenJaNein == "y")
               {
                   Kaufen();
               }
           }
       }
       */
    }
}