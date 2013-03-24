using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerDefense.Towers;

namespace TowerDefense
{
    public class Player {
        private int cellX;
        private int cellY;
        private int tileX;
        private int tileY;
        public int Money {
            get; set; }
        public int Lives {
        get; set; }
        private List<Tower> towers = new List<Tower>();
        private MouseState mouseState; // Mouse state for the current frame
        private MouseState oldState; // Mouse state for the previous frame
        private Level level;
        private Texture2D[] towerTextures;
        private Texture2D[] bulletTextures;
        // The type of tower to add.
        private string newTowerType;
        public string NewTowerType {
            set { newTowerType = value; }
        }
        // The index of the new towers texture.
        public int NewTowerIndex { get; set; }
        private Tower towerToAdd;
        public Tower TowerToAdd { get { return towerToAdd; } }


        public Player(Level level, Texture2D[] towerTextures, Texture2D[] bulletTextures)
        {
            this.level = level;
            this.towerTextures = towerTextures;
            this.bulletTextures = bulletTextures;
            towerToAdd = null;
            Money = 100;
            Lives = 10;
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)  {
            mouseState = Mouse.GetState();
            cellX = (int)(mouseState.X / 32); // Convert the position of the mouse
            cellY = (int)(mouseState.Y / 32); // from array space to level space
            tileX = cellX * 32; // Convert from array space to level space
            tileY = cellY * 32; // Convert from array space to level space
            if (mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed) {
                if (string.IsNullOrEmpty(newTowerType) == false) {
                    AddTower();
                }
            }
            foreach (Tower tower in towers) {
                if (tower.HasTarget == false) {
                    tower.GetClosestEnemy(enemies);
                }
                tower.Update(gameTime);

            }
            oldState = mouseState; // Set the oldState so it becomes the state of the previous frame.
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Tower tower in towers)  {
                tower.Draw(spriteBatch);
            }
        }

        private bool IsCellClear()  {
            bool inBounds = cellX >= 0 && cellY >= 0 && cellX < level.Width && cellY < level.RowCount; // Make sure tower is within limits              
            bool spaceClear = true;
            foreach (Tower tower in towers)  { // Check that there is no tower here
                spaceClear = (tower.Position != new Vector2(tileX, tileY));
                if (!spaceClear)
                    break;
            }
            bool onPath = (level.GetIndex(cellX, cellY) != 1);
            return inBounds && spaceClear && onPath; // If both checks are true return true
        }

        public void AddTower()  {
            
            switch (newTowerType)  {
                case "Cannon Tower":  {
                        towerToAdd = new CannonTower(towerTextures[0],
                            bulletTextures[0], new Vector2(tileX, tileY));
                        break;
                    }
                case "Spike Tower": {
                        towerToAdd = new SpikeTower(towerTextures[1],
                            bulletTextures[1], new Vector2(tileX, tileY));
                        break;
                    }
                case "Slow Tower":
                    {
                        towerToAdd = new SlowTower(towerTextures[2],
                            bulletTextures[2], new Vector2(tileX, tileY));
                        break;
                    }

            }
            // Only add the tower if there is a space and if the player can afford it.
            if (IsCellClear() == true && towerToAdd.Cost <= Money) {
                towers.Add(towerToAdd);              
                Money -= towerToAdd.Cost;
                // Reset the newTowerType field.
                newTowerType = string.Empty;
                towerToAdd = null;
            }
            else {
                newTowerType = string.Empty;
            }
        }

        public void DrawPreview(SpriteBatch spriteBatch) {
            // Draw the tower preview.
            if (string.IsNullOrEmpty(newTowerType) == false) {
                int cellX = (int)(mouseState.X / 32); // Convert the position of the mouse
                int cellY = (int)(mouseState.Y / 32); // from array space to level space
                int tileX = cellX * 32; // Convert from array space to level space
                int tileY = cellY * 32; // Convert from array space to level space
                Texture2D previewTexture = towerTextures[NewTowerIndex];
                if(level.GetIndex(cellX,cellY)!=1 && cellX<level.Map.GetLength(1) && cellY<level.Map.GetLength(0)) 
                spriteBatch.Draw(previewTexture, new Rectangle(tileX, tileY, previewTexture.Width, previewTexture.Height), Color.White);

            }
        }

    }
}
