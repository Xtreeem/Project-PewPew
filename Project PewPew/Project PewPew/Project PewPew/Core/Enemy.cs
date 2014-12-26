using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    public abstract class Enemy : Actor
    {
        public float Size { get; protected set; }
        public float AggroRange { get; protected set; }
        public float DeAggroRange { get; protected set; }
        public float DistanceToTarget { get { if (Target != null)return Vector2.Distance(Target.CenterPos, CenterPos); else return -1; } }
        protected Player Target { get; set; }
        protected Vector2 aiNewDir;
        protected int aiNumSeen;
        public Enemy()
        {
            fleeing = false;
            objecttype = ObjectType.Enemy;
        }

        public void Update(GameTime GameTime, ref AIParameters aiParams)
        {

            float elapsedTime = (float)GameTime.ElapsedGameTime.TotalSeconds;

            Move_To_Player();
            Vector2 randomDir = Vector2.Zero;

            if (aiNumSeen > 0)
            {
                aiNewDir = (Direction * aiParams.MoveInOldDirectionInfluence) +
                    (aiNewDir * (aiParams.MoveInFlockDirectionInfluence /
                    (float)aiNumSeen));
            }
            else
            {
                aiNewDir = Direction * aiParams.MoveInOldDirectionInfluence;
            }

            aiNewDir += (randomDir * aiParams.MoveInRandomDirectionInfluence);
            Vector2.Normalize(ref aiNewDir, out aiNewDir);
            aiNewDir = ChangeDirection(Direction, aiNewDir,
                aiParams.MaxTurnRadians * elapsedTime);
            Direction = aiNewDir;


            base.Update(GameTime);
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

        public void ReactTo(GameObject otherObject, ref AIParameters AIparams)
        {
            if (otherObject != null)
            {
                //setting the the reactionLocation and reactionDistance here is
                //an optimization, many of the possible reactions use the distance
                //and location of theAnimal, so we might as well figure them out
                //only once !
                Vector2 otherLocation = otherObject.Position;
                //ClosestLocation(ref location, ref otherLocation, 
                //    out reactionLocation);
                reactionDistance = Vector2.Distance(Position, reactionLocation);

                //we only react if theAnimal is close enough that we can see it
                if (reactionDistance < AIparams.DetectionDistance)
                {
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
}
