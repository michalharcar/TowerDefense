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
        public int Damage { get { return damage; }  }
        private bool active = true;
        private int speed;    
        
        // for Cannon and SlowTower
        public Bullet(Texture2D texture, Vector2 position, float rotation, int speed, int damage) : base(texture, position) {           
            this.damage = damage;
            this.speed = speed;
            this.rotation = rotation;
        }

        // for SpikeTower
        public Bullet(Texture2D texture, Vector2 position, Vector2 velocity, int speed, int damage) : base(texture, position) {
            this.damage = damage;
            this.speed = speed;
            this.velocity = velocity * speed;
        }

        public override void Update(GameTime gameTime) {
            position += velocity;
            base.Update(gameTime);
        }

        public void SetRotation(float value) {
            rotation = value;
            velocity = Vector2.Transform(new Vector2(0, -speed), Matrix.CreateRotationZ(rotation));
        }
        
        public bool IsActive() {
            return active;
        }

        public void Kill()  {
            active = false;
        }

    }      
}
