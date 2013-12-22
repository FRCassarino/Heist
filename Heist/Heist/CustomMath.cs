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
    public static class CustomMath
    {
        public static Vector2 transformPosIntoCameraPos(Vector2 pos, Vector2 cameraPos)
        {
            //gets an object pos, return the pos adjusted to take the camera into account
            Vector2 transformedPos = new Vector2(pos.X - cameraPos.X , pos.Y - cameraPos.Y);
            return transformedPos;
        }

        
    }
}
