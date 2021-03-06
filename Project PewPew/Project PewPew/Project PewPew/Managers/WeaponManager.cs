﻿using Microsoft.Xna.Framework;
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
        public float ProjectileRange;       //The distance a projectile may travel before despawning
        public float ProjectileSpread;      //Distance between each bullet
        public float Accuracy;              //How accurate the weapon is
        public int NumberOfProjecties;      //Number of shots fired
        public FireMode FireMode;           //How the weapon Fires
        public SpreadMode SpreadMode;       //Defines how the bullets will behave
        public bool Homing;                 //Is the attack Homing
        public bool Explosive;              //Will the attack explode on hit
        public Texture2D ProjectileTexture; //Texture used for the projectiles
        public Color Color;                 //Color of the projectile
        public bool FriendlyFire;           //Checks to see if the projectile can damage friendlies
    }

    public enum FireMode
    {
        Beam,
        Burst,
        Semi,
        FullAuto
    }

    public enum SpreadMode
    {
        Line,
        Cone,
        Cluster
    }

    public static class WeaponManager
    {
        private static List<Weapon> Weapons = new List<Weapon>();

        public static void Initialize()
        {
            Weapon TempWeapon = new Weapon();
            #region BasicPew
            TempWeapon.Name = "BasicPew";
            TempWeapon.AttackRate = 0.4f;
            TempWeapon.ProjectileSpeed = 0.1f;
            TempWeapon.ProjectileLifeTime = 5f;
            TempWeapon.ProjectileSpread = 8;
            TempWeapon.Accuracy = 100;
            TempWeapon.Damage = 50f;
            TempWeapon.Explosive = false;
            TempWeapon.FireMode = FireMode.FullAuto;
            TempWeapon.SpreadMode = SpreadMode.Line;
            TempWeapon.Homing = false;
            TempWeapon.NumberOfProjecties = 7;
            TempWeapon.ProjectileRange = 1000;
            TempWeapon.ProjectileTexture = TextureManager.BasicBullet;
            TempWeapon.Color = Color.White;
            TempWeapon.FriendlyFire = false;
            Weapons.Add(TempWeapon);
            #endregion
            #region SecondaryPew
            TempWeapon.Name = "SecondaryPew";
            TempWeapon.AttackRate = 0.2f;
            TempWeapon.ProjectileSpeed = 0.5f;
            TempWeapon.ProjectileLifeTime = 4f;
            TempWeapon.ProjectileSpread = 8f;
            TempWeapon.Accuracy = 50;
            TempWeapon.Damage = 10f;
            TempWeapon.Explosive = false;
            TempWeapon.FireMode = FireMode.FullAuto;
            TempWeapon.SpreadMode = SpreadMode.Line;
            TempWeapon.Homing = false;
            TempWeapon.NumberOfProjecties = 20;
            TempWeapon.ProjectileRange = 800;
            TempWeapon.ProjectileTexture = TextureManager.BasicBullet;
            TempWeapon.Color = Color.Lime;
            TempWeapon.FriendlyFire = false;
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
