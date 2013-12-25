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
    public static class PhysicsManager
    {
        

        internal static void IterateCollisionList(List<CollidableObject> collidableObjects)
        {
            //Iterates through every couple of objects in the list, and checks for collisions between them
            for (int i = 0; i < collidableObjects.Count; i++)
            {
                for (int j = i; j < collidableObjects.Count; j++)
                {
                    
                    if (i != j && PhysicsManager.CheckCollision(collidableObjects[i], collidableObjects[j]) == true)
                    {
                        //if collision is detected, it tells the colliding objects so they can execute their logic
                        collidableObjects[i].CollisionDetected();
                        collidableObjects[j].CollisionDetected();
                        

                    }
                    else if (j == (collidableObjects.Count -1))
                    {
                        collidableObjects[i].PassValidPos(collidableObjects[i].pos, collidableObjects[i].angle);
                    }
                     
                 
                    
                }
            }

           
        }

        internal static bool CheckCollision(CollidableObject collidableObject, CollidableObject collidableObject2)
        {

            //uses the intestects method built into RotatedRectangle and checks for collision between both objects
            if(collidableObject is Door)
            {
                Door collidableObjectDoor = (Door)collidableObject;
                if (collidableObjectDoor.currentState == Door._DoorState.Open)
                    return false;
            }

            if (collidableObject2 is Door)
            {
                Door collidableObjectDoor = (Door)collidableObject2;
                if (collidableObjectDoor.currentState == Door._DoorState.Open)
                    return false;
            }
            
            
            if (collidableObject.GetCollisionRotatedRectangle().Intersects(collidableObject2.GetCollisionRotatedRectangle()))
               return true;
            else
               return false;
        }
       
        
        
    }
}
