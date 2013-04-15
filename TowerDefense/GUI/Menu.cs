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
        static String level = "level1";
        
        public Menu() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.SetVisibleCore(false);
            new Game1(level).Run();
            this.SetVisibleCore(true);
            
        }

        private void button3_Click(object sender, EventArgs e) {
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e) {
            Form form = new Levels();
            form.Show();
        }

        public static void setLevel(String lvl) {
            level = lvl;
        }
        
    }
}
