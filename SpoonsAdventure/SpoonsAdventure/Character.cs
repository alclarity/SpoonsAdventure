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
        public Vector2 _position; // Render origin-based pos

        public Character(World world, Vector2 size, Vector2 pos)
        {
            // Calculate center-based world pos
            Vector2 cPos = new Vector2();
            cPos.X = pos.X + (size.X / 2);
            cPos.Y = pos.Y + (size.Y / 2);

            _body = BodyFactory.CreateRectangle(world, size.X, size.Y, 10f);
            _body.BodyType = BodyType.Dynamic;
            _body.Position = cPos;
            _body.Awake = true;

            _scale = 5f;
            _rotAboutY = (float)MathHelper.PiOver2;
        }
    }
}
