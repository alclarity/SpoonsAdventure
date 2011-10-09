using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace SpoonsAdventure
{
    class GameObject
    {
        protected Body _body; // Contains center-based pos
        protected Vector2 _position; // Render origin-based pos
    }
}
