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
        public SpriteFont font;
        private Vector2 position;
        private Vector2 textPos;  
        private Vector2 towerTextPos;
        private Vector2 goldPos;
        private Vector2 lifePos;
        private Texture2D goldTexture;
        private Texture2D lifeTexture;

        public Toolbar(Texture2D toolbarTexture, Texture2D goldTexture, Texture2D lifeTexture, SpriteFont font, Vector2 position, Level level) {
            this.toolbarTexture = toolbarTexture;
            this.goldTexture = goldTexture;
            this.lifeTexture = lifeTexture;
            this.font = font;
            this.position = position;
            towerTextPos = new Vector2(10, position.Y + 42);
            textPos = new Vector2((level.Map.GetLength(1) - 3) * 32, position.Y + 10);
            goldPos = new Vector2((level.Map.GetLength(1) - 4) * 32, position.Y);
            lifePos = new Vector2((level.Map.GetLength(1) - 2) * 32, position.Y);
            
        }

        public void Draw(SpriteBatch spriteBatch, Player player)  {
            spriteBatch.Draw(toolbarTexture, position, Color.White);
            spriteBatch.Draw(goldTexture, goldPos, Color.White);
            spriteBatch.Draw(lifeTexture, lifePos, Color.White);
            string text = string.Format(" {0}                {1}", player.Money, player.Lives);
            spriteBatch.DrawString(font, text, textPos, Color.White);
            if(player.TowerToAdd != null) {
                string towerText;
                if(player.TowerToAdd is SlowTower) {
                    spriteBatch.DrawString(font, "SlowTower:", towerTextPos, Color.Blue);
                    towerText = string.Format("                         Cost: {0}   Radius: {1}   Damage: slow effect", player.TowerToAdd.Cost, player.TowerToAdd.Radius);
                }
                else if(player.TowerToAdd is CannonTower) {
                    spriteBatch.DrawString(font, "CannonTower:", towerTextPos, Color.Orange);
                    towerText = string.Format("                             Cost: {0}   Radius: {1}   Damage: {2}", player.TowerToAdd.Cost, player.TowerToAdd.Radius, player.TowerToAdd.Damage);
                }
                else {
                    spriteBatch.DrawString(font, "SpikeTower:", towerTextPos, Color.Green);
                    towerText = string.Format("                          Cost: {0}   Radius: {1}   Damage: {2}", player.TowerToAdd.Cost, player.TowerToAdd.Radius, player.TowerToAdd.Damage);
                }
                spriteBatch.DrawString(font, towerText, towerTextPos, Color.White);
            }
        }

    }
}
