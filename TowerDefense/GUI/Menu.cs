using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TowerDefense.GUI {
    public partial class Menu : Form {
        int level = 1;
        
        public Menu() {
            InitializeComponent();
        }

        private void newGameButton_Click(object sender, EventArgs e) {
            this.SetVisibleCore(false);
            new Game1(level).Run();
            this.SetVisibleCore(true);        
        }

        private void levelsButton_Click(object sender, EventArgs e) {
            Form form = new Levels(this);
            form.ShowDialog();
        }

        private void scoreButton_Click(object sender, EventArgs e) {
            Form form = new Score();
            form.ShowDialog();
        }

        private void exitButton_Click(object sender, EventArgs e) {
            this.Dispose();
        }     

        public void setLevel(int lvl) {
            level = lvl;
        }      
        
    }
}
