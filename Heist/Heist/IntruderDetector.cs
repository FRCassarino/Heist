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
    abstract class IntruderDetector : InteractableObject
    {
        
          public IntruderDetector(Vector2 pos, Texture2D texture, Vector2 dimensions)
            : base(pos, texture, dimensions)
        {
			
        }
        
        public override void CheckIfPlayerInteracts()
        {
            base.CheckIfPlayerInteracts();
            
            

        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (isIntruderDetected() == true)
            {
                IntruderDetected();
            }
        }

        public virtual bool isIntruderDetected()
        {
            return false;
        }

        public void IntruderDetected()
        {

            GameLogic.AlertPolice(); 
        }


    }

}
