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

namespace SpoonsAdventure
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameLoop : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _gdm;
        SpriteBatch _sb;
        GameManager _gm;
        Renderer _rend;

        public GameLoop()
        {
            _gdm = new GraphicsDeviceManager(this);
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
            _rend = new Renderer(_gdm);
            _gm   = new GameManager();
            _gm.Init();

            // This method calls LoadContents
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _sb = new SpriteBatch(GraphicsDevice);

            // Map Texture
            _gm.Load(Content);

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
            Controller(Keyboard.GetState());

            _gm.Update(gameTime);

            base.Update(gameTime);
        }

        // Just playing with the model for now
        public void Controller(KeyboardState key)
        {
            if (key.IsKeyDown(Keys.W))
            {
                _rend._ModelPosition.Y += 5f;
            }
            else if (key.IsKeyDown(Keys.S))
            {
                _rend._ModelPosition.Y -= 5f;
            }

            if (key.IsKeyDown(Keys.A))
            {
                _rend._ModelPosition.X -= 0.1f;
            }
            else if (key.IsKeyDown(Keys.D))
            {
                _rend._ModelPosition.X += 0.1f;
            }

            if (key.IsKeyDown(Keys.Z))
            {
                _rend._ModelPosition.Z -= 5f;
            }
            else if (key.IsKeyDown(Keys.X))
            {
                _rend._ModelPosition.Z += 5f;
            }

            if (key.IsKeyDown(Keys.Up))
            {
                _rend._ModelRotationX += 0.1f;
            }
            if (key.IsKeyDown(Keys.Down))
            {
                _rend._ModelRotationX -= 0.1f;
            }
            if (key.IsKeyDown(Keys.Left))
            {
                _rend._ModelRotationY -= 0.1f;
            }
            if (key.IsKeyDown(Keys.Right))
            {
                _rend._ModelRotationY += 0.1f;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // This needs to get split up ...
            //map.Draw(mapDisplayDevice, viewport);

            _rend.Draw();

            _sb.Begin();

            _sb.End();

            base.Draw(gameTime);
        }
    }
}
