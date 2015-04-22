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
    /* this is the game screen Class any thing here will be displayed in the 
     * game
     * */

    public class GameScreen
    {
        #region Attributes
        //attributes for gamescreen
        private Game1 game;
        private SpriteFont font1;
        private KeyboardState lastState;
        private GameState gameState;
        public Player player;
        private List<GameObject> drawList;
        private Goal endGoal;
        private string levelName = "test.txt";
        private List<GameObject> grappleableObjectList;
        private List<Enemy> enemyList;

        // these are for a later update
        private string level;
        private int highScore;
        private double bestTime;

        private int gameWidth;
        private int gameHeight;

        private List<Rectangle> colList;
        #endregion Attributes

        #region Properties
        // properties
        #endregion Properties

        #region Constructor
        //basic game screen constructor
        public GameScreen(Game1 game)
        {
            this.game = game;
            gameState = new GameState(game); //creates new gameState object and assigns it to game screen
            font1 = game.Content.Load<SpriteFont>("Font1"); //loads Font1

            drawList = new List<GameObject>();

            colList = new List<Rectangle>();
            // reading in the file
            StreamReader input = null;

            input = new StreamReader(levelName); // this will load in whatever level the player picks
            string text = "";
            level = input.ReadLine(); // brings in the level name
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
                foreach (string piece in gamePiece)
                {
                    if (piece == "w")
                    {
                        Platform block = new Platform(game.wallSprite, xPos * x + x, yPos * y + y, xPos, yPos);
                        drawList.Add(block);
                        colList.Add(block.rect);
                    }
                    else if (piece == "c")
                    {
                        player = new Player(game.playerSprite, xPos * x + x, yPos * y + y, game.playerSprite.Width * 4, game.playerSprite.Height * 4, colList, game, this);
                    }
                    else if (piece == "f")
                    {
                        endGoal = new Goal(game.goalSprite, xPos * x + x, yPos * y + y, xPos, yPos);
                    }
                    else if (piece == "e")
                    {
                        enemyList.Add(new Enemy());
                        drawList.Add(enemyList[enemyList.Count - 1]);
                    }

                    // will add code later for all the other objects that are going to be shown
                    x++;
                }
                y++;
            }
            input.Close();
        } // is not updated to current
        //gamescreen constructor that takes specific level
        public GameScreen(Game1 game, string lvlfile)
        {
            this.game = game;
            gameState = new GameState(game); //creates new gameState object and assigns it to game screen
            font1 = game.Content.Load<SpriteFont>("Font1"); //loads Font1
            levelName = lvlfile;
            
            drawList = new List<GameObject>();
            
            colList = new List<Rectangle>();
            // reading in the file
            StreamReader input = null;

            input = new StreamReader(levelName); // this will load in whatever level the player picks
            string text = "";
            level = input.ReadLine(); // brings in the level name
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
                foreach (string piece in gamePiece)
                {
                    if (piece == "w")
                    {
                        Platform block = new Platform(game.wallSprite, xPos * x + x, yPos * y + y, xPos, yPos);
                        drawList.Add(block);
                        colList.Add(block.rect);
                        grappleableObjectList.Add(block);
                    }
                    else if (piece == "c")
                    {
                        player = new Player(game.playerSprite, xPos * x + x, yPos * y + y, game.playerSprite.Width * 4, game.playerSprite.Height * 4, colList, game, this);
                    }
                    else if (piece == "f")
                    {
                        endGoal = new Goal(game.goalSprite, xPos * x + x, yPos * y + y, xPos, yPos);
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
            GameTime gameTime = gt; // takes gametime object and assigns it to gametime variable
            player.Update(gameTime);
            KeyboardState keyBoardState = Keyboard.GetState(); //create a keyboard state variable to hold current keyboard state
            if (keyBoardState.IsKeyDown(Keys.P) && lastState.IsKeyDown(Keys.P))
            {
                gameState.PauseGame(game, this);
            }

            lastState = keyBoardState; // assigns current keyboard state to the last keyboard state
        }
        #endregion Update

        #region Draw
        public void Draw(SpriteBatch sprBatch,Texture2D background)
        {
            SpriteBatch spriteBatch = sprBatch;
            spriteBatch.Draw(background, new Rectangle(0, 0, game.screenWidth, game.screenHeight), Color.SlateGray);
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

        #region Properties
        // this will make the correct level load in
        public string LevelName
        {
            set
            {
                levelName = value;
            }
            get
            {
                return levelName;
            }
        }
        #endregion properties



    }
}
