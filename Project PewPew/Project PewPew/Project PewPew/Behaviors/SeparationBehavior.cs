using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    /// <summary>
    /// SeparationBehavior is a Behavior that will make an animal move away from
    /// another if it's too close for comfort
    /// </summary>
    class SeparationBehavior : Behavior
    {
        #region Initialization
        public SeparationBehavior(GameObject owner)
            : base(owner)
        {
        }
        #endregion

        #region Update

        /// <summary>
        /// separationBehavior.Update infuences the owning animal to move away from
        /// the otherAnimal is it’s too close, in this case if it’s inside 
        /// AIParameters.separationDistance.
        /// </summary>
        /// <param name="otherObject">the Animal to react to</param>
        /// <param name="aiParams">the Behaviors' parameters</param>
        public override void Update(GameObject otherObject, AIParameters aiParams)
        {
            base.ResetReaction();

            Vector2 pushDirection = Vector2.Zero;
            float weight = aiParams.PerMemberWeight;

            if (Owner.ReactionDistance > 0.0f &&
                Owner.ReactionDistance <= aiParams.SeparationDistance)
            {
                //The otherAnimal is too close so we figure out a pushDirection 
                //vector in the opposite direction of the otherAnimal and then weight
                //that reaction based on how close it is vs. our separationDistance

                pushDirection = Owner.Position - Owner.ReactionLocation;
                Vector2.Normalize(ref pushDirection, out pushDirection);

                //push away
                weight *= (1 -
                    (float)Owner.ReactionDistance / aiParams.SeparationDistance);

                pushDirection *= (weight * new Vector2(1.5f, 1.5f));

                reacted = true;
                reaction += pushDirection;
            }
        }
        #endregion
    }
}
