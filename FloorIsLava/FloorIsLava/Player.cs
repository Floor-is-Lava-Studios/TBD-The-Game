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
     * The player class will inherit from the MoveableGameObject class.  It will be controlled 
     * by the player’s input from the keyboard/gamepad.  It will also have an attribute for 
     * the grappling hook that will tell if the grappling hook is attached to ceiling and an 
     * attribute for how much gold the player has collected.
     */
    /// <summary>
    /// The object that the player controls.
    /// </summary>
    public class Player                    // ****NOTE: IS NOT CURRENTLY INHERITING FROM ANYTHING. RUNS INDEPENDENTLY. THIS WILL CHANGE IN A FUTURE UPDATE****
    {
        #region Constants
        // constants
        const string ASSET_NAME = "Player";
        const int MAX_SPEED = 10;
        const int ACCELERATION = 1;
        const int GRAVITY = -1;
        const int JUMP_STRENGTH = 20;
        const int MAX_JUMPS = 2;
        Vector2 RIGHT = new Vector2(1, 0);
        Vector2 LEFT = new Vector2(-1, 0);
        #endregion Constants

        #region Attributes
        // attributes
        private Rectangle playerRect;
        private Texture2D playerTexture;

        private Texture2D backwards;
        private int width;
        private int height;

        public enum State { Walking, Still, Grappled, Stopping }
        private State currentState;
        private Vector2 position;
        private Vector2 velocity;
        private KeyboardState oldState;
        private int direction;
        private bool hasJumped;
        private Vector2 jumpStartPosition;
        private bool newPress;
        private int jumpNumber;
        Rectangle nextPlayerRect;
        List<Rectangle> collisionsToCheck;
        int xPostion;
        int yPostion;
        private GameScreen gameScreen;
        #endregion Attributes

        #region Constructor
        // constructor
        /// <summary>
        /// Sets up the default values.
        /// </summary>
        /// <param name="plrTx"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="wid"></param>
        /// <param name="hgt"></param>
        public Player(Texture2D plrTx, int x, int y, int wid, int hgt, List<Rectangle> colls, Game1 game, GameScreen gS)
        {
            oldState = Keyboard.GetState();
            playerTexture = plrTx;
            width = wid;
            height = hgt;
            playerRect = new Rectangle(x, y, width, height);
            xPostion = x;
            yPostion = y;
            position = new Vector2(x, y);
            velocity = new Vector2();
            hasJumped = false;
            currentState = State.Still;
            collisionsToCheck = colls;
            backwards = game.playerSprite_Backwards;
            gameScreen = gS;
            
        }
        #endregion Constructor

        #region Properties
        //Collision to check Property
        public List<Rectangle> CollisionsToCheck
        {
            get { return collisionsToCheck; }
            set
            {
                collisionsToCheck = value;
            }
        }
        //XPostion Property
        public int X 
        {
            get { return xPostion; }
            set
            {
                xPostion = value;
            }
        }

        //y Postion
        public int Y
        {
            get { return yPostion; }
            set
            {
                yPostion = value;
            }
        }

        //Player rect Property
        public Rectangle PlayerRect
        {
            get { return playerRect; }
            set 
            {
                playerRect = value;
            }
        }
        #endregion Properties
        #region Methods
        public void DetectCollisions()
        {
            nextPlayerRect = new Rectangle((int)(position.X + velocity.X), (int)position.Y, width, height);
            foreach (Rectangle r in collisionsToCheck)
            {
                if (nextPlayerRect.Intersects(r))
                {
                    if (velocity.X > 0)
                        position.X = r.Left - width;
                    else if (velocity.X < 0)
                        position.X = r.Right;
                    velocity.X = 0;
                }
            }
            nextPlayerRect = new Rectangle((int)position.X, (int)(position.Y + velocity.Y), width, height);
            foreach (Rectangle r in collisionsToCheck)
            {
                if (nextPlayerRect.Intersects(r))
                {
                    if (velocity.Y < 0)
                        position.Y = r.Bottom;
                    else if (velocity.Y > 0)
                    {
                        position.Y = r.Top - height;
                        hasJumped = false;
                    }
                    velocity.Y = 0;
                }
            }
        }

        /// <summary>
        /// Updates the vertical velocity and handles the jump/double jumps.
        /// </summary>
        /// <param name="newPress"></param>
        private void UpdateJump(bool newPress)
        {
            if (!hasJumped && newPress)     // code that executes when a jump is performed
            {
                jumpStartPosition = position;
                velocity.Y = -JUMP_STRENGTH;
                jumpNumber++;
                if (jumpNumber >= MAX_JUMPS)    // stops it from executing when the maximum amount of midair jumps are performed
                {
                    jumpNumber = 0;
                    hasJumped = true;
                }
            }
        }

        /// <summary>
        /// Handles the controls for the player (jumping, movement, grappling)
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();   // gets keyboard state
            if (currentState != State.Grappled)             // sets state and direction (if player is not grappled)
            {
                if(direction == 0)
                {
                    direction = 1;
                }
                if (currentState != State.Still && newState.IsKeyUp(Keys.A) && newState.IsKeyUp(Keys.D))
                    currentState = State.Stopping;
                if (newState.IsKeyDown(Keys.A) && newState.IsKeyUp(Keys.D))
                {
                    currentState = State.Walking;
                    direction = -1;
                }
                if (newState.IsKeyDown(Keys.D) && newState.IsKeyUp(Keys.A))
                {
                    currentState = State.Walking;
                    direction = 1;
                }
                if (newState.IsKeyDown(Keys.A) && newState.IsKeyDown(Keys.D))
                {
                    if (oldState.IsKeyUp(Keys.A))
                        direction = -1;
                    if (oldState.IsKeyUp(Keys.D))
                        direction = 1;
                    currentState = State.Walking;
                }
            }
            else                                            // handles direction and movement when grappled
            {
                // code when grappled
            }


            if (newState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))     // handles jumping
            {
                newPress = true;
            }
            else
            {
                newPress = false;
            }
            this.UpdateJump(newPress);


            switch (currentState)                           // master switch statement for the various states
            {
                case State.Still:
                    {
                        break;
                    }
                case State.Walking:
                    {
                        if (direction == 1)
                        {
                            if (velocity.X < MAX_SPEED)
                            {
                                velocity += RIGHT * ACCELERATION;
                            }
                        }
                        else if (direction == -1)
                        {
                            if (velocity.X > -1 * MAX_SPEED)
                            {
                                velocity += LEFT * ACCELERATION;
                            }
                        }

                        break;
                    }
                case State.Stopping:
                    {
                        if (velocity.X > 0)
                            velocity -= RIGHT * ACCELERATION;
                        else if (velocity.X < 0)
                            velocity -= LEFT * ACCELERATION;

                        if (velocity.X == 0)
                        {
                            currentState = State.Still;
                        }
                        break;
                    }
                case State.Grappled:
                    {
                        break;
                    }


            }

            velocity.Y -= GRAVITY;

            this.DetectCollisions();       // detects and adjusts for collisions

            position += velocity;           // adds the player's velocity to his current position
            xPostion = (int)position.X;
            yPostion = (int)position.Y;
            playerRect = new Rectangle(xPostion, yPostion, width, height);    // sets the new position
            oldState = newState;            // sets the old keyboard state
        }

        /// <summary>
        /// Draws the player at the specified position (playerRect)
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if(direction == 1)
            {
                spriteBatch.Draw(playerTexture, playerRect, Color.White);
            }
            else
            {
                spriteBatch.Draw(backwards, playerRect, Color.White);
            }
        }
        #endregion Methods
    }
}
