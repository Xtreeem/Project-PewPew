using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    static class GameObjectManager
    {
        static List<Player> Players = new List<Player>();
        static List<GameObject> MainObjects = new List<GameObject>();       //List that will be used to track all GameObjects
        static List<Projectile> Projectiles = new List<Projectile>();   //List that will be used to track all the Projectiles
        static List<GameObject> NewObjects = new List<GameObject>();    //List used to temporarely store all new Objects added to the Manager during an update cycle 

        static bool Updating;   //Tracks if the manager is currently inside an updating cycle

        public static void Get_GameObjects(out List<GameObject> MainObjList)
        {
            MainObjList = MainObjects;
        }

        /// <summary>
        /// Function called to add an item to the Manager
        /// </summary>
        /// <param name="Obj">The object to be added</param>
        public static void Add(GameObject Obj)
        {
            if (Updating)               //Checks if the manager is currently in an updating cycle
                NewObjects.Add(Obj);    //If so adds the object to the list of new objects to be added after the update cycle
            else
            {
                MainObjects.Add(Obj);       //If not it adds the object to the Standard list
                if (Obj is Player)
                    Players.Add(Obj as Player);
            }
        }

        /// <summary>
        /// Checks if the item that was added during update was a projectile or not
        /// and might add it to the projectile list
        /// </summary>
        /// <param name="Obj">The object being tested</param>
        private static void AddNewObjects(GameObject Obj)
        {
            MainObjects.Add(Obj);                        //Adds the item to the main Object list
            if (Obj is Projectile)                   //Checks to see if the Object is a projectile
                Projectiles.Add(Obj as Projectile);  //Adds it to the projectile list if needed
            else if (Obj is Player)
                Players.Add(Obj as Player);
        }

        /// <summary>
        /// Managers Update-function
        /// -Will call each gameobjects update function
        /// -Add awaiting items to the main object list
        /// -Clear out dying objects
        /// </summary>
        /// <param name="GameTime">GameTime that will be used by the Objects in their update</param>
        public static void Update(GameTime GameTime)
        {
            Updating = true;                                            //Marks the start of an update cycle
            foreach (GameObject GO in MainObjects)                      //Loops all the current objects inside the mainlist
            {
                if (GO is Enemy)
                    EnemyUpdate(GO as Enemy);
                GO.Update(GameTime);                                    //Calls the currently indexed objects update function
            }
            Updating = false;                                           //Marks the end of the update cycle

            foreach (GameObject NGO in NewObjects)                      //Loops all the items awaiting to be added to the mainObject list
            {
                AddNewObjects(NGO);                                     //Calls the internal function to add the objects to either their apropriate lists
            }
            NewObjects.Clear();                                         //Clears the list of items that are waiting to be added

            MainObjects = MainObjects.Where(x => !x.Dying).ToList();    //Cleans out all of the dying objects from the Main Object list
            Projectiles = Projectiles.Where(x => !x.Dying).ToList();    //Cleans out all of the dying projectiles from the projeectile list

        }

        /// <summary>
        /// Managers Draw-Function
        /// Will call each gameobjects Draw function
        /// </summary>
        /// <param name="SpriteBatch">The spritebatch used to draw all of our objects</param>
        public static void Draw(SpriteBatch SpriteBatch)
        {
            foreach (GameObject GO in MainObjects)      //Loops all of the main gameobjects
            {
                GO.Draw(SpriteBatch);                   //Draws the indexed object
            }
        }

        private static void EnemyUpdate(Enemy Enemy)
        {
            if (Enemy.HasTarget())
            {

            }
            else
            {
                foreach (Player P in Players)
                {
                    if (Vector2.Distance(P.CenterPos, Enemy.CenterPos) < Enemy.AggroRange)
                        Enemy.Aggro(P);
                }
            }
        }
    }
}
