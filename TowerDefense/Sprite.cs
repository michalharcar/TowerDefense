﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense
{
    public class Sprite {
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 center;
        protected Vector2 origin;
        protected float rotation;
        protected float laserRotation;
        public Vector2 Center {
            get { return center; }
        }
        protected Vector2 top;
        public Vector2 Top {
            get { return top; }
        }
        public Vector2 Position {
            get { return position; }
        }

        public Sprite(Texture2D tex, Vector2 pos)
        {
            texture = tex;
            position = pos;
            velocity = Vector2.Zero;
            //center = new Vector2(position.X + texture.Width /2, position.Y + texture.Height / 2);
            //origin = new Vector2(texture.Width / 2, texture.Height / 2);
            center = new Vector2(position.X + 16, position.Y + 16);
            origin = new Vector2(16, 16);
            top = new Vector2(position.X + 16, position.Y+4);
        }

        public virtual void Update(GameTime gameTime) {
         //   this.center = new Vector2(position.X + texture.Width / 2,position.Y + texture.Height / 2);
            this.center = new Vector2(position.X + 16, position.Y + 16);

        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, center, null, Color.White,rotation, origin, 1.0f, SpriteEffects.None, 0);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Rectangle rectangle) {
            spriteBatch.Draw(texture, center, rectangle, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Rectangle rectangle, float laserRotation, Vector2 origin) {
            spriteBatch.Draw(texture, position, rectangle, Color.White, laserRotation, origin, 1.0f, SpriteEffects.None, 0);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Color color) {
            spriteBatch.Draw(texture, center, null, color, rotation, origin, 1.0f, SpriteEffects.None, 0);
        }

    }
}
