using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_PewPew
{
    abstract class PowerUp : Actor
    {
        protected float PowerUpStrength;
        public virtual void PickedUp(ref Player Player) { }


    }
}
