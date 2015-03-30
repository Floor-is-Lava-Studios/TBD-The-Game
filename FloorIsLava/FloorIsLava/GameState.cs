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
    public enum Screen
    {
        StartScreen,
        GameScreen,
        CreditScreen,
        OptionScreen,
        LevelScreen,
        InstructionScreen
    }
    #endregion GameStates

    class GameState
    {
        #region Attributes
        //Attributes
        private Game1 game;
        private static StartScreen startScreen;
        private static GameScreen gameScreen;
        private static LevelScreen levelScreen;
        private static CreditScreen creditScreen;
        private static InstructionScreen instructionScreen;
        private static OptionScreen optionScreen;
        private static Screen currentScreen;
        private int num;
        #endregion Attributes

        #region Constructors
        //Constructors
        public GameState(Game1 game)
        {
            this.game = game;
            currentScreen = Screen.StartScreen;
            num = 0;
        }
        #endregion Constructors

        #region Properties
        public int Num
        {
            get { return num; }
            set { Num = value; }
        }
        public Screen CurrentScreen
        {
            get { return currentScreen; }
            set
            {
                currentScreen = value;
            }
        }

        public GameScreen GameScreen
        {
            get { return gameScreen; }
            set
            {
                gameScreen = value;
            }
        }

        public StartScreen StartScreen
        {
            get { return startScreen; }
            set
            {
                startScreen = value;
            }
        }

        public InstructionScreen InstructionScreen
        {
            get { return instructionScreen; }
            set
            {
                instructionScreen = value;
            }
        }

        public OptionScreen OptionScreen
        {
            get { return optionScreen; }
            set
            {
                optionScreen = value;
            }
        }

        public CreditScreen CreditScreen
        {
            get { return creditScreen; }
            set
            {
                creditScreen = value;
            }
        }

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
        public void StartGame()
        {
            gameScreen = new GameScreen(game);
            currentScreen = Screen.GameScreen;
            num++;
        }

        public void SwitchInstruct(Game1 game)
        {
            instructionScreen = new InstructionScreen(game);
            currentScreen = Screen.InstructionScreen;
        }

        public void SwitchOption(Game1 game)
        {
            optionScreen = new OptionScreen(game);
            currentScreen = Screen.OptionScreen;
        }

        public void SwitchCredit(Game1 game)
        {
            creditScreen = new CreditScreen(game);
            currentScreen = Screen.CreditScreen;
        }

        public void SwitchLevel(Game1 game)
        {
            levelScreen = new LevelScreen(game);
            currentScreen = Screen.LevelScreen;
        }
        #endregion Methods
    }
}
