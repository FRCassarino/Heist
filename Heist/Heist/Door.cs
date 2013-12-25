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
    class Door : InteractableObject
    {
        
        public enum _DoorState { Open, Closed }
        public _DoorState currentState;
        
        

        

        public Door(Vector2 pos, Texture2D texture, Vector2 dimensions)
            : base(pos, texture, dimensions)
        {
            currentState = _DoorState.Closed;
            Game1.textures.Add("textures/OpenDoor", Game1.contentManager.Load<Texture2D>("textures/OpenDoor"));
            
        }

        public override void Update(GameTime time)
        {
            
            
           
            base.Update(time);
            
               
            
           
        }

        public override void PlayerInteracts()
        {
            
            base.PlayerInteracts();
            if ((int)currentState == 0)
            {
                ++currentState;
            }
            else if ((int)currentState == 1)
            {
                currentState = 0;
            }
            else
            {

                throw new SystemException("Door is neither open or closed");
            }

            if (sprite.texture == Game1.textures["textures/ClosedDoor"])
            {
                sprite.texture = Game1.textures["textures/OpenDoor"];
            }
            else
            {
                sprite.texture = Game1.textures["textures/ClosedDoor"];
            }

        }

        
        
    

    
    }
}
