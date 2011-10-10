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
        public bool _isGrounded;
        public bool _grounded;

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
            
            // Jumping
            Vector2 jumpStart = new Vector2(_centerOff.X + 1, _centerOff.Y);
            Vector2 jumpEnd = new Vector2(_centerOff.X - 1, _centerOff.Y);

            Fixture jumpFixture = FixtureFactory.AttachEdge(jumpStart, jumpEnd, _body);
            _isGrounded = false;

            _scale = 10f;
            _rotAboutY = (float)MathHelper.PiOver2;

            _jumping = false;
        }

        bool GroundCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            _grounded = true;
            return true;
        }
    }
}
