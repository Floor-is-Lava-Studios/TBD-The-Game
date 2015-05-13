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
    class EndLevelScreen
    {
        #region Attributes
        //attributes
        private SpriteFont font1;
        private Game1 game;
        private KeyboardState lastState; //holds previous keyboard state
        private GameState gameState; //hold the gameState class
        private int count;
        private string previousLevel;
        private string levelName;
        private string currentLevel;

        // all the images
        private Texture2D continue1;
        private Texture2D continue2;
        private Texture2D tryAgain1;
        private Texture2D tryAgain2;
        private Texture2D quit1;
        private Texture2D quit2;

        #endregion Attributes

        #region Constructors
        public EndLevelScreen(Game1 game1, string previousLevelIn, string currentLevelIn, string currentLevelName)
        {
            game = game1; //assigns the game1 object
            previousLevel = previousLevelIn;
            gameState = new GameState(game); //creates a new gamestate class object and assigns it to gamestate
            font1 = game.Content.Load<SpriteFont>("Font1"); // loads Font1 spriteFont
            count = 0;
            levelName = currentLevelName;
            currentLevel = currentLevelIn;

            // loading images
            continue1 = game.Content.Load<Texture2D>("continue1");
            continue2 = game.Content.Load<Texture2D>("continue2");

            tryAgain1 = game.Content.Load<Texture2D>("tryagain1");
            tryAgain2 = game.Content.Load<Texture2D>("tryagain2");

            quit1 = game.Content.Load<Texture2D>("quit1");
            quit2 = game.Content.Load<Texture2D>("quit2");
        }
        #endregion Constructors

        #region Update
        //update Method
        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState(); //create a keyboard state variable to hold current keyboard state
            if (keyState.IsKeyDown(Keys.Back) && keyState.IsKeyDown(Keys.Back))
            {
                gameState.CurrentScreen = Screen.StartScreen;
            }
            if ((keyState.IsKeyDown(Keys.W) && lastState.IsKeyUp(Keys.W)) || (keyState.IsKeyDown(Keys.Up) && lastState.IsKeyUp(Keys.Up)))
            {               
                count--;
                if (count < 0)
                {
                    count = 2;
                }
            }
            if (keyState.IsKeyDown(Keys.S) && lastState.IsKeyUp(Keys.S) || (keyState.IsKeyDown(Keys.Down) && lastState.IsKeyUp(Keys.Down)))
            {
                count++;
                if (count > 2)
                {
                    count = 0;
                }
            }
            if (keyState.IsKeyDown(Keys.Enter) || keyState.IsKeyDown(Keys.Space))
            {
                switch (count)
                {
                    case 0: gameState.StartGame(currentLevel, levelName); // this will go to the next level
                        break;
                    case 1: gameState.SwitchLevel(game);
                        break;
                    case 2: gameState.CurrentScreen = Screen.StartScreen;
                        break;
                }
            }
            lastState = keyState;
        }
        #endregion Update

        #region Draw
        //Draw Method
        public void Draw(SpriteBatch spriteBatch, Texture2D background)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, game.screenWidth, game.screenHeight), Color.White);
            //spriteBatch.DrawString(font1, "This is Level End Screen", new Vector2(500f, 500f), Color.Black);

            if(count == 0)
            {
                spriteBatch.Draw(tryAgain1, new Rectangle(game.screenWidth / 2 - 220, 200, 500, 250), Color.White);
                //spriteBatch.DrawString(font1, "Contine", new Vector2(700f, 400f), Color.Gold);
            }
            else
            {
                spriteBatch.Draw(tryAgain2, new Rectangle(game.screenWidth / 2 - 220, 200, 500, 250), Color.Gray);
                //spriteBatch.DrawString(font1, "Contine", new Vector2(700f, 400f), Color.Black);
            }
            if(count == 1)
            {
                spriteBatch.Draw(continue1, new Rectangle(game.screenWidth / 2 - 220, 275, 500, 250), Color.White);
                
            }
            else
            {
                spriteBatch.Draw(continue2, new Rectangle(game.screenWidth / 2 - 220, 275, 500, 250), Color.Gray);
                //spriteBatch.DrawString(font1, "Try Again", new Vector2(700f, 500f), Color.Black);
            }
            if(count == 2)
            {
                spriteBatch.Draw(quit1, new Rectangle(game.screenWidth / 2 - 220, 350, 500, 250), Color.White);
                //spriteBatch.DrawString(font1, "Quit", new Vector2(700f, 600f), Color.Gold);
            }
            else
            {
                spriteBatch.Draw(quit2, new Rectangle(game.screenWidth / 2 - 220, 350, 500, 250), Color.Gray);
                //spriteBatch.DrawString(font1, "Quit", new Vector2(700f, 600f), Color.Black);
            }
        }

        #endregion Draw
    }
}
