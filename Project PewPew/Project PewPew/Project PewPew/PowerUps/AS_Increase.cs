using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    class AS_Increase : PowerUp
    {
        public AS_Increase(Vector2 Position, float PowerUpStrength)
        {
            Texture = TextureManager.AttackSpeedPowerUp;
            Size = 10;
            this.PowerUpStrength = PowerUpStrength;
        }

        public override void PickedUp(ref Player Player)
        {
            Weapon TempWeapon = Player.Weapon;
            TempWeapon.AttackRate = MathHelper.Clamp(Player.Weapon.AttackRate - PowerUpStrength, 0.1f, 100f);
            Player.Weapon = TempWeapon; 
        }
    }
}
