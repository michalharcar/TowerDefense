using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerDefense.Enemies;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense.Towers
{
    public class SlowTower : Tower {
        // Defines how fast an enemy will move when hit.
        private float speedModifier;
        private float modifierDuration;
        public float ModifierDuration { get { return modifierDuration; } set { modifierDuration = value; } }

        public SlowTower(Texture2D texture, Texture2D bulletTexture, Vector2 position)
            : base(texture, bulletTexture, position) {
            this.damage = 0;
            this.cost = 15;
            this.radius = 80;
            this.name = "SlowTower";
            this.speedModifier = 0.4f;
            this.modifierDuration = 2.0f;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if(bulletTimer >= 0.75f && target != null) {
                Bullet bullet = new Bullet(bulletTexture, Vector2.Subtract(center, new Vector2(bulletTexture.Width / 2)), rotation, 6, damage);
                bulletList.Add(bullet);
                bulletTimer = 0;
            }
            for(int i = 0; i < bulletList.Count; i++) {
                Bullet bullet = bulletList[i];
                bullet.SetRotation(rotation);
                bullet.Update(gameTime);
                if(!IsInRange(bullet.Center))
                    bullet.Kill();
                if(target != null && Vector2.Distance(bullet.Center, target.Center) < 12) {
                    // Apply our speed modifier if it is better than
                    // the one currently affecting the target :
                    if(target.SpeedModifier <= speedModifier) {
                        target.SpeedModifier = speedModifier;
                        target.ModifierDuration = modifierDuration;
                    }
                    //             target.CurrentHealth -= bullet.Damage;
                    bullet.Kill();
                }
                if(!bullet.IsActive()) {
                    bulletList.Remove(bullet);
                    i--;
                }
            }
        }

        public override void GetClosestEnemy(List<Enemy> enemies) {
            target = null;
            base.GetClosestEnemy(enemies);
        }

        public override void Upgrade(Player player) {
            switch(UpgradeLevel) {
                case 1:
                    if(player.Money >= 150) {
                        radius = radius * 2;
                        modifierDuration = modifierDuration * 2;
                        player.Money -= 150;
                        player.MoneySpent += 150;
                        UpgradeLevel++;
                    }
                    break;
                case 2:
                    if(player.Money >= 300) {
                        radius = Radius * 2;
                        modifierDuration = modifierDuration * 2;
                        player.Money -= 300;
                        player.MoneySpent += 300;
                        UpgradeLevel++;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
