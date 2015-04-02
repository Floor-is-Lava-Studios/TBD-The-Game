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
    /* This class will control what happens on the credit screen
     * 
     * */
    class CreditScreen
    {
        #region Attributes
        //Attributes
        private SpriteFont font1;
        private Game1 game;
        private KeyboardState lastState; //holds previous keyboard state
        private GameState gameState; //hold the gameState class
        #endregion Attributes

        #region Constructor 
        //Constructor
        public CreditScreen(Game1 game1)
        {
            game = game1; //assigns the game1 object
            gameState = new GameState(game); //creates a new gamestate class object and assigns it to gamestate
            font1 = game.Content.Load<SpriteFont>("Font1"); // loads Font1 spriteFont
        }
        #endregion Constructor

        #region Methods
        //Update Method
        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState(); //create a keyboard state variable to hold current keyboard state
            if (keyState.IsKeyDown(Keys.S) && lastState.IsKeyDown(Keys.S)) // if s key is pressed
            {
                gameState.CurrentScreen = Screen.StartScreen; // switches to start screen
            }
            if (keyState.IsKeyDown(Keys.G) && lastState.IsKeyDown(Keys.G)) // if G key is pressed 
            {
                gameState.StartGame(); // calls start game method to switch to game
            }
            if (keyState.IsKeyDown(Keys.O) && lastState.IsKeyDown(Keys.O)) 
            {
                gameState.SwitchOption(game);//calls switch option method to switch to option screen
            }
            if (keyState.IsKeyDown(Keys.L) && lastState.IsKeyDown(Keys.L))
            {
                gameState.SwitchLevel(game);
            }
            if (keyState.IsKeyDown(Keys.I) && lastState.IsKeyDown(Keys.I))
            {
                gameState.SwitchInstruct(game);
            }
            lastState = keyState; // assigns current keyboard state to the last keyboard state
        }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font1, "Credits", new Vector2(100f, 50f), Color.Blue);
        }
        #endregion Methods
    }
}
