#region UsingStatements
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
#endregion UsingStatements

namespace FloorIsLava
{
    /* This is the startScreen Class it will control the start menu 
     * and what will appear on it.
     * */
    class StartScreen
    {
        #region Attributes
        private Texture2D background;
        private SpriteFont font1;
        private Game1 game;
        private KeyboardState lastState;
        private GameState gameState;

        // shit im adding
        private int count = 0;
        #endregion Attributes

        #region Constructor
        public StartScreen(Game1 game1)
        {
            game = game1;
            gameState = new GameState(game); // creates new gamestate object and assigns gameState 
            lastState = Keyboard.GetState();  // assigns keyboard state to lastState
        }
        #endregion Constructor

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState(); //create a keyboard state variable to hold current keyboard state
            if (keyboardState.IsKeyDown(Keys.S) && lastState.IsKeyUp(Keys.S))
            {
                count++;
                if(count > 4)
                {
                    count = 0;
                }
            }
            if (keyboardState.IsKeyDown(Keys.W) && lastState.IsKeyUp(Keys.W))
            {
                count--;
                if (count < 0)
                {
                    count = 4;
                }
            }
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                if (count == 0)
                {
                    gameState.SwitchInstruct(game);
                }
                else if (count == 1)
                {
                    gameState.StartGame();
                }
                else if (count == 2)
                {
                    gameState.SwitchOption(game);
                }
                else if (count == 3)
                {
                    gameState.SwitchLevel(game);
                }
                else if (count == 4)
                {
                    gameState.SwitchCredit(game);
                }
            }
            font1 = game.Content.Load<SpriteFont>("Font1"); // Loads Font1
            lastState = keyboardState; //assigns current keyboard state to lastState
        }

        public void Draw(SpriteBatch spritebatch, Texture2D background, Texture2D title, Texture2D creditButton1, Texture2D creditButton2, Texture2D levelButton1, Texture2D levelButton2, Texture2D startButton1, Texture2D startButton2, Texture2D options1, Texture2D options2, Texture2D instructions1, Texture2D instructions2)
        {
            if(background != null)
            {
                //spritebatch.Draw(background, new Vector2(0f, 0f), Color.Green);
                spritebatch.Draw(background, new Rectangle(0,0,game.screenWidth,game.screenHeight), Color.White);
                spritebatch.Draw(title, new Rectangle(game.screenWidth/2-500, 100, 1000, 512), Color.White);

            }
            //spritebatch.DrawString(font1, "This is start screen", new Vector2(50f, 50f), Color.White);
            //spritebatch.DrawString(font1, "" + gameState.Num, new Vector2(0, 0), Color.White);
            if(count == 0)
            {
                spritebatch.Draw(instructions2, new Rectangle(game.screenWidth / 2 - 220, 200, 500, 250), Color.White);
            }
            else
            {
                spritebatch.Draw(instructions1, new Rectangle(game.screenWidth / 2 - 220, 200, 500, 250), Color.White);
            }
            if(count == 1)
            {
                //start
                spritebatch.Draw(startButton1, new Rectangle(game.screenWidth / 2 - 220, 275, 500, 250), Color.White);
            }
            else
            {
                //start
                spritebatch.Draw(startButton2, new Rectangle(game.screenWidth / 2 - 220, 275, 500, 250), Color.White);
            }
            if(count == 2)
            {
                spritebatch.Draw(options1, new Rectangle(game.screenWidth / 2 - 220, 350, 500, 250), Color.White);
            }
            else
            {
                spritebatch.Draw(options2, new Rectangle(game.screenWidth / 2 - 220, 350, 500, 250), Color.White);
            }
            if(count == 3)
            {
                spritebatch.Draw(levelButton1, new Rectangle(game.screenWidth / 2 - 220, 425, 500, 250), Color.White);
            }
            else
            {
                spritebatch.Draw(levelButton2, new Rectangle(game.screenWidth / 2 - 220, 425, 500, 250), Color.White);
            }
            if(count == 4)
            {
                spritebatch.Draw(creditButton2, new Rectangle(game.screenWidth / 2 - 220, 500, 500, 250), Color.White);
            }
            else
            {
                spritebatch.Draw(creditButton1, new Rectangle(game.screenWidth / 2 - 220, 500, 500, 250), Color.White);
            }
        }
    }
}
