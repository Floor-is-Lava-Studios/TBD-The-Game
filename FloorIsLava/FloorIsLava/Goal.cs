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
     * The Goal class will inherit from the GameObject class and will proceed the player to 
     * the next level if it is collided with.
     */
    class Goal : GameObject
    {
        Texture2D goalTexture;
        int xPostion;
        int yPostion;
        int width;
        int height;
        public Goal(Texture2D texture, int x, int y, int width, int height)
        {
            rect = new Rectangle(x, y, width, height);
            xPostion = x;
            yPostion = y;
            this.width = width;
            this.height = height;
            goalTexture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(goalTexture, rect, Color.Gold);
        }

        internal void PostionChange(int x, int y)
        {
            xPostion = xPostion + x;
            yPostion = yPostion + y;
            rect = new Rectangle(xPostion, yPostion, width, height);
        }

        public void isColliding(Player play)
        {
            // checks to see if the player has reached the goal

            //is the player has switch to end level screen
            //also updates the unlocked levels
        }
    }
}
