using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    abstract class GameObject
    {
        public Vector2 Position { get; protected set; }
        public Texture2D Texture { get; protected set; }

        public bool Dying { get; protected set; }

        public virtual void Update(GameTime GameTime)
        { }

        public virtual void Draw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
