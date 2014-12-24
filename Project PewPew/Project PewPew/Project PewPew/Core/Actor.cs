using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    abstract class Actor : GameObject
    {
        public Vector2 CenterPos { get { return Position + Origin; } }
        public Vector2 Velocity { get; protected set; }
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
            if (Velocity != Vector2.Zero)
            {
                Rotation = (float)Math.Atan2(Velocity.Y, Velocity.X);
            }
            Position += (Velocity * GameTime.ElapsedGameTime.Milliseconds * MovementSpeed);
        }
    }
}
