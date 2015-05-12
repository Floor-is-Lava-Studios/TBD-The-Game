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
     * The Bullet class will inherit from the MoveableGameObject class and will only move 
     * in the direction it is facing until it collides with another entity.
     */
    public class Bullet
    {
        #region Attributes
        //Attribute for when the bullet is visible
        public bool isVisible;
        public Rectangle bullet;
        Texture2D image;
        private int timeTillFire;
        private Enemy enemy;
        private Game game;
        private GameScreen gameScreen;
        #endregion Attributes

        #region Properties
        public int TimeTillFire
        {
            get
            {
                return timeTillFire;
            }

            set
            {
                timeTillFire = value;
            }
        }
        #endregion Properties

        #region Constructor
        public Bullet(Texture2D image, int x, int y, int width, int height, Enemy enemy, Game game, GameScreen gameScreen)
        {
            //Setting isVisible to true. It won't be false until it moves off the screen
            isVisible = true;
            bullet = new Rectangle(x, y, width, height);
            this.image = image;
            this.enemy = enemy;
            this.game = game;
            this.gameScreen = gameScreen;
            timeTillFire = 200;
        }
        #endregion Constructor

        #region Update
        //Update method for the bullet
        public void Update(GameTime gameTime)
        {
            timeTillFire--;

            if (enemy.EnemyRect.X > game.GraphicsDevice.Viewport.Width / 2 && isVisible)
            {
                bullet.X = bullet.X - 5;
            }
            else if (enemy.EnemyRect.X < game.GraphicsDevice.Viewport.Width / 2 && isVisible)
            {
                bullet.X = bullet.X + 5;
            }
        }
        #endregion Update

        #region CollisionCheck
        //Method for checking collision
        public bool CollisionCheck(Rectangle rect)
        {
            //Checking for collision, making bullet dissapear if it intersects
            if (bullet.Intersects(rect))
            {
                isVisible = false;
                return true;
            }
            return false;
        }
        #endregion CollisionCheck

        #region Draw
        //Draw method for the bullet
        public void Draw(SpriteBatch spriteBatch)
        {
            if ((isVisible == true) && (enemy.EnemyRect.X > game.GraphicsDevice.Viewport.Width / 2))
            {
                spriteBatch.Draw(image, bullet, Color.White);
            }
            else if ((isVisible == true) && (enemy.EnemyRect.X < game.GraphicsDevice.Viewport.Width / 2))   //If enemy is on left hand side of the screen, flip bullet image
            {
                spriteBatch.Draw(image, bullet, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            }
        }
        #endregion Draw

        #region MoveDown
        //Move down method
        internal void MoveDown(int y)
        {
            bullet.Y += y;
        }
        #endregion MoveDown
    }
}