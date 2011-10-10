using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace SpoonsAdventure
{
    class MapTile : GameObject
    {
        public MapTile(World world, Vector2 size, Vector2 pos)
        {
            size /= Defs.MtrInPix;
            pos /= Defs.MtrInPix;

            _body = BodyFactory.CreateRectangle(world, size.X, size.Y, 1f);
            _body.IsStatic = true;
            _body.Position = pos;
        }
    }
}
