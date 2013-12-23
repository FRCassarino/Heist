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
        
        
        

        public PhysicalObject(Vector2 pos, Texture2D texture)
        {
            //all take a pos when created
            this.pos = pos;
            this.texture = texture;
        }

        public PhysicalObject(Vector2 pos, Texture2D texture, Vector2 dimensions)
        {
            //all take a pos when created
            this.pos = pos;
            this.dimensions = dimensions;
            this.texture = texture; 
        }

        public virtual void Update()
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //Draws itself
            if (dimensions == Vector2.Zero)
            {
                dimensions = new Vector2(texture.Width, texture.Height);
            }

            //Adjusts the pos to take the camera into account
            Vector2 transformedPosforCamera = CustomMath.transformPosIntoCameraPos(pos, Level.testCamera.cameraPos);

            //Draws a physical object.
            spriteBatch.Draw(texture, new Rectangle((int)transformedPosforCamera.X, (int)transformedPosforCamera.Y, (int)dimensions.X, (int)dimensions.Y), new Rectangle(0, 0, texture.Width, texture.Height), Color.White);

        }
       
    }
}
