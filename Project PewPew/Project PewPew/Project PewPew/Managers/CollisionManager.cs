using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    static class CollisionManager
    {
        static List<Enemy> Enemies = new List<Enemy>();
        static List<GameObject> MainObjects = new List<GameObject>();
        public static void Update(ref GameTime GameTime)
        {
            GameObjectManager.Get_GameObjects(out MainObjects);
            GameObjectManager.Get_Enemies(out Enemies);

            for (int I = 0; I < MainObjects.Count; I++)
            {
                if (MainObjects[I] is Actor)
                    for (int Y = I + 1; Y < MainObjects.Count; Y++) //To ensure that no objects are tested against each other twice
                    {
                        if (MainObjects[Y] is Actor)
                            if (CheckCollision(MainObjects[I] as Actor, MainObjects[Y] as Actor))
                                ManageCollision(MainObjects[I] as Actor, MainObjects[Y] as Actor, ref GameTime);
                    }
            }
        }

        private static void ManageCollision(Actor A, Actor B, ref GameTime GameTime)
        {
            if (A is Player)
            {
                if (B is Player)
                {
                    HandleCollision(A as Player, B as Player, ref GameTime);
                }
                else if (B is Enemy)
                {
                    HandleCollision(A as Player, B as Enemy, ref GameTime);
                }
                else if (B is Turret)
                {
                    HandleCollision(A as Player, B as Turret, ref GameTime);
                }
                else if (B is Projectile)
                {
                    HandleCollision(A as Player, B as Projectile, ref GameTime);
                }
            }
            else if (A is Enemy)
            {
                if (B is Player)
                {
                    HandleCollision(A as Enemy, B as Player, ref GameTime);
                }
                else if (B is Enemy)
                {
                    HandleCollision(A as Enemy, B as Enemy, ref GameTime);
                }
                else if (B is Turret)
                {
                    HandleCollision(A as Enemy, B as Turret, ref GameTime);
                }
                else if (B is Projectile)
                {
                    HandleCollision(A as Enemy, B as Projectile, ref GameTime);
                }
            }
            else if (A is Turret)
            {
                if (B is Player)
                {
                    HandleCollision(A as Turret, B as Player, ref GameTime);
                }
                else if (B is Enemy)
                {
                    HandleCollision(A as Turret, B as Enemy, ref GameTime);
                }
                else if (B is Turret)
                {
                    HandleCollision(A as Turret, B as Turret, ref GameTime);
                }
                else if (B is Projectile)
                {
                    HandleCollision(A as Turret, B as Projectile, ref GameTime);
                }
            }
            else if (A is Projectile)
            {
                if (B is Player)
                {
                    HandleCollision(A as Projectile, B as Player, ref GameTime);
                }
                else if (B is Enemy)
                {
                    HandleCollision(A as Projectile, B as Enemy, ref GameTime);
                }
                else if (B is Turret)
                {
                    HandleCollision(A as Projectile, B as Turret, ref GameTime);
                }
                else if (B is Projectile)
                {
                    HandleCollision(A as Projectile, B as Projectile, ref GameTime);
                }
            }
        }
        #region CollisionHandlers
        private static void HandleCollision(Player A, Player B, ref GameTime GameTime)
        {
            A.BumpBack();
            B.BumpBack();
        }
        private static void HandleCollision(Player A, Enemy B, ref GameTime GameTime)
        {
            A.Damage(B.CollisionDamage);
            B.Die();
        }
        private static void HandleCollision(Player A, Turret B, ref GameTime GameTime) {
            A.BumpBack();
            B.BumpBack();
        }
        private static void HandleCollision(Player A, Projectile B, ref GameTime GameTime) { }
        private static void HandleCollision(Enemy A, Player B, ref GameTime GameTime) {
            A.Die();
            B.Damage(A.CollisionDamage);
        }
        private static void HandleCollision(Enemy A, Enemy B, ref GameTime GameTime) { }
        private static void HandleCollision(Enemy A, Turret B, ref GameTime GameTime) { }
        private static void HandleCollision(Enemy A, Projectile B, ref GameTime GameTime) {
            A.Damage(B.Weapon.Damage);
            B.Die();
        }
        private static void HandleCollision(Turret A, Player B, ref GameTime GameTime) {
            A.BumpBack();
            B.BumpBack();
        }
        private static void HandleCollision(Turret A, Enemy B, ref GameTime GameTime) {
            A.BumpBack();
            B.BumpBack();
        }
        private static void HandleCollision(Turret A, Turret B, ref GameTime GameTime) { }
        private static void HandleCollision(Turret A, Projectile B, ref GameTime GameTime) { }
        private static void HandleCollision(Projectile A, Player B, ref GameTime GameTime) { }
        private static void HandleCollision(Projectile A, Enemy B, ref GameTime GameTime) {
            A.Die();
            B.Damage(A.Weapon.Damage);
        }
        private static void HandleCollision(Projectile A, Turret B, ref GameTime GameTime) { }
        private static void HandleCollision(Projectile A, Projectile B, ref GameTime GameTime) { }
        #endregion



        private static bool CheckCollision(Actor A, Actor B)
        {
            if (Vector2.Distance(A.CenterPos, B.CenterPos) <= (A.Size + B.Size))
                return true;
            else
                return false;
        }


    }
}
