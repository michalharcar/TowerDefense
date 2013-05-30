using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerDefense.Towers;
using TowerDefense.Enemies;

namespace TowerDefense
{
    public class Player {
        private int cellX;
        private int cellY;
        private int tileX;
        private int tileY;
        public int Money { get; set; }
        public int Lives { get; set; }
        public List<Tower> towers = new List<Tower>();
        private MouseState mouseState; // Mouse state for the current frame
        private MouseState oldState; // Mouse state for the previous frame
        private Level level;
        private Texture2D[] towerTextures;
        private Texture2D[] bulletTextures;
        private Texture2D laserTexture;
        private string newTowerType;
        public string NewTowerType { set { newTowerType = value; } }
        public int NewTowerIndex { get; set; }  // The index of the new towers texture.
        private Tower towerToAdd;
        public Tower TowerToAdd { get { return towerToAdd; } }
        public bool EnoughGold { get; set; }      
        public int TowersCreated { get; private set; }
        public int MoneySpent { get; set; }


        public Player(Level level, Texture2D[] towerTextures, Texture2D[] bulletTextures, Texture2D laserTexture) {
            this.level = level;
            this.towerTextures = towerTextures;
            this.bulletTextures = bulletTextures;
            this.laserTexture = laserTexture;
            towerToAdd = null;
            Money = 1000;
            Lives = 10;
            EnoughGold = true;
            TowersCreated = 0;
            MoneySpent = 0;
        }

        public void Update(GameTime gameTime, List<Enemy> enemies) {
            mouseState = Mouse.GetState();
            cellX = (int) (mouseState.X / 32); // Convert the position of the mouse
            cellY = (int) (mouseState.Y / 32); // from array space to level space
            tileX = cellX * 32; // Convert from array space to level space
            tileY = cellY * 32; // Convert from array space to level space
            if(mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed) {
                if(string.IsNullOrEmpty(newTowerType) == false) {
                    EnoughGold = true;
                    AddTower();
                }
            }
            if(mouseState.RightButton == ButtonState.Released && oldState.RightButton == ButtonState.Pressed) {
                  if(TowerToAdd!=null) {  
                    towerToAdd = null;
                    newTowerType = string.Empty;
                }
            }

            foreach(Tower tower in towers) {
                tower.GetClosestEnemy(enemies);
                tower.Update(gameTime);
            }
            oldState = mouseState; // Set the oldState so it becomes the state of the previous frame.
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach(Tower tower in towers) {
                tower.Draw(spriteBatch);
            }
        }

        private bool IsCellClear() {
            bool inBounds = cellX >= 0 && cellY >= 0 && cellX < level.Width && cellY < level.Height; // Make sure tower is within limits              
            bool spaceClear = true;
            foreach(Tower tower in towers) { // Check that there is no tower here
                spaceClear = (tower.Position != new Vector2(tileX, tileY));
                if(!spaceClear)
                    break;
            }
            bool onPath = (level.GetIndex(cellX, cellY) != 1);
            return inBounds && spaceClear && onPath; // If both checks are true return true
        }

        public void AddTower() {

            switch(newTowerType) {
                case "Cannon Tower": {
                        towerToAdd = new CannonTower(towerTextures[0],
                            bulletTextures[0], new Vector2(tileX, tileY));
                        break;
                    }
                case "Spike Tower": {
                        towerToAdd = new SpikeTower(towerTextures[1],
                            bulletTextures[1], new Vector2(tileX, tileY));
                        break;
                    }
                case "Slow Tower": {
                        towerToAdd = new SlowTower(towerTextures[2],
                            bulletTextures[2], new Vector2(tileX, tileY));
                        break;
                    }
                case "Laser Tower": {
                        towerToAdd = new LaserTower(towerTextures[3],
                            laserTexture, new Vector2(tileX, tileY));
                        break;
                    }

            }
            // Only add the tower if there is a space and if the player can afford it.
            if(IsCellClear() == true && towerToAdd.Cost <= Money) {
                EnoughGold = true;
                towers.Add(towerToAdd);
                Money -= towerToAdd.Cost;
                TowersCreated++;
                MoneySpent += towerToAdd.Cost;
                // Reset the newTowerType field.
                newTowerType = string.Empty;
                towerToAdd = null;
            }
            else if(IsCellClear() == true && towerToAdd.Cost > Money) {
                newTowerType = string.Empty;
                towerToAdd = null;
                EnoughGold = false;
            }
            else {
                  newTowerType = string.Empty;
            }
        }

        public void DrawPreview(SpriteBatch spriteBatch) {
            // Draw the tower preview.
            if(string.IsNullOrEmpty(newTowerType) == false) {
                Texture2D previewTexture = towerTextures[NewTowerIndex];
                if(level.GetIndex(cellX, cellY) != 1 && cellX < level.Map.GetLength(1) && cellY < level.Map.GetLength(0))
                    spriteBatch.Draw(previewTexture, new Rectangle(tileX, tileY, previewTexture.Width, previewTexture.Height), Color.White);

            }
        }

    }
}
