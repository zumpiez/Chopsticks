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

namespace Chopsticks
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D blockTexture;
        List<string> blockTextureReloadQueue = new List<string>();
        FileSystemWatcher blockTextureChangeDetector = new FileSystemWatcher(@"C:\Users\Jeff\Dropbox\Projects\Chopsticks\Chopsticks\ChopsticksContent\", "block.png");

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
            blockTexture = Content.Load<Texture2D>("block");

            blockTextureChangeDetector.Changed += (source, eventArgs) =>
            {
                blockTextureReloadQueue.Add(eventArgs.FullPath);
            };

            blockTextureChangeDetector.EnableRaisingEvents = true;

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

            foreach (string s in blockTextureReloadQueue.Distinct())
            {
                using (FileStream fs = new FileStream(@"C:\Users\Jeff\Dropbox\Projects\Chopsticks\Chopsticks\ChopsticksContent\block.png", FileMode.Open))
                {
                    try
                    {
                        blockTexture = Texture2D.FromStream(graphics.GraphicsDevice, fs);
                    }
                    catch
                    {
                        //todo alert the user that their file is jacked up somehow
                        blockTexture = Content.Load<Texture2D>("block");
                    }
                }
            }

            blockTextureReloadQueue.Clear();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(blockTexture, new Vector2(200, 200), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
