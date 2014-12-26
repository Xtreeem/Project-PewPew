using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    public abstract class Actor : GameObject
    {
        public Vector2 CenterPos { get { return Position + Origin; } }
        protected float Rotation { get; set; }
        protected float MovementSpeed { get; set; }
        protected Color Color { get; set; }
        protected Vector2 Origin
        {
            get
            {
                return (new Vector2(Texture.Width / 2, Texture.Height / 2));
            }
        }
        public override void Update(GameTime GameTime)
        {
            Move(GameTime);
            base.Update(GameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch)
        {
            SpriteBatch.Draw(Texture, Position, null, Color, Rotation, Origin, 1f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
        }


        private void Move(GameTime GameTime)
        {
            if (Direction != Vector2.Zero)
            {
                Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
            }
            Position += (Direction * GameTime.ElapsedGameTime.Milliseconds * MovementSpeed);
        }

        public void PushThisUnit(Vector2 PushVector)
        {
            Position += PushVector;
        }
    }
}
