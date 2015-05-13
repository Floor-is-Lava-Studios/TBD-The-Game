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
        private int count;
        private SaveInfo info;
        private Texture2D back;
        #endregion Attributes

        #region Constructor
        //Constructor
        public OptionScreen(Game1 game1)
        {
            game = game1;
            gameState = new GameState(game); // creates new gamestate object and assigns it to gameState
            font1 = game.Content.Load<SpriteFont>("Font1"); //loads Font1
            back = game.Content.Load<Texture2D>("back");
            info = new SaveInfo();
            count = 0;
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
            }
            else //if the pause screen is active only allow player to go back to pause menu
            {
                if (keyState.IsKeyDown(Keys.Back) && lastState.IsKeyUp(Keys.Back))
                {
                    gameState.SwitchPause(); //switches to pause screen
                }
            }
            if((keyState.IsKeyDown(Keys.W) && lastState.IsKeyUp(Keys.W)) || (keyState.IsKeyDown(Keys.Up) && lastState.IsKeyUp(Keys.Up)))
            {
                count = count - 1;
                if (count < 0)
                {
                    count = 2;
                }
            }
            if ((keyState.IsKeyDown(Keys.S) && lastState.IsKeyUp(Keys.S)) || (keyState.IsKeyDown(Keys.Down) && lastState.IsKeyUp(Keys.Down)))
            {
                count = count + 1;
                if (count > 2)
                {
                    count = 0;
                }
            }
            if (keyState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter))
            {
                if (count == 0)
                {
                    //toggle fullscreen code
                    if (game.playMusic == true)
                    {
                        game.playMusic = false;
                    }
                    else
                    {
                        if(Music.canPlay)
                        {
                            game.playMusic = true;
                            game.startMusic = true;
                        }
                    }
                }
                if (count == 1)
                {
                    //reset highscore code 
                    info.ResetHighScore();
                }
                if (count == 2)
                {
                    //reset level lock code goes here
                    info.ResetLevelLock();
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
            //spriteBatch.DrawString(font1, "Press \"Back\" to go back", new Vector2(50f, 70f), Color.Blue);
            spriteBatch.Draw(back, new Rectangle(50, 60, 150, 80), Color.White);
            if (count == 0)
            {
                if (game.playMusic == true)
                {
                    spriteBatch.DrawString(font1, "Sound: On", new Vector2(game.screenWidth / 2 - 100, 400f), Color.Crimson);
                }
                if (game.playMusic == false)
                {
                    spriteBatch.DrawString(font1, "Sound: OFF", new Vector2(game.screenWidth / 2 - 100, 400f), Color.Crimson);
                }
            }
            else
            {
                if (game.playMusic == true)
                {
                    spriteBatch.DrawString(font1, "Sound: On", new Vector2(game.screenWidth / 2 - 100, 400f), Color.White);
                }
                if (game.playMusic == false)
                {
                    spriteBatch.DrawString(font1, "Sound: OFF", new Vector2(game.screenWidth / 2 - 100, 400f), Color.White);
                }
               
            }
            if (count == 1)
            {
                spriteBatch.DrawString(font1, "Reset Level HighScores", new Vector2(game.screenWidth / 2 - 100, 500f), Color.Crimson);
            }
            else
            {
                spriteBatch.DrawString(font1, "Reset Level HighScores", new Vector2(game.screenWidth / 2 - 100, 500f), Color.White);
            }
            if (count == 2)
            {
                spriteBatch.DrawString(font1, "Reset Level Lock Status", new Vector2(game.screenWidth / 2 - 100, 600f), Color.Crimson);
            }
            else
            {
                spriteBatch.DrawString(font1, "Reset Level Lock Status", new Vector2(game.screenWidth / 2 - 100, 600f), Color.White);
            }
        }
        #endregion Methods
    }
}
