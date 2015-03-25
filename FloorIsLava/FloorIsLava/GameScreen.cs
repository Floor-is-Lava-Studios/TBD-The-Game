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
    /* this is the game screen Class any thing here will be displayed in the 
     * game
     * */

    public class GameScreen
    {
        #region Attributes
        private Game1 game;
        private SpriteFont font1;
        private KeyboardState lastState;
        private GameState gameState;
        #endregion Attributes

        #region Constructor
        public GameScreen(Game1 game)
        {
            this.game = game;
            gameState = new GameState(game);
            font1 = game.Content.Load<SpriteFont>("Font1");
            
        }
        #endregion Constructor

        #region Update
        public void Update(GameTime gametime)
        {
            KeyboardState keyBoardState = Keyboard.GetState();
            if (keyBoardState.IsKeyDown(Keys.S) && lastState.IsKeyDown(Keys.S))
            {
                gameState.CurrentScreen = Screen.StartScreen;
            }
            if (keyBoardState.IsKeyDown(Keys.I) && lastState.IsKeyDown(Keys.I))
            {
                gameState.SwitchInstruct(game);
            }
            if (keyBoardState.IsKeyDown(Keys.O) && lastState.IsKeyDown(Keys.O))
            {
                gameState.SwitchOption(game);
            }
            if (keyBoardState.IsKeyDown(Keys.L) && lastState.IsKeyDown(Keys.L))
            {
                gameState.SwitchLevel(game);
            }
            if (keyBoardState.IsKeyDown(Keys.C) && lastState.IsKeyDown(Keys.C))
            {
                gameState.SwitchCredit(game);
            }
            lastState = keyBoardState;
        }
        #endregion Update

        #region Draw
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(font1, "This is the Game Screen", new Vector2(50f, 50f), Color.Red);
        }
        #endregion Draw

    }
}
