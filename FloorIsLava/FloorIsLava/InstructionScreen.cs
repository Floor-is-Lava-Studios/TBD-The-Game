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
    class InstructionScreen
    {
        #region Attributes
        //Attributes
        private SpriteFont font1;
        private Game1 game;
        private KeyboardState lastState;
        private GameState gameState;

        // all the art!!!
        private Texture2D backwardsA;
        private Texture2D backwardsArrow;
        private Texture2D forwardD;
        private Texture2D forwardArrow;
        private Texture2D grapple1;
        private Texture2D grapple2;
        private Texture2D grapple3;
        private Texture2D space;
        private Texture2D pause;
        private Texture2D color;
        private Texture2D back;
        private Texture2D grappleDown1;
        private Texture2D grappleDown2;
        private Texture2D grappleUp1;
        private Texture2D grappleUp2;
        private Texture2D title;
        #endregion Attributes

        #region Constructor 
        //Constructor
        public InstructionScreen(Game1 game1)
        {
            game = game1; //assigns the game1 object to game attribute
            gameState = new GameState(game); // creates the gameState object and puts it into gamestate attribute
            font1 = game.Content.Load<SpriteFont>("Font1"); //loads the Font1 sprite font

            // LOAD ALL THE ART
            backwardsA = game.Content.Load<Texture2D>("backwards");
            backwardsArrow = game.Content.Load<Texture2D>("backwards arrow");
            forwardD = game.Content.Load<Texture2D>("forward ");
            forwardArrow = game.Content.Load<Texture2D>("forward arrow");
            grapple1 = game.Content.Load<Texture2D>("eGrapple");
            grapple2 = game.Content.Load<Texture2D>("leftshift");
            grapple3 = game.Content.Load<Texture2D>("rightShift");
            space = game.Content.Load<Texture2D>("space");
            pause = game.Content.Load <Texture2D>("pauseControl");
            color = game.Content.Load<Texture2D>("pause");
            back = game.Content.Load<Texture2D>("back");

            grappleDown1 = game.Content.Load<Texture2D>("grappleDownArrow");
            grappleDown2 = game.Content.Load<Texture2D>("grappleDownS");

            grappleUp1 = game.Content.Load<Texture2D>("grappleUPW");
            grappleUp2 = game.Content.Load<Texture2D>("grappleUPArrow");
            title = game.Content.Load<Texture2D>("instructionTitle");
        }
        #endregion Constructor

        #region Methods
        //Update Method
        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (gameState.PauseScreen == null)
            {
                
                if (keyState.IsKeyDown(Keys.S) && lastState.IsKeyDown(Keys.S))
                {
                    gameState.CurrentScreen = Screen.StartScreen;
                }
                if (keyState.IsKeyDown(Keys.Back) && keyState.IsKeyDown(Keys.Back))
                {
                    gameState.CurrentScreen = Screen.StartScreen;
                }
                lastState = keyState;

                keyState = Keyboard.GetState(); //create a keyboard state variable to hold current keyboard state
                if (keyState.IsKeyDown(Keys.S) && lastState.IsKeyDown(Keys.S))
                {
                    gameState.CurrentScreen = Screen.StartScreen;
                }
                if (keyState.IsKeyDown(Keys.G) && lastState.IsKeyDown(Keys.G))
                {
                    gameState.StartGame("test.txt");
                }
                if (keyState.IsKeyDown(Keys.O) && lastState.IsKeyDown(Keys.O))
                {
                    gameState.SwitchOption(game);
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
            else
            {
                if(keyState.IsKeyDown(Keys.Back)&&lastState.IsKeyDown(Keys.Back))
                {
                    gameState.SwitchPause();
                }
            }
            lastState = keyState; // assigns current keyboard state to lastState
        }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch, Texture2D background)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, game.screenWidth, game.screenHeight), Color.Wheat);
            spriteBatch.Draw(color, new Rectangle((game.screenWidth / 2) - 600, (game.screenHeight / 2 + 200) - 400, 1200, 500), Color.White);
;
            spriteBatch.Draw(back, new Rectangle(50, 60, 150, 80), Color.White);
            spriteBatch.Draw(title, new Rectangle(game.screenWidth / 2 - 500, -15, 1000, 512), Color.White);

            spriteBatch.Draw(forwardD, new Rectangle(game.screenWidth / 2 - 530, (game.screenHeight / 2 + 200) - 400, 300, 150), Color.White);
            spriteBatch.Draw(forwardArrow, new Rectangle(game.screenWidth / 2 - 530, (game.screenHeight / 2 + 300) - 400, 300, 150), Color.White);

            spriteBatch.Draw(backwardsA, new Rectangle(game.screenWidth / 2 - 530, (game.screenHeight / 2 + 400) - 400, 300, 150), Color.White);
            spriteBatch.Draw(backwardsArrow, new Rectangle(game.screenWidth / 2 - 530, (game.screenHeight / 2 + 500) - 400, 300, 150), Color.White);

            spriteBatch.Draw(grappleUp1, new Rectangle(game.screenWidth / 2 - 150, (game.screenHeight / 2 + 200) - 400, 300, 150), Color.White);
            spriteBatch.Draw(grappleUp2, new Rectangle(game.screenWidth / 2 - 150, (game.screenHeight / 2 + 300) - 400, 300, 150), Color.White);
            spriteBatch.Draw(grappleDown2, new Rectangle(game.screenWidth / 2 - 150, (game.screenHeight / 2 + 400) - 400, 300, 150), Color.White);
            spriteBatch.Draw(grappleDown1, new Rectangle(game.screenWidth / 2 - 150, (game.screenHeight / 2 + 500) - 400, 300, 150), Color.White);
            

            spriteBatch.Draw(grapple1, new Rectangle(game.screenWidth / 2 + 250, (game.screenHeight / 2 + 210) - 400, 250, 100), Color.White);
            spriteBatch.Draw(grapple2, new Rectangle(game.screenWidth / 2 + 250, (game.screenHeight / 2 + 275) - 400, 250, 100), Color.White);
            spriteBatch.Draw(grapple3, new Rectangle(game.screenWidth / 2 + 250, (game.screenHeight / 2 + 350) - 400, 250, 100), Color.White);
            spriteBatch.Draw(pause, new Rectangle(game.screenWidth / 2 + 250, (game.screenHeight / 2 + 425) - 400, 250, 100), Color.White);
            spriteBatch.Draw(space, new Rectangle(game.screenWidth / 2 + 250, (game.screenHeight / 2 + 520) - 400, 250, 100), Color.White);
        }
        #endregion Methods
    }
}
