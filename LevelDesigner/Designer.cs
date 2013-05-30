#region Using Statements
using System;
using System.Text;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using TowerDefense.GUI;
#endregion

namespace LevelDesigner {

    public class Designer : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Level level;
        PathMaker pathMaker;
        string levelName;
        int width, height; 
        int backgroundIndex = 0;
        Texture2D toolbar;
        Texture2D[] background;
        Texture2D[] path;
        Vector2 position;
        Button background1;
        Button background2;
        Button background3;
        Button background4;
        Button path1;
        Button path2;
        Button path3;
        Button save;

        public Designer(int width, int height, string name) : base() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.width = width;
            this.height = height;
            level = new Level(width, height);
            this.levelName = name;
            graphics.PreferredBackBufferWidth = width * 32;
            graphics.PreferredBackBufferHeight = height * 32 + 96;
        }


        protected override void Initialize() {
            base.Initialize();
        }


        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            toolbar = Content.Load<Texture2D>("toolbar.png");
            position = new Vector2(0, height * 32);
            font = Content.Load<SpriteFont>("font");
            Texture2D bg1_button1 = Content.Load<Texture2D>("bg1_button1.png");
            Texture2D bg1_button2 = Content.Load<Texture2D>("bg1_button2.png");
            Texture2D bg1_button3 = Content.Load<Texture2D>("bg1_button3.png");
            background1 = new Button(bg1_button1, bg1_button2, bg1_button3, new Vector2(0, height * 32 + 64));
            background1.Clicked += new EventHandler(background1_Clicked);

            Texture2D bg2_button1 = Content.Load<Texture2D>("bg2_button1.png");
            Texture2D bg2_button2 = Content.Load<Texture2D>("bg2_button2.png");
            Texture2D bg2_button3 = Content.Load<Texture2D>("bg2_button3.png");
            background2 = new Button(bg2_button1, bg2_button2, bg2_button3, new Vector2(32, height * 32 + 64));
            background2.Clicked += new EventHandler(background2_Clicked);

            Texture2D bg3_button1 = Content.Load<Texture2D>("bg3_button1.png");
            Texture2D bg3_button2 = Content.Load<Texture2D>("bg3_button2.png");
            Texture2D bg3_button3 = Content.Load<Texture2D>("bg3_button3.png");
            background3 = new Button(bg3_button1, bg3_button2, bg3_button3, new Vector2(64, height * 32 + 64));
            background3.Clicked += new EventHandler(background3_Clicked);

            Texture2D bg4_button1 = Content.Load<Texture2D>("bg4_button1.png");
            Texture2D bg4_button2 = Content.Load<Texture2D>("bg4_button2.png");
            Texture2D bg4_button3 = Content.Load<Texture2D>("bg4_button3.png");
            background4 = new Button(bg4_button1, bg4_button2, bg4_button3, new Vector2(96, height * 32 + 64));
            background4.Clicked += new EventHandler(background4_Clicked);

            background = new Texture2D[] {
                Content.Load<Texture2D>("lvl1_bg"),
                Content.Load<Texture2D>("lvl2_bg"),
                Content.Load<Texture2D>("lvl3_bg"),
                Content.Load<Texture2D>("lvl4_bg"),
            };

            Texture2D path1_button1 = Content.Load<Texture2D>("path1_button1.png");
            Texture2D path1_button2 = Content.Load<Texture2D>("path1_button2.png");
            Texture2D path1_button3 = Content.Load<Texture2D>("path1_button3.png");
            path1 = new Button(path1_button1, path1_button2, path1_button3, new Vector2(160, height * 32 + 64));
            path1.Clicked += new EventHandler(path1_Clicked);


            Texture2D path2_button1 = Content.Load<Texture2D>("path2_button1.png");
            Texture2D path2_button2 = Content.Load<Texture2D>("path2_button2.png");
            Texture2D path2_button3 = Content.Load<Texture2D>("path2_button3.png");
            path2 = new Button(path2_button1, path2_button2, path2_button3, new Vector2(192, height * 32 + 64));
            path2.Clicked += new EventHandler(path2_Clicked);

            Texture2D path3_button1 = Content.Load<Texture2D>("path3_button1.png");
            Texture2D path3_button2 = Content.Load<Texture2D>("path3_button2.png");
            Texture2D path3_button3 = Content.Load<Texture2D>("path3_button3.png");
            path3 = new Button(path3_button1, path3_button2, path3_button3, new Vector2(224, height * 32 + 64));
            path3.Clicked += new EventHandler(path3_Clicked);

            path = new Texture2D[] {
                Content.Load<Texture2D>("path1"),
                Content.Load<Texture2D>("path2"),
                Content.Load<Texture2D>("path3"),
            };

            Texture2D save_button1 = Content.Load<Texture2D>("save_button1.png");
            Texture2D save_button2 = Content.Load<Texture2D>("save_button2.png");
            Texture2D save_button3 = Content.Load<Texture2D>("save_button3.png");
            save = new Button(save_button1, save_button2, save_button3, new Vector2(width * 32 - 64, height * 32 + 32));
            save.Clicked += new EventHandler(save_Clicked);

            pathMaker = new PathMaker(level, path, font);
        }

        protected override void UnloadContent() {

        }


        protected override void Update(GameTime gameTime) {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            background1.Update(gameTime);
            background2.Update(gameTime);
            background3.Update(gameTime);
            background4.Update(gameTime);
            path1.Update(gameTime);
            path2.Update(gameTime);
            path3.Update(gameTime);
            save.Update(gameTime);
            pathMaker.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(background[backgroundIndex], new Rectangle(0, 0, level.Width*32, level.Height*32), Color.White);
            spriteBatch.Draw(toolbar, position, Color.White);
            background1.Draw(spriteBatch);
            background2.Draw(spriteBatch);
            background3.Draw(spriteBatch);
            background4.Draw(spriteBatch);
            path1.Draw(spriteBatch);
            path2.Draw(spriteBatch);
            path3.Draw(spriteBatch);
            save.Draw(spriteBatch);
            pathMaker.Draw(spriteBatch);
            pathMaker.drawPreview(spriteBatch);
            spriteBatch.DrawString(font, "Background                               Path", new Vector2(32, height * 32 + 45), Color.White);  
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void background1_Clicked(object sender, EventArgs e) {
            backgroundIndex = 0;
        }

        private void background2_Clicked(object sender, EventArgs e) {
            backgroundIndex = 1;
        }

        private void background3_Clicked(object sender, EventArgs e) {
            backgroundIndex = 2;
        }

        private void background4_Clicked(object sender, EventArgs e) {
            backgroundIndex = 3;
        }

        private void path1_Clicked(object sender, EventArgs e) {
            pathMaker.changeIndex(0);
        }

        private void path2_Clicked(object sender, EventArgs e) {
            pathMaker.changeIndex(1);
        }

        private void path3_Clicked(object sender, EventArgs e) {
            pathMaker.changeIndex(2);
        }

        private void save_Clicked(object sender, EventArgs e) {
            StringBuilder sb = new StringBuilder();
            string firstLine = string.Format("{0} {1} {2} {3}", level.Width, level.Height, backgroundIndex, pathMaker.UsedPathIndex);
            sb.AppendLine(firstLine);
            for(int i = 0; i < level.Height; i++) {
                for(int j = 0; j < level.Width; j++) {
                    sb.Append(level.PathArray[i, j] + " ");
                }
                sb.AppendLine();
            }
            string pathToFile = Directory.GetCurrentDirectory();
            using(StreamWriter outfile = new StreamWriter(pathToFile + @"\" + levelName +  ".txt")) {
                outfile.Write(sb.ToString());
            }
            Exit();
        }

    }
}
