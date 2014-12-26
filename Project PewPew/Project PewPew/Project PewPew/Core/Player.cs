using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    public class Player : Actor
    {
        public int PlayerIndex { get; private set; }
        public Player(Vector2 StartPos, int PlayerIndex)
        {
            Texture = TextureManager.Player;
            this.PlayerIndex = PlayerIndex;
            Position = StartPos;
            MovementSpeed = 0.6f;
            Color = Color.BlueViolet;
            objecttype = ObjectType.Player;
        }

        public override void Update(GameTime GameTime)
        {
            if (InputManager.Is_Button_Clicked(PlayerIndex, Buttons.RightShoulder))
                if (objecttype == ObjectType.Player)
                {
                    objecttype = ObjectType.Wall;
                    Color = Color.HotPink;
                }
                else
                {
                    objecttype = ObjectType.Player;
                    Color = Color.BlueViolet;
                }

            Direction = InputManager.Get_Movement_Direction(PlayerIndex);
            base.Move(GameTime);
        }


    }
}
