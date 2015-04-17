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
     * The MoveablePlatform class will inherit from the MoveableGameObject and act similar 
     * to the Platfrom class, but will be bound between two points that only that platform 
     * can hit.
     */
    class MoveablePlatform: GameObject
    {
        Texture2D blockTexture;
        public MoveablePlatform(Texture2D texture, int x, int y, int width, int height)
        {
            rect = new Rectangle(x, y, width, height);
            blockTexture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(blockTexture, rect, Color.White);
        }

        public void Update()
        {
            //this will make the platform move
        }

        // there should be a collide method with the player so that they move about the same
    }
}
