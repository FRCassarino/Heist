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
        
        //public Vector2 dimensions;
        public Texture2D texture;
		public Animation sprite;
        
        
        

        public PhysicalObject(Vector2 pos, Texture2D texture)
        {
            //all take a pos when created
            this.pos = pos;
            this.texture = texture;
			this.sprite = new Animation(texture,ref pos, new Rectangle(0, 0, texture.Width, texture.Height), new[] { 0 }, 0, 0);
        }

        public PhysicalObject(Vector2 pos, Texture2D texture, Vector2 dimensions)
        {
            //all take a pos when created
            this.pos = pos;
            //this.dimensions = dimensions;
            this.texture = texture;
			this.sprite = new Animation(texture,ref pos, new Rectangle(0, 0, texture.Width, texture.Height), new[] { 0 }, 0, 0);
			// this.sprite = new Animation(texture, pos, new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y), new[] { 0 }, 0, 0);
        }

        public virtual void Update()
        {
            
        }

        public virtual void Draw()
        {
            //Draws itself
			//if (dimensions == Vector2.Zero)
			//{
			//    dimensions = new Vector2(texture.Width, texture.Height);
			//}
			sprite.Draw();

            //Vector2 transformedPosforCamera = CustomMath.transformPosIntoCameraPos(pos, Level.currentCamera.position);
			//Rectangle source = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);
			//Game1.sb.Draw(texture, Level.currentCamera.posInCamera(pos), source, Color.White, 0.0f, Level.currentCamera.posInCamera(Vector2.Zero), SpriteEffects.None, 0);

        }
       
    }
}
