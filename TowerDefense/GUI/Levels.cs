using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TowerDefense.GUI {
    public partial class Levels : Form {
        public Levels() {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            GUI.Menu.setLevel(1);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            GUI.Menu.setLevel(2);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) {
            GUI.Menu.setLevel(3);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e) {
            GUI.Menu.setLevel(4);
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Dispose();
        }

  

        
    }
}
