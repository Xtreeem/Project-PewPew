using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    public abstract class GameObject : IHasRect
    {
        protected Dictionary<ObjectType, Behaviors> behaviors;
        public float Size { get; protected set; }

        public RectangleF Rectangle { get; set; }

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
        {
            UpdateRect();
        }

        private void UpdateRect()
        {
            Rectangle = new RectangleF(new PointF(Position.X, Position.Y), new SizeF(this.Size, this.Size));
        }

        public virtual void Draw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Draw(Texture, Position, Microsoft.Xna.Framework.Color.White);
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
            float differance = WrapAngle(desiredAngle - oldAngle);
            
            differance = MathHelper.Clamp(differance, -maxTurnRadians, maxTurnRadians);
            float Test = WrapAngle(oldAngle + differance);

            return new Vector2((float)Math.Cos(Test), (float)Math.Sin(Test));
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
