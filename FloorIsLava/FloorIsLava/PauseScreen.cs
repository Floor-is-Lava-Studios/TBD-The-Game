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

        // all the images
        private Texture2D title;
        private Texture2D continue1;
        private Texture2D continue2;
        private Texture2D options1;
        private Texture2D options2;
        private Texture2D howTo1;
        private Texture2D howTo2;
        private Texture2D screen;
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

            // load all the images
            title = game.Content.Load<Texture2D>("pauseTitle");
            continue1 = game.Content.Load<Texture2D>("continue1");
            continue2 = game.Content.Load<Texture2D>("continue2");
            options1 = game.Content.Load<Texture2D>("options1");
            options2 = game.Content.Load<Texture2D>("options2");
            howTo1 = game.Content.Load<Texture2D>("instruction1");
            howTo2 = game.Content.Load<Texture2D>("instruction2");
            screen = game.Content.Load<Texture2D>("pause");
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
               //  case 3:
               //      gameState.CurrentScreen = Screen.StartScreen;
               //      break;
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
            spriteBatch.Draw(screen, new Rectangle((game.screenWidth/2) - 500, (game.screenHeight/2) - 400 , screenWidth, screenHeight), Color.White);
            // fix the image 
            //spriteBatch.Draw(title, new Rectangle(game.screenWidth / 2 - 500, 100, 1000, 512), Color.White);
            spriteBatch.DrawString(font1,"PAUSE", new Vector2(game.screenWidth / 2, 100),Color.Blue);

            // drawing the buttons, if selected the color changes
            if (button == 0)
            {
                spriteBatch.Draw(continue1, new Rectangle(game.screenWidth / 2 - 220, 200, 500, 250), Color.White);
                //spriteBatch.DrawString(font1, "Contine", new Vector2(700f, 400f), Color.Gold);
            }
            else
            {
                spriteBatch.Draw(continue2, new Rectangle(game.screenWidth / 2 - 220, 200, 500, 250), Color.Gray);
                //spriteBatch.DrawString(font1, "Contine", new Vector2(700f, 400f), Color.Black);
            }
            if (button == 1)
            {
                spriteBatch.Draw(options1, new Rectangle(game.screenWidth / 2 - 220, 275, 500, 250), Color.White);
            }
            else
            {
                spriteBatch.Draw(options2, new Rectangle(game.screenWidth / 2 - 220, 275, 500, 250), Color.Gray);
            }
            if (button == 2)
            {
                spriteBatch.Draw(howTo1, new Rectangle(game.screenWidth / 2 - 220, 350, 500, 250), Color.White);
            }
            else
            {
                spriteBatch.Draw(howTo2, new Rectangle(game.screenWidth / 2 - 220, 350, 500, 250), Color.Gray);
            }
     //  if (button == 3)
     //  {
     //      spriteBatch.DrawString(font1, "Quit", new Vector2(game.screenWidth / 2, 500), Color.Yellow);
     //  }
     //  else
     //  {
     //      spriteBatch.DrawString(font1, "Quit", new Vector2(game.screenWidth / 2, 500), Color.Blue);
     //  }
        }
        #endregion Draw 
    }
}
