using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace SpoonsAdventure
{
    class Character : GameObject
    {
        private float _scale; // Object scaling factor

        public Character()
        {
            _scale = 1f;
        }
    }
}
