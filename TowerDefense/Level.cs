using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;




namespace TowerDefense {
    public class Level {
        int[,] map;
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

        public Level() {
            map = loadLevel("level");
            makePath();

        }

        public void AddTexture(Texture2D texture) {
            tileTextures.Add(texture);
        }

        public void Draw(SpriteBatch batch) {
            for(int col = 0; col < Width; col++) {
                for(int row = 0; row < RowCount; row++) {
                    int textureIndex = map[row, col];
                    if(textureIndex == -1)
                        continue;
                    Texture2D texture = tileTextures[textureIndex];
                    batch.Draw(texture, new Rectangle(col * 32, row * 32, 32, 32), Color.White);
                }
            }
        }

        public int GetIndex(int cellX, int cellY) {
            if(cellX < 0 || cellX > Width - 1 || cellY < 0 || cellY > RowCount - 1)
                return 0;
            else
                return map[cellY, cellX];
        }

        public static int[,] loadLevel(string level) {
            string path = "Content\\" + level + ".txt";
            using(StreamReader sr = new StreamReader(path)) {
                int linesCount = 0;
                string line;
                int lineLength = 0;
                while((line = sr.ReadLine()) != null) {
                    linesCount++;
                    lineLength = line.Length;
                }
                int[,] mapa = new int[linesCount, lineLength / 2 + 1];
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                int x = 0;
                string row;
                while((row = sr.ReadLine()) != null) {
                    int i = 0;
                    int y = 0;
                    while(i < row.Length) {
                        mapa[x, y] = row.ToCharArray()[i] - 48;
                        i += 2;
                        y++;
                    }
                    x++;
                }
                return mapa;
            }
        }

        public void makePath() {
            int returnback = 0;
            for(int x = 0; x < map.GetLength(1); x++) {
                if(map[0, x] == 1)
                    waypoints.Enqueue(new Vector2(x, 0) * 32);
            }
            int row = 1;
            while(row < map.GetLength(0)) {
                int col = 0;
                while(col < map.GetLength(1)) {
                    if(map[row, col] == 1) {
                        if(map[row - 1, col] == 1 || map[row, col - 1] == 1) {
                            waypoints.Enqueue(new Vector2(col, row) * 32);
                            if(returnback > 0) {
                                for(int i = 1; i <= returnback; i++)
                                    waypoints.Enqueue(new Vector2(col - i, row) * 32);
                                returnback = 0;
                                col = map.GetLength(1);
                            }
                            else
                                col++;
                        }
                        else {
                            returnback++;
                            col++;
                        }
                    }
                    else
                        col++;
                }
                row++;
            }           
        }
    }
}
