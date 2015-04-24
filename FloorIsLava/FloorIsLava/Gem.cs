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
     * The Gold class will inherit from the GameObject class and will add to the players 
     * gold attribute if it is collided with.
     */
    public class Gem : GameObject
    {
        //Attributes
        bool isVisible;
        Rectangle gem;
        Texture2D gemImage;
        public int numberOfGems;
        GameScreen gameScreen;

        //Constructor
        public Gem(Texture2D gemImage, int x, int y, int width, int height, GameScreen gameScreen, bool isVisible)
        {
            this.isVisible = isVisible;
            this.gemImage = gemImage;
            this.gameScreen = gameScreen;
        }

        //Method that will detect collisions between the gem and the player and increment player's score and make the gem disappear
        public void CollisionCheck(Rectangle rectangle)
        {
            foreach (Gem g in gameScreen.gemsList)
            {
                if ((gem.Intersects(rectangle)) && (isVisible == true))
                {
                    numberOfGems++;
                    isVisible = false;
                }
            }
        }

        //Draw method
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible == true)
            {
                spriteBatch.Draw(gemImage, gem, Color.White);
            }
        }
    }
}

