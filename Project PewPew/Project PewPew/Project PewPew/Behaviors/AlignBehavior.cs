using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    class AlignBehavior : Behavior
    {
        #region Initialization
        public AlignBehavior(GameObject owner)
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

            if (otherObject != null)
            {
                reacted = true;
                reaction = otherObject.Direction * aiParams.PerMemberWeight;
            }
        }
        #endregion
    }
}
