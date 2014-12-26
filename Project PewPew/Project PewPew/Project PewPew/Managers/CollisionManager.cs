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
        public static void Initialize()
        {
            GameObjectManager.Get_GameObjects(out MainObjects);
            GameObjectManager.Get_Enemies(out Enemies);
        }

        public static void Update()
        {
            //foreach (Enemy E in Enemies)
            //{
            //    test(E);
            //}
            //for (int I = 0; I < Enemies.Count; I++)
            //{
            //    for (int Y = I + 1; Y < Enemies.Count; Y++) //To ensure that no objects are tested against each other twice
            //    {
            //        HandleCollision(Enemies[I], Enemies[Y]);
            //    }
            //}
        }

        private static void HandleCollision(Enemy A, Enemy B)
        {
            //Console.WriteLine("I:" + I + " - Y: " + Y);
            float Spacing = Vector2.Distance(A.CenterPos, B.CenterPos);
            float CombinedSize = A.Size + B.Size;
            if (Spacing < CombinedSize)
            {
                bool LeftorRight = false;
                if (Misc.Determine_MovingVertical(A.Direction))
                {
                    if (A.CenterPos.X > B.CenterPos.X)
                        LeftorRight = true;
                }
                else
                {
                    if (A.CenterPos.Y > B.CenterPos.Y)
                        LeftorRight = true;
                }


                if (A.DistanceToTarget > B.DistanceToTarget)
                {
                    A.PushThisUnit((A.Direction * ((CombinedSize - Spacing) / 8)) * -1);

                    A.PushThisUnit(Misc.Perpendicular((A.Direction * ((CombinedSize - Spacing) / 1)), LeftorRight));
                }
                else
                {
                    B.PushThisUnit((B.Direction * ((CombinedSize - Spacing) / 8)) * -1);
                    B.PushThisUnit(Misc.Perpendicular((B.Direction * ((CombinedSize - Spacing) / 1)), LeftorRight));
                }

            }
        }

        //private static void test(Enemy E)
        //{

        //    foreach (Enemy En in Enemies)
        //    {
        //    Vector2 C = Vector2.Zero;
        //        if(E != En)
        //        {
        //            if((Vector2.Distance(E.CenterPos, En.CenterPos) < 30))
        //            {
        //                C = C - (E.CenterPos - En.CenterPos);
        //            }
        //        }
        //    E.PushThisUnit(C);
        //    }


        //}

    }
}
