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
using Microsoft.Xna.Framework.Audio;

namespace FloorIsLava
{
    /*
     * The Gold class will inherit from the GameObject class and will add to the players 
     * gold attribute if it is collided with.
     */
    public class Gold : GameObject
    {
        #region Attributes
        //Attributes
        bool isVisible = true;
        Rectangle gem;
        Texture2D gemImage;
        public int numberOfGems;
        GameScreen gameScreen;
        SoundEffect coinSound;
        #endregion Attributes


        #region Constructor
        //Constructor
        public Gold(Texture2D gemImage, int x, int y, int width, int height, GameScreen gameScreen, SoundEffect snd)
        {
            coinSound = snd;
            gem = new Rectangle(x, y, width, height);
            this.gemImage = gemImage;
            this.gameScreen = gameScreen;
        }
        #endregion Constructor

        #region CollisionCheck
        //Method that will detect collisions between the gem and the player
        public void CollisionCheck(Rectangle rectangle)
        {
            if ((gem.Intersects(rectangle)) && (isVisible == true))
            {
                gameScreen.Score = 10;
                numberOfGems++;
                coinSound.Play();
                isVisible = false;
            }
        }
        #endregion CollisionCheck

        #region Draw
        //Draw method
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible == true)
            {
                spriteBatch.Draw(gemImage, gem, Color.White);
            }
        }
        #endregion Draw

        #region MoveDown
        internal void MoveDown(int y)
        {
            gem.Y += y;
        }
        #endregion MoveDown
    }
}
