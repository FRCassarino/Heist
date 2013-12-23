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
        

        public static Camera currentCamera;         
        public Player player;
        
       
        
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
			currentCamera = new Camera(Vector2.Zero);

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
                        player = new Player(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]], Vector2.Zero);
                        break;
                    case "InertObject": // InertObject img x y w h
                        if (!Game1.textures.ContainsKey(ws[1]))
                        {
                            Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
                        }
                        new InertObject(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]], Vector2.Zero);
						break;
                    case "CollidableObject": // InertObject img x y w h
                        if (!Game1.textures.ContainsKey(ws[1]))
                        {
                            Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
                        }                                             
						new CollidableObject(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]], Vector2.Zero);
						break;
                    case "LivingObject": // InertObject img x y w h
                        if (!Game1.textures.ContainsKey(ws[1]))
                        {
                            Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
                        }
                        new LivingObject(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]], Vector2.Zero);
                        break;
                    case "PhysicalObject": // InertObject img x y w h
                        if (!Game1.textures.ContainsKey(ws[1]))
                        {
                            Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
                        }
                        new LivingObject(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]], Vector2.Zero);
                        break;
                    case "InteractableObject": // InertObject img x y w h
                        if (!Game1.textures.ContainsKey(ws[1]))
                        {
                            Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
                        }
                        new InteractableObject(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]], Vector2.Zero);
                        break;
				}
			}
            
        }

        public void UpdateLevel(GameTime time)
        {

			

            //placeholder right now, right now only updates player but it'll iterate through every PhysicalObject within it, and do lots of more stuff
            foreach (CollidableObject CollidableObject in collidableObjects)
            {
                CollidableObject.Update(time);
            }
            
            if (player.pos.X > levelDimensions.X)
            {
                player.SetValidPos();
            }

            if (player.pos.Y > levelDimensions.Y)
            {
                player.SetValidPos();
            }

            if (player.pos.X < 0)
            {
                player.SetValidPos();
            }

            if (player.pos.Y < 0)
            {
                player.SetValidPos();
            }



            //iterates through every collidableObject and check for collision
            PhysicsManager.IterateCollisionList(collidableObjects);
            
            currentCamera.position = player.pos;


            
        }

        public void DrawLevel()
        {
           
            //Placeholder draw for the level outer walls.  
            //Game1.sb.Draw(dot, new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), currentCamera.position).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), currentCamera.position).Y), (int)levelDimensions.X, 10), Color.White);
			//Game1.sb.Draw(dot, new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), currentCamera.position).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, (int)levelDimensions.Y), currentCamera.position).Y), (int)levelDimensions.X, 10), Color.White);
			//Game1.sb.Draw(dot, new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2((int)levelDimensions.X, 0), currentCamera.position).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), currentCamera.position).Y), 10, (int)levelDimensions.Y), Color.White);
			//Game1.sb.Draw(dot, new Rectangle((int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), currentCamera.position).X), (int)(CustomMath.transformPosIntoCameraPos(new Vector2(0, 0), currentCamera.position).Y), 10, (int)levelDimensions.Y), Color.White);

            //iterates through every collidableObject as a placeholder, it will iterate through every PhysicalObject, draws them. Checks what they are as to pass the
            //right texture
            foreach (CollidableObject CollidableObject in collidableObjects)
            {
				CollidableObject.Draw();
            } // TODO cambiar a PHys Obj

            
        } 

    }
}
