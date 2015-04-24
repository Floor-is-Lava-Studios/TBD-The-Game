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
        //Attributes
        public Rectangle enemyPathRect;
        public bool top;

        //Constructor
        public EnemyPathEnd(int posX, int posY, int width, int height, bool top, GameScreen game)
        {
            this.top = top;
            if (top == true)
            {
                enemyPathRect = new Rectangle(posX, posY, width, height);
            }
            else
            {
                enemyPathRect = new Rectangle(posX, posY + width - height, width, height);
            }
        }
    }
}
