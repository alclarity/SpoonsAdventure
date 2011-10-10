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
            _rend = new Renderer();
            _gm   = new GameManager();
            _rend.Init();
            _gm.Init();

            _gdm.PreferredBackBufferHeight = Defs.screenHeight;
            _gdm.PreferredBackBufferWidth  = Defs.screenWidth;
            _gdm.ApplyChanges();

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

            _gm.Load(Content);
            _rend.LoadContent(Content, _gdm.GraphicsDevice, _gm._map, _gm._spoon);
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

            _rend.Update();

            base.Update(gameTime);
        }

        // Just playing with the model for now
        public void Controller(KeyboardState key)
        {
            Vector2 dir = Vector2.Zero;

            // Jump and Crouch
            if (key.IsKeyDown(Keys.Up))
            {
                dir += new Vector2(0, -1);
            }
            if (key.IsKeyDown(Keys.Down))
            {
                dir += new Vector2(0, 1);
            }  

            // Left and Right Movement
            if (key.IsKeyDown(Keys.Left))
            {
                dir += new Vector2(-1, 0);
            }
            else if (key.IsKeyDown(Keys.Right))
            {
                dir += new Vector2(1, 0);
            }

            if (dir != Vector2.Zero)
                _gm.Move(dir);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _sb.Begin();

            _rend.Draw(_sb);

            _sb.End();

            base.Draw(gameTime);
        }
    }
}
