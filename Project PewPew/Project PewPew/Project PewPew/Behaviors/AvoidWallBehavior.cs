using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    public class AvoidWallBehavior : Behavior
    {
        #region Initialization
        public AvoidWallBehavior(GameObject owner)
            : base(owner)
        {
        }
        #endregion

        #region Update
        public override void Update(GameObject otherObject, AIParameters aiParams)
        {
            base.ResetReaction();
            if (!(Owner.ReactionDistance < aiParams.WallRadius))
                return;
            Vector2 dangerDirection = Vector2.Zero;

            if (Vector2.Dot(
                Owner.Position, Owner.ReactionLocation) >= -(Math.PI / 2))
            {
                Owner.Fleeing = true;
                reacted = true;

                dangerDirection = Owner.Position - Owner.ReactionLocation;
                Vector2.Normalize(ref dangerDirection, out dangerDirection);

                reaction = ((aiParams.PerDangerWeight * 2) * dangerDirection);
            }

        }
        #endregion

    }
}
