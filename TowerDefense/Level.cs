using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;




namespace TowerDefense
{
    public class Level {

        public static int[,] loadLevel() {
            using (StreamReader sr = new StreamReader("Content\\level.txt")) {
                int linesCount = 0;
                string line;
                int lineLength = 0;
                while ((line = sr.ReadLine()) != null) {
                    linesCount++;
                    lineLength = line.Length;
                }
                int[,] mapa = new int[linesCount, lineLength / 2+1];
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                int x = 0;
                string row;
                while ((row = sr.ReadLine()) != null) {
                    int i = 0;
                    int y = 0;
                    while (i < row.Length) {
                        mapa[x, y] = row.ToCharArray()[i]-48;
                        i += 2;
                        y++;
                    }
                    x++;
                }
                return mapa;
            }       
        }
        int[,] map = loadLevel();
        //int[,] map = new int[,] {
        // {0,1,0,0,0,0,0,0,0},
        // {0,1,0,0,0,0,0,0,0},
        // {0,1,1,1,1,1,1,0,0},
        // {0,0,0,0,0,0,1,0,0},
        // {0,0,0,0,0,0,1,0,0},
        // {0,0,0,0,1,1,1,0,0},
        // {0,0,0,0,1,0,0,0,0},
        // {0,0,0,0,1,0,0,0,0},
        // };
        private List<Texture2D> tileTextures = new List<Texture2D>();
        private Queue<Vector2> waypoints = new Queue<Vector2>();
        public Queue<Vector2> Waypoints {
            get { return waypoints; }
        }

        public int Width {
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
            for (int col = 0; col < Width; col++) {
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
            if (cellX < 0 || cellX > Width -1 || cellY < 0 || cellY > RowCount -1)
                return 0;
            else return map[cellY, cellX];

        }

    }
}
