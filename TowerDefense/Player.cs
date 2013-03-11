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
        private int money = 100;
        private int lives = 10;
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

        public int Money {
            get { return money; }
        }
        public int Lives {
            get { return lives; }
        }

        public Player(Level level, Texture2D[] towerTextures, Texture2D[] bulletTextures)
        {
            this.level = level;
            this.towerTextures = towerTextures;
            this.bulletTextures = bulletTextures;
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
            bool inBounds = cellX >= 0 && cellY >= 0 && cellX < level.ColCount && cellY < level.RowCount; // Make sure tower is within limits              
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
            Tower towerToAdd = null;
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

            }
            // Only add the tower if there is a space and if the player can afford it.
            if (IsCellClear() == true && towerToAdd.Cost <= money) {
                towers.Add(towerToAdd);
                money -= towerToAdd.Cost;
                // Reset the newTowerType field.
                newTowerType = string.Empty;
            }
        }

    }
}
