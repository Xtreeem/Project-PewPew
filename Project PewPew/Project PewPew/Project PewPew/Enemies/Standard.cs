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
            AggroRange = 2000f;
            DeAggroRange = 3000f;
            Size = 10;
            BuildBehaviors();
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

        public void BuildBehaviors()
        {
            Behaviors WallReactions = new Behaviors();
            WallReactions.Add(new FleeBehavior(this));
            behaviors.Add(ObjectType.Player, WallReactions);

            Behaviors FriendlyReactions = new Behaviors();
            FriendlyReactions.Add(new AlignBehavior(this));
            FriendlyReactions.Add(new CohesionBehavior(this));
            FriendlyReactions.Add(new SeparationBehavior(this));
            behaviors.Add(ObjectType.Enemy, FriendlyReactions);
        }

        public void ResetThink()
        {
            Fleeing = false;
            aiNewDir = Vector2.Zero;
            aiNumSeen = 0;
            reactionDistance = 0f;
            reactionLocation = Vector2.Zero;
        }
    }
}
