using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace ImmobilienSpiel
{
    class DBA
    {
        SqlConnection conn;

        public DBA()
        {
            conn = new SqlConnection("Server=LWZHNBLP1;Database=Immobilien;Trusted_Connection=true");
            conn.Open();
        }

        // Methoden für Spieler --------------------------------------------------------------------------------

        // erstellt neuen Spieler
        public Spieler MakeNewPlayer(string nName, int schwierigkeitsgrad)
        {
            Spieler spieler = null;
            try
            {
                int kontostand = 0;
                switch (schwierigkeitsgrad)
                {
                    case 1:
                        kontostand = 300000;
                        break;
                    case 2:
                        kontostand = 200000;
                        break;
                    case 3:
                        kontostand = 100000;
                        break;
                }

                string insertQuery = $"INSERT INTO Spieler (Name, Kontostand) VALUES ('{nName}', '{kontostand}'";
                SqlCommand insertCommand = new SqlCommand(insertQuery, conn);
                insertCommand.ExecuteNonQuery();
                Console.WriteLine("Erfolgreich gespeichert!");

                spieler = new Spieler(nName, kontostand);
                return spieler;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        // ruft Spieler auf und setzt ihn ins aktuelle Spiel
        public Spieler GetPlayer(string name)
        {
            Spieler spieler = null;
            try
            {
                string selectQuery = $"SELECT * FROM Spieler WHERE Name = '{name}'";
                SqlCommand selectCommand = new SqlCommand(selectQuery, conn);
                SqlDataReader reader = selectCommand.ExecuteReader();

                while (reader.Read())
                {
                    spieler = new Spieler(Convert.ToString(reader["Name"]), Convert.ToInt32(reader["Kontostand"]));
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return spieler;
        }

        // Methoden für Häuser ----------------------------------------------------------------------------

        // nimmt aktuelle Häuser und setzt sie in eine Liste 
        public List<Haus> GetHaeuser()
        {
            List<Haus> liste = new List<Haus>();
            try
            {
                string selectQuery = $"SELECT * FROM Haeuser";
                SqlCommand selectCommand = new SqlCommand(selectQuery, conn);
                SqlDataReader reader = selectCommand.ExecuteReader();

                while (reader.Read())
                {
                    //Console.WriteLine($"{reader["Hausnummer"]}, {reader["Hauspreis"]}, {reader["Zimmer"]}, {reader["Farbe"]}, {reader["Besitzer"]}, {reader["Rendite"]}");
                    liste.Add(new Haus(Convert.ToInt32(reader["Hausnummer"]), Convert.ToInt32(reader["Hauspreis"]), Convert.ToInt32(reader["Zimmer"]), Convert.ToString(reader["Farbe"]), Convert.ToInt32(reader["Rendite"])));
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return liste;
        }

        // kauf eines Hauses, muss Besitzer in Häuser überschreiben, die Häuser in Besitz 1++, auf HaeusernVonSpielern
        /*   public void HausKauf(int hausnummerAuswahl, string name)
           {
               // Hausnummer --- HausID
               string selectQuery = $"SELECT ID FROM Haeuser WHERE Hausnummer {hausnummerAuswahl}";
               SqlCommand selectCommand = new SqlCommand(selectQuery, conn);
               selectCommand.CommandText = selectQuery;
               Int32 getID = (Int32)selectCommand.ExecuteScalar();

               // Name --- SpielerID
               string selectQuery2 = $"SELECT ID FROM Spieler WHERE Name {name}";
               SqlCommand selectCommand2 = new SqlCommand(selectQuery, conn);
               selectCommand.CommandText = selectQuery;
               Int32 getID2 = (Int32)selectCommand.ExecuteScalar();
               string insertQuery = $"INSERT INTO HaeuserVonSpielern (SpielerID, HausID) VALUES (${getID2}, {getID})";
           } */

        // verkauf eines Hauses, muss Besitzer in Häuser überschreiben, die Häuser in Besitz 1--, auf HaeusernVonSpielern delete
        public void HausVerkauf()
        {

        }

        public Haus HausBau(int hausnummer, int hauspreis, int zimmer, string farbe, int rendite)
        {
            try
            {
                string insertQuery = $"INSERT INTO Haeuser (Hausnummer, Hauspreis, Zimmer, Farbe, Rendite) VALUES (${hausnummer}, {hauspreis}, {zimmer}, '{farbe}', {rendite})";
                SqlCommand insertCommand = new SqlCommand(insertQuery, conn);
                insertCommand.ExecuteNonQuery();
                Console.WriteLine("Erfolgreich gespeichert!");

                Haus haus = new Haus(hausnummer, hauspreis, zimmer, farbe, rendite);
                return haus;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /* public void HausRenovation(string farbeNeu, int zimmerNeu, int hausnummer)
         {
             string updateQuery = $"UPDATE Haeuser SET (Farbe, Zimmer) = '{farbeNeu}', '{zimmerNeu}' WHERE Hausnummer = '{hausnummer}'";
             SqlCommand updateCommand = new SqlCommand(updateQuery, conn);
             updateCommand.CommandText = $"UPDATE Kaeufer SET Name = {''} WHERE Name = {'Luka}";
             updateCommand.ExecuteNonQuery();
             Console.WriteLine("Erfolgreich geändert!");
         } */

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static void InsertQuery(SqlConnection conn)
        {
            try
            {
                string nameOfPlayer = Console.ReadLine();
                string insertQuery = $"INSERT INTO Kaeufer (Name) VALUES ('{nameOfPlayer}')";
                SqlCommand insertCommand = new SqlCommand(insertQuery, conn);
                insertCommand.ExecuteNonQuery();
                Console.WriteLine("Erfolgreich gespeichert!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void SelectQuery(SqlConnection conn)
        {
            try
            {
                string selectQuery = $"SELECT * FROM Haeuser";
                SqlCommand selectCommand = new SqlCommand(selectQuery, conn);
                SqlDataReader reader = selectCommand.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Hausnummer"]}, {reader["Hauspreis"]}, {reader["besitzer"]}");
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void UpdateQuery(SqlConnection conn)
        {
            try
            {
                string updateQuery = "UPDATE Kaeufer SET Name = 'Lucas' WHERE Name = 'Luka'";
                SqlCommand updateCommand = new SqlCommand(updateQuery, conn);
                updateCommand.CommandText = $"UPDATE Kaeufer SET Name = 'Lucas' WHERE Name = 'Luka'";
                updateCommand.ExecuteNonQuery();
                Console.WriteLine("Erfolgreich geändert!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}


//public Spieler GetPlayerNameOnly(string name)
//{
//    Spieler spieler = null;
//    try
//    {
//        string selectQuery = $"SELECT ID FROM Spieler WHERE Name = '{name}'";
//        SqlCommand selectCommand = new SqlCommand(selectQuery, conn);
//        string getID = selectCommand.ExecuteScalar().ToString();
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e);
//    }
//    return spieler;
//}

//string getID = selectCommand.ExecuteScalar().ToString();

//public void CreateSqlCommand(string selectQuery, SqlConnection conn)
//{
//    SqlCommand command = new SqlCommand(selectQuery, conn);
//    command.Connection.Open();
//    command.ExecuteScalar();
//    conn.Close();
//}