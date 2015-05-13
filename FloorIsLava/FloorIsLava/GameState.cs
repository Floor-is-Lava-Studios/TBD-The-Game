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
    /*
     *Tid class will manage the game states and the methods required 
     *to successfully switch between them
     */
    #region GameStates
    public enum Screen // sets up the Finite States for each Screen
    {
        StartScreen, //state for the Start Screen
        GameScreen, //state for the Game Screen
        CreditScreen, //state for Credit Screen
        OptionScreen, //state for Option Screen
        LevelScreen, //state for the Level Screen
        InstructionScreen, //state for the instruction Screen
        PauseScreen, //state for PauseScreen
        EndLevelScreen //state for EndLevel Screen
    }
    #endregion GameStates

    class GameState
    {
        #region Attributes
        //Attributes
        private Game1 game;

        // static attributes for each screen's class 
        private static StartScreen startScreen;
        private static GameScreen gameScreen;
        private static LevelScreen levelScreen;
        private static CreditScreen creditScreen;
        private static InstructionScreen instructionScreen;
        private static OptionScreen optionScreen;
        private static PauseScreen pauseScreen;
        private static EndLevelScreen endLevelScreen;

        private static Screen currentScreen; // attribute that controls which screen state is currently being displayed
        #endregion Attributes

        #region Constructors
        //Constructors
        public GameState(Game1 game)
        {
            this.game = game;
            currentScreen = Screen.StartScreen; //current screen set to StartScreen State
        }
        #endregion Constructors

        #region Properties
        //property for the currentScreen attribute
        public Screen CurrentScreen
        {
            get { return currentScreen; }
            set
            {
                currentScreen = value;
            }
        }
        //GameScreen Property
        public GameScreen GameScreen
        {
            get { return gameScreen; }
            set
            {
                gameScreen = value;
            }
        }
        //StartScreen Property
        public StartScreen StartScreen
        {
            get { return startScreen; }
            set
            {
                startScreen = value;
            }
        }
        //Instruction Screen Property
        public InstructionScreen InstructionScreen
        {
            get { return instructionScreen; }
            set
            {
                instructionScreen = value;
            }
        }
        //Option Screen Property
        public OptionScreen OptionScreen
        {
            get { return optionScreen; }
            set
            {
                optionScreen = value;
            }
        }
        //Credit Screen Property
        public CreditScreen CreditScreen
        {
            get { return creditScreen; }
            set
            {
                creditScreen = value;
            }
        }
        //Level Screen Property
        public LevelScreen LevelScreen
        {
            get { return levelScreen; }
            set
            {
                levelScreen = value;
            }
        }
        //Pause Screen Property
        public PauseScreen PauseScreen
        {
            get { return pauseScreen; }
            set
            {
                pauseScreen = value;
            }
        }

        //EndLevel Screen Property
        public EndLevelScreen EndLevelScreen
        {
            get { return endLevelScreen; }
            set
            {
                endLevelScreen = value;
            }
        }

        #endregion Properties

        #region Methods
        //Methods
        public void StartGame() //starts a new game and switch screen to game screen
        {
            string lvlName = "level1.txt";
            gameScreen = new GameScreen(game, lvlName, "level1"); // makes new gamescreen
            currentScreen = Screen.GameScreen; // sets current screen to game screen
        }

        public void StartGame(string lvlFile, string lvlName) //starts a new game using specific lvl and switch screen to game screen
        {
            gameScreen = new GameScreen(game, lvlFile, lvlName); // makes new gamescreen
            currentScreen = Screen.GameScreen; // sets current screen to game screen
        }

        public void SwitchInstruct(Game1 game) // switched screen to instruction screen
        {
            instructionScreen = new InstructionScreen(game); // makes new instruction screen
            currentScreen = Screen.InstructionScreen; // sets current screen to instruction screen
        }

        public void SwitchOption(Game1 game) // switched screen to Option screen
        {
            optionScreen = new OptionScreen(game); // makes new option screen
            currentScreen = Screen.OptionScreen; // sets current screen to option screen
        }

        public void SwitchCredit(Game1 game) // switched screen to Credit screen
        {
            creditScreen = new CreditScreen(game); //makes new credit screen
            currentScreen = Screen.CreditScreen; // sets current screen to credit screen
        }

        public void SwitchLevel(Game1 game) // switched screen to Level screen
        {
            levelScreen = new LevelScreen(game); //makes new level screen
            currentScreen = Screen.LevelScreen; // sets current screen to level screen
        }

        public void PauseGame(Game1 game1, GameScreen currentGame) //pauses game
        {
            pauseScreen = new PauseScreen(game1, currentGame);
            currentScreen = Screen.PauseScreen;
        }

        public void ResumeGame(GameScreen currentGame) //resumes game when it is paused
        {
            gameScreen = currentGame;
            currentScreen = Screen.GameScreen;
            pauseScreen = null;
        }

        public void SwitchPause() //switch to pause Screen 
        {
            currentScreen = Screen.PauseScreen;
        }

        public void EndGame(string level)
        {
            string currentLvl = gameScreen.LevelFile;
            string levelName = gameScreen.LevelName;
            gameScreen = null;
            endLevelScreen = new EndLevelScreen(game,  level, currentLvl, levelName);
            currentScreen = Screen.EndLevelScreen;
        }
        #endregion Methods
    }
}
