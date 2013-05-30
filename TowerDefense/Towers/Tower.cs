using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefense.Towers;
using TowerDefense.Enemies;

namespace TowerDefense.Towers
{
    public class Tower : Sprite {
        protected int cost; 
        protected int damage; 
        protected float radius; // How far the tower can shoot
        protected Enemy target;
        protected string name;
        protected Texture2D bulletTexture;
        protected Texture2D laserTexture;
        protected float bulletTimer; // How long ago was a bullet fired
        protected List<Bullet> bulletList = new List<Bullet>();
        protected List<Laser> laserList = new List<Laser>();
        protected bool laserOn;

        public int UpgradeLevel { get; set; }
        public virtual bool HasTarget { get { return target != null; } }
        public Enemy Target {  get { return target; } }

        public int Cost { get { return cost; } }
        public int Damage  { get { return damage; } }
        public float Radius  {  get { return radius; } }

        public Tower(Texture2D texture, Vector2 position) : base(texture, position) {
            UpgradeLevel = 1;
        }

        public Tower(Texture2D texture, Texture2D bulletTexture, Vector2 position) : base(texture, position) {
            this.bulletTexture = bulletTexture;
            UpgradeLevel = 1;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            bulletTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (target != null)  {
                FaceTarget();
                if (!IsInRange(target.Center) || target.IsDead) {
                    target = null;
                    bulletTimer = 0;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            foreach (Bullet bullet in bulletList)
                bullet.Draw(spriteBatch);
            foreach(Laser laser in laserList)
                laser.Draw(spriteBatch);
            
        }

        public bool IsInRange(Vector2 position)   {
            return Vector2.Distance(center, position) <= radius;
        }

        public virtual void GetClosestEnemy(List<Enemy> enemies)  {
            if(!HasTarget) {
                float smallestRange = radius;
                foreach(Enemy enemy in enemies) {
                    if(Vector2.Distance(center, enemy.Center) < smallestRange) {
                        smallestRange = Vector2.Distance(center, enemy.Center);
                        target = enemy;
                    }
                }
            }
        }

        protected virtual void FaceTarget()  {
              Vector2 direction = center - target.Center;
              direction.Normalize();
              rotation = (float) Math.Atan2(-direction.X, direction.Y);  
            }

        public string getName(){
            return name;
        }

        public virtual void Upgrade(Player player) {
            switch(UpgradeLevel) {
                case 1:
                    if(player.Money >= 150) {
                        damage = Damage * 2;
                        radius = Radius * 2;
                        player.Money -= 150;
                        player.MoneySpent += 150;
                        UpgradeLevel++;
                    }
                    break;
                case 2:
                    if(player.Money >= 300) {
                        damage = Damage * 2;
                        radius = Radius * 2;
                        player.Money -= 300;
                        player.MoneySpent += 300;
                        UpgradeLevel++;
                    }
                    break;
                default:
                    break;
            }
        }

        public void Sell(Player player) {
            player.Money += (Cost + (150 * (UpgradeLevel-1))) / 2;
            player.towers.Remove(this);
            
        }

    }
}
