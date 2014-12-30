using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    abstract class PowerUp : Actor
    {

        public virtual void PickedUp(ref Player Player)
        {
            //Player.Weapon.AttackRate += 0.4; etc
            Die();
        }


    }
}
