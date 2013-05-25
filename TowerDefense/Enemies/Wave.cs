using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense.Enemies
{
    class Wave {
        private int numOfEnemies; // Number of enemies to spawn
        private int waveNumber;
        private float spawnTimer = 0; // When should we spawn an enemy
        private int enemiesSpawned = 0; // How many enemies have spawned
        private bool spawningEnemies; 
        private Level level; 
        private Texture2D[] enemyTextures; 
        public List<Enemy> enemies = new List<Enemy>(); 
        private Player player; 
        private Texture2D healthTexture;
        public bool RoundOver {
            get { return enemies.Count == 0 && enemiesSpawned == numOfEnemies; }
        }
        public List<Enemy> Enemies {
            get { return enemies; }
        }

        public Wave(int waveNumber, int numOfEnemies, Player player, Level level, Texture2D[] enemyTextures, Texture2D healthTexture) {
            this.waveNumber = waveNumber;
            this.numOfEnemies = numOfEnemies;
            this.player = player;
            this.level = level;
            this.enemyTextures = enemyTextures;
            this.healthTexture = healthTexture;
        }

        private void AddEnemy() {
            Enemy enemy;
            if((waveNumber % 2 == 0) && (enemiesSpawned % 3 == 0)) {
                    enemy = new Bird(enemyTextures[1], level.Waypoints.Peek());
                    enemy.SetWaypoints(level.Waypoints);
                    enemies.Add(enemy);
            }
            if((waveNumber % 3 == 0) && (enemiesSpawned % 6 == 0)) {
                enemy = new Boss(enemyTextures[2], level.Waypoints.Peek());
                enemy.SetWaypoints(level.Waypoints);
                enemies.Add(enemy);
            }
            else {
                enemy = new Spider(enemyTextures[0], level.Waypoints.Peek());
                enemy.SetWaypoints(level.Waypoints);
                enemies.Add(enemy);
            }
            spawnTimer = 0;
            enemiesSpawned++;
        }

        public void Start() {
            spawningEnemies = true;
        }

        public void Update(GameTime gameTime) {
            if (enemiesSpawned == numOfEnemies)
                spawningEnemies = false; // We have spawned enough enemies
            if (spawningEnemies) {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (spawnTimer > 1.5)
                    AddEnemy(); 
            }
            // loops throuh the list of enemies and updates them
            for (int i = 0; i < enemies.Count; i++)  {
                Enemy enemy = enemies[i];
                enemy.Update(gameTime);
                if (enemy.IsDead) {
                    if (enemy.CurrentHealth > 0) {    // enemy at end of path  
                        player.Lives--;
                    }
                    else {
                        player.Money += enemy.GoldGiven;
                    }
                    enemies.Remove(enemy);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Enemy enemy in enemies) {
                enemy.Draw(spriteBatch);

                // healthbar
                Rectangle healthRectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y-5, healthTexture.Width, healthTexture.Height);
                spriteBatch.Draw(healthTexture, healthRectangle, Color.Gray);
                float healthPercentage = enemy.HealthPercentage;
                float visibleWidth = (float)healthTexture.Width * healthPercentage;

                healthRectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y-5, (int)(visibleWidth), healthTexture.Height);
                float red = (healthPercentage < 0.5 ? 1 : 1 - (2 * healthPercentage - 1));
                float green = (healthPercentage > 0.5 ? 1 : (2 * healthPercentage));
                Color healthColor = new Color(red, green, 0);
                spriteBatch.Draw(healthTexture, healthRectangle, healthColor);
            }
        }
 
    }
}
