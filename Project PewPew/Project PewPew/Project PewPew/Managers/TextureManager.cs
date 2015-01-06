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
        public static Texture2D Enemy { get; private set; }
        public static Texture2D BasicBullet { get; private set; }
        public static Texture2D TurretBase { get; private set; }
        public static Texture2D TurretCanon { get; private set; }
        public static Texture2D BasicPowerUp { get; private set; }
        public static Texture2D AttackSpeedPowerUp { get; private set; }
        public static Texture2D Debug_OnePixel { get; private set; }

        public static void LoadContent(ContentManager Content)
        {
            Player = Content.Load<Texture2D>("../../Textures/Player");
            Enemy = Content.Load<Texture2D>("../../Textures/Enemy/Enemy");
            BasicBullet = Content.Load<Texture2D>("../../Textures/BasicBullet");
            TurretBase = Content.Load<Texture2D>("../../Textures/Turrets/TurretBase");
            TurretCanon = Content.Load<Texture2D>("../../Textures/Turrets/TurretCanon");
            BasicPowerUp = Content.Load<Texture2D>("../../Textures/PowerUp/PowerUp");
            AttackSpeedPowerUp = Content.Load<Texture2D>("../../Textures/PowerUp/ASPowerUp");
            Debug_OnePixel = Content.Load<Texture2D>("../../Textures/Debug/OnePixel");

        }
    }
}
