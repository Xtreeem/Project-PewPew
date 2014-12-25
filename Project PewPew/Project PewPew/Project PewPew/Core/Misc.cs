using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    static class Misc
    {
        static Random Random = new Random();
        public static Vector2 Perpendicular(Vector2 Original, bool InvertY)
        {
            //To create a perpendicular vector switch X and Y, then make Y negative
            float X = Original.X;
            float Y = Original.Y;
            if (InvertY)
                Y = -Y;
            else 
                X = -X;
            return new Vector2(Y, X);
        }

        public static bool Random_TrueFalse()
        {
            if (Random.Next(0, 100) > 49)
                return true;
            else return false;
        }

        public static bool Determine_MovingVertical(Vector2 Velocity)
        {
            if (Math.Abs(Velocity.Y) > Math.Abs(Velocity.X))
                return true;
            else
                return false;
        }


    }
}
