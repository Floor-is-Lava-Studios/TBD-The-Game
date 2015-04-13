using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace FloorIsLava
{
    class PauseScreen
    {
        #region Attributes
        //Attributes
        private SpriteFont font1; //hold spritefont font1
        private Game1 game; // holds game 1 object
        private KeyboardState lastState; // holds last KeyboardState
        private GameState gameState; //holds gameState object 
        private GameScreen gameScreen; //holds current Gamescreen
        private int screenWidth; //Width for Pause screen
        private int screenHeight; //Height for PauseScreen
        #endregion Attributes

        #region Constructor
        //Constructor
        public PauseScreen(Game1 game1, GameScreen currentGame)
        {
            game = game1;// assigns game1 to game
            gameState = new GameState(game); // creates new gamestate object and assigns it to gameState
            font1 = game.Content.Load<SpriteFont>("Font1"); // loads Font1
            lastState = Keyboard.GetState();// sets keyboard state
            gameScreen = currentGame; // sets gameScreen to current Gamescreen
            screenWidth = 1000; //sets width to 1000
            screenHeight = 800; //sets height to 800
        }
        #endregion Constructor

        #region Update
        //Update
        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.R) && lastState.IsKeyDown(Keys.R))
            {
                gameState.ResumeGame(gameScreen);
            }
            lastState = keyState;
           
        }
        #endregion Update

        #region Draw
        //Draw
        public void Draw(SpriteBatch spriteBatch, Texture2D background)
        {
            gameScreen.Draw(spriteBatch, background);
            spriteBatch.Draw(background, new Rectangle((game.screenWidth/2) - 500, (game.screenHeight/2) - 400 , screenWidth, screenHeight), Color.White);
            //spriteBatch.Draw(title, new Rectangle(game.screenWidth / 2 - 500, 100, 1000, 512), Color.White);
        }
        #endregion Draw 
    }
}
