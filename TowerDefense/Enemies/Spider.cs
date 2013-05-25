using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense.Enemies {
    public class Spider : Enemy {

        public Spider(Texture2D texture, Vector2 position) : base(texture, position) {
            startHealth = 100;
            CurrentHealth = startHealth;
            GoldGiven = 5;
            speed = 0.5f;
        }

        protected override void Move(GameTime gameTime) {
            switch(EnemyDirection) {
                case Direction.NORTH:
                    if(currentFrame < 0 || currentFrame > 2)
                        currentFrame = 0;
                    timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                    if(timer > interval) {
                        currentFrame++;
                        if(currentFrame > 2)
                            currentFrame = 0;
                        timer = 0f;
                    }
                    break;
                case Direction.EAST:
                    if(currentFrame < 3 || currentFrame > 5)
                        currentFrame = 3;
                    timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                    if(timer > interval) {
                        currentFrame++;
                        if(currentFrame > 5)
                            currentFrame = 3;
                        timer = 0f;
                    }
                    break;
                case Direction.WEST:
                    if(currentFrame < 6 || currentFrame > 8)
                        currentFrame = 6;
                    timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                    if(timer > interval) {
                        currentFrame++;
                        if(currentFrame > 8)
                            currentFrame = 6;
                        timer = 0f;
                    }
                    break;
                case Direction.SOUTH:
                    if(currentFrame < 9 || currentFrame > 11)
                        currentFrame = 9;
                    timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
                    if(timer > interval) {
                        currentFrame++;
                        if(currentFrame > 11)
                            currentFrame = 9;
                        timer = 0f;
                    }
                    break;

            }
        }

    }
}
