#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using TowerDefense.GUI;
using TowerDefense.Towers;
#endregion

namespace TowerDefense
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Level level = new Level();
        WaveManager waveManager;
        Player player;
        Toolbar toolbar;
        Button cannonButton;
        Button spikeButton;
        Button slowButton;
        Button laserButton;
        Button playButton;
        Button upgradeButton;
        UpgradeManager upgradeManager;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = level.Width * 32;
            graphics.PreferredBackBufferHeight = level.Height * 32 + 96;
            graphics.ApplyChanges();
            IsMouseVisible = true;



        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D grass = Content.Load<Texture2D>("grass");
            Texture2D path = Content.Load<Texture2D>("path");
            Texture2D[] enemyTextures = new Texture2D[]{
                Content.Load<Texture2D>("enemymove"),
                Content.Load<Texture2D>("birdmove"),
                Content.Load<Texture2D>("bossmove"),
        };
            Texture2D[] towerTextures = new Texture2D[] {
              Content.Load<Texture2D>("cannonTower"),
              Content.Load<Texture2D>("spikeTower"),
              Content.Load<Texture2D>("slowTower"),
              Content.Load<Texture2D>("laserTower"),
            };
            Texture2D[] bulletTextures = new Texture2D[] { 
              Content.Load<Texture2D>("bullet"),
              Content.Load<Texture2D>("spike"),
              Content.Load<Texture2D>("sbullet")
            };
            Texture2D laserTexture = Content.Load<Texture2D>("laser");
            Texture2D topBar = Content.Load<Texture2D>("interface\\toolbar");
            Texture2D gold = Content.Load<Texture2D>("interface\\gold");
            Texture2D life = Content.Load<Texture2D>("interface\\life");
            SpriteFont font = Content.Load<SpriteFont>("font");
            Texture2D healthTexture = Content.Load<Texture2D>("healthbar");

            Texture2D cannonButtonNormal = Content.Load<Texture2D>("interface\\cannon_button1");
            Texture2D cannonButtonHover = Content.Load<Texture2D>("interface\\cannon_button2");
            Texture2D cannonButtonPressed = Content.Load<Texture2D>("interface\\cannon_button3");
            cannonButton = new Button(cannonButtonNormal, cannonButtonHover, cannonButtonPressed, new Vector2(0, level.Height * 32), player);
            cannonButton.Clicked += new EventHandler(cannonButton_Clicked);
            
            Texture2D spikeNormal = Content.Load<Texture2D>("interface\\spike_button1");
            Texture2D spikeHover = Content.Load<Texture2D>("interface\\spike_button2");
            Texture2D spikePressed = Content.Load<Texture2D>("interface\\spike_button3");
            spikeButton = new Button(spikeNormal, spikeHover, spikePressed, new Vector2(32, level.Height * 32), player);
            spikeButton.Clicked += new EventHandler(spikeButton_Clicked);

            Texture2D slowNormal = Content.Load<Texture2D>("interface\\slow_button1");
            Texture2D slowHover = Content.Load<Texture2D>("interface\\slow_button2");
            Texture2D slowPressed = Content.Load<Texture2D>("interface\\slow_button3");
            slowButton = new Button(slowNormal, slowHover, slowPressed, new Vector2(64, level.Height * 32), player);
            slowButton.Clicked += new EventHandler(slowButton_Clicked);

            Texture2D laserButtonNormal = Content.Load<Texture2D>("interface\\laser_button1");
            Texture2D laserButtonHover = Content.Load<Texture2D>("interface\\laser_button2");
            Texture2D laserButtonPressed = Content.Load<Texture2D>("interface\\laser_button3");
            laserButton = new Button(laserButtonNormal, laserButtonHover, laserButtonPressed, new Vector2(96, level.Height * 32), player);
            laserButton.Clicked += new EventHandler(laserButton_Clicked);

            Texture2D playButtonNormal = Content.Load<Texture2D>("interface\\play_button1");
            Texture2D playButtonHover = Content.Load<Texture2D>("interface\\play_button2");
            Texture2D playButtonPressed = Content.Load<Texture2D>("interface\\play_button3");
            playButton = new Button(playButtonNormal, playButtonHover, playButtonPressed, new Vector2((level.Width/2)*32, level.Height * 32), player);
            playButton.Clicked += new EventHandler(playButton_Clicked);

            Texture2D upgradeButtonNormal = Content.Load<Texture2D>("interface\\upgrade_button1");
            Texture2D upgradeButtonHover = Content.Load<Texture2D>("interface\\upgrade_button2");
            Texture2D upgradeButtonPressed = Content.Load<Texture2D>("interface\\upgrade_button3");
            upgradeButton = new Button(upgradeButtonNormal, upgradeButtonHover, upgradeButtonPressed, new Vector2((level.Width - 1) * 32, level.Height * 32 + 64), player);
            upgradeButton.Clicked += new EventHandler(upgradeButton_Clicked);
                    
            cannonButton.OnPress += new EventHandler(cannonButton_OnPress);
            spikeButton.OnPress += new EventHandler(spikeButton_OnPress);
            slowButton.OnPress += new EventHandler(slowButton_OnPress);
            laserButton.OnPress += new EventHandler(laserButton_OnPress);

            toolbar = new Toolbar(topBar, gold, life, font, new Vector2(0, level.Height * 32), level);       
            player = new Player(level, towerTextures, bulletTextures, laserTexture);
            waveManager = new WaveManager(player, level, 24, enemyTextures, healthTexture);
            upgradeManager = new UpgradeManager(level, player, toolbar, upgradeButton);
            level.AddTexture(grass);
            level.AddTexture(path);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            waveManager.Update(gameTime);
            player.Update(gameTime, waveManager.Enemies);
            cannonButton.Update(gameTime);
            spikeButton.Update(gameTime);
            slowButton.Update(gameTime);
            laserButton.Update(gameTime);
            playButton.Update(gameTime);
            upgradeButton.Update(gameTime);
            upgradeManager.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
      
            spriteBatch.Begin();
            level.Draw(spriteBatch);
            waveManager.Draw(spriteBatch);
            player.Draw(spriteBatch);
            toolbar.Draw(spriteBatch, player);
            cannonButton.Draw(spriteBatch);
            spikeButton.Draw(spriteBatch);
            slowButton.Draw(spriteBatch);
            laserButton.Draw(spriteBatch);
            playButton.Draw(spriteBatch);
            upgradeButton.Draw(spriteBatch);
            player.DrawPreview(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void cannonButton_Clicked(object sender, EventArgs e) {
            player.NewTowerType = "Cannon Tower";
            player.NewTowerIndex = 0;
            toolbar.Upgrading = false;
        }

        private void spikeButton_Clicked(object sender, EventArgs e) {
            player.NewTowerType = "Spike Tower";
            player.NewTowerIndex = 1;
            toolbar.Upgrading = false;
        }

        private void slowButton_Clicked(object sender, EventArgs e) {
            player.NewTowerType = "Slow Tower";
            player.NewTowerIndex = 2;
            toolbar.Upgrading = false;
        }

        private void laserButton_Clicked(object sender, EventArgs e) {
            player.NewTowerType = "Laser Tower";
            player.NewTowerIndex = 3;
            toolbar.Upgrading = false;
        }

        private void playButton_Clicked(object sender, EventArgs e) {
            waveManager.StartNextWave();
        }

        private void upgradeButton_Clicked(object sender, EventArgs e) {
            if(upgradeManager.SelectedTower != null) {
                upgradeManager.SelectedTower.Upgrade(player);
            }
        }

        private void cannonButton_OnPress(object sender, EventArgs e) {
            player.NewTowerType = "Cannon Tower";
            player.NewTowerIndex = 0;
            toolbar.Upgrading = false;
        }
        private void spikeButton_OnPress(object sender, EventArgs e) {
            player.NewTowerType = "Spike Tower";
            player.NewTowerIndex = 1;
            toolbar.Upgrading = false;
        }
        private void slowButton_OnPress(object sender, EventArgs e) {
            player.NewTowerType = "Slow Tower";
            player.NewTowerIndex = 2;
            toolbar.Upgrading = false;
        }

        private void laserButton_OnPress(object sender, EventArgs e) {
            player.NewTowerType = "Laser Tower";
            player.NewTowerIndex = 3;
            toolbar.Upgrading = false;
        }

    }
}
