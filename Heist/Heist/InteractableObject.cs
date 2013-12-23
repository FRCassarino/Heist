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
			upperInteractionArea = new RotatedRectangle(new Rectangle((int)GetCollisionRotatedRectangle().UpperLeftCorner().X, (int)GetCollisionRotatedRectangle().UpperLeftCorner().Y - (int)INTERACTION_DISTANCE, (int)GetCollisionRotatedRectangle().Width, (int)INTERACTION_DISTANCE), angle);
			rightInteractionArea = new RotatedRectangle(new Rectangle((int)GetCollisionRotatedRectangle().UpperRightCorner().X, (int)GetCollisionRotatedRectangle().UpperRightCorner().Y, (int)INTERACTION_DISTANCE, (int)GetCollisionRotatedRectangle().Height), angle);
			leftInteractionArea = new RotatedRectangle(new Rectangle((int)GetCollisionRotatedRectangle().UpperLeftCorner().X - (int)INTERACTION_DISTANCE, (int)GetCollisionRotatedRectangle().UpperLeftCorner().Y, (int)INTERACTION_DISTANCE, (int)GetCollisionRotatedRectangle().Height), angle);
			bottomInteractionArea = new RotatedRectangle(new Rectangle((int)GetCollisionRotatedRectangle().LowerLeftCorner().X, (int)GetCollisionRotatedRectangle().LowerLeftCorner().Y, (int)GetCollisionRotatedRectangle().Width, (int)INTERACTION_DISTANCE), angle);
            //objects you can interact with
        }

        
        public void PlayerInteracts()
        {
            if (sprite.texture == Game1.textures["textures/ClosedDoor"])
            {
            sprite.texture = Game1.textures["textures/Wall"];
            }
            else 
            {
            sprite.texture =  Game1.textures["textures/ClosedDoor"];
            }
        }

    }   
}
