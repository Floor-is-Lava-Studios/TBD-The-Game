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
     * The GameObject class will be the abstract class that all entities will inherit
     * from. It will have a rectangle attribute to represent the location of the entity.
     */ 
    public class GameObject
    {
        // attributes
        public Rectangle rect;
        public bool isGrappleable;
    }
}
