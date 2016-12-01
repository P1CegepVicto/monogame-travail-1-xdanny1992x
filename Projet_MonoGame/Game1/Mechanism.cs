using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Mechanism
    {
        public Texture2D sprite;
        public Rectangle position;
        public Rectangle spriteAfficher; //Le rectangle affiché à l'écran

        public enum etats { idle, moving };
        public etats objetState;


        //Compteur qui changera le sprite affiché
        public int cpt = 0;

        public int state = 0;
        public int nbEtatUp = 4;
        public int nbEtatDown = 4;

        public Rectangle[] tabGear =
        {
            new Rectangle(0, 0, 104, 41),
            new Rectangle(0, 42, 104, 41),
            new Rectangle(0, 84, 104, 41),
            new Rectangle(0, 126, 104, 41)
        };

        public Rectangle[] tabPulley =
        {
            new Rectangle(0, 236, 179, 73),
            new Rectangle(0, 151, 179, 73),
            new Rectangle(0, 73, 179, 73),
            new Rectangle(0, 0, 179, 73)

        };



    }
}
