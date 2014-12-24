using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    class Player : Actor
    {
        public int PlayerIndex { get; private set; }
        public Player(Vector2 StartPos, int PlayerIndex)
        {
            Texture = TextureManager.Player;
            this.PlayerIndex = PlayerIndex;
            Position = StartPos;
            MovementSpeed = 0.6f;
            Color = Color.BlueViolet;
        }

        public override void Update(GameTime GameTime)
        {
            Velocity = InputManager.Get_Movement_Direction(PlayerIndex);
            base.Update(GameTime);
        }


    }
}
