using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense.Enemies {
    class Bird : Enemy {

         public Bird(Texture2D texture, Vector2 position) : base(texture, position) {
            startHealth = 50;
            CurrentHealth = startHealth;
            GoldGiven = 3;
            speed = 0.8f;
        }
    }
}
