using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense.Towers {
    public class Laser {
        private Rectangle laser;
        public int Length { get { return laser.Width; } set { laser.Width = value; } }


        public Laser(Vector2 position, int length) {
            laser = new Rectangle((int)position.X, (int)position.Y, length, 3);
            Length = length;     
        }


    }
}
