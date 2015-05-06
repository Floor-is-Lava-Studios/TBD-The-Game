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
    public class EnemyPathEnd
    {
        #region Attributes
        //Attributes
        public Rectangle enemyPathRect;
        #endregion Attributes

        #region Constructor
        //Constructor
        public EnemyPathEnd(int posX, int posY, int width, int height, bool top, Game1 game)
        {
            if (top == true)
            {
                enemyPathRect = new Rectangle(posX, posY, width, height);
            }
            else
            {
                enemyPathRect = new Rectangle(posX, posY + game.wallSprite.Height, width, height);
            }
        }
        #endregion Constructor

        #region MoveDown
        //Move down method
        internal void MoveDown(int y)
        {
            enemyPathRect.Y += y;
        }
        #endregion MoveDown
    }
}

