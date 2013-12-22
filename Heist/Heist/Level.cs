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
    class Level
    {
        //List of every object you can collide with in the current level
        public static List<CollidableObject> collidableObjects = new List<CollidableObject>();
        
        
        
        //placeholder player, camera and inert object to test stuff
        
        
        public Camera testCamera;         
        public Player testPlayer;  
        public InertObject testInertObject;


        public InertObject testInertObject2;
        
       
        
        //the outer walls of the level, for camera and limit purposes
        public Vector2 levelDimensions;

        
        //CO Rectangle topOuterWall;
        //CO Rectangle bottomOuterWall;
        //CO Rectangle rightOuterWall;
        //CO Rectangle leftOuterWall;




        //placeholder textures
        public Texture2D testTexture;
        
        
        
        
        public Level(Vector2 levelDimensions)
        
        {   //will be probably expanded, right now only takes basic leveldimensions
            this.levelDimensions = levelDimensions;

            testCamera = new Camera();
            testPlayer = new Player(new Vector2(0,0), collidableObjects, testCamera);
            testInertObject = new InertObject(new Vector2(600, 600), collidableObjects, testCamera, new Vector2(400,200));
            testInertObject2 = new InertObject(new Vector2(300, 0), collidableObjects, testCamera);
            


            //CO topOuterWall = new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).Y), (int)levelDimensions.X, 10);
            //CO bottomOuterWall = new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, (int)levelDimensions.Y), testCamera.cameraPos).Y), (int)levelDimensions.X, 10);
            //CO rightOuterWall = new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2((int)levelDimensions.X, 0), testCamera.cameraPos).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).Y), 10, (int)levelDimensions.Y);
            //CO leftOuterWall = new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).Y), 10, (int)levelDimensions.Y);



           
            
        }

        public void UpdateLevel()
        {
            //placeholder right now, right now only updates player but it'll iterate through every PhysicalObject within it, and do lots of more stuff
            testPlayer.Update();
            
            
            if (testPlayer.pos.X > levelDimensions.X)
            {
                testPlayer.SetValidPos();
            }

            if (testPlayer.pos.Y > levelDimensions.Y)
            {
                testPlayer.SetValidPos();
            }

            if (testPlayer.pos.X < 0)
            {
                testPlayer.SetValidPos();
            }

            if (testPlayer.pos.Y < 0)
            {
                testPlayer.SetValidPos();
            }




            PhysicsManager.IterateCollisionList(collidableObjects);
            
            Vector2 centeredPlayerPos = new Vector2(testPlayer.pos.X -testCamera.cameraWidth /2, testPlayer.pos.Y - testCamera.cameraHeight/2 );
            testCamera.cameraPos = centeredPlayerPos;


            
        }

        public void DrawLevel(SpriteBatch spriteBatch)
        {
           
            //Placeholder draw for the level outer walls.  
            spriteBatch.Draw(testPlayer.dot, new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).Y), (int)levelDimensions.X, 10), Color.White);
            spriteBatch.Draw(testPlayer.dot, new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, (int)levelDimensions.Y), testCamera.cameraPos).Y), (int)levelDimensions.X, 10), Color.White);
            spriteBatch.Draw(testPlayer.dot, new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2((int)levelDimensions.X, 0), testCamera.cameraPos).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).Y), 10, (int)levelDimensions.Y), Color.White);
            spriteBatch.Draw(testPlayer.dot, new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).Y), 10, (int)levelDimensions.Y), Color.White);

            //iterates through every collidableObject as a placeholder, it will iterate through every PhysicalObject, draws them. Checks what they are as to pass the
            //right texture
            foreach (CollidableObject CollidableObject in collidableObjects)
            {
                if (CollidableObject is Player)
                {
                    
                    CollidableObject.Draw(spriteBatch, testTexture); 
                }

                if (CollidableObject is InertObject)
                {
                    CollidableObject.Draw(spriteBatch, testTexture);
                }
               
            }

            
        } 

          


        
    }
}
