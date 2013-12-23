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
using System.IO;
using System.Text.RegularExpressions;


namespace Heist
{
    class Level
    {
        //List of every object you can collide with in the current level
        public static List<CollidableObject> collidableObjects = new List<CollidableObject>();
        
        
        
        //placeholder player, camera and inert object to test stuff
        
        
        public static Camera testCamera;         
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
        public static Texture2D dot = Game1.contentManager.Load<Texture2D>("textures/testcollidable");
        public Texture2D testTexture = Game1.contentManager.Load<Texture2D>("textures/wall1");
        
        
        
        public Level(string name)
        {
			string filestr = File.ReadAllText("../../../../HeistContent/levels/" + name + ".txt");
			// string filestr = Game1.contentManager.Load<string>("levels/" + name + ".txt");
			string[] lines = filestr.Split('\n');

			// level 1 3000 4000
			Match match = Regex.Match(lines[0], @"^level ([a-zA-Z0-9]+) (\d+) (\d+)");
			if (!match.Success) throw new System.Exception("level file must start with /^level name w h$/");
			this.levelDimensions = new Vector2(Convert.ToInt32(match.Groups[2].Value), Convert.ToInt32(match.Groups[3].Value));

			foreach (string l in lines.Take(2)) {
				string[] ws = l.Split(' ');
				switch (ws[0]) {
					case "InertObject": // InertObject img x y w h
						Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
						new InertObject(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]]);
						break;
                    //case "Player": // InertObject img x y w h
                      //  Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
                        //new Player(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]], testCamera);
                        //break;
				}
			}
            

            testCamera = new Camera();
            testPlayer = new Player(new Vector2(0,0), testTexture);
            testInertObject = new InertObject(new Vector2(600, 600), testTexture, new Vector2(400,200));
            testInertObject2 = new InertObject(new Vector2(300, 0), testTexture);
            


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
            spriteBatch.Draw(dot, new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).Y), (int)levelDimensions.X, 10), Color.White);
            spriteBatch.Draw(dot, new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, (int)levelDimensions.Y), testCamera.cameraPos).Y), (int)levelDimensions.X, 10), Color.White);
            spriteBatch.Draw(dot, new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2((int)levelDimensions.X, 0), testCamera.cameraPos).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).Y), 10, (int)levelDimensions.Y), Color.White);
            spriteBatch.Draw(dot, new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).Y), 10, (int)levelDimensions.Y), Color.White);

            //iterates through every collidableObject as a placeholder, it will iterate through every PhysicalObject, draws them. Checks what they are as to pass the
            //right texture
            foreach (CollidableObject CollidableObject in collidableObjects)
            {
                if (CollidableObject is Player)
                {
                    
                    CollidableObject.Draw(spriteBatch); 
                }

                if (CollidableObject is InertObject)
                {
                    CollidableObject.Draw(spriteBatch);
                }
               
            }

            
        } 

          


        
    }
}
