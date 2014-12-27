using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    public abstract class Actor : GameObject
    {
        protected float Armor { get; set; }
        public float Health { get; protected set; }
        public float Size { get; protected set; }
        public Vector2 CenterPos { get { return Position + Origin; } }
        protected float Rotation { get; set; }
        public float MovementSpeed { get; protected set; }
        protected Color Color { get; set; }
        public Vector2 LastPosition{get; protected set;}
        protected Vector2 Origin
        {
            get
            {
                return (new Vector2(Texture.Width / 2, Texture.Height / 2));
            }
        }
        public override void Update(GameTime GameTime)
        {
            if (Direction != null)
                Move(GameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch)
        {
            SpriteBatch.Draw(Texture, Position, null, Color, Rotation, Origin, 1f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
        }


        protected void Move(GameTime GameTime)
        {
            LastPosition = Position;
            if (Direction != Vector2.Zero)
            {
                Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
            }
            Position += (Direction * GameTime.ElapsedGameTime.Milliseconds * MovementSpeed);
            
        }

        public void BumpBack()
        {
            Position = LastPosition;
        }

        public void PushThisUnit(Vector2 PushVector)
        {
            Position += PushVector;
        }
        public void Damage(float Amount)
        {
            Health -= (Amount * (Armor / 100));
        }
        public void Heal(float Amount)
        {
            Health += Amount;
        }
    }
}
