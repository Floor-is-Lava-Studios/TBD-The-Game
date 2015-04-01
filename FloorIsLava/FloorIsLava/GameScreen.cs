﻿using System;
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
        private Goal endGoal;

        // these are for a later update
        private string levelName;
        private int highScore;
        private double bestTime;

        private int gameWidth;
        private int gameHeight;
        #endregion Attributes

        #region Constructor
        public GameScreen(Game1 game)
        {
            this.game = game;
            gameState = new GameState(game);
            font1 = game.Content.Load<SpriteFont>("Font1");
            
            drawList = new List<GameObject>();
            
            List<Rectangle> colList = new List<Rectangle>();
 //        Random rand = new Random();
 //
 //        player = new Player(game.playerSprite, 0, 0, game.playerSprite.Width, game.playerSprite.Height, colList);
 //
 //        for (int i = 0; i < 30; i++)
 //        {
 //            Platform block = new Platform(game.wallSprite, rand.Next(game.screenWidth), rand.Next(game.screenHeight), 150, 150);
 //            drawList.Add(block);
 //            colList.Add(block.rect);
 //        }

            // reading in the file
            StreamReader input = null;

                input = new StreamReader("level1.txt"); // this will load in whatever level the player picks
                string text = "";
                levelName = input.ReadLine(); // brings in the level name
                text = input.ReadLine(); // brings in the high score as a string
                int.TryParse(text, out highScore); // now its an int
                text = input.ReadLine(); // brings in the best time as a string
                double.TryParse(text, out bestTime); // now its a double

                text = input.ReadLine(); // read in width
                int.TryParse(text, out gameWidth);

                text = input.ReadLine(); // read in height
                int.TryParse(text, out gameHeight);

                int xPos = game.screenWidth / gameWidth;
                int yPos = game.screenHeight / gameHeight;
                
                int y = 0;
                while ((text = input.ReadLine()) != null)
                {

                    int x = 0;
                    string[] gamePiece = text.Split();
                    foreach(string piece in gamePiece)
                    {
                        if(piece == "w")
                        {
                            Platform block = new Platform(game.wallSprite, xPos * x + x, yPos * y + y, xPos, yPos);
                            drawList.Add(block);
                            colList.Add(block.rect);
                        }
                        else if(piece == "c")
                        {
                            player = new Player(game.playerSprite, xPos * x + x , yPos * y + y, xPos, yPos, colList);
                        }
                        else if(piece == "f")
                        {
                            endGoal = new Goal(game.goalSprite,xPos * x + x, yPos * y + y, xPos, yPos);
                        }

                        // will add code later for all the other objects that are going to be shown
                        x++;
                    }
                    y++;
                }
                input.Close();
            }

        #endregion Constructor

        #region Update
        public void Update(GameTime gt)
        {
            GameTime gameTime = gt;
            player.Update(gameTime);
            KeyboardState keyBoardState = Keyboard.GetState();
            if (keyBoardState.IsKeyDown(Keys.M) && lastState.IsKeyDown(Keys.M))
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
        public void Draw(SpriteBatch sprBatch)
        {
            SpriteBatch spriteBatch = sprBatch;
            //spriteBatch.DrawString(font1, "This is the Game Screen", new Vector2(50f, 50f), Color.Red);
            endGoal.Draw(spriteBatch);
            player.Draw(spriteBatch);
            foreach (Platform b in drawList)
                b.Draw(spriteBatch);
            spriteBatch.DrawString(font1, "Level Name: " + levelName, new Vector2(100f, 70f), Color.Red);
            spriteBatch.DrawString(font1, "High Score: " + highScore, new Vector2(100f, 90f), Color.Red);
            spriteBatch.DrawString(font1, "Best Time: " + bestTime, new Vector2(100f, 110f), Color.Red);
        }
        #endregion Draw

    }
}
