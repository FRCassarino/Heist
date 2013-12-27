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
    class LaserAlarm : IntruderDetector
    {
        RotatedRectangle detectionZone;
        Animation detectionZoneSprite;
        List<int> collisionXPositions = new List<int>();
        
        
        public LaserAlarm(Vector2 pos, Texture2D texture, Vector2 dimensions, Vector2 levelDimensions, string laserDirection)
            : base(pos, texture, dimensions)
        {
            if (laserDirection == "right")
            {

                //acordarse de excluir al player y a él mismo, qe solo sean paredes y eso, capaz cambiar a inertobjects
                foreach (CollidableObject CollidableObject in Level.collidableObjects)
                {
                    if (new RotatedRectangle(new Rectangle((int)pos.X, (int)pos.Y /*acordarse de hacerlo del medio o algo así*/, (int)levelDimensions.X - (int)pos.X, 5), 0).Intersects(CollidableObject.GetCollisionRotatedRectangle()))
                    {
                        if (!(CollidableObject is Player) && !(CollidableObject is LaserAlarm))
                        {
                        collisionXPositions.Add((int)CollidableObject.GetCollisionRotatedRectangle().UpperLeftCorner().X);
                        }
                    }
                }



                detectionZone = new RotatedRectangle(new Rectangle((int)pos.X /*mismo*/, (int)pos.Y, (int)collisionXPositions.Min()- (int)pos.X, 30), 0);
                detectionZoneSprite = new Animation(texture, 0, destination: new Rectangle((int)pos.X /*mismo*/, (int)pos.Y, (int)collisionXPositions.Min()- (int)pos.X, 5 ));

            }

			
        }

        public override bool isIntruderDetected()
        {
              foreach (LivingObject LivingObject in Level.livingObjects)
              {
                  if (detectionZone.Intersects(LivingObject.GetCollisionRotatedRectangle()) /*y chequear si tiene keypass válido*/)
                  {
                      return true;             
         
                  }
                  
              }
              return false;
         }

        public override void Draw()
        {
            base.Draw();
            
            detectionZoneSprite.Draw(pos, angle);
        }

    

    }
}
