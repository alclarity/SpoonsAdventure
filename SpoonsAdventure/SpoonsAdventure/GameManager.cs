using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

using FarseerPhysics.Dynamics;

using xTile;

namespace SpoonsAdventure
{
    class GameManager
    {
        Map _Map;

        public GameManager() { }

        public void Init()
        {
            _Map = new Map();
        }

        public void Load(ContentManager cm)
        {
            _Map = cm.Load<Map>("Maps\\Level1"); // Test Map
        }

        public void Update(GameTime gameTime)
        {
            // Update Logic
        }
    }
}
