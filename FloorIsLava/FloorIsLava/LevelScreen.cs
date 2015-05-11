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
using System.IO;

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
        private SaveInfo save;
        Dictionary<string, bool> levels;
        private string currentLvl;
        private string nextLvl;

        private int count = 0;

        private Texture2D back;
        private Texture2D lvl1;
        private Texture2D lvl2;
        private Texture2D lvl3;
        private Texture2D lvl4;
        private Texture2D lvl5;
        #endregion Attributes

        #region Constructor
        //Constructor
        public LevelScreen(Game1 game1)
        {
            save = new SaveInfo();
            game = game1;
            gameState = new GameState(game); // creates new gamestate object and assigns it to gameState
            font1 = game.Content.Load<SpriteFont>("Font1"); // loads Font1
            lastState = Keyboard.GetState();
            levels = save.ReadUnlock();

            back = game.Content.Load<Texture2D>("back");
            lvl1 = game.Content.Load<Texture2D>("level1");
            lvl2 = game.Content.Load<Texture2D>("level2");
            lvl3 = game.Content.Load<Texture2D>("level3");
            lvl4 = game.Content.Load<Texture2D>("level4");
            lvl5 = game.Content.Load<Texture2D>("level5");

            currentLvl = "level1.txt";
            nextLvl = "test.txt";
        }
        #endregion Constructor

        #region Methods
        //Update Method
        public void Update(GameTime gameTime)
        {
            levels = save.ReadUnlock();
            KeyboardState keyState = Keyboard.GetState();
            
            if (keyState.IsKeyDown(Keys.Back) && lastState.IsKeyDown(Keys.Back))
            {
                gameState.CurrentScreen = Screen.StartScreen;
            }
            if (keyState.IsKeyDown(Keys.A) && lastState.IsKeyUp(Keys.A) || keyState.IsKeyDown(Keys.Left) && lastState.IsKeyUp(Keys.Left))
            {
                count--;
                if (count < 0)
                {
                    count = 4;
                }
                
            }
            if (keyState.IsKeyDown(Keys.D) && lastState.IsKeyUp(Keys.D) || keyState.IsKeyDown(Keys.Right) && lastState.IsKeyUp(Keys.Right))
            {
                count++;
                if (count > 4)
                {
                    count = 0;
                }
            }
            if (keyState.IsKeyDown(Keys.Enter) && lastState.IsKeyUp(Keys.Enter) || keyState.IsKeyDown(Keys.Space) && lastState.IsKeyUp(Keys.Space))
            {
                if (count == 0)
                {
                    gameState.StartGame("level1.txt", "level1");
                    currentLvl = "level1.txt";
                    nextLvl = "level2.txt";
                }
                else if (count == 1 && levels["level2"] == true)
                {
                    gameState.StartGame("test.txt", "level2");
                    currentLvl = "test.txt";
                }
                else if (count == 2 && levels["level3"] == true)
                {
                    gameState.StartGame("level3.txt", "level3");
                    currentLvl = "level3.txt";
                }
                else if (count == 3 && levels["level4"] == true)
                {
                    gameState.StartGame("level4.txt", "level4");
                    currentLvl = "level4.txt";
                }
                else if (count == 4 && levels["level5"] == true)
                {
                    gameState.StartGame("level5.txt", "level5");
                    currentLvl = "level5.txt";
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
            spriteBatch.Draw(back, new Rectangle(50, 60, 150, 80), Color.White);
            //spriteBatch.DrawString(font1, "This is the Level Selection Screen", new Vector2(50f, 50f), Color.Black);
            //level 1
            if (count == 0)
            {
                //spriteBatch.DrawString(font1, "Level 1", new Vector2(game.screenWidth / 2 - 250, 400f), Color.Gold);
                spriteBatch.Draw(lvl1, new Rectangle(200, 400, 100, 100), Color.White);
            }
            else
            {
                spriteBatch.Draw(lvl1, new Rectangle(200, 400, 100, 100), Color.Gold);
            }
            //level 2
            if (count == 1 && levels["level2"] == true)
            {
                spriteBatch.Draw(lvl2, new Rectangle(400, 400, 100, 100), Color.White);
            }
            else if (count == 1 && levels["level2"] == false)
            {
                spriteBatch.Draw(lvl2, new Rectangle(400, 400, 100, 100), Color.Gray);
            }
            else if(count != 1)
            {
                spriteBatch.Draw(lvl2, new Rectangle(400, 400, 100, 100), Color.Gold);
            }
            // level 3
            if (count == 2 && levels["level3"] == true)
            {
                spriteBatch.Draw(lvl3, new Rectangle(600, 400, 100, 100), Color.White);
            }
            else if (count == 2 && levels["level3"] == false)
            {
                spriteBatch.Draw(lvl3, new Rectangle(600, 400, 100, 100), Color.Gray);
            }
            else
            {
                spriteBatch.Draw(lvl3, new Rectangle(600, 400, 100, 100), Color.Gold);
            }
            // level 4
            if (count == 3 && levels["level4"] == true)
            {
                spriteBatch.Draw(lvl4, new Rectangle(800, 400, 100, 100), Color.White);
            }
            else if (count == 3 && levels["level4"] == false)
            {
                spriteBatch.Draw(lvl4, new Rectangle(800, 400, 100, 100), Color.Gray);
            }
            else
            {
                spriteBatch.Draw(lvl4, new Rectangle(800, 400, 100, 100), Color.Gold);
            }
            // level 5
            if (count == 4 && levels["level5"] == true)
            {
                spriteBatch.Draw(lvl5, new Rectangle(1000, 400, 100, 100), Color.White);
            }
            else if (count == 4 && levels["level5"] == false)
            {
                spriteBatch.Draw(lvl5, new Rectangle(1000, 400, 100, 100), Color.Gray);
            }
            else
            {
                spriteBatch.Draw(lvl5, new Rectangle(1000, 400, 100, 100), Color.Gold);
            }
        }

       


        #endregion Methods

        public string CurrentLevel
        {
            get { return currentLvl; }
            set { currentLvl = value; }
        }
        public string NextLevel
        {
            get { return nextLvl; }
            set { nextLvl = value; }
        }
    }
}
