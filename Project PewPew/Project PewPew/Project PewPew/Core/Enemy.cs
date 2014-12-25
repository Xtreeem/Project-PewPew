using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    abstract class Enemy : Actor
    {
        public float Size { get; protected set; }
        public float AggroRange { get; protected set; }
        public float DeAggroRange { get; protected set; }
        public float DistanceToTarget { get { if (Target != null)return Vector2.Distance(Target.CenterPos, CenterPos); else return -1; } }
        protected Player Target { get; set; }
        public override void Update(Microsoft.Xna.Framework.GameTime GameTime)
        {
            base.Update(GameTime);
        }

        protected void Move_To_Player()
        {
            Velocity = Target.CenterPos - CenterPos;
            if (Velocity != Vector2.Zero)
                Velocity = Vector2.Normalize(Velocity);

        }

        public void DeAggro()
        {
            Target = null;
            Velocity = Vector2.Zero;
        }

        public void Aggro(Player Player)
        {
            Target = Player;
        }

        public bool HasTarget()
        {
            if (Target != null)
                return true;
            else
                return false;
        }

    }
}
