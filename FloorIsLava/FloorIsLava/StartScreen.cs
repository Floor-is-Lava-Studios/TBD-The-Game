#region UsingStatements
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
#endregion UsingStatements

namespace FloorIsLava
{
    /* This is the startScreen Class it will control the start menu 
     * and what will appear on it.
     * */
    class StartScreen
    {
        #region Attributes
        private Texture2D background;
        private SpriteFont font1;
        private Game1 game;
        private KeyboardState lastState;
        private GameState gameState;

        // shit im adding
        private int count = 0;
        #endregion Attributes

        #region Constructor
        public StartScreen(Game1 game1)
        {
            game = game1;
            gameState = new GameState(game);
            lastState = Keyboard.GetState(); 
        }
        #endregion Constructor

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
     //    if (keyboardState.IsKeyDown(Keys.I) && lastState.IsKeyDown(Keys.I))
     //    {
     //        gameState.SwitchInstruct(game);
     //    }
     //    if (keyboardState.IsKeyDown(Keys.G) && lastState.IsKeyDown(Keys.G))
     //    {
     //        gameState.StartGame();
     //    }
     //    if (keyboardState.IsKeyDown(Keys.O) && lastState.IsKeyDown(Keys.O))
     //    {
     //        gameState.SwitchOption(game);
     //    }
     //    if (keyboardState.IsKeyDown(Keys.L) && lastState.IsKeyDown(Keys.L))
     //    {
     //        gameState.SwitchLevel(game);
     //    }
     //    if (keyboardState.IsKeyDown(Keys.C) && lastState.IsKeyDown(Keys.C))
     //    {
     //        gameState.SwitchCredit(game);
     //    }
            if (keyboardState.IsKeyDown(Keys.S) && lastState.IsKeyUp(Keys.S))
            {
                count++;
                if(count > 4)
                {
                    count = 0;
                }
            }
            if (keyboardState.IsKeyDown(Keys.W) && lastState.IsKeyUp(Keys.W))
            {
                count--;
                if (count < 0)
                {
                    count = 4;
                }
            }
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                if (count == 0)
                {
                    gameState.SwitchInstruct(game);
                }
                else if (count == 1)
                {
                    gameState.StartGame();
                }
                else if (count == 2)
                {
                    gameState.SwitchOption(game);
                }
                else if (count == 3)
                {
                    gameState.SwitchLevel(game);
                }
                else if (count == 4)
                {
                    gameState.SwitchCredit(game);
                }
            }
            font1 = game.Content.Load<SpriteFont>("Font1");
            lastState = keyboardState;
        }

        public void Draw(SpriteBatch spritebatch, Texture2D background, Texture2D title)
        {
            if(background != null)
            {
                //spritebatch.Draw(background, new Vector2(0f, 0f), Color.Green);
                spritebatch.Draw(background, new Rectangle(0,0,game.screenWidth,game.screenHeight), Color.White);
                spritebatch.Draw(title, new Rectangle(200, 100, 1000, 512), Color.White);

            }
            //spritebatch.DrawString(font1, "This is start screen", new Vector2(50f, 50f), Color.White);
            //spritebatch.DrawString(font1, "" + gameState.Num, new Vector2(0, 0), Color.White);
            if(count == 0)
            {
                spritebatch.DrawString(font1, "Instruction", new Vector2(650f, 400f), Color.Gold);
            }
            else
            {
                spritebatch.DrawString(font1, "Instruction", new Vector2(650f, 400f), Color.White);
            }
            if(count == 1)
            {
                spritebatch.DrawString(font1, "Game", new Vector2(650f, 420f), Color.Gold);
            }
            else
            {
                spritebatch.DrawString(font1, "Game", new Vector2(650f, 420f), Color.White);
            }
            if(count == 2)
            {
                spritebatch.DrawString(font1, "Options", new Vector2(650f, 440f), Color.Gold);
            }
            else
            {
                spritebatch.DrawString(font1, "Options", new Vector2(650f, 440f), Color.White);
            }
            if(count == 3)
            {
                spritebatch.DrawString(font1, "Level", new Vector2(650f, 460f), Color.Gold);
            }
            else
            {
                spritebatch.DrawString(font1, "Level", new Vector2(650f, 460f), Color.White);
            }
            if(count == 4)
            {
                spritebatch.DrawString(font1, "Credits", new Vector2(650f, 480f), Color.Gold);
            }
            else
            {
                spritebatch.DrawString(font1, "Credits", new Vector2(650f, 480f), Color.White);
            }
        }
    }
}
