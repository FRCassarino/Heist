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
		public float runBoost = 1.0f;
		public Animation sprite;
		public int[] walkingFrames = { 0, 1 };
		public int[] runningFrames = { 2, 3 };
        
        //placeholder textures
        
        
        //This are pos and angles that guarantee a collision will not be present
        Vector2 validPos;
        float validAngle;
        


        public Player(Vector2 pos, Texture2D texture)
            : base(pos, texture)
        {
			this.sprite = new Animation(texture,ref pos, new Rectangle(0, 0, texture.Width / 2, texture.Height / 2), walkingFrames, 300, angle); 
        }

        public void Update(GameTime time)
        {
			Level.currentCamera.position = pos;
			sprite.Update(time);
            Move();

        }

		public override void Draw()
        {
            
            //Draws the Player pos for testing purposes
            //spriteBatch.Draw(dot, pos, Color.White);

           
            // Vertices colission box for testing purposes
			Game1.sb.Draw(Level.dot, Level.currentCamera.posInCamera(GetCollisionRotatedRectangle().LowerLeftCorner()), Color.White);
			Game1.sb.Draw(Level.dot, Level.currentCamera.posInCamera(GetCollisionRotatedRectangle().UpperLeftCorner()), Color.White);
			Game1.sb.Draw(Level.dot, Level.currentCamera.posInCamera(GetCollisionRotatedRectangle().LowerRightCorner()), Color.White);
			Game1.sb.Draw(Level.dot, Level.currentCamera.posInCamera(GetCollisionRotatedRectangle().UpperRightCorner()), Color.White);

			sprite.Draw();

            ///Vector2 transformedPosforCamera = CustomMath.transformPosIntoCameraPos(pos, Level.currentCamera.cameraPos);
            //CO spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y, 116, 85), new Rectangle((int)pos.X, (int)pos.Y, 116, 85), Color.White, angle + (float)Math.PI, new Vector2(new Rectangle((int)pos.X, (int)pos.Y, 116, 85).Width / 2, new Rectangle((int)pos.X, (int)pos.Y, 116, 85).Height / 2), SpriteEffects.None, 0);
            ///spriteBatch.Draw(texture, new Rectangle((int)transformedPosforCamera.X + (116 / 2)/*no se pq funciona*/ , (int)transformedPosforCamera.Y + (85 / 2) /*no se pq funciona*/, 116, 85), new Rectangle(0, 0, 116, 85), Color.White, angle, new Vector2(58, 43), SpriteEffects.None, 0);
            //CO spriteBatch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height), null, Color.White, angle + (float)Math.PI, new Vector2(68, 43), SpriteEffects.None, 0);
        }

        public override RotatedRectangle GetCollisionRotatedRectangle()
        {
            return new RotatedRectangle(new Rectangle((int)pos.X, (int)pos.Y, 116, 85), angle);
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

			if (KS.IsKeyDown(Keys.LeftShift)) {
				runBoost = 1.3f;
				sprite.frames = runningFrames;
			} else {
				runBoost = 1.0f;
				sprite.frames = walkingFrames;
			}

            //This if loops make the player move when the keys are pressed, and use some trigonometry to make sure they move in the right angle
            if (KS.IsKeyDown(Keys.Up)) // UP 
            {
                pos.X = pos.X + FW_VELOCITY * runBoost * (float)(Math.Cos((double)angle));
				pos.Y = pos.Y + FW_VELOCITY * runBoost * (float)(Math.Sin((double)angle));
            }


            if (KS.IsKeyDown(Keys.Down)) // DOWN 
            {
				pos.X = pos.X - FW_VELOCITY * runBoost * (float)(Math.Cos((double)angle));
				pos.Y = pos.Y - FW_VELOCITY * runBoost * (float)(Math.Sin((double)angle));
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
