using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Enemy
    {
        public bool isAlive = true;
        public Texture2D sprite;
        public Rectangle position;
        public float speed;
        public float fall;
        public bool isDead = false;
        public bool canKill = true;
        public Rectangle rectCollision = new Rectangle();
        public Rectangle GetRect()
        {
            rectCollision.X = (int)this.position.X;
            rectCollision.Y = (int)this.position.Y;
            rectCollision.Width = (int)this.sprite.Width;
            rectCollision.Height = 44;
            return rectCollision;

        }

        public Rectangle spriteAfficher; //Le rectangle affiché à l'écran

        public enum etats { flying, dead };
        public etats objetState;


        //Compteur qui changera le sprite affiché
        public int cpt = 0;

        public int state = 0;
        public int nbEtatFlying = 2;
        public int nbEtatDead = 1;

        public Rectangle[] tabFlying =
        {
            new Rectangle(0, 0, 64, 43),
            new Rectangle(0, 44, 64, 42)
        };

        public Rectangle[] tabDead =
        {
            new Rectangle(0, 88, 64, 44),
            new Rectangle(0, 88, 64, 44)
        };

    }
}
