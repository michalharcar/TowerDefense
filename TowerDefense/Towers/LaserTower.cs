using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense.Towers {
    public class LaserTower : Tower {
    //    private Texture2D laserTexture;

        public LaserTower(Texture2D texture, Texture2D laserTexture, Vector2 position) : base(texture, laserTexture, position) {
            this.damage = 1;
            this.cost = 50;   
            this.radius = 50;
            this.name = "LaserTower";
            base.laserTexture = laserTexture;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if(target != null && laserOn==false) {
                Laser laser = new Laser(this.laserTexture, Top, (int)Vector2.Distance(Top, target.Center), damage);
                laserList.Add(laser);
                laserOn = true;
            }
            for(int i = 0; i < laserList.Count; i++) {
                Laser laser = laserList[i];
                laser.TargetForLaser = target;
                laser.SetRotation(laserRotation);
                laser.Update(gameTime);
                if(target==null)
                    laserOn = false;
                if(target != null && laserOn) {
                    target.CurrentHealth -= laser.Damage;
                }
                if(!laserOn) {
                    laserList.Remove(laser);
                    i--;
                }
            }
        }
        
    }
}
