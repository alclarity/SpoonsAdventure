using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using xTile;
using xTile.Dimensions;
using xTile.Display;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace SpoonsAdventure
{
    class GameManager
    {
        Map map;
        IDisplayDevice mapDisplayDevice;
        xTile.Dimensions.Rectangle viewport;

        public GameManager() { }

        public void Init()
        {
            map = new Map();

            // Map
            mapDisplayDevice = new XnaDisplayDevice(this.Content, this.GraphicsDevice);
            map.LoadTileSheets(mapDisplayDevice);
            viewport = new xTile.Dimensions.Rectangle(new Size(800, 600));
        }

        public void Load(ContentManager cm)
        {
            map = cm.Load<Map>("Maps\\Map01"); // Test Map
        }

        public void Update(GameTime gameTime)
        {
            // Update Logic
            map.Update(gameTime.ElapsedGameTime.Milliseconds);
        }
    }
}
