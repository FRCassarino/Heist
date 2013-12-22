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
    class Player : LivingObject
    {
        //The basic forward speed
        public const float FW_VELOCITY = 10; 
        
        //placeholder textures
        public Texture2D dot;
        
        //This are pos and angles that guarantee a collision will not be present
        Vector2 validPos;
        float validAngle;
        


        public Player(Vector2 pos, List<CollidableObject> collidableObjects, Camera camera)
            : base(pos, camera)
        {
            
        }

        override public void Update()
        {
            base.Update();
            Move();

        }

        override public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            
            //Draws the Player pos for testing purposes
            spriteBatch.Draw(dot, pos, Color.White);

           
            // Vertices colission box for testing purposes
            spriteBatch.Draw(dot, GetCollisionRotatedRectangle().LowerLeftCorner(), Color.White);
            spriteBatch.Draw(dot, GetCollisionRotatedRectangle().UpperLeftCorner(), Color.White);
            spriteBatch.Draw(dot, GetCollisionRotatedRectangle().LowerRightCorner(), Color.White);
            spriteBatch.Draw(dot, GetCollisionRotatedRectangle().UpperRightCorner(), Color.White);


            Vector2 transformedPosforCamera = CustomMath.transformPosIntoCameraPos(pos, camera.cameraPos);
            //CO spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y, 116, 85), new Rectangle((int)pos.X, (int)pos.Y, 116, 85), Color.White, angle + (float)Math.PI, new Vector2(new Rectangle((int)pos.X, (int)pos.Y, 116, 85).Width / 2, new Rectangle((int)pos.X, (int)pos.Y, 116, 85).Height / 2), SpriteEffects.None, 0);
            spriteBatch.Draw(texture, new Rectangle((int)transformedPosforCamera.X + (116 / 2)/*no se pq funciona*/ , (int)transformedPosforCamera.Y + (85 / 2) /*no se pq funciona*/, 116, 85), new Rectangle(0, 0, 116, 85), Color.White, angle, new Vector2(58, 43), SpriteEffects.None, 0);
            //CO spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height), null, Color.White, angle + (float)Math.PI, new Vector2(68, 43), SpriteEffects.None, 0);
        }

        public override RotatedRectangle GetCollisionRotatedRectangle()
        {
            
            //CO return new RotatedRectangle((int)pos.X, (int)pos.Y, 116, 85);
            
            //return the bounding box for the player
            Vector2 transformedPosforCamera = CustomMath.transformPosIntoCameraPos(pos, camera.cameraPos);
                        
            return new RotatedRectangle(new Rectangle((int)transformedPosforCamera.X, (int)transformedPosforCamera.Y, 116, 85), angle);
            //CO return base.GetCollisionRectangle();
        }

        override public void CollisionDetected()
        {
            //If a collision is detected, this sets the player's position and angle back to one where it doesn't collide
            SetValidPos();
        }

        public void SetValidPos()
        {
            pos = validPos;
            angle = validAngle;
        }


        override public void PassValidPos(Vector2 validPos, float validAngle) 
        {
            //This is called when no collision is detected, and saves the current pos and angle as to potentially use it later when a collision is detected
            this.validPos = validPos;
            this.validAngle = validAngle;
        }

        public void Move()
        {
            
           
            KeyboardState KS = Keyboard.GetState();

            //This if loops make the player move when the keys are pressed, and use some trigonometry to make sure they move in the right angle
            if (KS.IsKeyDown(Keys.Up)) // UP 
            {
                pos.X = pos.X + FW_VELOCITY * (float)(Math.Cos((double)angle));
                pos.Y = pos.Y + FW_VELOCITY * (float)(Math.Sin((double)angle));
            }


            if (KS.IsKeyDown(Keys.Down)) // DOWN 
            {
                pos.X = pos.X - FW_VELOCITY * (float)(Math.Cos((double)angle));
                pos.Y = pos.Y - FW_VELOCITY * (float)(Math.Sin((double)angle));
            }

            if (KS.IsKeyDown(Keys.Right)) // RIGHT
            {
                angle += (float)Math.PI / 32;
            }

            if (KS.IsKeyDown(Keys.Left)) // LEFT
            {
                angle -= (float)Math.PI / 32;
            }

        }
    }
}
