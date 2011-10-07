using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using xTile;
using xTile.Dimensions;
using xTile.Display;

namespace SpoonsAdventure
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameLoop : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        GameManager _gm;
        Renderer _rend;

        Map map;
        IDisplayDevice mapDisplayDevice;
        xTile.Dimensions.Rectangle viewport;

        public GameLoop()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // 3D Renderer
            _rend = new Renderer(_graphics);
            _gm   = new GameManager();
            map   = new Map();

            // This method calls LoadContents
            base.Initialize();

            // Map
            mapDisplayDevice = new XnaDisplayDevice(this.Content, this.GraphicsDevice);
            map.LoadTileSheets(mapDisplayDevice);
            viewport = new xTile.Dimensions.Rectangle(new Size(800, 600));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Map Texture
            map = Content.Load<Map>("Maps\\Map01"); // Test Map

            // 3D Renderer
            _rend.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Xbox Controls
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Windows Controls
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // Testing Model
            _rend.Controller(Keyboard.GetState());

            // Testing Map
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                viewport.X += 10;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                viewport.X -= 10;

            // Update Logic
            map.Update(gameTime.ElapsedGameTime.Milliseconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            map.Draw(mapDisplayDevice, viewport);

            _rend.Draw();

            _spriteBatch.Begin();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
