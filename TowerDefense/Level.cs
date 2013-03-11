using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;




namespace TowerDefense
{
    public class Level {
        int[,] map = new int[,]{
         {0,1,0,0,0,0,0,0,0},
         {0,1,0,0,0,0,0,0,0},
         {0,1,1,1,1,1,1,0,0},
         {0,0,0,0,0,0,1,0,0},
         {0,0,0,0,0,0,1,0,0},
         {0,0,0,0,1,1,1,0,0},
         {0,0,0,0,1,0,0,0,0},
         {0,0,0,0,1,0,0,0,0},
         };
        private List<Texture2D> tileTextures = new List<Texture2D>();
        private Queue<Vector2> waypoints = new Queue<Vector2>();
        public Queue<Vector2> Waypoints {
            get { return waypoints; }
        }

        public int ColCount {
            get { return map.GetLength(1); }
        }
        public int RowCount {
            get { return map.GetLength(0); }
        }

        public Level(){
            waypoints.Enqueue(new Vector2(1, 0) * 32);
            waypoints.Enqueue(new Vector2(1, 1) * 32);
            waypoints.Enqueue(new Vector2(1, 2) * 32);
            waypoints.Enqueue(new Vector2(2, 2) * 32);
            waypoints.Enqueue(new Vector2(3, 2) * 32);
            waypoints.Enqueue(new Vector2(4, 2) * 32);
            waypoints.Enqueue(new Vector2(5, 2) * 32);
            waypoints.Enqueue(new Vector2(6, 2) * 32);
            waypoints.Enqueue(new Vector2(6, 3) * 32);
            waypoints.Enqueue(new Vector2(6, 4) * 32);
            waypoints.Enqueue(new Vector2(6, 5) * 32);
            waypoints.Enqueue(new Vector2(5, 5) * 32);
            waypoints.Enqueue(new Vector2(4, 5) * 32);
            waypoints.Enqueue(new Vector2(4, 6) * 32);
            waypoints.Enqueue(new Vector2(4, 7) * 32);
        }

        public void AddTexture(Texture2D texture) {
            tileTextures.Add(texture);
        }

        public void Draw(SpriteBatch batch)
        {
            for (int col = 0; col < ColCount; col++) {
                for (int row = 0; row < RowCount; row++) {
                    int textureIndex = map[row, col];
                    if (textureIndex == -1)
                        continue;
                    Texture2D texture = tileTextures[textureIndex];
                    batch.Draw(texture, new Rectangle(col * 32, row * 32, 32, 32), Color.White);
                }
            }
        }

        public int GetIndex(int cellX, int cellY) {
            if (cellX < 0 || cellX > ColCount -1 || cellY < 0 || cellY > RowCount -1)
                return 0;
            else return map[cellY, cellX];

        }

    }
}
