using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    class Standard : Enemy
    {
        public Standard(Vector2 StartPos, ref Player Target)
        {
            Position = StartPos;
            this.Target = Target;
            MovementSpeed = 0.4f;
            Color = Color.DarkRed;
            Texture = TextureManager.Player;
        }




    }
}
