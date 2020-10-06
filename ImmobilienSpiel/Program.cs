using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Channels;

namespace ImmobilienSpiel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.WriteLine("DAS IMMOBILIENSPIEL");

            Logik logik = new Logik();
            logik.Spiel(); 
        }
    }
}
