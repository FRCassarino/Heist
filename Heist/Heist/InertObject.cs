using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Heist
{
    class InertObject : CollidableObject
    {
        public InertObject(Vector2 pos, List<CollidableObject> collidableObjects, Camera camera)
            : base(pos, collidableObjects, camera)
        {
            
            //walls and stuff that is for the most part inanimate 
        }

        public InertObject(Vector2 pos, List<CollidableObject> collidableObjects, Camera camera, Vector2 dimensions)
            : base(pos, collidableObjects, camera, dimensions)
        {
            this.dimensions = dimensions;
            //walls and stuff that is for the most part inanimate 
        }
    }
}
