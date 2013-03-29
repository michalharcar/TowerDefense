﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefense.GUI;

namespace TowerDefense.Towers {
    class UpgradeManager {

        private Level level;
        private Player player;
        private Toolbar toolbar;
        private MouseState previousState;

        public UpgradeManager(Level level, Player player, Toolbar toolbar) {
            this.level = level;
            this.player = player;
            this.toolbar = toolbar;
        }

        public void upgradeTower(Tower tower) {
            if(tower.UpgradeLevel < 3)
                tower.UpgradeLevel++;
        }

        public void Update(GameTime gameTime) {
            MouseState mouseState = Mouse.GetState();
            int mouseX = mouseState.X/32;
            int mouseY = mouseState.Y/32;
            if(mouseState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released) {
                foreach(Tower tower in player.towers) {
                    if(tower.Position.X / 32 == mouseX && tower.Position.Y / 32 == mouseY) {
                        toolbar.Upgrading = true;
                        toolbar.setTower(tower);
                        break;
                    }
                    else {
                        toolbar.Upgrading = false;
                    }
                }
            }
            previousState = mouseState;
        }

        public void check() {
            MouseState mouseState = Mouse.GetState();
            int mouseX = mouseState.X;
            int mouseY = mouseState.Y;
        }
    }
}
