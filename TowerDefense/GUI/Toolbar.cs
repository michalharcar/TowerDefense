using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using TowerDefense.Towers;

namespace TowerDefense.GUI
{
    class Toolbar {
        private Texture2D toolbarTexture;
        private Texture2D wallsTexture;
        private Texture2D goldTexture;
        private Texture2D lifeTexture;
        private SpriteFont font;
        private Vector2 position;
        private Vector2 textPos;  
        private Vector2 towerTextPos;
        private Vector2 towerTextPos2;
        private Vector2 towerTextPos3;
        private Vector2 goldPos;
        private Vector2 lifePos;
        private Vector2 finalPos;
        private Vector2 wallsPos;       
        public bool Upgrading { get; set; }
        private Tower tower;
        private Level level;

        public Toolbar(Texture2D walls, Texture2D toolbarTexture, Texture2D goldTexture, Texture2D lifeTexture, SpriteFont font, Vector2 position, Level level) {
            this.wallsTexture = walls;
            this.toolbarTexture = toolbarTexture;
            this.goldTexture = goldTexture;
            this.lifeTexture = lifeTexture;
            this.font = font;
            this.position = position;
            this.level = level;
            towerTextPos = new Vector2(10, position.Y + 42);
            towerTextPos3 = new Vector2(10, position.Y + 70);
            textPos = new Vector2((level.Width - 3) * 32, position.Y + 10);
            goldPos = new Vector2((level.Width - 4) * 32, position.Y);
            lifePos = new Vector2((level.Width - 2) * 32, position.Y);
            finalPos = new Vector2((level.Width/2) * 32-32, 10);
            wallsPos = new Vector2(0, position.Y -32);
            Upgrading = false;
            
        }

        public void Draw(SpriteBatch spriteBatch, Player player)  {
            spriteBatch.Draw(wallsTexture, wallsPos, Color.White);
            spriteBatch.Draw(toolbarTexture, position, Color.White);
            spriteBatch.Draw(goldTexture, goldPos, Color.White);
            spriteBatch.Draw(lifeTexture, lifePos, Color.White);
            string text = string.Format(" {0}              {1}", player.Money, player.Lives);
            spriteBatch.DrawString(font, text, textPos, Color.White);
            string towerText;
            string towerText3;
            if(Upgrading) {
              //  towerText = string.Format("");
                towerTextPos2 = new Vector2(12 + tower.getName().Length * 8, position.Y + 42);
                spriteBatch.DrawString(font, tower.getName() + ":  ", towerTextPos, Color.Red);
                towerText = string.Format("");
                towerText3 = string.Format("");
                if(tower is SlowTower) {
                    spriteBatch.DrawString(font, "SlowTower:", towerTextPos, Color.Blue);
                    switch(tower.UpgradeLevel) {
                        case 1:
                            towerText = string.Format("lvl 1(lvl 2)  Radius: {0}({1})  Slowmo duration: {2}({3})", tower.Radius, tower.Radius * 2, ((SlowTower) tower).ModifierDuration, ((SlowTower) tower).ModifierDuration * 2);
                            towerText3 = string.Format("UPGRADE - 150 coins      SELL + {0} coins", tower.Cost/2);
                            break;
                        case 2:
                            towerText = string.Format("lvl 2(lvl 3)  Radius: {0}({1})  Slowmo duration: {2}({3})", tower.Radius, tower.Radius * 2, ((SlowTower) tower).ModifierDuration, ((SlowTower) tower).ModifierDuration * 2);
                            towerText3 = string.Format("UPGRADE - 300 coins          SELL + {0} coins", (tower.Cost + 150) / 2);
                            break;
                        case 3:
                            towerText = string.Format("lvl 3  Radius: {0}  Slowmo duration: {1}", tower.Radius, ((SlowTower) tower).ModifierDuration);
                            towerText3 = string.Format("FULLY UPGRADED          SELL + {0} coins", (tower.Cost + 150 * 2) / 2);
                            break;
                    }
                }
                else {
                    switch(tower.UpgradeLevel) {
                        case 1:
                            towerText = string.Format("lvl 1(lvl 2)  Radius: {0}({1})   Damage: {2}({3}) ", tower.Radius, tower.Radius * 2, tower.Damage, tower.Damage * 2);
                            towerText3 = string.Format("UPGRADE - 150 coins          SELL + {0} coins", tower.Cost / 2);
                            break;
                        case 2:
                            towerText = string.Format("lvl 2(lvl 3)  Radius: {0}({1})   Damage: {2}({3})", tower.Radius, tower.Radius * 2, tower.Damage, tower.Damage * 2);
                            towerText3 = string.Format("UPGRADE - 300 coins          SELL + {0} coins", (tower.Cost + 150) / 2);
                            break;
                        case 3:
                            towerText = string.Format("lvl 3  Radius: {0}   Damage: {1}", tower.Radius, tower.Damage);
                            towerText3 = string.Format("FULLY UPGRADED          SELL + {0} coins", (tower.Cost + 150 * 2) / 2);
                            break;
                    }

                }
                spriteBatch.DrawString(font, towerText, towerTextPos2, Color.White);
                spriteBatch.DrawString(font, towerText3, towerTextPos3, Color.Blue);

            }
            else if(player.TowerToAdd != null) {
                towerTextPos2 = new Vector2(12 + player.TowerToAdd.getName().Length * 8, position.Y + 42);
                spriteBatch.DrawString(font, player.TowerToAdd.getName()+":  ", towerTextPos, Color.Blue);
                if(player.TowerToAdd is SlowTower) {
                    towerText = string.Format("Cost: {0}   Radius: {1}   Damage: slow effect", player.TowerToAdd.Cost, player.TowerToAdd.Radius);
                }
                else {
                    towerText = string.Format("Cost: {0}   Radius: {1}   Damage: {2}", player.TowerToAdd.Cost, player.TowerToAdd.Radius, player.TowerToAdd.Damage);
                }

                //if(player.TowerToAdd is SlowTower) {
                //    spriteBatch.DrawString(font, "SlowTower:", towerTextPos, Color.Blue);
                //    towerText = string.Format("                         Cost: {0}   Radius: {1}   Damage: slow effect", player.TowerToAdd.Cost, player.TowerToAdd.Radius);
                //}
                //else if(player.TowerToAdd is CannonTower) {
                //    spriteBatch.DrawString(font, "CannonTower:", towerTextPos, Color.Orange);
                //    towerText = string.Format("                             Cost: {0}   Radius: {1}   Damage: {2}", player.TowerToAdd.Cost, player.TowerToAdd.Radius, player.TowerToAdd.Damage);
                //}
                //else if(player.TowerToAdd is SpikeTower) {
                //    spriteBatch.DrawString(font, "SpikeTower:", towerTextPos, Color.Green);
                //    towerText = string.Format("                          Cost: {0}   Radius: {1}   Damage: {2}", player.TowerToAdd.Cost, player.TowerToAdd.Radius, player.TowerToAdd.Damage);
                //}
                //else {
                //    spriteBatch.DrawString(font, "LaserTower:", towerTextPos, Color.Red);
                //    towerText = string.Format("                          Cost: {0}   Radius: {1}   Damage: {2}", player.TowerToAdd.Cost, player.TowerToAdd.Radius, player.TowerToAdd.Damage);
                //}
                spriteBatch.DrawString(font, towerText, towerTextPos2, Color.White);
            }
                if(!player.EnoughGold) {
                    spriteBatch.DrawString(font, "Not enough coins for this tower", towerTextPos, Color.Red);
                }

                if(level.GameState == State.FINISHED) {
                    string finalText = "You finished level " + level.LvlNumber;
                    spriteBatch.DrawString(font, finalText, finalPos, Color.Black);
                }

                    if(level.GameState == State.PAUSED) {
                string pauseText = string.Format("Playing time: {0} seconds", level.PlayingTime);
                spriteBatch.DrawString(font, pauseText, finalPos, Color.Black);
                }
                
        }

        public void setTower(Tower tower){
            this.tower = tower;
        }

    }
}
