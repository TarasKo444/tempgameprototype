using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using ProjectMono.Source.Engine;
using ProjectMono.Source;
using System;
using System.Diagnostics;

namespace ProjectMono
{
    public class Main : Game
    {
        private Map map;

        public Main()
        {
            Globals.Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Globals.Graphics.PreferredBackBufferWidth = 800;
            Globals.Graphics.PreferredBackBufferHeight= 600;
        }

        protected override void Initialize()
        {
            base.Initialize();
            map = new Map(1000, 1000);
        }

        protected override void LoadContent()
        {
            Globals.Content = this.Content;
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Globals.Graphics.GraphicsDevice.Clear(Color.White);
            Globals.SpriteBatch.Begin();

            map.Update();
            if (gameTime.TotalGameTime.Milliseconds % 200 == 0 && Mouse.GetState().LeftButton == ButtonState.Released)
            {
                map.NextStep();
            }
            map.Draw();

            Globals.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

class Program
{
    public static void Main()
    {
        using var game = new ProjectMono.Main();
        game.Run();
    }
}