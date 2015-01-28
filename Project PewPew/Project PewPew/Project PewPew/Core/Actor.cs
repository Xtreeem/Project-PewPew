using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{


    public abstract class Actor : GameObject
    {
        public Weapon Weapon { get; set; }
        protected float Armor { get; set; }
        public float Health { get; protected set; }
        public Vector2 CenterPos { get { return Position + Origin; } }
        protected float Rotation { get; set; }
        public float MovementSpeed { get; protected set; }
        protected Color Color { get; set; }
        public bool Friendly { get; protected set; }
        public Vector2 LastPosition { get; protected set; }
        protected Vector2 Origin
        {
            get
            {
                return (new Vector2(Texture.Width / 2, Texture.Height / 2));
            }
        }
        private float ShootTimer = 0;
        public override void Update(GameTime GameTime)
        {
            ShootTimer += (float)GameTime.ElapsedGameTime.TotalSeconds;
            if (Direction != null)
                Move(GameTime);
            base.Update(GameTime);
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
        public void Die()
        {
            Dying = true;
        }
        public void BumpBack()
        {
            Position = LastPosition;
        }

        public void Shoot(Vector2 Direction)
        {
            if (Weapon.AttackRate < ShootTimer)
            {
                //Projectile Temp;
                //ShootTimer = 0;
                //if (Direction != Vector2.Zero)
                //    Temp = new Projectile(Position, Direction, this);
                //else
                //    Temp = new Projectile(Position, new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)), this);
                //GameObjectManager.Add(Temp);
                //if(Weapon.NumberOfProjecties % 2 != 0)
                //{
                //    if (Direction == Vector2.Zero)
                //        Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
                //    float AimAngle = Misc.ToAngle(Direction);                                                     //Converts the players input into an angle
                //    Quaternion AimQuat = Quaternion.CreateFromYawPitchRoll(0, 0, AimAngle);             //Confusing magic used to Rotate the projectiles around ship
                //    float RandomSpread = Misc.NextFloat(-0.04f, 0.04f) + Misc.NextFloat(-0.04f, 0.04f); //Adds a bit of random spread to the shot
                //    Vector2 Velocity = Misc.FromPolar(AimAngle + RandomSpread, 11f);                //Creates the velocity tthat the bullets will be using
                //    GameObjectManager.Add(new Projectile(Position, Velocity, this));                 //Adds the first projectile to the GameObjectManager
                //}
                //if (Weapon.NumberOfProjecties % 2 == 0)
                //{

                //    if (Direction == Vector2.Zero)
                //        Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
                //    float AimAngle = Misc.ToAngle(Direction);                                                     //Converts the players input into an angle
                //    Quaternion AimQuat = Quaternion.CreateFromYawPitchRoll(0, 0, AimAngle);             //Confusing magic used to Rotate the projectiles around ship
                //    float RandomSpread = Misc.NextFloat(-0.04f, 0.04f) + Misc.NextFloat(-0.04f, 0.04f); //Adds a bit of random spread to the shot
                //    Vector2 Velocity = Misc.FromPolar(AimAngle + RandomSpread, 11f);                //Creates the velocity tthat the bullets will be using
                //    Vector2 Offset = Vector2.Transform(new Vector2(35, -8), AimQuat);                   //Calculates the offset the projectiles will use
                //    GameObjectManager.Add(new Projectile(Position + Offset, Velocity, this));                 //Adds the first projectile to the GameObjectManager
                //    Offset = Vector2.Transform(new Vector2(35, 8), AimQuat);                            //Inverts the offset (Sorta) 
                //    GameObjectManager.Add(new Projectile(Position + Offset, Velocity, this));                 //Adds the first projectile to the GameObjectManager
                //}

                switch (Weapon.SpreadMode)
                {
                    case SpreadMode.Line:
                        #region LineFire
                        bool Up = true;
                        int IncrementOffsetCounter = 2;
                        if (Direction == Vector2.Zero)
                            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
                        float AimAngle = Misc.ToAngle(Direction);
                        Quaternion AimQuat = Quaternion.CreateFromYawPitchRoll(0, 0, AimAngle);
                        float RandomSpread = Misc.NextFloat(-0.04f, 0.04f) + Misc.NextFloat(-0.04f, 0.04f);
                        Vector2 Velocity = Misc.FromPolar(AimAngle + RandomSpread, 11f);
                        float Spread = Weapon.ProjectileSpread;
                        Vector2 Offset = Vector2.Transform(new Vector2(35, 0), AimQuat);
                        int LoopStart = 0;
                        if (Weapon.NumberOfProjecties % 2 != 0)
                        {
                            GameObjectManager.Add(new Projectile(Position + Offset, Velocity, this));
                            LoopStart = 1;
                        }
                        else
                            Spread /= 2;

                        for (int I = LoopStart; I < Weapon.NumberOfProjecties; I++)
                        {
                            if (Up)
                            {
                                Up = false;
                                Offset = Vector2.Transform(new Vector2(35, -Spread), AimQuat);
                            }
                            else
                            {
                                Up = true;
                                Offset = Vector2.Transform(new Vector2(35, Spread), AimQuat);
                            }
                            if (--IncrementOffsetCounter == 0)
                            {
                                Spread += Weapon.ProjectileSpread;
                                IncrementOffsetCounter = 2;
                            }
                            GameObjectManager.Add(new Projectile(Position + Offset, Velocity, this));
                        }
                        #endregion
                        break;
                    case SpreadMode.Cone:
                        break;
                    case SpreadMode.Cluster:
                        break;
                    default:
                        break;
                }
                ShootTimer = 0;
            }
        }

        public void PushThisUnit(Vector2 PushVector)
        {
            Position += PushVector;
        }
        public void Damage(float Amount)
        {
            Health -= (Amount * (1 - (Armor / 100)));
        }
        public void Heal(float Amount)
        {
            Health += Amount;
        }
    }
}
