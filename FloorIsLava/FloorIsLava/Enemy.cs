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
    class Enemy : MoveableGameObject
    {
        //Attributes
        private Rectangle enemy;
        private Texture2D enemyImage;
        private Bullet myBullet;
        private Player player;
        private Game1 game;
        private EnemyPathEnd enemyPath;
        private EnemyPathEnd enemyPath2;
        private GameScreen gameScreen;
        private int timeTillFire;
        private int x;
        private int y;
        private bool top;
        private int width;
        private int height;
        private List<Rectangle> colList;

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

        //Constructor
        public Enemy(Texture2D image, int xpos, int ypos, int wid, int ht, Player plyr, Game1 gm, GameScreen gS, bool tp, List<Rectangle> colLi)
        {            
            enemyImage = image;
            player = plyr;
            game = gm;
            gameScreen = gS;
            x = xpos;
            y = xpos;
            width = wid;
            height = ht;
            top = tp;
            colList = colLi;
            timeTillFire = 200;
            myBullet = new Bullet(game.bulletSprite, x, y, game.bulletSprite.Width / 10, game.bulletSprite.Height / 10);
            enemy = new Rectangle(x, y, width, height);
        }

        //Update method
        public void Update(GameTime gameTime, Player plyr)
        {
            timeTillFire--; //decrementing timeTillFire

            //Setting timeTillFire to 200 again when it reaches 0
            if (timeTillFire == 0)
            {
                myBullet.isVisible = true;
                myBullet.bullet.X = enemy.X + enemy.Height / 3;
                myBullet.bullet.Y = enemy.Y + enemy.Height / 3;
                timeTillFire = 200;
            }

            //Updating the bullet and checking for collision with the bullet
            myBullet.Update(gameTime);
            myBullet.CollisionCheck(plyr.PlayerRect);
            foreach (Rectangle r in colList)
            {
                myBullet.CollisionCheck(r);
            }

            //Top starts as true, so it goes up first
            if (!top)
            {
                y += 3;
            }
            else
            {
                y -= 3;
            }
            
            enemy = new Rectangle(x, y, width, height);

            foreach (EnemyPathEnd epe in gameScreen.enemyPathList)
            {
                if ((top == false) && (enemy.Intersects(epe.enemyPathRect)))
                {
                    top = true;
                }
                else if ((top == true) && (enemy.Intersects(epe.enemyPathRect)))
                {
                    top = false;
                }
            }
        }

        public void MoveDown(int dist)
        {
            y += dist;
        }

        //Draw method
        public void Draw(SpriteBatch spriteBatch)
        {
            myBullet.Draw(spriteBatch);
            spriteBatch.Draw(enemyImage, enemy, Color.White);
        }
    }
}
