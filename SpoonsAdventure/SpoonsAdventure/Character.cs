﻿using System;
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

            _grounded = true;
            _jumping = true;

            // Matrix
            _scale = 10f;
            _rotAboutY = (float)MathHelper.PiOver2;
        }

        bool GroundCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            FarseerPhysics.Collision.Manifold man = new FarseerPhysics.Collision.Manifold();
            //get collision manifold
            contact.GetManifold(out man);

            if (man.LocalNormal.X == 0 && man.LocalNormal.Y == 1)
                _grounded = true;

            return true;
        }
    }
}
