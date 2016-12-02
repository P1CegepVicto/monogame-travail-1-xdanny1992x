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

            bool[] tableau = new bool[100];//tableau de 100 cases
            Random hasard = new Random();
            int tempo; //valeur temporaire, à convertir en bool;
            int caseActuelle = 0;
            int deplacement = 0;
            int tentativesB = 0;
            int tentativesM = 0;
            int tentativesT = 0;
            string choix = "";


            tableau[0] = true;
            tableau[99] = true;

            for (int i = 1; i < 99; i++)
            {
                tempo = hasard.Next(0, 2);
                if (tempo ==0)
                {
                    tableau[i] = false;
                }
                else if (tempo ==1)
                {
                    tableau[i] = true;
                }

            }
            Console.WriteLine("Le but du jeu est de se rendre à la case 99.");

            while (caseActuelle != 99)
            {
                Console.WriteLine("Vous êtes actuellement à la case " + caseActuelle + ".");
                Console.WriteLine("Veuillez entrer un choix de déplacement. \nA = 3 cases vers la gauche\nS = 2 cases vers la gauche\nD = 1 case vers la gauche");
                Console.WriteLine("G = 2 cases vers la droite\nH = 4 cases vers la droite");
                Console.WriteLine("Y = Pour afficher la carte complète \nP = Pour afficher les 10 prochaines cases");
                choix = Console.ReadLine();

                switch (choix.ToLower())
                {
                    case "a":
                        {
                            deplacement = -3;
                            break;
                        }
                    case "s":
                        {
                            deplacement = -2;
                            break;
                        }
                    case "d":
                        {
                            deplacement = -1;
                            break;
                        }
                    case "g":
                        {
                            deplacement = 2;
                            break;
                        }
                    case "h":
                        {
                            deplacement = 4;
                            break;
                        }
                    case "q":
                        {
                            deplacement = 10;
                            break;
                        }
                    case "y":
                        {
                            deplacement = 20;
                            break;
                        }
                    case "p":
                        {
                            deplacement = 30;
                            break;
                        }

                }

                if (caseActuelle + deplacement > -1 && caseActuelle + deplacement < 99)
                {
                    if (deplacement == 10)
                        break;
                    else if (deplacement == 20)
                    {
                        for (int i = 0; i < tableau.Length; i++)
                        {
                            Console.WriteLine(tableau[i]);

                        }
                        Console.ReadLine();
                    }
                    else if (deplacement ==30)
                    {
                        if (caseActuelle < 89)
                        {
                            for(int i = caseActuelle+1; i < caseActuelle+10; i++)
                            {
                                Console.WriteLine(tableau[i]);
                            }
                            Console.ReadLine();
                        }
                        else
                        {
                            for (int i = caseActuelle+1; i<tableau.Length;i++)
                            {
                                Console.WriteLine(tableau[i]);
                            }
                            Console.ReadLine();
                        }
                    }


                    else if (tableau[caseActuelle + deplacement] == true)
                    {
                        caseActuelle += deplacement;
                        tentativesB++;
                    }
                    else if (tableau[caseActuelle + deplacement] != true)
                    {
                        tentativesM++;
                        Console.WriteLine("Si vous faites ce déplacement, vous atterissez droit dans un 'False'. \nVeuillez faire un autre choix de déplacement.");
                        Console.ReadLine();
                    }
                }
                else 
                {
                    tentativesM++;
                    Console.WriteLine("Si vous faites ce déplacement, vous atterissez hors du tableau. \nVeuillez faire un autre choix de déplacement.");
                    Console.ReadLine();
                }

                Console.Clear();
            }
            if (caseActuelle == 99)
            {
                tentativesT = tentativesM + tentativesB;
                Console.WriteLine("Félicitation, vous venez d'arriver à la case 99.");
                Console.WriteLine("Pour y arriver, vous avez fait " + tentativesB + " bonne tentatives, ainsi que \n" + tentativesM + " mauvaises tentatives, pour un grand total de " + tentativesT + ".");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Meilleure chance la prochaine fois, au revoir!");
                Console.ReadLine();
            }


        }

    }
}

