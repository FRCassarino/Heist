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
    class InteractableObject : InertObject
    {
        KeyboardState oldState;

        public const float INTERACTION_DISTANCE = 25;
        public RotatedRectangle upperInteractionArea;
        public RotatedRectangle rightInteractionArea;
        public RotatedRectangle leftInteractionArea;
        public RotatedRectangle bottomInteractionArea;


       
        public InteractableObject(Vector2 pos, Texture2D texture, Vector2 dimensions)
            : base(pos, texture, dimensions)
        {
			Level.interactableObjects.Add(this);
			//objects you can interact with
			upperInteractionArea = new RotatedRectangle(new Rectangle((int)rectangle.UpperLeftCorner().X, (int)rectangle.UpperLeftCorner().Y - (int)INTERACTION_DISTANCE, (int)rectangle.Width, (int)INTERACTION_DISTANCE), angle);
			rightInteractionArea = new RotatedRectangle(new Rectangle((int)rectangle.UpperRightCorner().X, (int)rectangle.UpperRightCorner().Y, (int)INTERACTION_DISTANCE, (int)rectangle.Height), angle);
			leftInteractionArea = new RotatedRectangle(new Rectangle((int)rectangle.UpperLeftCorner().X - (int)INTERACTION_DISTANCE, (int)rectangle.UpperLeftCorner().Y, (int)INTERACTION_DISTANCE, (int)rectangle.Height), angle);
			bottomInteractionArea = new RotatedRectangle(new Rectangle((int)rectangle.LowerLeftCorner().X, (int)rectangle.LowerLeftCorner().Y, (int)rectangle.Width, (int)INTERACTION_DISTANCE), angle);
            //objects you can interact with
        }

        
        public virtual void PlayerInteracts()
        {
           
        }

        public void CheckIfPlayerInteracts()
        {
            var newState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !oldState.IsKeyDown(Keys.Space))
            {
                PlayerInteracts();


            }

            oldState = newState;
        }

    }   
}
