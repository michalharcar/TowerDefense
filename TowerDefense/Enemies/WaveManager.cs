using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense.Enemies
{
    class WaveManager{
        private int numberOfWaves; // How many waves the game will have
      //  private float timeSinceLastWave; // How long since the last wave ended
        private Queue<Wave> waves = new Queue<Wave>(); // A queue of all our waves
        private Texture2D[] enemyTextures; // The texture used to draw the enemies
    //    private bool waveFinished = false; // Is the current wave over?
        private Level level; // A reference to our level class
        public bool Finished { get; private set; }

        public Wave CurrentWave {    // Get the wave at the front of the queue
            get { return waves.Peek(); }
        }
        public List<Enemy> Enemies { // Get a list of the current enemeies
            get { return CurrentWave.Enemies; }
        }
        public int Round  {  // Returns the wave number
            get { return CurrentWave.RoundNumber + 1; }
        }

        public WaveManager(Player player, Level level, int levelNumber, Texture2D[] enemyTextures, Texture2D healthTexture)
        {
            this.numberOfWaves = 1; // levelNumber * 5;
            this.enemyTextures = enemyTextures;
            this.level = level;
            Finished = false;
            for (int i = 1; i <= numberOfWaves; i++)  {
                int NumberOfEnemies = 3 + 3*i;
                Wave wave = new Wave(i, NumberOfEnemies, player, level, enemyTextures, healthTexture);
                waves.Enqueue(wave);
            }
        }

        public void Update(GameTime gameTime)  {
                CurrentWave.Update(gameTime); // Update the wave
                if(CurrentWave.RoundOver) { // Check if it has finished
         //           waveFinished = true;
                    if(waves.Count == 1) {
                        Finished = true;
                    }
                    else {
                        waves.Dequeue();
                    }
                    
                }
         //   if (waveFinished)  { // If it has finished
        //        timeSinceLastWave += (float)gameTime.ElapsedGameTime.TotalSeconds; // Start the timer
        //    }
       //     if (timeSinceLastWave > 2.0f)  { // If 30 seconds has passed
                // Remove the finished wave
            //    StartNextWave(); // Start the next wave
       //     }
        }

        public void Draw(SpriteBatch spriteBatch) {
            CurrentWave.Draw(spriteBatch);
        }

        public void StartNextWave() {
            if(waves.Count > 0) { // If there are still waves left
                waves.Peek().Start(); // Start the next one
       //         timeSinceLastWave = 0; // Reset timer
      //          waveFinished = false;
            }
        }


    }
}
