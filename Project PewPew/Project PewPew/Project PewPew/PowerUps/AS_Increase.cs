using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    class AS_Increase : PowerUp
    {
            Weapon Modification;
        public AS_Increase(Vector2 Position, float PowerUpStrength)
        {
            this.Position = Position;
            Texture = TextureManager.AttackSpeedPowerUp;
            Color = Color.White;
            Size = 10;
            this.PowerUpStrength = PowerUpStrength;
            Modification.AttackRate = PowerUpStrength;
        }

        public override void PickedUp(ref Player Player)
        {
            Player.GetWeaponPoweredUp(Modification);
            Die();
        }


    }
}
