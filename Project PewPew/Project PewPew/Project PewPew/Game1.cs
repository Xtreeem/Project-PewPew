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
    #region AIParameters
    public struct AIParameters
    {
        /// <summary>
        /// how far away the animals see each other
        /// </summary>
        public float DetectionDistance;
        /// <summary>
        /// seperate from animals inside this distance
        /// </summary>
        public float SeparationDistance;
        /// <summary>
        /// how much the animal tends to move in it's previous direction
        /// </summary>
        public float MoveInOldDirectionInfluence;
        /// <summary>
        /// how much the animal tends to move with animals in it's detection distance
        /// </summary>
        public float MoveInFlockDirectionInfluence;
        /// <summary>
        /// how much the animal tends to move randomly
        /// </summary>
        public float MoveInRandomDirectionInfluence;
        /// <summary>
        /// how quickly the animal can turn
        /// </summary>
        public float MaxTurnRadians;
        /// <summary>
        /// how much each nearby animal influences it's behavior
        /// </summary>
        public float PerMemberWeight;
        /// <summary>
        /// how much dangerous animals influence it's behavior
        /// </summary>
        public float PerDangerWeight;


        // Default value for the AI parameters
        const float detectionDefault = 70.0f;
        const float separationDefault = 50.0f;
        const float moveInOldDirInfluenceDefault = 1.0f;
        const float moveInFlockDirInfluenceDefault = 1.0f;
        const float moveInRandomDirInfluenceDefault = 0.05f;
        const float maxTurnRadiansDefault = 6.0f;
        const float perMemberWeightDefault = 1.0f;
        const float perDangerWeightDefault = 50.0f;
    }
    #endregion


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
            Graphics.PreferredBackBufferHeight = 1080;
            Graphics.PreferredBackBufferWidth = 1920;
            Graphics.PreferMultiSampling = true;
            Graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            TextureManager.LoadContent(Content);
            CollisionManager.Initialize();
            PlayerOne = new Player(new Vector2(200, 200), 1);
            SpawnMassEnemies();
            GameObjectManager.Add(PlayerOne);
            CollisionManager.Test();
            CollisionManager.Update();
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
            CollisionManager.Update();
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
            for (int X = 0; X < 19; X++)
            {
                for (int Y = 0; Y < 11; Y++)
                {
                    Standard Test = new Standard(new Vector2(X*100, Y*100), ref PlayerOne);
                    GameObjectManager.Add(Test);
                }
            }


        }

    }
}
