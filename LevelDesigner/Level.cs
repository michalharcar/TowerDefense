using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LevelDesigner {
    public class Level {
    private int[,] pathArray;
    public int[,] PathArray { get { return pathArray; } }
    public int Width { get; set; }
    public int Height { get; set; }

        public Level(int width, int height) {
        this.Width = width;
        this.Height = height;
        pathArray = new int[Height, Width];
        zeroArray();      
        }

        public void addPath(int col, int row) {
            pathArray[row, col] = 1;
        }

        private void zeroArray() {
            for(int i = 0; i < Height; i++)
                for(int j = 0; j < Width; j++)
                    pathArray[i, j] = 0;
        }

    }
}
