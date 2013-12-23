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
        //Lists differently typed objects
        public static List<CollidableObject> collidableObjects = new List<CollidableObject>(); //Objects you can collide to
        public static List<InteractableObject> interactableObjects = new List<InteractableObject>();
        
        
        
        //placeholder objects
               
        public static Camera testCamera;         
        public Player testPlayer;  
       
        
       
        
        //the outer walls of the level, for camera and limit purposes
        public Vector2 levelDimensions;

        
        //CO Rectangle topOuterWall;
        //CO Rectangle bottomOuterWall;
        //CO Rectangle rightOuterWall;
        //CO Rectangle leftOuterWall;




        //placeholder textures
        public static Texture2D dot = Game1.contentManager.Load<Texture2D>("textures/testcollidable");
        public static Texture2D testTexture = Game1.contentManager.Load<Texture2D>("textures/wall1");
        
        
        
        public Level(string name)
        {
            testCamera = new Camera();
            //uses the levelid passed in initialization to open the corresponding level file
			string filestr = File.ReadAllText("../../../../HeistContent/levels/" + name + ".txt");
			//Splits the file into each line
			string[] lines = filestr.Split('\n');

			//Uses regular expression to find the line that sets the level 
			Match match = Regex.Match(lines[0], @"^level ([a-zA-Z0-9]+) (\d+) (\d+)");
			if (!match.Success) throw new System.Exception("level file must start with /^level name w h$/"); //makes sure the first line gives the level info
			//sets the levelDimensions as read in the file
            this.levelDimensions = new Vector2(Convert.ToInt32(match.Groups[2].Value), Convert.ToInt32(match.Groups[3].Value));
            //creates an array of every line but the first one
            string[] rest = lines.Skip(1).ToArray();
			//iterates through every line but the first one
            foreach (string l in rest) {
				
                string[] ws = l.Split(' ');
                //switch that creates an object depending on the type specified 
				switch (ws[0]) {

                    case "Player": // InertObject img x y w h
                        if (!Game1.textures.ContainsKey(ws[1]))
                        {
                            Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
                        }
                        testPlayer = new Player(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]]);
                        break;
                    case "InertObject": // InertObject img x y w h
                        if (!Game1.textures.ContainsKey(ws[1]))
                        {
                            Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
                        }
                        new InertObject(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]]);
						break;
                    case "CollidableObject": // InertObject img x y w h
                        if (!Game1.textures.ContainsKey(ws[1]))
                        {
                            Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
                        }                                             
						new CollidableObject(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]]);
						break;
                    case "LivingObject": // InertObject img x y w h
                        if (!Game1.textures.ContainsKey(ws[1]))
                        {
                            Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
                        }
                        new LivingObject(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]]);
                        break;
                    case "PhysicalObject": // InertObject img x y w h
                        if (!Game1.textures.ContainsKey(ws[1]))
                        {
                            Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
                        }
                        new LivingObject(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]]);
                        break;
                    case "InteractableObject": // InertObject img x y w h
                        if (!Game1.textures.ContainsKey(ws[1]))
                        {
                            Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
                        }
                        new InteractableObject(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]]);
                        break;
                    
                    
				}
			}
            

            
            //CO testPlayer = new Player(new Vector2(0,0), testTexture);
     
            


            //CO topOuterWall = new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).Y), (int)levelDimensions.X, 10);
            //CO bottomOuterWall = new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, (int)levelDimensions.Y), testCamera.cameraPos).Y), (int)levelDimensions.X, 10);
            //CO rightOuterWall = new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2((int)levelDimensions.X, 0), testCamera.cameraPos).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).Y), 10, (int)levelDimensions.Y);
            //CO leftOuterWall = new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), testCamera.cameraPos).Y), 10, (int)levelDimensions.Y);



           
            
        }

        public void UpdateLevel()
        {
            //placeholder right now, right now only updates player but it'll iterate through every PhysicalObject within it, and do lots of more stuff
            foreach (CollidableObject CollidableObject in collidableObjects)
            {
                CollidableObject.Update();
            }
            
            //Checks if player is out of bounds, and acts accordingly
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



            //iterates through every collidableObject and check for collision
            PhysicsManager.IterateCollisionList(collidableObjects);
            
            //Makes sure the player is centered within the camera
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
              

                if (CollidableObject is InertObject)
                {
                    CollidableObject.Draw(spriteBatch);
                }

                if (CollidableObject is Player)
                {

                    CollidableObject.Draw(spriteBatch);
                }
               
            }

            
        } 

          


        
    }
}
