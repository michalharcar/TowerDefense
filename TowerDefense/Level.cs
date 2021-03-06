﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;




namespace TowerDefense {
    public enum State {
        PLAYING,
        PAUSED, 
        FINISHED
    };
    
    public class Level {
        int[,] map;
        public int[,] Map { get { return map; } }
        private string level;
        public int LvlNumber { get; private set; }
        private Player player;
        public State GameState { get; set; }
        private List<Texture2D> tileTextures = new List<Texture2D>();
        private Texture2D[] backgrounds;
        private Texture2D[] paths;
        private Queue<Vector2> waypoints = new Queue<Vector2>();
        public Queue<Vector2> Waypoints {
            get { return waypoints; }
        }
        public int Width {
            get { return map.GetLength(1); }
        }
        public int Height {
            get { return map.GetLength(0); }
        }
        private int startTime;
        private int playingTime;
        public int PlayingTime { get { return playingTime; } }



        public Level(int lvl) {
            LvlNumber = lvl;
            long ticks = DateTime.Now.Ticks;
            startTime = (int) (ticks / TimeSpan.TicksPerSecond);
            this.level = "level" + lvl;
            map = loadLevel(level);
            makePath();
        }

        public void AddTexture(Texture2D texture) {
            tileTextures.Add(texture);
        }
        public void AddBackground(Texture2D[] textures) {
            this.backgrounds = textures;
        }
        public void AddPath(Texture2D[] paths) {
            this.paths = paths;
        }

        public void SetPlayingTime(){
            playingTime = (int) (DateTime.Now.Ticks / TimeSpan.TicksPerSecond) - startTime;
        }

        public void SetPlayer(Player player) {
            this.player = player;
        }

        public void Draw(SpriteBatch batch) {
            Texture2D bgtexture;
            switch(level) {
                case "level1":
                    bgtexture = backgrounds[0];
                    break;
                case "level2":
                    bgtexture = backgrounds[1];
                    break;
                case "level3":
                    bgtexture = backgrounds[2];
                    break;
                case "level4":
                    bgtexture = backgrounds[3];
                    break;
                default:
                    bgtexture = backgrounds[0];
                    break;
            }
                    batch.Draw(bgtexture, new Rectangle(0, 0, 512, 512), Color.White);
            for(int col = 0; col < Width; col++) {
                for(int row = 0; row < Height; row++) {
                    int textureIndex = map[row, col];
                    //if(textureIndex == -1)
                    //    continue;
                    //Texture2D texture = tileTextures[textureIndex];
                    if(textureIndex == 1) {
                        Texture2D texture;
                        switch(level) {
                            case "level1":
                                texture = paths[0];
                                break;
                            case "level2":
                                texture = paths[1];
                                break;
                            case "level4":
                                texture = paths[2];
                                break;
                            default:
                                texture = paths[0];
                                break;
                        }
                        batch.Draw(texture, new Rectangle(col * 32, row * 32, 32, 32), Color.White);
                    }
                }
            }
        }

        public int GetIndex(int cellX, int cellY) {
            if(cellX < 0 || cellX > Width - 1 || cellY < 0 || cellY > Height - 1)
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
            Vector2 last=new Vector2();
            for(int i = 0; i < map.GetLength(1); i++) {
                if(map[0, i] == 1) {
                    waypoints.Enqueue(new Vector2(i, 0) * 32);
                    last = waypoints.ElementAt(waypoints.Count-1);
                }
            }
            for(int i = 0; i < map.GetLength(1); i++) {
                if(map[1, i] == 1 && (map[0, i] == 1))
                    waypoints.Enqueue(new Vector2(i, 1) * 32);
            }
            bool next = true;
            int x = (int) waypoints.ElementAt(waypoints.Count - 1).X / 32;
            int y = (int) waypoints.ElementAt(waypoints.Count - 1).Y / 32;
            cycle:
            while(next) {
                for(int row = -1; row <= 1; row++) {
                    next = false;
                        for(int col = -1; col <= 1; col++) {
                            int yPos = y + row;
                            int xPos = x + col;
                            if((row!=0 && col==0) || (row==0 && col!=0)){
                                if(xPos > -1 && yPos > -1 && yPos < map.GetLength(0) && xPos < map.GetLength(1)) {
                                    if(map[yPos, xPos] == 1 && (last.X / 32 != xPos || last.Y / 32 != yPos)) {
                                        last = waypoints.ElementAt(waypoints.Count - 1);
                                        waypoints.Enqueue(new Vector2(xPos, yPos) * 32);
                                        x = (int) waypoints.ElementAt(waypoints.Count - 1).X / 32;
                                        y = (int) waypoints.ElementAt(waypoints.Count - 1).Y / 32;
                                        next = true;
                                        goto cycle;
                                    }
                                }
                        }
                    }
                }
            }
        }
    }
}
