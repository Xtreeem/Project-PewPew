using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    class Projectile : Actor
    {
        public Actor Creator { get; protected set; }
        private float LifeTimer;

        public Projectile(Vector2 StartPos, Vector2 StartDir, Actor Creator)
        {
            Weapon = Creator.Weapon;
            Position = StartPos;
            Direction = Vector2.Normalize(StartDir);
            this.Creator = Creator;
            Texture = Weapon.ProjectileTexture;
            MovementSpeed = Weapon.ProjectileSpeed;
            Color = Weapon.Color;
            LifeTimer = Weapon.ProjectileLifeTime;
        }

        public override void Update(GameTime GameTime)
        {
            if (LifeTimer < 0)
                Dying = true;
            else
                LifeTimer -= (float)GameTime.ElapsedGameTime.TotalSeconds;
            base.Update(GameTime);
        }
    }
}
