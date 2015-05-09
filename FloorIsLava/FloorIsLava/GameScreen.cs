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
        public Platform block;
        private Game1 game;
        private SpriteFont font1;
        private KeyboardState lastState;
        private GameState gameState;
        public Player player;
        public Gold gem;
        private List<GameObject> drawList;
        private Goal endGoal;
        private string levelName = "test2.txt";
        private List<GameObject> grappleableObjectList;
        private Enemy enemy;
        public EnemyPathEnd epe;
        public List<Enemy> enemyList;
        private int timeSinceLastMove;

        // these are for a later update
        private string level;
        private int highScore;
        private double bestTime;

        private int score;

        private int gameWidth;
        private int gameHeight;

        private List<Rectangle> colList;
        public List<Platform> platformList;
        public List<EnemyPathEnd> enemyPathList;
        private Rectangle lavaRect;
        public List<Gold> gemsList;
        #endregion Attributes

        #region Properties
        // properties
        public List<GameObject> GrappleableObjectList
        {
            get { return grappleableObjectList; }
            set { grappleableObjectList = value; }
        }

        public int Score
        {
            set { score += value; }
        }
        #endregion Properties

        #region Constructor
        //basic game screen constructor
        public GameScreen(Game1 game)
        {
            this.game = game;
            gameState = new GameState(game); //creates new gameState object and assigns it to game screen
            font1 = game.Content.Load<SpriteFont>("Font1"); //loads Font1

            drawList = new List<GameObject>();
            enemyList = new List<Enemy>();
            timeSinceLastMove = 0;
            enemyPathList = new List<EnemyPathEnd>();
            colList = new List<Rectangle>();
            gemsList = new List<Gold>();
            platformList = new List<Platform>();
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
            //int yPos = game.screenHeight / gameHeight;

            grappleableObjectList = new List<GameObject>();
            int y = -gameHeight + 8;
            while ((text = input.ReadLine()) != null)
            {

                int x = 0;
                string[] gamePiece = text.Split();
                foreach (string piece in gamePiece)
                {
                    if (piece == "w")
                    {
                        Platform block = new Platform(game.wallSprite, xPos * x + x, xPos * y + y, xPos, xPos);
                        drawList.Add(block);
                        colList.Add(block.rect);
                        platformList.Add(block);
                        grappleableObjectList.Add(block);
                    }
                    else if (piece == "c")
                    {
                        player = new Player(game.playerSprite, xPos * x + x, xPos * y + y, game.playerSprite.Width * 4, game.playerSprite.Height * 4, colList, game, this, enemy, block);
                    }
                    else if (piece == "f")
                    {
                        endGoal = new Goal(game.goalSprite, xPos * x + x, xPos * y + y, xPos, xPos, game);
                    }
                    else if (piece == "g")
                    {
                        gem = new Gold(game.gemSprite, xPos * x + 25, xPos * y + 35, game.gemSprite.Width, game.gemSprite.Height, this);
                        gemsList.Add(gem);
                    }
                    else if (piece == "e")
                    {
                        enemy = new Enemy(game.enemySprite, xPos * x, xPos * y + y, game.enemySprite.Width, game.enemySprite.Height, game, this, true);
                        enemyList.Add(enemy);
                    }
                    else if (piece == "t")
                    {
                        epe = new EnemyPathEnd(xPos * x + x, xPos * y + y, game.wallSprite.Width, 1, true, game);
                        enemyPathList.Add(epe);
                    }

                    // will add code later for all the other objects that are going to be shown

                    x++;
                }
                y++;
            }
            input.Close();
            input.Close();
            y--;
            lavaRect = new Rectangle(0, game.screenHeight - game.lavaBack.Height, game.screenWidth, game.lavaBack.Height);
        } // is not updated to current
        //gamescreen constructor that takes specific level
        public GameScreen(Game1 game, string lvlfile)
        {
            this.game = game;
            gameState = new GameState(game); //creates new gameState object and assigns it to game screen
            font1 = game.Content.Load<SpriteFont>("Font1"); //loads Font1
            levelName = lvlfile;
            enemyList = new List<Enemy>();
            drawList = new List<GameObject>();
            timeSinceLastMove = 0;
            colList = new List<Rectangle>();
            gemsList = new List<Gold>();
            enemyPathList = new List<EnemyPathEnd>();
            platformList = new List<Platform>();
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
            //int yPos = game.screenHeight / gameHeight;

            grappleableObjectList = new List<GameObject>();
            int y = -gameHeight + 8;;
            while ((text = input.ReadLine()) != null)
            {

                int x = 0;
                string[] gamePiece = text.Split();
                foreach (string piece in gamePiece)
                {
                    if (piece == "w")
                    {
                        Platform block = new Platform(game.wallSprite, xPos * x + x, xPos * y + y, xPos + 1, xPos + 1);
                        drawList.Add(block);
                        colList.Add(block.rect);
                        platformList.Add(block);
                        grappleableObjectList.Add(block);
                    }
                    else if (piece == "c")
                    {
                        player = new Player(game.playerSprite, xPos * x + x, xPos * y + y, game.playerSprite.Width * 4, game.playerSprite.Height * 4, colList, game, this, enemy, block);
                    }
                    else if (piece == "f")
                    {
                        endGoal = new Goal(game.goalSprite, xPos * x + x, xPos * y + y, xPos, xPos, game);
                    }
                    else if (piece == "g")
                    {
                        gem = new Gold(game.gemSprite, xPos * x + 25, xPos * y + 35, game.gemSprite.Width, game.gemSprite.Height, this);
                        gemsList.Add(gem);
                    }
                    else if (piece == "e")
                    {
                        enemy = new Enemy(game.enemySprite, xPos * x, xPos * y + y, game.enemySprite.Width, game.enemySprite.Height, game, this, true);
                        enemyList.Add(enemy);
                    }
                    else if (piece == "t")
                    {
                        epe = new EnemyPathEnd(xPos * x + x, xPos * y + y, game.wallSprite.Width, 1, true, game);
                        enemyPathList.Add(epe);
                    }

                    // will add code later for all the other objects that are going to be shown
                    x++;
                }
                y++;
            }
            input.Close();
            y--;
            lavaRect = new Rectangle(0, game.screenHeight - game.lavaBack.Height, game.screenWidth, game.lavaBack.Height);
        }

        #endregion Constructor  

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

        #region Update
        public void Update(GameTime gt)
        {
            endGoal.isColliding(player.PlayerRect);
            GameTime gameTime = gt; // takes gametime object and assigns it to gametime variable
            player.Update(gameTime);
            foreach (Gold g in gemsList) // getting the score
                g.CollisionCheck(player.PlayerRect);
            foreach (Enemy e in enemyList)
                e.Update(gameTime);
            KeyboardState keyBoardState = Keyboard.GetState(); //create a keyboard state variable to hold current keyboard state
            if (keyBoardState.IsKeyDown(Keys.P) && lastState.IsKeyDown(Keys.P))
            {
                gameState.PauseGame(game, this);
            }
            if (keyBoardState.IsKeyDown(Keys.G) && lastState.IsKeyDown(Keys.G))
            {
                gameState.EndGame(levelName);
            }
            /*if (keyBoardState.IsKeyDown(Keys.K) && lastState.IsKeyUp(Keys.K))
            {
                MoveDown(5);
            }
            if (keyBoardState.IsKeyDown(Keys.I) && lastState.IsKeyUp(Keys.I))
            {
                MoveUp(-5);
            }*/
            this.MoveScreen(gameTime);

            /*if (player.PlayerRect.Y <= 20)
            {
                MoveDown((game.screenHeight/2));
            }*/

            if (player.PlayerRect.Y >= (game.screenHeight ))
            {
                gameState.EndGame(levelName);
            }
            
            lastState = keyBoardState; // assigns current keyboard state to the last keyboard state

            
        }
        #endregion Update

        #region Draw
        public void Draw(SpriteBatch sprBatch,Texture2D background)
        {
            SpriteBatch spriteBatch = sprBatch;
            spriteBatch.Draw(background, new Rectangle(0, 0, game.screenWidth, game.screenHeight), Color.SlateGray);
            spriteBatch.Draw(game.lavaBack, lavaRect, Color.White);
            //spriteBatch.DrawString(font1, "This is the Game Screen", new Vector2(50f, 50f), Color.Red);
            endGoal.Draw(spriteBatch);
            player.Draw(spriteBatch);
            foreach (Gold g in gemsList)
                g.Draw(spriteBatch);
            foreach (Platform b in drawList)
                b.Draw(spriteBatch);
            foreach (Enemy e in enemyList)
                e.Draw(spriteBatch);
            //spriteBatch.DrawString(font1, "Level Name: " + levelName, new Vector2(100f, 70f), Color.Red);
            spriteBatch.DrawString(font1, "High Score: " + highScore, new Vector2(100f, 90f), Color.Red);
            //spriteBatch.DrawString(font1, "Best Time: " + bestTime, new Vector2(100f, 110f), Color.Red);
            spriteBatch.DrawString(font1, "Score: " + score, new Vector2(100f, 110f), Color.Red);
            spriteBatch.Draw(game.lavaFront, lavaRect, Color.White);
        }
        #endregion Draw

        #region Methods
        //MoveScreen Down Method
        public void MoveDown(int y)
        {
            colList = new List<Rectangle>();
            endGoal.PostionChange(0, y);
            foreach (Platform b in drawList)
            {
                b.PostionChange(0, y);
                colList.Add(b.rect);
            }
            foreach (Gold g in gemsList)
            {
                g.MoveDown(y);
            }
            if(enemy != null)
            {
                enemy.MyBullet.MoveDown(y);
            }
            foreach (EnemyPathEnd epe in enemyPathList)
            {
                epe.MoveDown(y);
            }
            foreach (Enemy e in enemyList)
            {
                e.MoveDown(y);
            }
            player.MoveDown(y);
            player.CollisionsToCheck = colList;
        }
        
        //MoveScreen UP Method
        public void MoveUp(int y)
        {
            colList = new List<Rectangle>();
            endGoal.PostionChange(0, y);
            foreach (Platform b in drawList)
            {
                b.PostionChange(0, y);
                colList.Add(b.rect);
            }
            foreach (Enemy e in enemyList)
            {
                e.MoveUp(y);
            }
            player.MoveUp(y);
            player.CollisionsToCheck = colList;
        }


        public void MoveScreen(GameTime gt)
        {
            GameTime gameTime = gt;
            timeSinceLastMove += gameTime.ElapsedGameTime.Milliseconds;

            if(timeSinceLastMove >= 5)
            {
                timeSinceLastMove = 0;
                this.MoveDown(1);
            }
        }
        #endregion Methods
    }
}
