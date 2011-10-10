using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Factories;

namespace SpoonsAdventure
{
    class Character : GameObject
    {
        public Model _model;
        public float _scale; // Object scaling factor
        public float _rotAboutY;
        public Vector2 _centerOff;

        public Character(World world, Vector2 size, Vector2 pos)
        {
            size /= Defs.MtrInPix;
            pos /= Defs.MtrInPix;

            _centerOff = size / 2f * Defs.MtrInPix;

            _body = BodyFactory.CreateRectangle(world, size.X, size.Y, 10f);
            _body.BodyType = BodyType.Dynamic;
            _body.Position = pos / Defs.MtrInPix;

            _scale = 10f;
            _rotAboutY = (float)MathHelper.PiOver2;
        }
    }
}
