using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using xTile;
using xTile.Tiles;
using xTile.Display;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace SpoonsAdventure
{
    class GameManager
    {
        public Map _map;
        public Character _spoon;
        public World _world;
        List<MapTile> _tiles;
        public Body _edge;

        public GameManager() { }

        public void Init()
        {
            _world = new World(new Vector2(0, Defs.Gravity));
            _tiles = new List<MapTile>();

            Vector2 spoonSize = new Vector2(26, 64);
            Vector2 spoonPos = Vector2.Zero;
            _spoon = new Character(_world, spoonSize, spoonPos);

            // Edge to Block Start from falling off
            Vector2 edgeStart = new Vector2(0, 0);
            Vector2 edgeEnd   = new Vector2(0, Defs.ScreenHeight);

            _edge = BodyFactory.CreateEdge(_world, edgeStart, edgeEnd);
        }

        public void Load(ContentManager cm)
        {
            _map = cm.Load<Map>("Maps\\Level1");

            // Figure out map rows/cols
            TileSheet ts = _map.GetTileSheet("Front");
            Vector2 tileSize = new Vector2(ts.TileWidth, ts.TileHeight);

            int mapSizeX = (int) (_map.DisplayWidth / tileSize.X);
            int mapSizeY = (int) (_map.DisplayHeight / tileSize.Y);

            TileArray tiles = _map.GetLayer("StaticCollidable").Tiles;

            // Get all static objects
            for (int row = 0; row < mapSizeY; ++row)
            {
                for (int col = 0; col < mapSizeX; ++col)
                {
                    if (tiles[col, row] == null)
                        continue;

                    Tile tile = tiles[col, row];

                    // Calculate origin-based position
                    Vector2 oPos = new Vector2(col, row);
                    oPos.X = oPos.X * tileSize.X;
                    oPos.Y = oPos.Y * tileSize.Y;

                    // Create MapTile
                    MapTile mTile = new MapTile(_world, tileSize, oPos);
                    _tiles.Add(mTile);
                }
            }
        }

        public void Move(Vector2 dir, int rot)
        {
            dir.Y *= 10f;
            _spoon._body.ApplyLinearImpulse(dir * 1f);

            if(rot != 0)
            {
                _spoon._rotAboutY %= (float)MathHelper.TwoPi;

                if (rot == Defs.Left)
                {
                    if(_spoon._rotAboutY > -(float)MathHelper.PiOver2)
                        _spoon._rotAboutY -= 0.1f;
                }
                else if (rot == Defs.Right)
                {
                    if (_spoon._rotAboutY < (float)MathHelper.PiOver2)
                        _spoon._rotAboutY += 0.1f;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            _world.Step(gameTime.ElapsedGameTime.Milliseconds * 0.001f);
            
        }
    }
}
