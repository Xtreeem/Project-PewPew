using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    public struct Weapon
    {
        public string Name;                 //Name of Weapon
        public float AttackRate;            //Rate of Fire
        public float Damage;                //Damage per projectile
        public float ProjectileSpeed;       //Speed of the Projectile
        public float ProjectileLifeTime;    //The Time a projectile will live
        public int NumberOfProjecties;      //Number of shots fired
        public FireMode FireMode;           //How the weapon Fires
        public bool Homing;                 //Is the attack Homing
        public bool Explosive;              //Will the attack explode on hit
        public Texture2D ProjectileTexture; //Texture used for the projectiles
        public Color Color;                 //Color of the projectile
    }

    public enum FireMode
    {
        Beam,
        Burst,
        Semi,
        FullAuto
    }
    public static class WeaponManager
    {
        private static List<Weapon> Weapons = new List<Weapon>();

        public static void Initialize()
        {
            Weapon TempWeapon = new Weapon();
            #region BasicPew
            TempWeapon.Name = "BasicPew";
            TempWeapon.AttackRate = 0.1f;
            TempWeapon.ProjectileSpeed = 0.9f;
            TempWeapon.ProjectileLifeTime = 5f;
            TempWeapon.Damage = 50f;
            TempWeapon.Explosive = false;
            TempWeapon.FireMode = FireMode.FullAuto;
            TempWeapon.Homing = false;
            TempWeapon.NumberOfProjecties = 1;
            TempWeapon.ProjectileTexture = TextureManager.BasicBullet;
            TempWeapon.Color = Color.White;
            Weapons.Add(TempWeapon);
            #endregion
        }

        public static Weapon Get_Weapon(string Name)
        {
            foreach (Weapon W in Weapons)
            {
                if (W.Name == Name)
                    return W;
            }
            throw (new Exception("Something Fucked Up: Weapon Not Found In Manager"));
            //return Weapons[0];
        }


    }
}
