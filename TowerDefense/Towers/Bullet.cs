using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense.Towers
{
    public class Bullet : Sprite  {
        private int damage;
        private int age;
        private int speed;
        public int Damage   {
            get { return damage; }
        }
        
        public Bullet(Texture2D texture, Vector2 position, float rotation, int speed, int damage) : base(texture, position) {
            this.rotation = rotation;
            this.damage = damage;
            this.speed = speed;
        }

        public Bullet(Texture2D texture, Vector2 position, Vector2 velocity, int speed, int damage) : base(texture, position) {
            this.damage = damage;
            this.speed = speed;
            this.velocity = velocity * speed;
        }

        public override void Update(GameTime gameTime) {
            age++;
            position += velocity;
            base.Update(gameTime);
        }

        public void SetRotation(float value) {
            rotation = value;
            velocity = Vector2.Transform(new Vector2(0, -speed), Matrix.CreateRotationZ(rotation));
        }
        
        public bool IsDead() {
            return age > 100;
        }

        public void Kill()  {
            this.age = 200;
        }

    }      
}
