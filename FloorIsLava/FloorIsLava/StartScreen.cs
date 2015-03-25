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
            if (keyboardState.IsKeyDown(Keys.I) && lastState.IsKeyDown(Keys.I))
            {
                gameState.SwitchInstruct(game);
            }
            if (keyboardState.IsKeyDown(Keys.G) && lastState.IsKeyDown(Keys.G))
            {
                gameState.StartGame();
            }
            if (keyboardState.IsKeyDown(Keys.O) && lastState.IsKeyDown(Keys.O))
            {
                gameState.SwitchOption(game);
            }
            if (keyboardState.IsKeyDown(Keys.L) && lastState.IsKeyDown(Keys.L))
            {
                gameState.SwitchLevel(game);
            }
            if (keyboardState.IsKeyDown(Keys.C) && lastState.IsKeyDown(Keys.C))
            {
                gameState.SwitchCredit(game);
            }
            font1 = game.Content.Load<SpriteFont>("Font1");
            lastState = keyboardState;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if(background != null)
            {
                spritebatch.Draw(background, new Vector2(0f, 0f), Color.Green);
            }
            spritebatch.DrawString(font1, "This is start screen", new Vector2(50f, 50f), Color.White);
            spritebatch.DrawString(font1, "" + gameState.Num, new Vector2(0, 0), Color.White);
        }
    }
}
