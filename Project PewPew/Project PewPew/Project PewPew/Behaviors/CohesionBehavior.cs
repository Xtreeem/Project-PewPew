using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    class CohesionBehavior : Behavior
    {
        #region Initialization
        public CohesionBehavior(GameObject owner)
            : base(owner)
        {
        }
        #endregion

        #region Update

        /// <summary>
        /// CohesionBehavior.Update infuences the owning animal to move towards the
        /// otherAnimal that it sees as long as it isn’t too close, in this case 
        /// that means inside the separationDist in the passed in AIParameters.
        /// </summary>
        /// <param name="otherObject">the Animal to react to</param>
        /// <param name="aiParams">the Behaviors' parameters</param>
        public override void Update(GameObject otherObject, AIParameters aiParams)
        {
            base.ResetReaction();

            Vector2 pullDirection = Vector2.Zero;
            float weight = aiParams.PerMemberWeight;

            //if the otherAnimal is too close we dont' want to fly any
            //closer to it
            if (Owner.ReactionDistance > 0.0f
                && Owner.ReactionDistance > aiParams.SeparationDistance)
            {
                //We want to make the animal move closer the the otherAnimal so we 
                //create a pullDirection vector pointing to the otherAnimal bird and 
                //weigh it based on how close the otherAnimal is relative to the 
                //AIParameters.separationDistance.
                pullDirection = -(Owner.Position - Owner.ReactionLocation);
                Vector2.Normalize(ref pullDirection, out pullDirection);

                weight *= (float)Math.Pow((double)
                    (Owner.ReactionDistance - aiParams.SeparationDistance) /
                        (aiParams.DetectionDistance - aiParams.SeparationDistance), 2);

                pullDirection *= weight;

                reacted = true;
                reaction = pullDirection;
            }
        }
        #endregion
    }
}
