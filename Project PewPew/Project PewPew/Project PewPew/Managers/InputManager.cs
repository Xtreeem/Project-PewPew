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
        private static GamePadState GamePadThree, GamePadThreeOld;
        private static GamePadState GamePadFour, GamePadFourOld;

        public static Vector2 Get_Movement_Direction(int PlayerNum)
        {
            GamePadState CurrentPad = SetControlerState(PlayerNum);
            Vector2 Direction = CurrentPad.ThumbSticks.Left;
            Direction.Y *= -1;
            if (Direction.LengthSquared() > 1)
                Direction.Normalize();

            return Direction;
        }

        public static Vector2 Get_Aim_Direction(int PlayerNum)
        {
            GamePadState CurrentPad = SetControlerState(PlayerNum);
            Vector2 Direction = CurrentPad.ThumbSticks.Right;
            Direction.Y *= -1;
            if (Direction.LengthSquared() > 1)
                Direction.Normalize();
            return Direction;
        }

        public static bool Is_Button_Clicked(int PlayNum, Buttons Button)
        {
            GamePadState CurrentPadState = SetControlerState(PlayNum);
            GamePadState CurrentOldPadState = SetOldControlerState(PlayNum);
            if (CurrentPadState.IsButtonDown(Button) && CurrentOldPadState.IsButtonUp(Button))
                return true;
            else
                return false;
        }
        public static bool Is_Button_Pressed(int PlayNum, Buttons Button)
        {
            GamePadState CurrentPadState = SetControlerState(PlayNum);
            if (CurrentPadState.IsButtonDown(Button))
                return true;
            else
                return false;
        }

        public static void Update()
        {
            GamePadOneOld = GamePadOne;
            GamePadTwoOld = GamePadTwo;
            GamePadThreeOld = GamePadFour;
            GamePadFourOld = GamePadFour;

            GamePadOne = GamePad.GetState(PlayerIndex.One);
            GamePadTwo = GamePad.GetState(PlayerIndex.Two);
            GamePadThree = GamePad.GetState(PlayerIndex.Three);
            GamePadFour = GamePad.GetState(PlayerIndex.Four);
        
        }

        private static GamePadState SetControlerState(int PlayerNum)
        {
            if (PlayerNum == 1)
                return GamePadOne;
            else if (PlayerNum == 2)
                return GamePadTwo;
            else if (PlayerNum == 3)
                return GamePadThree;
            else if (PlayerNum == 4)
                return GamePadFour;
            else
                return GamePadOne;
        }

        private static GamePadState SetOldControlerState(int PlayerNum)
        {
            if (PlayerNum == 1)
                return GamePadOneOld;
            else if (PlayerNum == 2)
                return GamePadTwoOld;
            else if (PlayerNum == 3)
                return GamePadThreeOld;
            else if (PlayerNum == 4)
                return GamePadFourOld;
            else
                return GamePadOne;
        }
    }
}
