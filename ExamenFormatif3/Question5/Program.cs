using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question5
{
    class Program
    {
        static void Main(string[] args)
        {

            bool[] tableau = new bool[100];
            Random hasard = new Random();
            int tempo;
            int caseActuelle = 0;


            tableau[0] = true;
            tableau[99] = true;

            for (int i = 1; i<99; i++)
            {
                tempo = hasard.Next(0, 2);
                if (tempo == 0)
                {
                    tableau[i] = false;
                }
                else
                {
                    tableau[i] = true;
                }
            }





        }
    }
}
