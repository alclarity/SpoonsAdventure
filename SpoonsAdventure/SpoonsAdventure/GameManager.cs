using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

using xTile;
using xTile.Tiles;

namespace SpoonsAdventure
{
    class GameManager
    {
        Map _map;
        public Vector2 _mapSize;
        public MapTile[] _tiles;

        public GameManager() { }

        public void Init()
        {
            _map = new Map();
        }

        public void Load(ContentManager cm)
        {
            _map = cm.Load<Map>("Maps\\Level1"); // Test Map

            // Figure out map rows/cols
            TileSheet ts = _map.GetTileSheet("Foreground");

            Vector2 tileSize = new Vector2(ts.TileSize.Width, ts.TileSize.Height);
            
            _mapSize = new Vector2();
            _mapSize.X = _map.DisplayWidth / tileSize.X;
            _mapSize.Y = _map.DisplayHeight / tileSize.Y;
            
            TileArray tiles = _map.GetLayer("Front").Tiles;

            for (int row = 0; row < _mapSize.Y; ++row)
            {
                for (int col = 0; col < _mapSize.X; ++col)
                {
                    if (tiles[col, row].Properties.ContainsKey("collidable"))
                    {
                        Vector2 pos = new Vector2(col, row);
                        pos *= tileSize;

                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            // Update Logic
        }
    }
}
