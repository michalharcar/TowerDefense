using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LevelDesigner {
    public partial class Menu : Form {
        int width, height;
        string levelName;
        public Menu() {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            textBox1.Text = "level";
        }

        private void menuButton_Click(object sender, EventArgs e) {           
            processInput();
            this.Dispose();
            new Designer(width, height, levelName).Run();
        }

        private void processInput() {
            switch(comboBox1.SelectedIndex) {
                case 0:
                    width = 12;
                    height = 10;
                    break;
                case 1:
                    width = 14;
                    height = 12;
                    break;
                case 2:
                    width = 16;
                    height = 14;
                    break;
                default:
                    break;
            }
            levelName = textBox1.Text;
        }
    }
}
