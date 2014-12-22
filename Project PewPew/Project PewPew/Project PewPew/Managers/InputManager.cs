using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    static class InputManager
    {
        private static GamePadState GamePadOne, GamePadOneOld;
        private static GamePadState GamePadTwo, GamePadTwoOld;
        public static Vector2 Get_Movement_Direction(int PlayerNum)
        {
            GamePadState CurrentPad = SetControlerState(PlayerNum);
            Vector2 Direction = CurrentPad.ThumbSticks.Left;
            Direction.Y *= -1;
            return Direction;
        }

        public static void Update()
        {
            GamePadOneOld = GamePadOne;
            GamePadTwoOld = GamePadTwo;

            GamePadOne = GamePad.GetState(PlayerIndex.One);
            GamePadTwo = GamePad.GetState(PlayerIndex.Two);
        }

        private static GamePadState SetControlerState(int PlayerNum)
        {
            if (PlayerNum == 1)
                return GamePadOne;
            else
                return GamePadTwo;
        }

        private static GamePadState SetOldControlerState(int PlayerNum)
        {
            if (PlayerNum == 1)
                return GamePadOneOld;
            else
                return GamePadTwoOld;
        }
    }
}
