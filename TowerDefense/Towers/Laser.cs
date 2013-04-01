using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefense.Enemies;

namespace TowerDefense.Towers {
    public class Laser : Tower {
        private Rectangle laser;
        public Enemy TargetForLaser;
        public int Length { get { return laser.Width; } set { laser.Width = value; } }

        public Laser(Texture2D laserTexture, Vector2 position, int length, int damage) : base(laserTexture, position) {
            laserOn = false;
            this.laserTexture = laserTexture;
            this.damage = damage;
            laser = new Rectangle(0, 0, length, 3);
            Length = length;     
        }

        public override void Update(GameTime gameTime) {
            if(TargetForLaser!=null)           
            Length = (int) Vector2.Distance(Top, TargetForLaser.Center);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch, laser, laserRotation, new Vector2(2,2));
            }

        public void SetRotation(float value) {
            laserRotation = value;
        }

    }
}
