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
using System.IO;
using Chopsticks.Resources;
using System.Diagnostics;

namespace Chopsticks
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        TextureAtlas library;
        private Texture2D blockTex;
        private Map map;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            library = new TextureAtlas(GraphicsDevice, "../../../../ChopsticksContent/");
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            RuntimeTests();

            this.map = new Map();
            map.Insert("cloud", new Transformation(0, 0, 1, 0.1f, 2, 1));
            map.Insert("cloud", new Transformation(100, 100, 0.5f));
            map.Insert("block", new Transformation(75, 95, 0.3f, -0.1f, 1.2f, 0.8f));
            map.Insert("block", new Transformation(150, 150));
            
            // TODO: use this.Content to load your game content here
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);
            foreach(var textureName in map.Scene.Keys) {
                var texture = library.GetTexture(textureName);
                foreach (var transformation in map.Scene[textureName])
                {
                    spriteBatch.Draw(texture, transformation.Translation, null, Color.White, transformation.Rotation, Vector2.Zero, transformation.Scale, SpriteEffects.None, transformation.SortDepth);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Assert things that aren't easily tested in unit tests because XNA... XNA, she is an untestable bitch.
        /// </summary>
        private void RuntimeTests()
        {
            //assert that two calls to get the same texture result in the same texture instance.
            var blockTex = library.GetTexture("block");
            var otherBlockTex = library.GetTexture("block");
            Debug.Assert(blockTex == otherBlockTex, "Got two different references to the same image from GetTexture");
        }
    }
}
