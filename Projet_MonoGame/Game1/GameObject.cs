using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_03
{
    class GameObject
    {
        // Tout ce qui est : fond, décor, ou objet inanimé (par sprite).
        public Texture2D sprite;
        public Vector2 position;
        public float speed;
        public int height;
        public bool isAlive;
        public Rectangle rectCollision = new Rectangle();
        public Rectangle GetRect()
        {
            rectCollision.X = (int)this.position.X;
            rectCollision.Y = (int)this.position.Y;
            rectCollision.Width = (int)this.sprite.Width;
            rectCollision.Height = (int)this.sprite.Height;
            return rectCollision;

        }

    }
}
