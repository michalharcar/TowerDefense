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
        private SpriteFont font;
        private Vector2 position;
        private Vector2 textPos;  
        private Vector2 towerTextPos;
        private Vector2 goldPos;
        private Vector2 lifePos;
        private Texture2D goldTexture;
        private Texture2D lifeTexture;
        public bool Upgrading { get; set; }
        private Tower tower;

        public Toolbar(Texture2D toolbarTexture, Texture2D goldTexture, Texture2D lifeTexture, SpriteFont font, Vector2 position, Level level) {
            this.toolbarTexture = toolbarTexture;
            this.goldTexture = goldTexture;
            this.lifeTexture = lifeTexture;
            this.font = font;
            this.position = position;
            towerTextPos = new Vector2(10, position.Y + 42);
            textPos = new Vector2((level.Width - 3) * 32, position.Y + 10);
            goldPos = new Vector2((level.Width - 4) * 32, position.Y);
            lifePos = new Vector2((level.Width - 2) * 32, position.Y);
            Upgrading = false;
            
        }

        public void Draw(SpriteBatch spriteBatch, Player player)  {
            spriteBatch.Draw(toolbarTexture, position, Color.White);
            spriteBatch.Draw(goldTexture, goldPos, Color.White);
            spriteBatch.Draw(lifeTexture, lifePos, Color.White);
            string text = string.Format(" {0}                {1}", player.Money, player.Lives);
            spriteBatch.DrawString(font, text, textPos, Color.White);
            if(player.TowerToAdd != null) {
                string towerText;
                if(Upgrading){
                    if(tower is CannonTower){
                        spriteBatch.DrawString(font, "CannonTower:", towerTextPos, Color.Orange);
                    }
                    if(tower is SlowTower){
                        spriteBatch.DrawString(font, "SlowTower:", towerTextPos, Color.Blue);
                        if(tower.UpgradeLevel==1){
                            towerText = string.Format("                lvl 1(lvl 2)  Radius: {1}({2})  Slowmo duration: {3}({4})", tower.Radius, tower.Radius * 2, ((SlowTower) tower).SpeedModifier, ((SlowTower) tower).SpeedModifier*2);
                    }
                    else{
                        towerText = string.Format("                lvl 2 - fully upgraded");
                    }
                    }
                    if(tower is LaserTower){
                        spriteBatch.DrawString(font, "LaserTower:", towerTextPos, Color.Red);
                    }
                    if(tower is SpikeTower){
                        spriteBatch.DrawString(font, "SpikeTower:", towerTextPos, Color.Green);
                    }
                    if(!(tower is SlowTower)){ 
                    if(tower.UpgradeLevel==1){
                    towerText = string.Format("                lvl 1(lvl 2)  Radius: {1}({2})  Damage: {3}({4})",tower.Radius,tower.Radius*2,tower.Damage,tower.Damage*2);
                    }
                    else{
                        towerText = string.Format("                lvl 2(lvl 3)  Radius: {1}({2})  Damage: {3}({4})",tower.Radius,tower.Radius*2,tower.Damage,tower.Damage*2);
                    }
                    spriteBatch.DrawString(font, towerText, towerTextPos, Color.White);
                    }
                        
                } else {
                if(player.TowerToAdd is SlowTower) {
                    spriteBatch.DrawString(font, "SlowTower:", towerTextPos, Color.Blue);
                    towerText = string.Format("                         Cost: {0}   Radius: {1}   Damage: slow effect", player.TowerToAdd.Cost, player.TowerToAdd.Radius);
                }
                else if(player.TowerToAdd is CannonTower) {
                    spriteBatch.DrawString(font, "CannonTower:", towerTextPos, Color.Orange);
                    towerText = string.Format("                             Cost: {0}   Radius: {1}   Damage: {2}", player.TowerToAdd.Cost, player.TowerToAdd.Radius, player.TowerToAdd.Damage);
                }
                else if(player.TowerToAdd is SpikeTower) {
                    spriteBatch.DrawString(font, "SpikeTower:", towerTextPos, Color.Green);
                    towerText = string.Format("                          Cost: {0}   Radius: {1}   Damage: {2}", player.TowerToAdd.Cost, player.TowerToAdd.Radius, player.TowerToAdd.Damage);
                }
                else {
                    spriteBatch.DrawString(font, "LaserTower:", towerTextPos, Color.Red);
                    towerText = string.Format("                          Cost: {0}   Radius: {1}   Damage: {2}", player.TowerToAdd.Cost, player.TowerToAdd.Radius, player.TowerToAdd.Damage);
                }
                spriteBatch.DrawString(font, towerText, towerTextPos, Color.White);
            }
           
            if(!player.EnoughGold) {
                spriteBatch.DrawString(font, "Not enough coins for this tower", towerTextPos, Color.Red);
            }
            }
        }

        public void setTower(Tower tower){
            this.tower = tower;
        }

    }
}
