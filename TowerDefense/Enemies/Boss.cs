using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense.Enemies {
   public class Boss : Enemy {

        public Boss(Texture2D texture, Vector2 position) : base(texture, position) {
            startHealth = 1000;
            CurrentHealth = startHealth;
            GoldGiven = 150;
            speed = 0.4f;
        }
    }
}
