using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    public abstract class Enemy : Actor
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
            Direction = Target.CenterPos - CenterPos;
            if (Direction != Vector2.Zero)
                Direction = Vector2.Normalize(Direction);

        }

        public void DeAggro()
        {
            Target = null;
            Direction = Vector2.Zero;
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
