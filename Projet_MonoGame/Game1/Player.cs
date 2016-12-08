using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_03
{
    class Player
    {
        public int live;
        public Texture2D sprite;
        public Rectangle position;
        public Rectangle spriteAfficher; //Le rectangle affiché à l'écran

        public enum etats { idle, shoot };
        public etats objetState;


        //Compteur qui changera le sprite affiché
        public int cpt = 0;

        public int shootState = 0; //État de départ
        public int nbEtatShoot = 5; //Combien il y a de rectangles pour l’état “shoot”
        public Rectangle[] tabShoot = {
            new Rectangle(260, 144, 130, 144),
            new Rectangle(0, 288, 130, 144),
            new Rectangle(130, 288, 130, 144),
            new Rectangle(260, 288, 130, 144),
            new Rectangle(0, 432, 130, 144)};

        public int idleState = 0; //Combien d’état pour l’état attendre
        public int nbEtatIdle = 15;
        public Rectangle[] tabIdle =
        {
            new Rectangle(0, 0, 130, 144),
            new Rectangle(0, 0, 130, 144),
            new Rectangle(0, 0, 130, 144),
            new Rectangle(130, 0, 130, 144),
            new Rectangle(130, 0, 130, 144),
            new Rectangle(130, 0, 130, 144),
            new Rectangle(260, 0, 130, 144),
            new Rectangle(260, 0, 130, 144),
            new Rectangle(260, 0, 130, 144),
            new Rectangle(0, 144, 130, 144),
            new Rectangle(0, 144, 130, 144),
            new Rectangle(0, 144, 130, 144),
            new Rectangle(130, 144, 130, 144),
            new Rectangle(130, 144, 130, 144),
            new Rectangle(130, 144, 130, 144)

        };


    }
}
