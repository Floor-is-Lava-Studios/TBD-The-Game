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
        public Player player;
        private List<GameObject> drawList;
        #endregion Attributes

        #region Constructor
        public GameScreen(Game1 game)
        {
            this.game = game;
            gameState = new GameState(game);
            font1 = game.Content.Load<SpriteFont>("Font1");
            
            drawList = new List<GameObject>();
            
            List<Rectangle> colList = new List<Rectangle>();
            Random rand = new Random();

            player = new Player(game.playerSprite, 0, 0, game.playerSprite.Width, game.playerSprite.Height, colList);

            for (int i = 0; i < 30; i++)
            {
                Platform block = new Platform(game.wallSprite, rand.Next(game.screenWidth), rand.Next(game.screenHeight), 150, 150);
                drawList.Add(block);
                colList.Add(block.rect);
            }
            
        }
        #endregion Constructor

        #region Update
        public void Update(GameTime gt)
        {
            GameTime gameTime = gt;
            player.Update(gameTime);
            KeyboardState keyBoardState = Keyboard.GetState();
            //if (keyBoardState.IsKeyDown(Keys.S) && lastState.IsKeyDown(Keys.S))
            //{
            //    gameState.CurrentScreen = Screen.StartScreen;
            //}
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
        public void Draw(SpriteBatch sprBatch)
        {
            SpriteBatch spriteBatch = sprBatch;
            spriteBatch.DrawString(font1, "This is the Game Screen", new Vector2(50f, 50f), Color.Red);
            player.Draw(spriteBatch);
            foreach (Platform b in drawList)
                b.Draw(spriteBatch);
        }
        #endregion Draw

    }
}
