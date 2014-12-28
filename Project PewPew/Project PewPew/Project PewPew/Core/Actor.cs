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
        public Weapon Weapon { get; protected set; }
        protected float Armor { get; set; }
        public float Health { get; protected set; }
        public float Size { get; protected set; }
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
                Projectile Temp;
                ShootTimer = 0;
                if (Direction != Vector2.Zero)
                Temp = new Projectile(Position, Direction, this);

                else 
                Temp = new Projectile(Position, new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation)), this);

                GameObjectManager.Add(Temp);
            }
        }

        public void PushThisUnit(Vector2 PushVector)
        {
            Position += PushVector;
        }
        public void Damage(float Amount)
        {
            Health -= (Amount * (1- (Armor / 100)));
        }
        public void Heal(float Amount)
        {
            Health += Amount;
        }
    }
}
