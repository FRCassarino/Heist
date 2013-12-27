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
		public const float FW_VELOCITY = 2; //The basic forward speed
		public float runBoost = 1.0f;
		public new Animation sprite;
        Vector2 validPos;
		float validAngle;
		
		public Player(Vector2 pos, Texture2D texture, Vector2 dimensions)
			: base(pos, texture, dimensions)
		{
			this.sprite = new Animation(texture, 100, destination: new Rectangle((int)pos.X, (int)pos.Y, (int)dimensions.X, (int)dimensions.Y), horizontalFrameCount: 4);   
		}

        public override void Update(GameTime time) 
        {			
			Move();
			//Interact();
			base.Update();
			// Better to put these two lines last, after any other code has changed the player's position.
			Level.currentCamera.position = pos;
			sprite.Update(time);

        }

		public override void Draw()
		{
			sprite.Draw(pos, angle);
			DrawBoundingBox();
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
				runBoost = 1.5f;
				sprite.ActiveFrames = null;
			} else {
				runBoost = 1.0f;
				sprite.ActiveFrames = new[] {0};
			}

			//This if loops make the player move when the keys are pressed, and use some trigonometry to make sure they move in the right angle
			if (KS.IsKeyDown(Keys.Up)) // UP 
            {
				pos.X = pos.X - FW_VELOCITY * runBoost * (float)(Math.Cos((double)angle + Math.PI / 2.0));
				pos.Y = pos.Y - FW_VELOCITY * runBoost * (float)(Math.Sin((double)angle + Math.PI / 2.0));
			}


			if (KS.IsKeyDown(Keys.Down)) // DOWN 
            {
				pos.X = pos.X + FW_VELOCITY * runBoost * (float)(Math.Cos((double)angle + Math.PI / 2.0));
				pos.Y = pos.Y + FW_VELOCITY * runBoost * (float)(Math.Sin((double)angle + Math.PI / 2.0));
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

		void Interact()
		{
			foreach (InteractableObject InteractableObject in Level.interactableObjects) {
				RotatedRectangle asdf = rectangle;
				if (InteractableObject.upperInteractionArea.Intersects(rectangle)
					|| InteractableObject.rightInteractionArea.Intersects(rectangle)
					|| InteractableObject.leftInteractionArea.Intersects(rectangle)
					|| InteractableObject.bottomInteractionArea.Intersects(rectangle)) {
					if (!InteractableObject.rectangle.Intersects(rectangle))
						InteractableObject.CheckIfPlayerInteracts();

					//if (Keyboard.GetState().IsKeyDown(Keys.Space))
					//    InteractableObject.PlayerInteracts();
					//break;
				}
			}
		}
	}
}
