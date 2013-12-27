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
    class PhysicalObject
    {
       // The most basic blueprint for any object that will be visible on-screen

        public Vector2 pos;
        public float angle;
        public Vector2 dimensions;
		public Animation sprite;
		public Texture2D texture;
        
        public PhysicalObject(Vector2 pos, Texture2D texture, Vector2 dimensions)
        {
            this.pos = pos;
            this.texture = texture;
			this.dimensions = dimensions;
			if (dimensions == Vector2.Zero) 
            {
				this.dimensions = new Vector2(texture.Width, texture.Height);
			}

            this.sprite = new Animation(texture, 0, destination: new Rectangle(0 , 0, (int)dimensions.X, (int)dimensions.Y)); // Actually might not be needed to create an Animation for every PhysicalObject.
			Level.physicalObjects.Add(this);

        }

		public virtual void Update(GameTime time)
        {
        }

        public virtual void Draw()
        {
			sprite.Draw(pos, angle);
			DrawBoundingBox();
        }

		public void DrawBoundingBox()
		{
			//Vector2 off = new Vector2(2f, 2f);
			//Game1.spriteBatch.Draw(Level.dot, Level.currentCamera.posInCamera(rectangle.LowerLeftCorner()) - off, Color.White);
			//Game1.spriteBatch.Draw(Level.dot, Level.currentCamera.posInCamera(rectangle.UpperLeftCorner()) - off, Color.White);
			//Game1.spriteBatch.Draw(Level.dot, Level.currentCamera.posInCamera(rectangle.LowerRightCorner()) - off, Color.White);
			//Game1.spriteBatch.Draw(Level.dot, Level.currentCamera.posInCamera(rectangle.UpperRightCorner()) - off, Color.White);	
		}
    }
}
