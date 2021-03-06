﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_02
{
    class GameObject
    {
        public bool isAlive;
        public Texture2D sprite;
        public Vector2 position;
        public Vector2 velocity;
        public int lives;
        public float speed;
        public int randomDirection = 0;
        public float timeSinceLast = 0f;
        public float duration = 0;
        public Vector2 origin;
        public float rotation;
        public bool canBeHurt = true;
        public bool isOnScreen = false;
        public Vector2 posDepart;
        public float tanVelocity = 5f;
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
