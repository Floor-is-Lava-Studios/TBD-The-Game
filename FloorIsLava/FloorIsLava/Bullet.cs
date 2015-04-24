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
    class Bullet
    {
        //Attribute for when the bullet is visible
        public bool isVisible;
        public Rectangle bullet;
        Texture2D bulletImage;

        public Bullet(Texture2D image, int x, int y, int width, int height)
        {
            //Setting isVisible to true. It won't be false until it moves off the screen
            isVisible = true;
            bullet = new Rectangle(x, y, width, height);
            bulletImage = image;
        }

        //Update method for the bullet
        public void Update(GameTime gameTime)
        {
            bullet.X = bullet.X - 5;

            //Moving the bullet across the screen
            if (bullet.X == 0)
            {
                bullet.X = bullet.X + 5;
            }
            else
            {
                bullet.X = bullet.X - 5;
            }
        }

        //Method for checking collision
        public void CollisionCheck(Rectangle rect)
        {
            //Checking for collision
            if (bullet.Intersects(rect))
            {
                isVisible = false;
            }
        }

        //Draw method for the bullet
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible == true)
            {
                spriteBatch.Draw(bulletImage, bullet, Color.White);
            }
        }
    }
}
