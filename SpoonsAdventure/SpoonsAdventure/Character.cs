using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;

namespace SpoonsAdventure
{
    class Character : GameObject
    {
        public Model _model;
        public float _scale; // Object scaling factor
        public float _rotAboutY;
        public Vector2 _centerOff;
        public bool _grounded;
        public bool _jumping;
        public Fixture _jumpFixture;

        public Character(World world, Vector2 size, Vector2 pos)
        {
            _centerOff = size / 2f;

            size /= Defs.MtrInPix;
            pos /= Defs.MtrInPix;

            _body = BodyFactory.CreateRectangle(world, size.X, size.Y, 10f);
            _body.BodyType = BodyType.Dynamic;
            _body.Position = pos + _centerOff / Defs.MtrInPix;
            _body.FixedRotation = true;
            
            // Jumping
            Vector2 jumpStart = new Vector2(_centerOff.X + 1, _centerOff.Y - 50);
            Vector2 jumpEnd = new Vector2(_centerOff.X - 1, _centerOff.Y - 50);
            _jumpFixture = _body.CreateFixture(new EdgeShape(jumpStart, jumpEnd));

            //_jumpFixture = FixtureFactory.AttachEdge(jumpStart, jumpEnd, _body);

            _jumpFixture.OnCollision += GroundCollision;

            _grounded = true;
            _jumping = true;

            // Matrix
            _scale = 10f;
            _rotAboutY = (float)MathHelper.PiOver2;
        }

        bool GroundCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            _grounded = true;
            return true;
        }
    }
}
