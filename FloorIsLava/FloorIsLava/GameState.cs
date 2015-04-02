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
        InstructionScreen //state for the instruction Screen
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

        private static Screen currentScreen; // attribute that controls which screen state is currently being displayed
        private int num;
        #endregion Attributes

        #region Constructors
        //Constructors
        public GameState(Game1 game)
        {
            this.game = game;
            currentScreen = Screen.StartScreen; //current screen set to StartScreen State
            num = 0;
        }
        #endregion Constructors

        #region Properties
        public int Num
        {
            get { return num; }
            set { Num = value; }
        }
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
        #endregion Properties

        #region Methods
        //Methods
        public void StartGame() //starts a new game and switch screen to game screen
        {
            gameScreen = new GameScreen(game); // makes new gamescreen
            currentScreen = Screen.GameScreen; // sets current screen to game screen
            num++;
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
        #endregion Methods
    }
}
