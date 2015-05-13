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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

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
        private bool showdown;

        //sound button images
        private Texture2D soundOn1;
        private Texture2D soundOn2;
        private Texture2D soundOff1;
        private Texture2D soundOff2;

        //unlock reset images
        private Texture2D levelUnlock1;
        private Texture2D levelUnlock2;

        // high score reset images
        private Texture2D resetHS1;
        private Texture2D resetHS2;
        #endregion Attributes

        #region Constructor
        //Constructor
        public OptionScreen(Game1 game1)
        {
            game = game1;
            gameState = new GameState(game); // creates new gamestate object and assigns it to gameState
            font1 = game.Content.Load<SpriteFont>("Font1"); //loads Font1
            back = game.Content.Load<Texture2D>("back");
            soundOn1 = game.Content.Load<Texture2D>("soundOn1");
            soundOn2 = game.Content.Load<Texture2D>("soundOn2");
            soundOff1 = game.Content.Load<Texture2D>("soundOff1");
            soundOff2 = game.Content.Load<Texture2D>("soundOff2");
            levelUnlock1 = game.Content.Load<Texture2D>("levelUnlock1");
            levelUnlock2 = game.Content.Load<Texture2D>("levelUnlock2");
            resetHS1 = game.Content.Load<Texture2D>("resetHS1");
            resetHS2 = game.Content.Load<Texture2D>("resetHS2");
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
                game.grappleS.Play();
                count = count - 1;
                if (count < 0)
                {
                    count = 2;
                }
            }
            if ((keyState.IsKeyDown(Keys.S) && lastState.IsKeyUp(Keys.S)) || (keyState.IsKeyDown(Keys.Down) && lastState.IsKeyUp(Keys.Down)))
            {
                game.grappleS.Play();
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
                game.grappleS.Play();
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
                    //spriteBatch.DrawString(font1, "Sound: On", new Vector2(game.screenWidth / 2 - 100, 400f), Color.Crimson);
                    spriteBatch.Draw(soundOn2, new Vector2(game.screenWidth / 2 - 130, game.screenHeight / 2), Color.White);
                }
                if (game.playMusic == false)
                {
                    //spriteBatch.DrawString(font1, "Sound: OFF", new Vector2(game.screenWidth / 2 - 100, 400f), Color.Crimson);
                    spriteBatch.Draw(soundOff2, new Vector2(game.screenWidth / 2 - 130, game.screenHeight / 2), Color.White);
                }
            }
            else
            {
                if (game.playMusic == true)
                {
                    //spriteBatch.DrawString(font1, "Sound: On", new Vector2(game.screenWidth / 2 - 100, 400f), Color.White);
                    spriteBatch.Draw(soundOn1, new Vector2(game.screenWidth / 2 - 130, game.screenHeight / 2), Color.White);
                }
                if (game.playMusic == false)
                {
                    //spriteBatch.DrawString(font1, "Sound: OFF", new Vector2(game.screenWidth / 2 - 100, 400f), Color.White);
                    spriteBatch.Draw(soundOff1, new Vector2(game.screenWidth / 2 - 130, game.screenHeight / 2), Color.White);
                }
               
            }
            if (count == 1)
            {
                //spriteBatch.DrawString(font1, "Reset Level HighScores", new Vector2(game.screenWidth / 2 - 100, 500f), Color.Crimson);
                spriteBatch.Draw(resetHS2, new Vector2(game.screenWidth / 2 - 130, game.screenHeight / 2 + 100), Color.White);
            }
            else
            {
                //spriteBatch.DrawString(font1, "Reset Level HighScores", new Vector2(game.screenWidth / 2 - 100, 500f), Color.White);
                spriteBatch.Draw(resetHS1, new Vector2(game.screenWidth / 2 - 130, game.screenHeight / 2 + 100), Color.White);
            }
            if (count == 2)
            {
                //spriteBatch.DrawString(font1, "Reset Level Lock Status", new Vector2(game.screenWidth / 2 - 100, 600f), Color.Crimson);
                spriteBatch.Draw(levelUnlock2, new Vector2(game.screenWidth / 2 - 130, game.screenHeight / 2 + 200), Color.White);
            }
            else
            {
                //spriteBatch.DrawString(font1, "Reset Level Lock Status", new Vector2(game.screenWidth / 2 - 100, 600f), Color.White);
                spriteBatch.Draw(levelUnlock1, new Vector2(game.screenWidth / 2 - 130, game.screenHeight / 2 + 200), Color.White);
            }
        }
        #endregion Methods
    }
}
