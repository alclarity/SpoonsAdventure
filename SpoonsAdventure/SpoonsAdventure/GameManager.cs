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

namespace SpoonsAdventure
{
    class GameManager
    {
        //Map _map;
        public int _mapSizeX, _mapSizeY;
        public MapTile[,] _tiles;
        public Character _character;

        public GameManager() { }

        public void Init()
        {
            //_map = new Map();
        }

        public void Load(ContentManager cm, GraphicsDevice gd)
        {
            // CreateMapObjects(cm);
        }

        public void Update(GameTime gameTime)
        {
            
        }

        // Create Bodies for the Tiles
        private void CreateMapObjects(ContentManager cm)
        {
            Map _map = cm.Load<Map>("Maps\\Level1");

            // Figure out map rows/cols
            TileSheet ts = _map.GetTileSheet("Foreground"); // Returning Null
            int sheetX = ts.SheetWidth / ts.TileWidth;
            int sheetY = ts.SheetHeight / ts.TileHeight;

            int tileX = ts.TileSize.Width;
            int tileY = ts.TileSize.Height;

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
                        pos.Y = pos.Y * tileY + (tileY / 2);
                        // Create MapTile
                        _tiles[col, row] = new MapTile(pos);
                    }
                }
            }
        }
    }
}
