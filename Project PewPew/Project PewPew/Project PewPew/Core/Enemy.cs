using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    abstract class Enemy : Actor
    {
        protected Player Target { get; set; }
        public override void Update(Microsoft.Xna.Framework.GameTime GameTime)
        {
            Move_To_Player();
            base.Update(GameTime);
        }

        private void Move_To_Player()
        {
            Velocity = InputManager.Get_Aim_Direction(1);
            //Velocity = Position - Target.Position;
            //Velocity.Normalize();
        }

    }
}
