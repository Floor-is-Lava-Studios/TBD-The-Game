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
        #endregion Attributes

        #region Constructors
        public EndLevelScreen(Game1 game1)
        {
            game = game1; //assigns the game1 object
            gameState = new GameState(game); //creates a new gamestate class object and assigns it to gamestate
            font1 = game.Content.Load<SpriteFont>("Font1"); // loads Font1 spriteFont
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
            lastState = keyState;
        }
        #endregion Update

        #region Draw
        //Draw Method
        public void Draw(SpriteBatch spriteBatch, Texture2D background)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, game.screenWidth, game.screenHeight), Color.White);
            spriteBatch.DrawString(font1, "This is Level End Screen", new Vector2(500f, 500f), Color.Black);
        }

        #endregion Draw
    }
}
