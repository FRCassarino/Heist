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
       //The most basic blueprint for any object that will be visible on-screen
        
        //All share the fact that they have a position and a given angle       
        public Vector2 pos;
        public float angle;
        
        public Vector2 dimensions;
        public Texture2D texture;
		public Animation sprite;
        
        
        

        public PhysicalObject(Vector2 pos, Texture2D texture, Vector2 dimensions)
        {
            //all take a pos when created
            this.pos = pos;
            this.texture = texture;
			this.dimensions = dimensions;
			if (dimensions == Vector2.Zero) {
				this.dimensions = new Vector2(texture.Width, texture.Height);
			}
			this.sprite = new Animation(texture, new Rectangle((int)pos.X, (int)pos.Y, (int)this.dimensions.X, (int)this.dimensions.Y), new Rectangle(0, 0, (int)this.dimensions.X, (int)this.dimensions.Y), new[] { 0 }, 0, 0);
			this.dimensions = dimensions; //???
        }

		public virtual void Update(GameTime time)
        {

        }

        public virtual void Draw()
        {
			
			sprite.Draw(pos, angle);
        }
       
    }
}
