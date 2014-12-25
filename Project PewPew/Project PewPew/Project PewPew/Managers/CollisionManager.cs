using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    static class CollisionManager
    {
        static List<GameObject> MainObjects = new List<GameObject>();
        public static void Initialize()
        {
            GameObjectManager.Get_GameObjects(out MainObjects);
        }

        public static void Test()
        {
            foreach (GameObject GO in MainObjects)
            {
                Console.WriteLine("test");
            }
        }


    }
}
