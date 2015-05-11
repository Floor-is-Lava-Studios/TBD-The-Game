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
    /*
     * The Enemy class will inherit from the MoveableGameObject and will either be stationary 
     * or be bound between two points, similar to the MoveablePlatform.  It will also have a 
     * bullet object that it will shoot on a timer.
     */
    public class Enemy : MoveableGameObject
    {
        #region Attributes
        //Attributes
        private Rectangle enemyRect;
        private Texture2D enemyImage;
        private Bullet myBullet;
        private Game1 game;
        private GameScreen gameScreen;
        private EnemyPathEnd enemyPathEnd;
        private int x;
        private int y;
        public bool top;
        private int width;
        private int height;
        private List<Bullet> bulletList;
        #endregion Attributes

        #region Properties
        //Properties
        public Bullet MyBullet
        {
            get
            {
                return myBullet;
            }

            set
            {
                myBullet = value;
            }
        }

        public Rectangle EnemyRect
        {
            get
            {
                return enemyRect;
            }
        }
        #endregion Properties

        #region Constructor
        //Constructor
        public Enemy(Texture2D enemyImage, int x, int y, int width, int height, Game1 game, GameScreen gameScreen, bool top, List<Bullet> bulletList)
        {
            enemyRect = new Rectangle(x, y, width, height);
            this.enemyImage = enemyImage;
            this.game = game;
            this.gameScreen = gameScreen;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.top = top;
            this.bulletList = bulletList;
            myBullet = new Bullet(game.bulletSprite, x, y, game.bulletSprite.Width, game.bulletSprite.Height, this, game, gameScreen);
            enemyPathEnd = gameScreen.epe;
        }
        #endregion Constructor

        #region Update
        //Update method
        public void Update(GameTime gameTime)
        {
            //To update the top bool value if needed
            gameScreen.player.DetectCollisions();

            //Updating bullet
            myBullet.Update(gameTime);

            foreach (Bullet b in bulletList)
            {
                //Setting timeTillFire to 300 again when it reaches 0
                if ((b.TimeTillFire == 0) && (b.isVisible == false))
                {
                    b.isVisible = true;
                    b.bullet.X = enemyRect.X + enemyRect.Height / 3;
                    b.bullet.Y = enemyRect.Y + enemyRect.Height / 3;
                    b.TimeTillFire = 200;
                }

                //Checking for collision with the bullet
                foreach (Platform p in gameScreen.platformList)
                {
                    b.CollisionCheck(p.rect);
                }
                if (b.CollisionCheck(gameScreen.player.PlayerRect))
                {
                    gameScreen.player.Stun();
                }
            }

            //If top is false and enemy intersects epe, make top true. If top is true and enemy intersects epe, make top false.
            foreach (EnemyPathEnd epe in gameScreen.enemyPathList)
            {
                if ((top == false) && (enemyRect.Intersects(epe.enemyPathRect)))
                {
                    top = true;
                }
                else if ((top == true) && (enemyRect.Intersects(epe.enemyPathRect)))
                {
                    top = false;
                }
            }

            //Top starts as true, so it goes up first
            if (top == false)
            {
                enemyRect.Y = enemyRect.Y + 3;  //Since screen is moving up, down value must be twice as much in order to even it out
            }
            else if (top == true)
            {
                enemyRect.Y = enemyRect.Y - 3;
            }
        }
        #endregion Update

        #region Draw
        //Draw method
        public void Draw(SpriteBatch spriteBatch)
        {
            //Drawing every bullet in bulletList to screen
            foreach (Bullet b in bulletList)
            {
                b.Draw(spriteBatch);
            }

            //Drawing every enemy in enemyList to screen
            foreach (Enemy e in gameScreen.enemyList)
            {
                //Checking where enemy is to be drawn and drawing the sprite accordingly
                if (enemyRect.X > game.GraphicsDevice.Viewport.Width / 2)
                {
                    spriteBatch.Draw(enemyImage, enemyRect, Color.White);
                }
                else if (enemyRect.X < game.GraphicsDevice.Viewport.Width / 2)    //If the enemy is on the left hand side of the screen, flip image
                {
                    spriteBatch.Draw(enemyImage, enemyRect, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                }
            }
        }
        #endregion Draw

        #region MoveDown
        //Move down method
        internal void MoveDown(int y)
        {
            enemyRect.Y += y;
        }
        #endregion MoveDown

        #region MoveUp
        //Move down method
        internal void MoveUp(int y)
        {
            enemyRect.Y -= y;
        }
        #endregion MoveUp
    }
}
