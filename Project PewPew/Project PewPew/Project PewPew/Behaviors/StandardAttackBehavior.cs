using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    class StandardAttackBehavior : Behavior
    {
        #region Initialization
        public StandardAttackBehavior(GameObject owner)
            : base(owner)
        {
        }
        #endregion

        #region Update

        /// <summary>
        /// AlignBehavior.Update infuences the owning animal to move in same the 
        /// direction as the otherAnimal that it sees.
        /// </summary>
        /// <param name="otherObject">the Animal to react to</param>
        /// <param name="aiParams">the Behaviors' parameters</param>
        public override void Update(GameObject otherObject, AIParameters aiParams)
        {
            base.ResetReaction();

            if (otherObject != null && (Owner as Enemy).HasTarget())
            {
                Vector2 pullDirection = otherObject.Position - Owner.Position;
                Vector2.Normalize(ref pullDirection, out pullDirection);
                reacted = true;
                reaction = pullDirection * aiParams.PerPlayerWeight;
                reaction = reaction;
            }
        }
        #endregion
    }
}
