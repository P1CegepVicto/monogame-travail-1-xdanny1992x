using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            float montant;

            int terme;

            int taux;

            //valeur à la fin d’une année

            float valeur = 0;


            Console.WriteLine("Veuillez entrer un montant.");

            montant = float.Parse(Console.ReadLine());

            Console.WriteLine("Veuillez entrer un terme (en années).");

            terme = int.Parse(Console.ReadLine());

            Console.WriteLine("Veuillez entrer un taux d’intérêt.");

            taux = int.Parse(Console.ReadLine());



            Console.WriteLine("Prêt de" + montant + "$ à " + taux + "% annuel avec un terme de " + terme + " ans : ");


            for (int i = 0; i < terme; i++)

            {

                valeur = montant + (montant / taux);


                Console.WriteLine("Année #" + (i + 1) + ": " + montant + "$ à " + taux + "% = " + valeur + "$");

                montant = valeur;

            }

            Console.WriteLine("Valeur du placement après " + terme + "ans : " + valeur + "$");

            Console.ReadLine();

        }
    }
}
    