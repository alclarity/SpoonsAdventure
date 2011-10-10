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
            _centerOff = size / 2f;

            size /= Defs.MtrInPix;
            pos /= Defs.MtrInPix;

            _body = BodyFactory.CreateRectangle(world, size.X, size.Y, 10f);
            _body.BodyType = BodyType.Dynamic;
            _body.Position = pos + _centerOff / Defs.MtrInPix;
            _body.FixedRotation = true;
            _body.OnCollision += GroundCollision;

            _scale = 10f;
            _rotAboutY = (float)MathHelper.PiOver2;
        }

        bool GroundCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            // fixtureA: The fixture of 'Body'
            // fixtureB: The fixture of the body that collided with 'Body'

            return true;
        }
    }
}
