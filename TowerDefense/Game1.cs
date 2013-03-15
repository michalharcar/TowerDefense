﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using TowerDefense.GUI;
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
        Toolbar toolBar;
        Button cannonButton;
        Button spikeButton;
        Button slowButton;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = level.Width * 32;
            graphics.PreferredBackBufferHeight = level.RowCount * 32 + 32;
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
            Texture2D enemyTexture = Content.Load<Texture2D>("enemy");
            Texture2D[] towerTextures = new Texture2D[] {
              Content.Load<Texture2D>("cannonTower"),
              Content.Load<Texture2D>("spikeTower"),
              Content.Load<Texture2D>("slowTower")
            };
            Texture2D[] bulletTextures = new Texture2D[] { 
              Content.Load<Texture2D>("bullet"),
              Content.Load<Texture2D>("spike"),
              Content.Load<Texture2D>("sbullet")
            };
            Texture2D topBar = Content.Load<Texture2D>("toolbar");
            SpriteFont font = Content.Load<SpriteFont>("font");

            Texture2D cannonButtonNormal = Content.Load<Texture2D>("interface\\cannon_button1");
            // The "MouseOver" texture for the arrow button.
            Texture2D cannonButtonHover = Content.Load<Texture2D>("interface\\cannon_button2");
            // The "Pressed" texture for the arrow button.
            Texture2D cannonButtonPressed = Content.Load<Texture2D>("interface\\cannon_button3");
            // Initialize the arrow button.
            cannonButton = new Button(cannonButtonNormal, cannonButtonHover, cannonButtonPressed, new Vector2(0, level.RowCount * 32));
            cannonButton.Clicked += new EventHandler(cannonButton_Clicked);
            
            // The "Normal" texture for the spike button.
            Texture2D spikeNormal = Content.Load<Texture2D>("interface\\spike_button1");
            // The "MouseOver" texture for the spike button.
            Texture2D spikeHover = Content.Load<Texture2D>("interface\\spike_button2");
            // The "Pressed" texture for the spike button.
            Texture2D spikePressed = Content.Load<Texture2D>("interface\\spike_button3");
            spikeButton = new Button(spikeNormal, spikeHover, spikePressed, new Vector2(32, level.RowCount * 32));
            spikeButton.Clicked += new EventHandler(spikeButton_Clicked);

            // The "Normal" texture for the spike button.
            Texture2D slowNormal = Content.Load<Texture2D>("interface\\slow_button1");
            // The "MouseOver" texture for the spike button.
            Texture2D slowHover = Content.Load<Texture2D>("interface\\slow_button2");
            // The "Pressed" texture for the spike button.
            Texture2D slowPressed = Content.Load<Texture2D>("interface\\slow_button3");
            slowButton = new Button(slowNormal, slowHover, slowPressed, new Vector2(64, level.RowCount * 32));
            slowButton.Clicked += new EventHandler(slowButton_Clicked);

            cannonButton.OnPress += new EventHandler(cannonButton_OnPress);
            spikeButton.OnPress += new EventHandler(spikeButton_OnPress);
            slowButton.OnPress += new EventHandler(slowButton_OnPress);

            toolBar = new Toolbar(topBar, font, new Vector2(0, level.RowCount * 32));
            Texture2D healthTexture = Content.Load<Texture2D>("healthbar");
            player = new Player(level, towerTextures, bulletTextures);
            waveManager = new WaveManager(player, level, 24, enemyTexture, healthTexture);
            level.AddTexture(grass);
            level.AddTexture(path);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
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
            toolBar.Draw(spriteBatch, player);
            cannonButton.Draw(spriteBatch);
            spikeButton.Draw(spriteBatch);
            slowButton.Draw(spriteBatch);
            player.DrawPreview(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void cannonButton_Clicked(object sender, EventArgs e) {
            player.NewTowerType = "Cannon Tower";
            player.NewTowerIndex = 0;
        }

        private void spikeButton_Clicked(object sender, EventArgs e) {
            player.NewTowerType = "Spike Tower";
            player.NewTowerIndex = 1;
        }

        private void slowButton_Clicked(object sender, EventArgs e) {
            player.NewTowerType = "Slow Tower";
            player.NewTowerIndex = 2;
        }

        private void cannonButton_OnPress(object sender, EventArgs e) {
            player.NewTowerType = "Cannon Tower";
            player.NewTowerIndex = 0;
        }
        private void spikeButton_OnPress(object sender, EventArgs e) {
            player.NewTowerType = "Spike Tower";
            player.NewTowerIndex = 1;
        }
        private void slowButton_OnPress(object sender, EventArgs e) {
            player.NewTowerType = "Slow Tower";
            player.NewTowerIndex = 2;
        }

    }
}
