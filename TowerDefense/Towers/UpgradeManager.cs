using System;
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
        private Button upgradeButton;
        private Rectangle bounds;
        private Rectangle bounds2;
        public Tower SelectedTower { get; set; }
        private MouseState previousState;
    

        public UpgradeManager(Level level, Player player, Toolbar toolbar, Button upgradeButton, Button sellButton) {
            this.level = level;
            this.player = player;
            this.toolbar = toolbar;
            this.upgradeButton = upgradeButton;
            this.bounds = new Rectangle((int) upgradeButton.Position.X, (int) upgradeButton.Position.Y, 32, 32);
            this.bounds2 = new Rectangle((int) sellButton.Position.X, (int) sellButton.Position.Y, 32, 32);
        }

        public void upgradeTower(Tower tower) {
            if(tower.UpgradeLevel < 3)
                tower.UpgradeLevel++;
        }

        public void Update(GameTime gameTime) {
            MouseState mouseState = Mouse.GetState();
            int mouseX =mouseState.X/32;
            int mouseY = mouseState.Y/32;
            if(mouseState.LeftButton == ButtonState.Pressed) {
                player.EnoughGold = true;
                foreach(Tower tower in player.towers) {
                    if(tower.Position.X / 32 == mouseX && tower.Position.Y / 32 == mouseY) {
                        toolbar.Upgrading = true;
                        toolbar.setTower(tower);
                        SelectedTower = tower;
                        break;
                    }
                    else if(!bounds.Contains(mouseState.X, mouseState.Y) && !bounds2.Contains(mouseState.X, mouseState.Y)) {
                        toolbar.Upgrading = false;
                        SelectedTower = null;
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
