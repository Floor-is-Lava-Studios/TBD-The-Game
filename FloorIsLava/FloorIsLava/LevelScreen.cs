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
    class LevelScreen
    {
        #region Attributes
        //Attributes
        private SpriteFont font1;
        private Game1 game;
        private KeyboardState lastState;
        private GameState gameState;
        private GameScreen gameScreen;
        private string levelName;

        private int count = 0;
        #endregion Attributes

        #region Constructor
        //Constructor
        public LevelScreen(Game1 game1)
        {
            game = game1;
            gameState = new GameState(game); // creates new gamestate object and assigns it to gameState
            font1 = game.Content.Load<SpriteFont>("Font1"); // loads Font1
        }
        #endregion Constructor

        #region Methods
        //Update Method
        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
  //      if (keyState.IsKeyDown(Keys.S) && lastState.IsKeyDown(Keys.S))
  //      {
  //          gameState.CurrentScreen = Screen.StartScreen;
  //      }
  //      if (keyState.IsKeyDown(Keys.G) && lastState.IsKeyDown(Keys.G))
  //      {
  //          gameState.StartGame();
  //      }
  //      if (keyState.IsKeyDown(Keys.O) && lastState.IsKeyDown(Keys.O))
  //      {
  //          gameState.SwitchOption(game);
  //      }
  //      if (keyState.IsKeyDown(Keys.I) && lastState.IsKeyDown(Keys.I))
  //      {
  //          gameState.SwitchInstruct(game);
  //      }
  //      if (keyState.IsKeyDown(Keys.C) && lastState.IsKeyDown(Keys.C))
  //      {
  //          gameState.SwitchCredit(game);
  //      }
            if (keyState.IsKeyDown(Keys.A) && lastState.IsKeyUp(Keys.A))

            keyState = Keyboard.GetState(); //create a keyboard state variable to hold current keyboard state
            
            if (keyState.IsKeyDown(Keys.S) && lastState.IsKeyDown(Keys.S))
            {
                gameState.CurrentScreen = Screen.StartScreen;
            }
            if (keyState.IsKeyDown(Keys.G) && lastState.IsKeyDown(Keys.G))
            {
                gameState.StartGame();
            }
            if (keyState.IsKeyDown(Keys.O) && lastState.IsKeyDown(Keys.O))

            {
                count++;
                if (count > 1)
                {
                    count = 0;
                }
            }
            if (keyState.IsKeyDown(Keys.D) && lastState.IsKeyUp(Keys.D))
            {
                count--;
                if (count < 0)
                {
                    count = 1;
                }
            }
            if (keyState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter))
            {
                if (count == 0)
                {
                    //gameScreen = new GameScreen(game, "test.txt");
                    gameState.StartGame();
                }
                else if (count == 1)
                {
                    //gameScreen = new GameScreen(game, "level1.txt");
                    //gameState.StartGame();
                }
  //         else if (count == 2)
  //         {
  //             gameState.SwitchOption(game);
  //         }
  //         else if (count == 3)
  //         {
  //             gameState.SwitchLevel(game);
  //         }
  //         else if (count == 4)
  //         {
  //             gameState.SwitchCredit(game);
  //         }
            }

            font1 = game.Content.Load<SpriteFont>("Font1");
            lastState = keyState;

            lastState = keyState; //assigns current keyboard state to lastState
        }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch, Texture2D background)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, game.screenWidth, game.screenHeight), Color.White);

            spriteBatch.DrawString(font1, "This is the Level Selection Screen", new Vector2(50f, 50f), Color.Black);
            if (count == 0)
            {
                spriteBatch.DrawString(font1, "Level 1", new Vector2(200f, 100f), Color.Gold);
            }
            else
            {
                spriteBatch.DrawString(font1, "Level 1", new Vector2(200f, 100f), Color.White);
            }
            if (count == 1)
            {
                spriteBatch.DrawString(font1, "Level 2", new Vector2(300f, 100f), Color.Gold);
            }
            else
            {
                spriteBatch.DrawString(font1, "Level 2", new Vector2(300f, 100f), Color.White);
            }
        }


        #endregion Methods
    }
}
