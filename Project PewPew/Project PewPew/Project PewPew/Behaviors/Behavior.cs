using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    public abstract class Behavior
    {
        #region Fields
        /// <summary>
        /// Keep track of the animal that this behavior belongs to.
        /// </summary>
        public GameObject Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        private GameObject owner;

        /// <summary>
        /// Store the behavior reaction here.
        /// </summary>
        public Vector2 Reaction
        {
            get { return reaction; }
        }
        protected Vector2 reaction;

        /// <summary>
        /// Store if the behavior has reaction results here.
        /// </summary>
        public bool Reacted
        {
            get { return reacted; }
        }
        protected bool reacted;
        #endregion


        #region Initialization
        protected Behavior(GameObject owner)
        {
            this.owner = owner;
        }
        #endregion

        #region Update
        /// <summary>
        /// Abstract function that the subclass must impliment. Figure out the 
        /// Behavior reaction here.
        /// </summary>
        /// <param name="otherAnimal">the Animal to react to</param>
        /// <param name="aiParams">the Behaviors' parameters</param>
        public abstract void Update(GameObject otherObject, AIParameters aiParams);

        /// <summary>
        /// Reset the behavior reactions from the last Update
        /// </summary>
        protected void ResetReaction()
        {
            reacted = false;
            reaction = Vector2.Zero;
        }
        #endregion
    }
}
