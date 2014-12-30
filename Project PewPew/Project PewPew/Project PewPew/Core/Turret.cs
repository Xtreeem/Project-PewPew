using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    public enum TurretType
    {
        Canon,
        Laser
    }
    class Turret : Actor
    {
        public TurretType TypeOfTurret { get; protected set; }
        public Player Creator { get; protected set; }
        Texture2D TurretTex;
        Vector2 TurretOrigin;
        public float DistanceToTarget
        {
            get
            {
                if (Target != null)
                    return (Vector2.Distance(Position, Target.Position));
                else
                    return 1000000;
            }
        }
        public Actor Target { get; protected set; }
        public float AttackRange { get; protected set; }
        public bool NeedNewTarget { get { if (DistanceToTarget > AttackRange) return true; else return false; } }
        public Turret(Player Creator, Vector2 Position, TurretType TypeOfTurret)
        {
            this.TypeOfTurret = TypeOfTurret;
            this.Position = Position;
            Friendly = true;
            switch (this.TypeOfTurret)
            {
                case TurretType.Canon:
                    TurretTex = TextureManager.TurretCanon;
                    Color = Color.LightGray;
                    break;
                case TurretType.Laser:
                    TurretTex = TextureManager.TurretCanon; //TODO: REplace with laser texture
                    Color = Color.Azure;
                    break;
                default:
                    break;
            }
            Texture = TextureManager.TurretBase;
            TurretOrigin = new Vector2(TurretTex.Width / 2, TurretTex.Height / 2);
            objecttype = ObjectType.Wall;
            AttackRange = 3000;
            Size = 20;
            Dying = false;
            this.Weapon = Creator.Weapon;
        }

        public override void Update(GameTime GameTime)
        {
            IsTargetStillAlive();

            if (Target != null)
                Shoot(Vector2.Zero);
            base.Update(GameTime);
        }

        public void Set_Target(Enemy newTarget)
        {
            Target = newTarget;
        }

        public void FaceTarget()
        {
            if (Target != null)
            {
                Vector2 tempVector = Target.CenterPos - CenterPos;
                Rotation = (float)Math.Atan2(tempVector.Y, tempVector.X);
            }
        }


        public override void Draw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Draw(Texture, Position, null, Color, 0f, Origin, 1f, SpriteEffects.None, 0f);
            SpriteBatch.Draw(TurretTex, Position, null, Color, Rotation, TurretOrigin, 1f, SpriteEffects.None, 0f);
        }
        private void IsTargetStillAlive()
        {
            if (Target != null && Target.Dying == true)
                Target = null;

        }
    }
}
