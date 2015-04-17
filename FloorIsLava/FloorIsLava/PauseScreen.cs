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

        private int button; //what the user wants to do next
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

            button = 0;
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
            if (keyState.IsKeyDown(Keys.O) && lastState.IsKeyDown(Keys.O))
            {
                gameState.SwitchOption(game);
            }

            // using either the "W" or "S" key or the up and down arrow picking what to do next
            if ((keyState.IsKeyDown(Keys.W) && lastState.IsKeyUp(Keys.W)) || (keyState.IsKeyDown(Keys.Up) && lastState.IsKeyUp(Keys.Up)))
            {
                button--;
                if(button < 0)
                {
                    button = 2;
                }
            }
            if ((keyState.IsKeyDown(Keys.S) && lastState.IsKeyUp(Keys.S)) || (keyState.IsKeyDown(Keys.Down) && lastState.IsKeyUp(Keys.Down)))
            {
                button++;
                if (button > 2)
                {
                    button = 0;
                }
            }
            if ((keyState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter)) || (keyState.IsKeyDown(Keys.Space) && lastState.IsKeyUp(Keys.Space)))
            {
                switch(button)
                {
                    case 0:
                        gameState.ResumeGame(gameScreen);
                        break;
                    case 1:
                        gameState.SwitchOption(game);
                        break;
                    case 2:
                        gameState.SwitchInstruct(game);
                        break;
                }
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
            spriteBatch.DrawString(font1,"PAUSE", new Vector2(game.screenWidth / 2, 100),Color.Blue);

            // drawing the buttons, if selected the color changes
            if(button == 0)
            {
                spriteBatch.DrawString(font1, "Continue", new Vector2(game.screenWidth / 2, 200), Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(font1, "Contine", new Vector2(game.screenWidth / 2, 200), Color.Blue);
            }
            if (button == 1)
            {
                spriteBatch.DrawString(font1, "Options", new Vector2(game.screenWidth / 2, 300), Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(font1, "Options", new Vector2(game.screenWidth / 2, 300), Color.Blue);
            }
            if (button == 2)
            {
                spriteBatch.DrawString(font1, "How To Play", new Vector2(game.screenWidth / 2, 400), Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(font1, "How To Play", new Vector2(game.screenWidth / 2, 400), Color.Blue);
            }
        }
        #endregion Draw 
    }
}
