using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    class Standard : Enemy
    {
        public Standard(Vector2 StartPos, ref Player Target)
        {
            Position = StartPos;
            this.Target = Target;
            MovementSpeed = 0.3f;
            Color = Color.DarkRed;
            Texture = TextureManager.Player;
            AggroRange = 200f;
            DeAggroRange = 300f;
        }

        public override void Update(GameTime GameTime)
        {
            if (Target != null)
            {
                if (Vector2.Distance(Target.CenterPos, CenterPos) > DeAggroRange)
                    DeAggro();
                else
                    Move_To_Player();
            }
            base.Update(GameTime);
        }
    }
}
