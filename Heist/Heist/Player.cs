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
		public new Animation sprite;
        Vector2 validPos;
		float validAngle;
        
		//public int[] walkingFrames = { 1, 0, 2, 3};
		//public int[] runningFrames = { 2, 3 };


        public override void Update(GameTime time) 
        {
			
			
			Level.currentCamera.position = pos;
            Move();
			sprite.Update(time);
         
            foreach (InteractableObject InteractableObject in Level.interactableObjects)
            {
                RotatedRectangle asdf = GetCollisionRotatedRectangle();
                if (InteractableObject.upperInteractionArea.Intersects(GetCollisionRotatedRectangle())
                    || InteractableObject.rightInteractionArea.Intersects(GetCollisionRotatedRectangle())
                    || InteractableObject.leftInteractionArea.Intersects(GetCollisionRotatedRectangle())
                    || InteractableObject.bottomInteractionArea.Intersects(GetCollisionRotatedRectangle()
                    ))
                {
                    
                    if (!InteractableObject.GetCollisionRotatedRectangle().Intersects(GetCollisionRotatedRectangle()))
                    {
                        InteractableObject.CheckIfPlayerInteracts();
                    }

                    

                    //if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    //    InteractableObject.PlayerInteracts();
                    //break;
                }
            }
        }


		public Player(Vector2 pos, Texture2D texture, Vector2 dimensions)
			: base(pos, texture, dimensions)
		{
			
			//this.sprite = new Animation(texture, 500, horizontalFrameCount: 3);
            this.sprite = new Animation(texture, 0, destination: new Rectangle(0,0, (int)dimensions.X, (int)dimensions.Y));
            
		}

	

		public override void Draw()
		{
			// Vertices colission box for testing purposes
			Game1.spriteBatch.Draw(Level.dot, Level.currentCamera.posInCamera(GetCollisionRotatedRectangle().LowerLeftCorner()), Color.White);
			Game1.spriteBatch.Draw(Level.dot, Level.currentCamera.posInCamera(GetCollisionRotatedRectangle().UpperLeftCorner()), Color.White);
			Game1.spriteBatch.Draw(Level.dot, Level.currentCamera.posInCamera(GetCollisionRotatedRectangle().LowerRightCorner()), Color.White);
			Game1.spriteBatch.Draw(Level.dot, Level.currentCamera.posInCamera(GetCollisionRotatedRectangle().UpperRightCorner()), Color.White);
			sprite.Draw(pos, angle);
		}


		override public void CollisionDetected()
		{
			//If a collision is detected, this sets the player's position and angle back to one where it doesn't collide
            
			SetValidPos();
            
            
		}

		public void SetValidPos()
		{
			//self fucking explanatory
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
				//sprite.ActiveFrames = runningFrames;
			} else {
				runBoost = 1.0f;
				//sprite.ActiveFrames = walkingFrames;
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
