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
            ObjectType oldObjectType;
        public Player(Vector2 StartPos, int PlayerIndex)
        {
            Texture = TextureManager.Player;
            this.PlayerIndex = PlayerIndex;
            Position = StartPos;
            MovementSpeed = 0.6f;
            Color = Color.BlueViolet;
            objecttype = ObjectType.Player;
            Size = 20;
            Health = 10000;
            Armor = 10;
        }

        public override void Update(GameTime GameTime)
        {
            if (InputManager.Is_Button_Clicked(PlayerIndex, Buttons.RightShoulder))
                if (objecttype == ObjectType.Player)
                {
                    objecttype = ObjectType.Wall;
                    Color = Color.HotPink;
                }
                else if (objecttype == ObjectType.Generic)
                {
                    objecttype = ObjectType.Player;
                    Color = Color.BlueViolet;
                }
                else if (objecttype == ObjectType.Wall)
                {
                    objecttype = ObjectType.Generic;
                    Color = Color.Yellow;
                }

            if(InputManager.Is_Button_Pressed(PlayerIndex, Buttons.LeftTrigger) && InputManager.Is_Button_Clicked(PlayerIndex, Buttons.RightTrigger))
            {
                Vector2 TurretPos = Position + (InputManager.Get_Aim_Direction(PlayerIndex) * 120);

                Turret TempTurret = new Turret(this, TurretPos, TurretType.Canon);
                GameObjectManager.Add(TempTurret);
            }

            Direction = InputManager.Get_Movement_Direction(PlayerIndex);
            base.Move(GameTime);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch)
        {
            if(InputManager.Is_Button_Pressed(PlayerIndex, Buttons.LeftTrigger))
            {
                Vector2 TurretPos = Position + (InputManager.Get_Aim_Direction(PlayerIndex) * 120);
                SpriteBatch.Draw(TextureManager.TurretBase, TurretPos, null, Color.Lime *0.2f, 0f, new Vector2(TextureManager.TurretBase.Width / 2, TextureManager.TurretBase.Height / 2), 1f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
                SpriteBatch.Draw(TextureManager.TurretCanon, TurretPos, null, Color.Lime *0.2f, 0f, new Vector2(TextureManager.TurretCanon.Width / 2, TextureManager.TurretCanon.Height / 2), 1f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
            
            }
            base.Draw(SpriteBatch);
        }


    }
}
