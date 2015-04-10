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
            lastState = Keyboard.GetState();
        }
        #endregion Constructor

        #region Methods
        //Update Method
        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            
            if (keyState.IsKeyDown(Keys.Back) && lastState.IsKeyDown(Keys.Back))
            {
                gameState.CurrentScreen = Screen.StartScreen;
            }
            if (keyState.IsKeyDown(Keys.A) && lastState.IsKeyUp(Keys.A) || keyState.IsKeyDown(Keys.Left) && lastState.IsKeyUp(Keys.Left))
            {
                count++;
                if (count > 1)
                {
                    count = 0;
                }
            }
            if (keyState.IsKeyDown(Keys.D) && lastState.IsKeyUp(Keys.D) || keyState.IsKeyDown(Keys.Right) && lastState.IsKeyUp(Keys.Right))
            {
                count--;
                if (count < 0)
                {
                    count = 1;
                }
            }
            if (keyState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter) || keyState.IsKeyDown(Keys.Space) && lastState.IsKeyUp(Keys.Space))
            {
                if (count == 0)
                {
                    gameState.StartGame("test.txt");
                }
                else if (count == 1)
                {
                    gameState.StartGame("level1.txt");
                }
            }

            font1 = game.Content.Load<SpriteFont>("Font1");
            lastState = keyState;

            lastState = keyState; //assigns current keyboard state to lastState
        }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch, Texture2D background, Texture2D title)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, game.screenWidth, game.screenHeight), Color.White);
            spriteBatch.Draw(title, new Rectangle(game.screenWidth / 2 - 500, 100, 1000, 512), Color.White);
            //spriteBatch.DrawString(font1, "This is the Level Selection Screen", new Vector2(50f, 50f), Color.Black);
            if (count == 0)
            {
                spriteBatch.DrawString(font1, "Level 1", new Vector2(game.screenWidth / 2 - 250, 400f), Color.Gold);
            }
            else
            {
                spriteBatch.DrawString(font1, "Level 1", new Vector2(game.screenWidth / 2 - 250, 400f), Color.White);
            }
            if (count == 1)
            {
                spriteBatch.DrawString(font1, "Level 2", new Vector2(game.screenWidth / 2 + 150, 400f), Color.Gold);
            }
            else
            {
                spriteBatch.DrawString(font1, "Level 2", new Vector2(game.screenWidth / 2 + 150, 400f), Color.White);
            }
        }


        #endregion Methods
    }
}
