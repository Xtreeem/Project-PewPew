using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    public enum ObjectType
    {
        Generic,
        Wall,
        Enemy,
        Player
    }
    public abstract class GameObject
    {
        protected Dictionary<ObjectType, Behaviors> Behaviors;

        public ObjectType AnimalType
        {
            get
            {
                return objecttype;
            }
        }
        protected ObjectType objecttype = ObjectType.Generic;

        /// <summary>
        /// Reaction distance
        /// </summary>
        public float ReactionDistance
        {
            get
            {
                return reactionDistance;
            }
        }
        protected float reactionDistance;

        /// <summary>
        /// Reaction location
        /// </summary>
        public Vector2 ReactionLocation
        {
            get
            {
                return reactionLocation;
            }
        }
        protected Vector2 reactionLocation;

        public bool Fleeing
        {
            get
            {
                return fleeing;
            }
            set
            {
                fleeing = value;
            }
        }
        protected bool fleeing = false;






        public Vector2 Direction { get; protected set; }

        public Vector2 Position { get; protected set; }
        public Texture2D Texture { get; protected set; }

        public Object Lock = new Object();

        public bool Dying { get; protected set; }

        public virtual void Update(GameTime GameTime)
        { }

        public virtual void Draw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
