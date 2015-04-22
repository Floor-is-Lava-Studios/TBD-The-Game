#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using LineBatch;
#endregion

namespace FloorIsLava
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Texture2D playerSprite, playerSprite_Forward, playerSprite_Backwards, backgroundSprite, titleSprite, wallSprite, goalSprite,
            creditButton1, creditButton2, levelButton1, levelButton2, startButton1, startButton2, options1, options2, instructions1, instructions2,
            levelTitle, optionsTitle;


        public int screenWidth;
        public int screenHeight;
        
        // Anna's Stuff
        public int picHeight;
        public int picWidth;

        private GameState gameState;

        public Game1()
            : base()
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
            // TODO: Add your initialization logic here
            SpriteBatchEx.GraphicsDevice = GraphicsDevice;
            // Makes the game fullscreen
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            screenWidth = GraphicsDevice.DisplayMode.Width;
            screenHeight = GraphicsDevice.DisplayMode.Height;

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
            gameState = new GameState(this);
            gameState.StartScreen = new StartScreen(this);

            playerSprite = Content.Load<Texture2D>("player_forward");
            playerSprite_Forward = Content.Load<Texture2D>("player_forward");
            playerSprite_Backwards = Content.Load<Texture2D>("player_backwards");
            wallSprite = Content.Load<Texture2D>("stone box");
            goalSprite = Content.Load<Texture2D>("goal");
            backgroundSprite = Content.Load<Texture2D>("background");
            titleSprite = Content.Load<Texture2D>("title");
            creditButton1 = Content.Load<Texture2D>("credit1");
            creditButton2 = Content.Load<Texture2D>("credit2");
            levelButton1 = Content.Load<Texture2D>("levelbutton1");
            levelButton2 = Content.Load<Texture2D>("levelbutton2");
            startButton1 = Content.Load<Texture2D>("startbutton1");
            startButton2 = Content.Load<Texture2D>("startbutton2");
            options1 = Content.Load<Texture2D>("options1");
            options2 = Content.Load<Texture2D>("options2");
            instructions1 = Content.Load<Texture2D>("instruction1");
            instructions2 = Content.Load<Texture2D>("instruction2");
            optionsTitle = Content.Load<Texture2D>("OptionsTitle");
            levelTitle = Content.Load<Texture2D>("levelSelectTitle");

            // Anna Stuff
            picHeight = 0;
            picWidth = 0;
            


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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            switch (gameState.CurrentScreen)
            {
                case Screen.StartScreen:
                    if (gameState.StartScreen != null)
                    {
                        gameState.StartScreen.Update();
                    }
                    break;

                case Screen.GameScreen:
                    if (gameState.GameScreen != null)
                    {
                        gameState.GameScreen.Update(gameTime);
                    }
                    break;

                case Screen.InstructionScreen:
                    if (gameState.InstructionScreen != null)
                    {
                        gameState.InstructionScreen.Update(gameTime);
                    }
                    break;

                case Screen.OptionScreen:
                    if (gameState.OptionScreen != null)
                    {
                        gameState.OptionScreen.Update(gameTime);
                    }
                    break;

                case Screen.CreditScreen:
                    if (gameState.CreditScreen != null)
                    {
                        gameState.CreditScreen.Update(gameTime);
                    }
                    break;

                case Screen.LevelScreen:
                    if (gameState.LevelScreen != null)
                    {
                        gameState.LevelScreen.Update(gameTime);
                    }
                    break;

                case Screen.PauseScreen:
                    if (gameState.PauseScreen != null)
                    {
                        gameState.PauseScreen.Update(gameTime);
                    }
                    break;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            switch(gameState.CurrentScreen)
            {
                case Screen.StartScreen:
                    if (gameState.StartScreen != null)
                    {
                        GraphicsDevice.Clear(Color.Green);
                        gameState.StartScreen.Draw(spriteBatch, backgroundSprite, titleSprite, creditButton1, creditButton2,levelButton1,levelButton2, startButton1,startButton2, options1,options2, instructions1, instructions2);
                    }
                    break;

                case Screen.GameScreen:
                    if (gameState.GameScreen != null)
                    {
                        GraphicsDevice.Clear(Color.SteelBlue);
                        gameState.GameScreen.Draw(spriteBatch, backgroundSprite);
                    }
                    break;

                case Screen.InstructionScreen:
                    if(gameState.InstructionScreen != null)
                    {
                        GraphicsDevice.Clear(Color.Red);
                        gameState.InstructionScreen.Draw(spriteBatch, backgroundSprite);
                    }
                    break;

                case Screen.OptionScreen:
                    if(gameState.OptionScreen != null)
                    {
                        GraphicsDevice.Clear(Color.Gold);
                        gameState.OptionScreen.Draw(spriteBatch, backgroundSprite, optionsTitle);
                    }
                    break;

                case Screen.CreditScreen:
                    if (gameState.CreditScreen != null)
                    {
                        GraphicsDevice.Clear(Color.Black);
                        gameState.CreditScreen.Draw(spriteBatch, backgroundSprite);
                    }
                    break;

                case Screen.LevelScreen:
                    if (gameState.LevelScreen != null)
                    {
                        GraphicsDevice.Clear(Color.Gray);
                        gameState.LevelScreen.Draw(spriteBatch, backgroundSprite, levelTitle);
                    }
                    break;

                case Screen.PauseScreen:
                    if (gameState.PauseScreen != null)
                    {
                        GraphicsDevice.Clear(Color.Blue);
                        gameState.PauseScreen.Draw(spriteBatch, backgroundSprite);
                    }
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
