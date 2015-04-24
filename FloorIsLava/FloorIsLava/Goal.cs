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
        SaveInfo save;
        GameState state;
        Texture2D goalTexture;
        int xPostion;
        int yPostion;
        int width;
        int height;
        public Goal(Texture2D texture, int x, int y, int width, int height, Game1 game)
        {
            save = new SaveInfo();
            state = new GameState(game);
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

        public void isColliding(Rectangle pos)
        {
            if ((pos.X > xPostion && pos.X < xPostion + width) && (pos.Y > yPostion && pos.Y < yPostion + height))
            {
                state.EndGame();
            }
        }
    }
}
