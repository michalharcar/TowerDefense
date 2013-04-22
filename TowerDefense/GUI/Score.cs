using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TowerDefense.GUI {
    public partial class Score : Form {
        public Score() {
            InitializeComponent();
            textBox1.Text = new BestScore().ToString();
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Dispose();
        }
    }
}
