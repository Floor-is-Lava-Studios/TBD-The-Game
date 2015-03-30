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
        private KeyboardState lastState;
        private GameState gameState;
        #endregion Attributes

        #region Constructor 
        //Constructor
        public CreditScreen(Game1 game1)
        {
            game = game1;
            gameState = new GameState(game);
            font1 = game.Content.Load<SpriteFont>("Font1");
        }
        #endregion Constructor

        #region Methods
        //Update Method
        public void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
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
                gameState.SwitchOption(game);
            }
            if (keyState.IsKeyDown(Keys.L) && lastState.IsKeyDown(Keys.L))
            {
                gameState.SwitchLevel(game);
            }
            if (keyState.IsKeyDown(Keys.I) && lastState.IsKeyDown(Keys.I))
            {
                gameState.SwitchInstruct(game);
            }
            lastState = keyState;
        }

        //Draw Method
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font1, "Credits", new Vector2(100f, 50f), Color.Blue);
        }
        #endregion Methods
    }
}
