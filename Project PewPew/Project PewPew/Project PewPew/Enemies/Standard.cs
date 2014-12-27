using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    class Standard : Enemy
    {
        public Standard(Vector2 StartPos, ref Player Target, ref Random random) : base(ref random)
        {
            Position = StartPos;
            this.Target = Target;
            MovementSpeed = 0.3f;
            Color = Color.DarkRed;
            Texture = TextureManager.Enemy;
            AggroRange = 200f;
            DeAggroRange = 3000f;
            Size = 15;
            Armor = 5;
            BuildBehaviors();
            Health = 100f;
        }

        public override void Update(GameTime GameTime)
        {
            if (Target != null)
            {
                if (Vector2.Distance(Target.CenterPos, CenterPos) > DeAggroRange)
                    DeAggro();
                //else
                //    Aggro()
            }
            base.Update(GameTime);
        }

        public void BuildBehaviors()
        {
            Behaviors WallReactions = new Behaviors();
            WallReactions.Add(new AvoidWallBehavior(this));
            behaviors.Add(ObjectType.Wall, WallReactions);

            Behaviors FriendlyReactions = new Behaviors();
            FriendlyReactions.Add(new CohesionBehavior(this));
            FriendlyReactions.Add(new SeparationBehavior(this));
            behaviors.Add(ObjectType.Enemy, FriendlyReactions);

            Behaviors TargetReactions = new Behaviors();
            TargetReactions.Add(new StandardAttackBehavior(this));
            behaviors.Add(ObjectType.Player, TargetReactions);

        }


    }
}
