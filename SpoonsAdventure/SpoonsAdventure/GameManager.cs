using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

using xTile;
using xTile.Tiles;
using Microsoft.Xna.Framework.Graphics;
using xTile.Display;

namespace SpoonsAdventure
{
    class GameManager
    {
        Map _map;
        public int _mapSizeX, _mapSizeY;
        public MapTile[,] _tiles;

        public GameManager() { }

        public void Init()
        {
            _map = new Map();
        }

        public void Load(ContentManager cm, GraphicsDevice gd)
        {
            _map = cm.Load<Map>("Maps\\Level1"); // Test Map

            // Figure out map rows/cols
            TileSheet ts = _map.GetTileSheet("Foreground");
            int sheetX = ts.SheetWidth / ts.TileWidth;
            int sheetY = ts.SheetHeight / ts.TileHeight;
            
            
            int tileX = ts.TileSize.Width;
            int tileY = ts.TileSize.Height;

            // This definitely doesn't belong here. lol
            xTile.Display.IDisplayDevice idd = new XnaDisplayDevice(cm, gd);
          
            _mapSizeX = _map.DisplayWidth / tileX;
            _mapSizeY = _map.DisplayHeight / tileY;
            _tiles = new MapTile[_mapSizeX, _mapSizeY];
            
            TileArray tiles = _map.GetLayer("Front").Tiles;

            for (int row = 0; row < _mapSizeY; ++row)
            {
                for (int col = 0; col < _mapSizeX; ++col)
                {
                    Tile tile = tiles[col, row];
                    
                    if (tile.Properties.ContainsKey("collidable"))
                    {
                        // Get tile array pos
                        Vector2 pos = new Vector2(col, row);
                        // Convert to center-based world pos
                        pos.X = pos.X * tileX + (tileX / 2);
                        pos.X = pos.Y * tileY + (tileY / 2);
                        // Create MapTile
                        _tiles[col, row] = new MapTile(pos);
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
