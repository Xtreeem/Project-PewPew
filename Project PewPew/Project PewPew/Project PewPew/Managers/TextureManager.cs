using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    static class TextureManager
    {
        public static Texture2D Player { get; private set; }
        public static Texture2D TurretBase { get; private set; }
        public static Texture2D TurretCanon { get; private set; }


        public static void LoadContent(ContentManager Content)
        {
            Player = Content.Load<Texture2D>("../../Textures/Player");
            TurretBase = Content.Load<Texture2D>("../../Textures/TurretBase");
            TurretCanon = Content.Load<Texture2D>("../../Textures/TurretCanon");

        }
    }
}
