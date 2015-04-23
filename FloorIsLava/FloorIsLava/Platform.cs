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
     * The Platform class will inherit from the GameObject class and will not have any other 
     * attributes because they will not be moving.
     */
    class Platform : GameObject
    {
        Texture2D blockTexture;
        int yPostion;
        int xPostion;
        int width;
        int height;

        public Platform(Texture2D texture, int x, int y, int width, int height)
        {
            rect = new Rectangle(x, y, width, height);
            xPostion = x;
            yPostion = y;
            this.width = width;
            this.height = height;

            blockTexture = texture;
            isGrappleable = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(blockTexture,  rect, Color.White);
        }

        public void PostionChange( int x, int y)
        {
            xPostion = xPostion + x;
            yPostion = yPostion + y;
            rect = new Rectangle(xPostion, yPostion, width, height);
        }
    }
}
