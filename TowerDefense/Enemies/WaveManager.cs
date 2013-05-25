using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefense.Enemies
{
    class WaveManager{
        private int numberOfWaves;
    //  private float timeSinceLastWave; 
        private Queue<Wave> waves = new Queue<Wave>(); // A queue of all our waves
        private Texture2D[] enemyTextures; 
    //  private bool waveFinished = false; // Is the current wave over?
        public bool Finished { get; private set; }

        public Wave CurrentWave {    // Get the wave at the front of the queue
            get { return waves.Peek(); }
        }
        public List<Enemy> Enemies {
            get { return CurrentWave.Enemies; }
        }

        public WaveManager(Player player, Level level, int levelNumber, Texture2D[] enemyTextures, Texture2D healthTexture)
        {
            this.numberOfWaves = levelNumber * 5;
            this.enemyTextures = enemyTextures;
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
            if(waves.Count > 0) {
                waves.Peek().Start(); 
       //         timeSinceLastWave = 0; // Reset timer
      //          waveFinished = false;
            }
        }


    }
}
