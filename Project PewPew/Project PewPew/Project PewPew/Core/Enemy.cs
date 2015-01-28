using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Project_PewPew
{
    public abstract class Enemy : Actor
    {
        public float AggroRange { get; protected set; }
        public float DeAggroRange { get; protected set; }
        public float DistanceToTarget { get { if (Target != null)return Vector2.Distance(Target.CenterPos, CenterPos); else return -1; } }
        public float CollisionDamage { get; protected set; }
        protected Player Target { get; set; }
        protected Vector2 aiNewDir;
        protected int aiNumSeen;
        private Random random;
        public Enemy(ref Random random)
        {
            fleeing = false;
            objecttype = ObjectType.Enemy;
            this.random = random;
            CollisionDamage = 100;
            Friendly = false;
        }

        public void Update(GameTime GameTime, ref AIParameters aiParams)
        {
            if (Health <= 0)
                Dying = true;
            if (!Dying)
            {

                float elapsedTime = (float)GameTime.ElapsedGameTime.TotalSeconds;


                if (aiNumSeen > 0)
                {
                    aiNewDir = (Direction * aiParams.MoveInOldDirectionInfluence) + (aiNewDir * (aiParams.MoveInFlockDirectionInfluence / (float)aiNumSeen));
                }
                else
                {
                    aiNewDir = Direction * aiParams.MoveInOldDirectionInfluence;
                }



                Vector2.Normalize(ref aiNewDir, out aiNewDir);
                aiNewDir = ChangeDirection(Direction, aiNewDir, aiParams.MaxTurnRadians * elapsedTime);

                Direction = aiNewDir;

                if (Direction.LengthSquared() > .01f)
                    base.Update(GameTime);
            }
        }

        protected void Move_To_Player()
        {
            Direction = Target.CenterPos - CenterPos;
            if (Direction != Vector2.Zero)
                Direction = Vector2.Normalize(Direction);

        }

        public void DeAggro()
        {
            Target = null;
            Direction = Vector2.Zero;
        }

        public void Aggro(Player Player)
        {
            Target = Player;
        }

        

        public bool HasTarget()
        {
            if (Target != null)
                return true;
            else
                return false;
        }
        public void ResetThink()
        {
            Fleeing = false;
            aiNewDir = Vector2.Zero;
            aiNumSeen = 0;
            reactionDistance = 0f;
            reactionLocation = Vector2.Zero;
        }



        public void ReactTo(GameObject otherObject, ref AIParameters AIparams)
        {
            if (otherObject != null)
            {
                Vector2 otherLocation = otherObject.Position;
                reactionLocation = otherLocation;
                reactionDistance = Vector2.Distance(Position, otherLocation);

                    if (otherObject.ObjectType != ObjectType.Generic)
                    {
                        Behaviors reactions = behaviors[otherObject.ObjectType];
                        foreach (Behavior reaction in reactions)
                        {
                            reaction.Update(otherObject, AIparams);
                            if (reaction.Reacted)
                            {
                                aiNewDir += reaction.Reaction;
                                aiNumSeen++;
                            }
                        }
                    }
            }

        }
    }
}
