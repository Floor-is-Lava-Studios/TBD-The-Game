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
    class OptionScreen
    {
        #region Attributes
        //Attributes
        private SpriteFont font1;
        private Game1 game;
        private KeyboardState lastState;
        private GameState gameState;
        #endregion Attributes

        #region Constructor
        //Constructor
        public OptionScreen(Game1 game1)
        {
            game = game1;
            gameState = new GameState(game); // creates new gamestate object and assigns it to gameState
            font1 = game.Content.Load<SpriteFont>("Font1"); //loads Font1
        }
        #endregion Constructor

        #region Methods
        //Update Method
        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState(); //create a keyboard state variable to hold current keyboard state
            if (gameState.PauseScreen == null) //if pausescreen is not active
            {
                if (keyState.IsKeyDown(Keys.Back) && keyState.IsKeyDown(Keys.Back))
                {
                    gameState.CurrentScreen = Screen.StartScreen;
                }
                lastState = keyState;

                if (keyState.IsKeyDown(Keys.S) && lastState.IsKeyDown(Keys.S))
                {
                    gameState.CurrentScreen = Screen.StartScreen;
                }
                if (keyState.IsKeyDown(Keys.G) && lastState.IsKeyDown(Keys.G))
                {
                    gameState.StartGame("text.txt");
                }
                if (keyState.IsKeyDown(Keys.I) && lastState.IsKeyDown(Keys.I))
                {
                    gameState.SwitchInstruct(game);
                }
                if (keyState.IsKeyDown(Keys.L) && lastState.IsKeyDown(Keys.L))
                {
                    gameState.SwitchLevel(game);
                }
                if (keyState.IsKeyDown(Keys.C) && lastState.IsKeyDown(Keys.C))
                {
                    gameState.SwitchCredit(game);
                }
            }
            else //if the pause screen is active only allow player to go back to pause menu
            {
                if (keyState.IsKeyDown(Keys.Back) && keyState.IsKeyDown(Keys.Back))
                {
                    gameState.SwitchPause(); //switches to pause screen
                }
                lastState = keyState;

                if (keyState.IsKeyDown(Keys.S) && lastState.IsKeyDown(Keys.S))
                {
                    gameState.CurrentScreen = Screen.StartScreen;
                }
                if (keyState.IsKeyDown(Keys.G) && lastState.IsKeyDown(Keys.G))
                {
                    gameState.StartGame("text.txt");
                }
                if (keyState.IsKeyDown(Keys.I) && lastState.IsKeyDown(Keys.I))
                {
                    gameState.SwitchInstruct(game);
                }
                if (keyState.IsKeyDown(Keys.L) && lastState.IsKeyDown(Keys.L))
                {
                    gameState.SwitchLevel(game);
                }
                if (keyState.IsKeyDown(Keys.C) && lastState.IsKeyDown(Keys.C))
                {
                    gameState.SwitchCredit(game);
                }
            }
            lastState = keyState; //assigns current keyboard state to lastState
        }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch, Texture2D background, Texture2D title)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, game.screenWidth, game.screenHeight), Color.White);
            //spriteBatch.DrawString(font1, "This is Option Screen", new Vector2(50f, 50f), Color.Black);
            spriteBatch.Draw(title, new Rectangle(game.screenWidth / 2 - 500, 100, 1000, 512), Color.White);
            spriteBatch.DrawString(font1, "Press \"Back\" to go back", new Vector2(50f, 70f), Color.Blue);
            spriteBatch.DrawString(font1, "COMING SOON TO A UPDATE NEAR YOU", new Vector2(game.screenWidth / 2 - 200, 300f), Color.Gold);
        }
        #endregion Methods
    }
}
