#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefense.GUI;
using System.Windows.Forms;
#endregion

namespace TowerDefense
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        private static Game1 game;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI.Menu());
              
        }
    }
}
