using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    public class Player : Actor
    {
        public int PlayerIndex { get; private set; }
        public Weapon SecondaryWeapon { get; private set; }
        public Player(Vector2 StartPos, int PlayerIndex)
        {
            Texture = TextureManager.Player;
            this.PlayerIndex = PlayerIndex;
            Position = StartPos;
            MovementSpeed = 0.6f;
            Color = Color.BlueViolet;
            objecttype = ObjectType.Player;
            Size = 20;
            Health = 10000;
            Armor = 10;
            Weapon = WeaponManager.Get_Weapon("BasicPew");
            SecondaryWeapon = WeaponManager.Get_Weapon("BasicPew");
            Friendly = true;
        }

        public override void Update(GameTime GameTime)
        {
            if (InputManager.Is_Button_Clicked(PlayerIndex, Buttons.RightShoulder))
                if (objecttype == ObjectType.Player)
                {
                    objecttype = ObjectType.Wall;
                    Color = Color.HotPink;
                }
                else if (objecttype == ObjectType.Generic)
                {
                    objecttype = ObjectType.Player;
                    Color = Color.BlueViolet;
                }
                else if (objecttype == ObjectType.Wall)
                {
                    objecttype = ObjectType.Generic;
                    Color = Color.Yellow;
                }

            if (InputManager.Is_Button_Pressed(PlayerIndex, Buttons.LeftTrigger))
            {
                if (InputManager.Is_Button_Clicked(PlayerIndex, Buttons.RightTrigger))
                {
                    Vector2 TurretPos = Position + (InputManager.Get_Aim_Direction(PlayerIndex) * 120);
                    Turret TempTurret = new Turret(this, TurretPos, TurretType.Canon);
                    GameObjectManager.Add(TempTurret);
                }
            }
            else if (InputManager.Is_Button_Pressed(PlayerIndex, Buttons.RightTrigger))
            {
                Shoot(InputManager.Get_Aim_Direction(PlayerIndex));
            }
            Direction = InputManager.Get_Movement_Direction(PlayerIndex);
            base.Update(GameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch)
        {
            if (InputManager.Is_Button_Pressed(PlayerIndex, Buttons.LeftTrigger))
            {
                Vector2 TurretPos = Position + (InputManager.Get_Aim_Direction(PlayerIndex) * 120);
                SpriteBatch.Draw(TextureManager.TurretBase, TurretPos, null, Color.Lime * 0.2f, 0f, new Vector2(TextureManager.TurretBase.Width / 2, TextureManager.TurretBase.Height / 2), 1f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
                SpriteBatch.Draw(TextureManager.TurretCanon, TurretPos, null, Color.Lime * 0.2f, 0f, new Vector2(TextureManager.TurretCanon.Width / 2, TextureManager.TurretCanon.Height / 2), 1f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);

            }
            base.Draw(SpriteBatch);
        }

        private void SwapWeapon()
        {
            Weapon T = Weapon;
            Weapon = SecondaryWeapon;
            SecondaryWeapon = Weapon;
        }

        public void GetWeaponPoweredUp(Weapon Modification)
        {
            Weapon TempWeapon = new Weapon();
            TempWeapon.AttackRate = MathHelper.Clamp((Weapon.AttackRate - Modification.AttackRate), 0.1f, 1000f);
            if (Modification.Color != new Color(0, 0, 0, 0))
                TempWeapon.Color = Modification.Color;
            else
                TempWeapon.Color = Weapon.Color;
            TempWeapon.Damage = Weapon.Damage + Modification.Damage;
            TempWeapon.NumberOfProjecties = Weapon.NumberOfProjecties + Modification.NumberOfProjecties;
            TempWeapon.ProjectileRange = Weapon.ProjectileRange + TempWeapon.ProjectileRange;
            TempWeapon.ProjectileLifeTime = Weapon.ProjectileLifeTime + Modification.ProjectileLifeTime;
            TempWeapon.ProjectileSpeed = Weapon.ProjectileSpeed + Modification.ProjectileSpeed;
            TempWeapon.FriendlyFire = Weapon.FriendlyFire;
            TempWeapon.Explosive = Weapon.Explosive;
            if (Modification.ProjectileTexture != null)
                TempWeapon.ProjectileTexture = Modification.ProjectileTexture;
            else
                TempWeapon.ProjectileTexture = Weapon.ProjectileTexture;
            Weapon = TempWeapon;
        }
    }
}
