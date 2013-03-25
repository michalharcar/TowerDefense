using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense.Towers {
    class LaserTower : Tower {

        public LaserTower(Texture2D texture, Vector2 position) : base(texture, position) {
            this.damage = 1;
            this.cost = 50;   
            this.radius = 50; 
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if(target != null && laserOn==false) {
                Bullet bullet = new Bullet(bulletTexture, Vector2.Subtract(center, new Vector2(bulletTexture.Width / 2)), rotation, 6, damage);
    
            }
            for(int i = 0; i < bulletList.Count; i++) {
                Bullet bullet = bulletList[i];
                bullet.SetRotation(rotation);
                bullet.Update(gameTime);
                if(!IsInRange(bullet.Center))
                    bullet.Kill();
                if(target != null && Vector2.Distance(bullet.Center, target.Center) < 12) {
                    target.CurrentHealth -= bullet.Damage;
                    bullet.Kill();
                }
                if(bullet.IsDead()) {
                    bulletList.Remove(bullet);
                    i--;
                }
            }
        }
        
    }
}
