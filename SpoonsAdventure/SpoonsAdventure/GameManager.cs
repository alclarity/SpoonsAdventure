﻿using System;
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

namespace SpoonsAdventure
{
    class GameManager
    {
        public Map _map;
        public Character _character;
        public World _world;
        List<MapTile> _tiles;

        public GameManager() { }

        public void Init()
        {
            _world = new World(Vector2.Zero); // 0 Gravity
            _tiles = new List<MapTile>();
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

        public void Update(GameTime gameTime)
        {
            _world.Step(gameTime.ElapsedGameTime.Milliseconds * 0.001f);
        }
    }
}
