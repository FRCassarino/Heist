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
    class CollidableObject : PhysicalObject
    {
		public RotatedRectangle rectangle;

        public CollidableObject(Vector2 pos, Texture2D texture, Vector2 dimensions)
            : base(pos, texture, dimensions)
        {
            //Makes sure every collidableObject is added to the list, that is later used to iterate
            //through all the collidableObjects by the collision Manager
			if (dimensions == Vector2.Zero)
				dimensions = new Vector2(texture.Width, texture.Height);
			rectangle = new RotatedRectangle(new Rectangle((int)pos.X, (int)pos.Y, (int)dimensions.X, (int)dimensions.Y), angle);
			//rectangle = new RotatedRectangle(new Rectangle((int)pos.X, (int)pos.Y, (int)dimensions.X, (int)dimensions.Y), angle);
            Level.collidableObjects.Add(this);
        }

		virtual public void Update()
		{
			rectangle = new RotatedRectangle(new Rectangle((int)pos.X, (int)pos.Y, (int)dimensions.X, (int)dimensions.Y), angle);
		}

        virtual public void CollisionDetected()
        {
            //Left blank because it should only be used by inherited classes
        }

        virtual public void PassValidPos(Vector2 validPos, float validAngle)
        {
            //Left blank because it should only be used by inherited classes
        }

        
    }
}
