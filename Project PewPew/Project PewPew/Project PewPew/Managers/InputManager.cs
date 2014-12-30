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
        private static bool PlayerOneActive = false, PlayerTwoActive = false, PlayerThreeActive = false, PlayerFourActive = false;

        public static void Set_PlayerActive(int PlayerIndex, bool Input)
        {
            switch (PlayerIndex)
            {
                case(1):
                    PlayerOneActive = Input;
                    break;
                case(2):
                    PlayerTwoActive = Input;
                    break;
                case(3):
                    PlayerThreeActive = Input;
                    break;
                case(4):
                    PlayerFourActive = Input;
                    break;
                default:
                    throw(new Exception("Unimplemented PlayerIndex"));
            }
        }

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

        public static bool Is_Button_Clicked(int PlayerNum, Buttons Button)
        {
            GamePadState CurrentPad = SetControlerState(PlayerNum);
            GamePadState CurrentOldPad = SetOldControlerState(PlayerNum);
            if (CurrentPad.IsButtonDown(Button) && CurrentOldPad.IsButtonUp(Button))
                return true;
            else
                return false;
        }

        public static void Update()
        {
            GamePadOneOld = GamePadOne;
            GamePadTwoOld = GamePadTwo;
            GamePadThreeOld = GamePadThree;
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
            else
                return GamePadFour;
        }

        private static GamePadState SetOldControlerState(int PlayerNum)
        {
            if (PlayerNum == 1)
                return GamePadOneOld;
            else if (PlayerNum == 2)
                return GamePadTwoOld;
            else if (PlayerNum == 3)
                return GamePadThreeOld;
            else
                return GamePadFourOld;
        }

        private static void AddNewPlayers()
        {

        }
    }
}
