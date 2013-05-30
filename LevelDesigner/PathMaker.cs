using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LevelDesigner {
    public class PathMaker {
        private int pathIndex = 0;
        private int usedPathIndex = 0;
        public int UsedPathIndex { get { return usedPathIndex; } }
        private int tilesCounter = 0;
        private int cellX;
        private int cellY;
        private int tileX;
        private int tileY;
        private bool differentTexture = false;
        private MouseState mouseState;
        private MouseState oldState;
        private Level level;
        private Texture2D[] pathTextures;
        SpriteFont font;

        public PathMaker(Level level, Texture2D[] path, SpriteFont font) {
            this.level = level;
            this.pathTextures = path;
            this.font = font;
        }


        public void Update(GameTime gameTime) {
            mouseState = Mouse.GetState();
            cellX = (int) (mouseState.X / 32); 
            cellY = (int) (mouseState.Y / 32); 
            tileX = cellX * 32; 
            tileY = cellY * 32; 
            if(mouseState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed && cellX<level.Width && cellY<level.Height) {
                if(SamePathTexture() && IsTileClear(cellX, cellY) && pathIndex != -1) {
                    level.addPath(cellX, cellY);
                    usedPathIndex = pathIndex;
                    tilesCounter++;
                    differentTexture = false;
                }
                if(!SamePathTexture()) {
                    differentTexture = true;
                }
            }
            if(mouseState.RightButton == ButtonState.Released && oldState.RightButton == ButtonState.Pressed && cellX < level.Width && cellY < level.Height) {
                if(pathIndex != -1) {
                    pathIndex = -1;
                    differentTexture = false;
                }
                else if(!IsTileClear(cellX,cellY)){
                    level.PathArray[cellY, cellX] = 0;
                    tilesCounter--;
                }
            }
            oldState = mouseState;
        }

        private bool SamePathTexture() {
            if(tilesCounter > 0 && pathIndex != usedPathIndex && pathIndex != -1)
                return false;
            return true;
        }

        private bool IsTileClear(int col, int row) {
            return level.PathArray[row, col] != 1;
        }

        public void Draw(SpriteBatch spriteBatch) {
            Texture2D texture = pathTextures[usedPathIndex];
            for(int i = 0; i < level.PathArray.GetLength(0); i++)
                for(int j = 0; j < level.PathArray.GetLength(1); j++)
                    if(level.PathArray[i, j] == 1) {
                        spriteBatch.Draw(texture, new Rectangle(j * 32, i * 32, 32, 32), Color.White);
                    }
            if(differentTexture){
                spriteBatch.DrawString(font, "Use the same texture for path in single level", new Vector2(12, level.Height * 32 + 5), Color.Red);
            }
        }


        public void drawPreview(SpriteBatch spriteBatch) {
            if(pathIndex != -1) {
                Texture2D previewTexture = pathTextures[pathIndex];
                if(cellX < level.PathArray.GetLength(1) && cellY < level.PathArray.GetLength(0))
                    spriteBatch.Draw(previewTexture, new Rectangle(tileX, tileY, previewTexture.Width, previewTexture.Height), Color.White);

            }
        }

        public void changeIndex(int index) {
            this.pathIndex = index;
        }
    }
}
