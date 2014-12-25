using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project_PewPew
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;
        Player PlayerOne;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            TextureManager.LoadContent(Content);
            CollisionManager.Initialize();
            PlayerOne = new Player(new Vector2(200, 200), 1);
            Standard Test = new Standard(new Vector2(400, 400), ref PlayerOne);
            Standard Test2 = new Standard(new Vector2(100, 400), ref PlayerOne);
            Standard Test3 = new Standard(new Vector2(400, 100), ref PlayerOne);
            GameObjectManager.Add(Test);
            GameObjectManager.Add(Test2);
            GameObjectManager.Add(Test3);
            GameObjectManager.Add(PlayerOne);
            CollisionManager.Test();
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime GameTime)
        {
            InputManager.Update();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            GameObjectManager.Update(GameTime);
            base.Update(GameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            SpriteBatch.Begin();
            GameObjectManager.Draw(SpriteBatch);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
