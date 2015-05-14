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

        private Texture2D back;
        private Texture2D title;

        private Texture2D logo;
        private Texture2D names;
        private Texture2D bg;
        private Texture2D music;

        #endregion Attributes

        #region Constructor 
        //Constructor
        public CreditScreen(Game1 game1)
        {
            game = game1; //assigns the game1 object
            gameState = new GameState(game); //creates a new gamestate class object and assigns it to gamestate
            font1 = game.Content.Load<SpriteFont>("Font1"); // loads Font1 spriteFont
            back = game.Content.Load<Texture2D>("back");
            title = game.Content.Load<Texture2D>("creditsTitle");
            logo = game.Content.Load<Texture2D>("LavaLogo");
            names = game.Content.Load<Texture2D>("Names");
            bg = game.Content.Load<Texture2D>("pause");
            music = game.Content.Load<Texture2D>("musicCredits");
        }
        #endregion Constructor

        #region Methods
        //Update Method
        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState(); //create a keyboard state variable to hold current keyboard state
            if (keyState.IsKeyDown(Keys.Back) && keyState.IsKeyDown(Keys.Back))
            {
                gameState.CurrentScreen = Screen.StartScreen;
            }
            lastState = keyState;

            if (keyState.IsKeyDown(Keys.S) && lastState.IsKeyDown(Keys.S)) // if s key is pressed
            {
                gameState.CurrentScreen = Screen.StartScreen; // switches to start screen
            }
            if (keyState.IsKeyDown(Keys.G) && lastState.IsKeyDown(Keys.G)) // if G key is pressed 
            {
                gameState.StartGame("test.txt", "level2"); // calls start game method to switch to game
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
        public void Draw(SpriteBatch spriteBatch, Texture2D background)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, game.screenWidth, game.screenHeight), Color.White);
            //spriteBatch.DrawString(font1, "Credits", new Vector2(100f, 50f), Color.Blue);
            //spriteBatch.DrawString(font1, "Press \"Back\" to go back", new Vector2(100f, 70f), Color.Blue);
            spriteBatch.Draw(title, new Rectangle(game.screenWidth / 2 - 500, 100, 1000, 512), Color.White);
            spriteBatch.Draw(back, new Rectangle(50, 60, 150, 80), Color.White);
            spriteBatch.Draw(bg, new Rectangle(game.screenWidth / 2 + 75, 475, 350, 206), Color.White);
            spriteBatch.Draw(logo, new Rectangle(game.screenWidth / 2 - 250, 300, 500, 156), Color.White);

            spriteBatch.Draw(names, new Rectangle(game.screenWidth / 2 + 100, 500, 300, 156),Color.Blue);
            spriteBatch.Draw(music, new Rectangle(game.screenWidth / 2 - 450, 500, 300, 156), Color.White);
           
        }
        #endregion Methods
    }
}
