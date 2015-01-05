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
        Random TestRandom;
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TestRandom = new Random();
        }

        protected override void Initialize()
        {
            Graphics.PreferredBackBufferHeight = 1080;
            Graphics.PreferredBackBufferWidth = 1920;
            Graphics.PreferMultiSampling = true;
            Graphics.ApplyChanges();
            GameObjectManager.IniatlizeAIParamaters();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            TextureManager.LoadContent(Content);
            WeaponManager.Initialize();
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime GameTime)
        {
            InputManager.Update();
            DebugStuff();
            GameObjectManager.Update(GameTime);
            CollisionManager.Update(ref GameTime);
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

        private void SpawnMassEnemies()
        {
            for (int X = 0; X < 10; X++)
            {
                for (int Y = 0; Y < 10; Y++)
                {
                    Standard Test = new Standard(new Vector2(X * 100, Y * 100), ref TestRandom);
                    GameObjectManager.Add(Test);
                }
            }
        }

        private void DebugStuff()
        {
            for (int I = 0; I < 4; I++)
            {
                if (InputManager.Is_Button_Clicked(I, Buttons.DPadDown))
                {
                    GameObjectManager.Add(new AS_Increase(new Vector2(500, 500), 10f));
                }
                if(InputManager.Is_Button_Clicked(I, Buttons.DPadUp))
                {
                    SpawnMassEnemies();
                }
                if(InputManager.Is_Button_Clicked(I, Buttons.DPadLeft))
                {
                    GameObjectManager.DebugTestQuadTree();
                }
            }
        }

    }
}
