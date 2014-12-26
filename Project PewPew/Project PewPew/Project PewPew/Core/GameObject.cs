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
        protected Dictionary<ObjectType, Behaviors> behaviors;

        public ObjectType ObjectType
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
        public GameObject()
        {
            behaviors = new Dictionary<ObjectType, Behaviors>();
        }

        /// <summary>
        /// This function clamps turn rates to no more than maxTurnRadians
        /// </summary>
        /// <param name="oldDir">current movement direction</param>
        /// <param name="newDir">desired movement direction</param>
        /// <param name="maxTurnRadians">max turn in radians</param>
        /// <returns></returns>
        protected static Vector2 ChangeDirection(
            Vector2 oldDir, Vector2 newDir, float maxTurnRadians)
        {
            float oldAngle = (float)Math.Atan2(oldDir.Y, oldDir.X);
            float desiredAngle = (float)Math.Atan2(newDir.Y, newDir.X);
            //float newAngle = MathHelper.Clamp(desiredAngle, (oldAngle - maxTurnRadians), (oldAngle + maxTurnRadians));
            float newAngle = MathHelper.Clamp(desiredAngle, WrapAngle(oldAngle - maxTurnRadians), WrapAngle(oldAngle + maxTurnRadians));
            
            return new Vector2((float)Math.Cos(newAngle), (float)Math.Sin(newAngle));
        }

        /// <summary>
        /// clamps the angle in radians between -Pi and Pi.
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }


}}
