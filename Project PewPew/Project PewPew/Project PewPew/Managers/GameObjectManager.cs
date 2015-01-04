using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    #region AIParameters
    public struct AIParameters
    {
        /// <summary>
        /// how far away the animals see each other
        /// </summary>
        public float DetectionDistance;
        /// <summary>
        /// seperate from animals inside this distance
        /// </summary>
        public float SeparationDistance;
        /// <summary>
        /// how much the animal tends to move in it's previous direction
        /// </summary>
        public float MoveInOldDirectionInfluence;
        /// <summary>
        /// how much the animal tends to move with animals in it's detection distance
        /// </summary>
        public float MoveInFlockDirectionInfluence;
        /// <summary>
        /// how much the animal tends to move randomly
        /// </summary>
        public float MoveInRandomDirectionInfluence;
        /// <summary>
        /// how quickly the animal can turn
        /// </summary>
        public float MaxTurnRadians;
        /// <summary>
        /// how much each nearby animal influences it's behavior
        /// </summary>
        public float PerMemberWeight;
        /// <summary>
        /// how much dangerous animals influence it's behavior
        /// </summary>
        public float PerDangerWeight;
        /// <summary>
        /// how much the player influence it's behavior
        /// </summary>
        public float PerPlayerWeight;
        /// <summary>
        /// how far the Walls influence it's behavior
        /// </summary>
        public float WallRadius;



    }
    #endregion

    static class GameObjectManager
    {
        // Default value for the AI parameters
        const float detectionDefault = 1000.0f;
        const float separationDefault = 120.0f;
        const float moveInOldDirInfluenceDefault = 1.0f;
        const float moveInFlockDirInfluenceDefault = 1.0f;
        const float moveInRandomDirInfluenceDefault = 0.05f;
        const float maxTurnRadiansDefault = (float)Math.PI * 2;
        const float perMemberWeightDefault = 1.0f;
        const float perDangerWeightDefault = 50.0f;
        const float perPlayerWeightDefault = 20.0f;
        const float WallRadiusDefault = 80f;

        //Used to pass all AI parameters
        static AIParameters aiParameters = new AIParameters();

        static List<Player> Players = new List<Player>();
        static List<Enemy> Enemies = new List<Enemy>();
        static List<GameObject> MainObjects = new List<GameObject>();       //List that will be used to track all GameObjects
        static List<Projectile> Projectiles = new List<Projectile>();   //List that will be used to track all the Projectiles
        static List<GameObject> NewObjects = new List<GameObject>();    //List used to temporarely store all new Objects added to the Manager during an update cycle 

        static bool Updating;   //Tracks if the manager is currently inside an updating cycle

        public static void Get_GameObjects(out List<GameObject> MainObjList)
        {
            MainObjList = MainObjects;
        }
        public static void Get_Players(out List<Player> PlayerList)
        {
            PlayerList = Players;
        }
        public static void Get_Enemies(out List<Enemy> EnemyList)
        {
            EnemyList = Enemies;
        }
        /// <summary>
        /// Returns the list of projectiles as it is atm
        /// </summary>
        /// <param name="ProjectileList">List of all active projectiles</param>
        public static void Get_Projectiles(out List<Projectile> ProjectileList)
        {
            ProjectileList = Projectiles;
        }

        /// <summary>
        /// Sets all the AI parameters to match the default values
        /// </summary>
        public static void IniatlizeAIParamaters()
        {
            aiParameters.DetectionDistance = detectionDefault;
            aiParameters.SeparationDistance = separationDefault;
            aiParameters.MoveInOldDirectionInfluence = moveInOldDirInfluenceDefault;
            aiParameters.MoveInFlockDirectionInfluence = moveInFlockDirInfluenceDefault;
            aiParameters.MoveInRandomDirectionInfluence = moveInRandomDirInfluenceDefault;
            aiParameters.MaxTurnRadians = maxTurnRadiansDefault;
            aiParameters.PerMemberWeight = perMemberWeightDefault;
            aiParameters.PerDangerWeight = perDangerWeightDefault;
            aiParameters.PerPlayerWeight = perPlayerWeightDefault;
            aiParameters.WallRadius = WallRadiusDefault;
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
                else if (Obj is Enemy)
                    Enemies.Add(Obj as Enemy);
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
            else if (Obj is Enemy)
                Enemies.Add(Obj as Enemy);
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
                    EnemyUpdate(GameTime, GO as Enemy);
                else if (GO is Turret)
                    TurretUpdate(GameTime, GO as Turret);
                else
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
            Enemies = Enemies.Where(x => !x.Dying).ToList();    //Cleans out all of the dying projectiles from the projeectile list

            PressStartToJoinCheck();

        }

        /// <summary>
        /// Function used to detect if any In-active player has pressed their start button
        /// if so adds the player to the game.
        /// </summary>
        private static void PressStartToJoinCheck()
        {
            for (int I = 0; I < 4; I++)                                                             //Loops the max number of supported players
            {
                if (InputManager.Is_Button_Clicked(I, Microsoft.Xna.Framework.Input.Buttons.Start)) //Checks if the player has pressed their start-button
                {
                    bool Found = false;                                                             //Sets a temp bool to false, indicating that we havent found that palyer ID yet
                    foreach (Player P in Players)                                                   //Checks each active players ID
                    {
                        if (P.PlayerIndex == I)                                                     //IF the indexed players ID matches the one who pressed start
                            Found = true;                                                           //Flip the tempbool to indicate that we have found the an identical player
                    }
                    if (!Found)                                                                     //IF we didnt find a player with the same ID 
                        GameObjectManager.Add(new Player(new Vector2(I * 100, I * 100), I));        //Adds a new player to the game with the new ID
                }
            }
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

        private static void TurretUpdate(GameTime GameTime, Turret Turret)
        {
            if(Turret.Target == null)
            {
                foreach (Enemy E in Enemies)
                {
                    if (InputManager.Is_Button_Clicked(1, Microsoft.Xna.Framework.Input.Buttons.DPadRight))
                        Console.WriteLine("break");

                    if(Vector2.Distance(E.CenterPos, Turret.CenterPos) < Turret.DistanceToTarget)
                    {
                        Turret.Set_Target(E);
                    }
                }
            }
            Turret.Update(GameTime);
        }

        private static void EnemyUpdate(GameTime GameTime, Enemy Enemy)
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
            Enemy.ResetThink();
            foreach (GameObject OtherObject in MainObjects)
            {
                if (Enemy != OtherObject)
                    Enemy.ReactTo(OtherObject, ref aiParameters);
            }
            Enemy.Update(GameTime, ref aiParameters);
        }

        public static void KillEverything()
        {
            Enemies = new List<Enemy>();
            MainObjects = new List<GameObject>();
            Projectiles = new List<Projectile>();
            Players = new List<Player>();
            NewObjects = new List<GameObject>();
        }

    }
}
