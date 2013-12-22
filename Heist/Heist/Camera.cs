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
    class Camera
    {
        //The dimensions of the camera
        public int cameraHeight;
        public int cameraWidth;
        public Vector2 cameraPos;
        

        public Camera()
        {
            //the constructor takes the dimensions
            this.cameraHeight = Game1.WindowHeight;
			this.cameraWidth = Game1.WindowWidth;
            
        }

      

        

    
    }
}
