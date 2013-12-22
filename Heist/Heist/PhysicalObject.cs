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
        internal Camera camera;
        public Vector2 dimensions;
        
        
        

        public PhysicalObject(Vector2 pos)
        {
            //all take a pos when created
            this.pos = pos;
            
        }

        public PhysicalObject(Vector2 pos, Vector2 dimensions)
        {
            //all take a pos when created
            this.pos = pos;
            this.dimensions = dimensions;

        }

        public virtual void Update()
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            //Draws itself
            if (dimensions == Vector2.Zero)
            {
                dimensions = new Vector2(texture.Width, texture.Height);
            }
            

            Vector2 transformedPosforCamera = CustomMath.transformPosIntoCameraPos(pos, camera.cameraPos);
            spriteBatch.Draw(texture, new Rectangle((int)transformedPosforCamera.X, (int)transformedPosforCamera.Y, texture.Width, texture.Height), new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y), Color.White);

        }
       
    }
}
