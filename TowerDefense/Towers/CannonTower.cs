using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense.Towers
{
    public class CannonTower : Tower {

        public CannonTower(Texture2D texture, Texture2D bulletTexture, Vector2 position) : base(texture, bulletTexture, position){
            this.damage = 15; 
            this.cost = 15;   
            this.radius = 80; 
            this.name = "CannonTower";
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (bulletTimer >= 0.5f && target != null) {
                Bullet bullet = new Bullet(bulletTexture, Vector2.Subtract(center, new Vector2(bulletTexture.Width / 2)), rotation, 6, damage);
                bulletList.Add(bullet);
                bulletTimer = 0;
            }
            for (int i = 0; i < bulletList.Count; i++)  {
                Bullet bullet = bulletList[i];
                bullet.SetRotation(rotation);
                bullet.Update(gameTime);
                if (!IsInRange(bullet.Center))
                    bullet.Kill();
                if (target != null && Vector2.Distance(bullet.Center, target.Center) < 12) {
                    target.CurrentHealth -= bullet.Damage;
                    bullet.Kill();
                }
                if (!bullet.IsActive()) {
                    bulletList.Remove(bullet);
                    i--;
                }
            }
        }
    }
}
