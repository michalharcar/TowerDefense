using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TowerDefense.GUI
{
    class Toolbar {
        private Texture2D texture;
        private SpriteFont font;
        private Vector2 position;   // The position of the toolbar
        private Vector2 textPosition;  // The position of the text

        public Toolbar(Texture2D texture, SpriteFont font, Vector2 position)  {
            this.texture = texture;
            this.font = font;
            this.position = position;
            // Offset the text to the bottom right corner
            textPosition = new Vector2(130, position.Y + 10);
            
        }

        public void Draw(SpriteBatch spriteBatch, Player player)  {
            spriteBatch.Draw(texture, position, Color.White);
            string text = string.Format("Gold : {0} Lives : {1}", player.Money, player.Lives);
            spriteBatch.DrawString(font, text, textPosition, Color.White);

        }

    }
}
