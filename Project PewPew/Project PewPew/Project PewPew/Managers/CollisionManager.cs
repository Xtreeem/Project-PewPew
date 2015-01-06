using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    static class CollisionManager
    {
        static List<Enemy> Enemies = new List<Enemy>();                 //List of all enemies
        static List<GameObject> MainObjects = new List<GameObject>();   //List of all game objects
        public static void Update(ref GameTime GameTime)
        {
            GameObjectManager.Get_GameObjects(out MainObjects);         //Gets the latest list of gameobjects from the GameObjectManager to ensure its still up to date.   
            GameObjectManager.Get_Enemies(out Enemies);                 //Same for the list of enemies.

            ///TODO: Implement QuadTree so as to only check each object in the main list against objects in an box 
            ///around it using its own size, something like 
            ///foreach (Actor A in MainObjects)
            ///{
            ///     foreach(GameObject GO in GameObjectManager.Query(A, A.Size + 20)
            ///}



            for (int I = 0; I < MainObjects.Count; I++)                 //Loops all game objects
            {
                if (MainObjects[I] is Actor)                            //Checks if its an actor
                    for (int Y = I + 1; Y < MainObjects.Count; Y++)     //To ensure that no objects are tested against each other twice
                    {
                        if (MainObjects[Y] is Actor)                    //Checks to see if the second object is also an actor
                            if (CheckCollision(MainObjects[I] as Actor, MainObjects[Y] as Actor))               //Checks to see if the basic collision is true
                                ManageCollision(MainObjects[I] as Actor, MainObjects[Y] as Actor, ref GameTime);//Manages collisiion between the two objects
                    }
            }
        }

        /// <summary>
        /// Your worst Nightmare.
        /// Nested if-statements beyond belife. 
        /// Checks two objects for their type and send them into the proper collision functions
        /// </summary>
        /// <param name="A">The first object in the collision</param>
        /// <param name="B">The second object in the collision</param>
        /// <param name="GameTime"> a referance to GameTime</param>
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
                else if (B is PowerUp)
                {
                    HandleCollision(A as Player, B as PowerUp, ref GameTime);
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
                else if (B is PowerUp)
                {
                    HandleCollision(A as Enemy, B as PowerUp, ref GameTime);
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
                else if (B is PowerUp)
                {
                    HandleCollision(A as Turret, B as PowerUp, ref GameTime);
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
                else if (B is PowerUp)
                {
                    HandleCollision(A as Projectile, B as PowerUp, ref GameTime);
                }
            }
            else if (A is PowerUp)
            {
                if (B is Player)
                {
                    HandleCollision(A as PowerUp, B as Player, ref GameTime);
                }
                else if (B is Enemy)
                {
                    HandleCollision(A as PowerUp, B as Enemy, ref GameTime);
                }
                else if (B is Turret)
                {
                    HandleCollision(A as PowerUp, B as Turret, ref GameTime);
                }
                else if (B is Projectile)
                {
                    HandleCollision(A as PowerUp, B as Projectile, ref GameTime);
                }
                else if (B is PowerUp)
                {
                    HandleCollision(A as PowerUp, B as PowerUp, ref GameTime);
                }
            }
        }
        #region CollisionHandlers
        /// <summary>
        /// Handles collision between two players by returning both players to their last known position
        /// </summary>
        private static void HandleCollision(Player A, Player B, ref GameTime GameTime)
        {
            A.BumpBack();
            B.BumpBack();
        }
        /// <summary>
        /// Handles collision between a player and an enemy, by killing the enemy and inflicting damage to the player
        /// </summary>
        private static void HandleCollision(Player A, Enemy B, ref GameTime GameTime)
        {
            A.Damage(B.CollisionDamage);
            B.Die();
        }
        /// <summary>
        /// Handles collision between a player and a drone by returning them both to their last known position (Will be used for drones later) 
        /// </summary>
        private static void HandleCollision(Player A, Turret B, ref GameTime GameTime)
        {
            A.BumpBack();
            B.BumpBack();
        }
        /// <summary>
        /// Handles the collision between player and a projectile
        /// Checks if the projectile is fired by a friendly player and if whether or not its indended to cause friendly fire.
        /// </summary>
        private static void HandleCollision(Player A, Projectile B, ref GameTime GameTime)
        {
            if (!B.Creator.Friendly || B.Weapon.FriendlyFire)
            {
                A.Damage(B.Weapon.Damage);
                B.Die();
            }
        }
        /// <summary>
        /// Handles collision between a Player and a PowerUp
        /// Triggers the PowerUps PickedUp Functiona and passes in a referance to the Player
        /// </summary>
        private static void HandleCollision(Player A, PowerUp B, ref GameTime GameTime)
        {
            B.PickedUp(ref A);
        }
        /// <summary>
        /// Handles collision between a player and an enemy, by killing the enemy and inflicting damage to the player
        /// </summary>
        private static void HandleCollision(Enemy A, Player B, ref GameTime GameTime)
        {
            A.Die();
            B.Damage(A.CollisionDamage);
        }
        /// <summary>
        /// Not implemented, unsure if it has a purpose
        /// </summary>
        private static void HandleCollision(Enemy A, Enemy B, ref GameTime GameTime) { }
        /// <summary>
        /// Handles collision between an Enemy and a Turret
        /// Bumps both of them backwards to their last known position
        /// </summary>
        private static void HandleCollision(Enemy A, Turret B, ref GameTime GameTime) {
            A.BumpBack();
            B.BumpBack();
        }
        /// <summary>
        /// Handles the collision between an Enemy and a Projectile
        /// Damages the enemy based on the projectiles weapon then kills the projectile
        /// </summary>
        private static void HandleCollision(Enemy A, Projectile B, ref GameTime GameTime)
        {
            A.Damage(B.Weapon.Damage);
            B.Die();
        }
        /// <summary>
        /// Not yet implemented
        /// </summary>
        private static void HandleCollision(Enemy A, PowerUp B, ref GameTime GameTime)
        {
        }
        /// <summary>
        /// Handles collision between a player and a turret
        /// Bumps both the player and turret backwards to their last known position. 
        /// Will be used for drones
        /// </summary>
        private static void HandleCollision(Turret A, Player B, ref GameTime GameTime)
        {
            A.BumpBack();
            B.BumpBack();
        }
        /// <summary>
        /// Handles collision between an Enemy and a Turret.
        /// Bumps back both objects to their last known position
        /// </summary>
        private static void HandleCollision(Turret A, Enemy B, ref GameTime GameTime)
        {
            A.BumpBack();
            B.BumpBack();
        }
        /// <summary>
        /// Not Yet Implemented
        /// </summary>
        private static void HandleCollision(Turret A, Turret B, ref GameTime GameTime) { }
        /// <summary>
        /// Not Yet Implemented
        /// </summary>
        private static void HandleCollision(Turret A, Projectile B, ref GameTime GameTime) { }
        /// <summary>
        /// Not Yet Implemented
        /// </summary>
        private static void HandleCollision(Turret A, PowerUp B, ref GameTime GameTime) { }
        /// <summary>
        /// Not Yet Implemented
        /// </summary>
        private static void HandleCollision(Projectile A, Player B, ref GameTime GameTime) { }
        /// <summary>
        /// Handles Collision between a Projectile and an Enemy
        /// Damages the Enemy based on the Projectiles weapon damage
        /// </summary>
        private static void HandleCollision(Projectile A, Enemy B, ref GameTime GameTime)
        {
            A.Die();
            B.Damage(A.Weapon.Damage);
        }
        /// <summary>
        /// Not Yet Implemented
        /// </summary>
        private static void HandleCollision(Projectile A, Turret B, ref GameTime GameTime) { }
        /// <summary>
        /// Not Yet Implemented
        /// </summary>
        private static void HandleCollision(Projectile A, Projectile B, ref GameTime GameTime) { }
        /// <summary>
        /// Not Yet Implemented
        /// </summary>
        private static void HandleCollision(Projectile A, PowerUp B, ref GameTime GameTime) { }
        /// <summary>
        /// Handles collision between a Player and a PowerUp
        /// Triggers the PowerUps PickedUp Functiona and passes in a referance to the Player
        /// </summary>
        private static void HandleCollision(PowerUp A, Player B, ref GameTime GameTime) {
           A.PickedUp(ref B);
        }
        /// <summary>
        /// Not Yet Implemented
        /// </summary>
        private static void HandleCollision(PowerUp A, Enemy B, ref GameTime GameTime) { }
        /// <summary>
        /// Not Yet Implemented
        /// </summary>
        private static void HandleCollision(PowerUp A, Turret B, ref GameTime GameTime) { }
        /// <summary>
        /// Not Yet Implemented
        /// </summary>
        private static void HandleCollision(PowerUp A, Projectile B, ref GameTime GameTime) { }

        /// <summary>
        /// Not Yet Implemented
        /// </summary>
        private static void HandleCollision(PowerUp A, PowerUp B, ref GameTime GameTime) { }

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
