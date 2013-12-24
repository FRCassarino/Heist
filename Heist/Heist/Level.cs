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
		public static List<CollidableObject> collidableObjects = new List<CollidableObject>(); //Objects you can collide to
		public static List<InteractableObject> interactableObjects = new List<InteractableObject>();
		public static Camera currentCamera;
		public Player player;
		public Vector2 levelDimensions;

		public static Texture2D dot = Game1.contentManager.Load<Texture2D>("textures/testcollidable");
		public static Texture2D testTexture = Game1.contentManager.Load<Texture2D>("textures/wall1");

		public Level(string name)
		{
			currentCamera = new Camera(Vector2.Zero);

			string filestr = File.ReadAllText("../../../../HeistContent/levels/" + name + ".txt");
			string[] lines = filestr.Split('\n');

			Match match = Regex.Match(lines[0], @"^level ([a-zA-Z0-9]+) (\d+) (\d+)"); //Parse the first line (which declares the level)
			if (!match.Success)	throw new System.Exception("level file must start with /^level name w h$/");
			this.levelDimensions = new Vector2(Convert.ToInt32(match.Groups[2].Value), Convert.ToInt32(match.Groups[3].Value));
			
			string[] rest = lines.Skip(1).ToArray();
			foreach (string l in rest) {

				string[] ws = l.Split(' ');
				switch (ws[0]) { //switch that creates an object depending on the type specified 

					case "Player": // InertObject img x y w h
						if (!Game1.textures.ContainsKey(ws[1])) {
							Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
						}
						player = new Player(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]], new Vector2(Convert.ToInt32(ws[4]), Convert.ToInt32(ws[5])));
						break;

					case "InertObject": // InertObject img x y w h
						if (!Game1.textures.ContainsKey(ws[1])) {
							Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
						}
						new InertObject(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]], new Vector2(Convert.ToInt32(ws[4]), Convert.ToInt32(ws[5])));
						break;

					case "CollidableObject": // InertObject img x y w h
						if (!Game1.textures.ContainsKey(ws[1])) {
							Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
						}
						new CollidableObject(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]], new Vector2(Convert.ToInt32(ws[4]), Convert.ToInt32(ws[5])));
						break;

					case "LivingObject": // InertObject img x y w h
						if (!Game1.textures.ContainsKey(ws[1])) {
							Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
						}
						new LivingObject(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]], new Vector2(Convert.ToInt32(ws[4]), Convert.ToInt32(ws[5])));
						break;

					case "PhysicalObject": // InertObject img x y w h
						if (!Game1.textures.ContainsKey(ws[1])) {
							Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
						}
						new LivingObject(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]], new Vector2(Convert.ToInt32(ws[4]), Convert.ToInt32(ws[5])));
						break;

					case "InteractableObject": // InertObject img x y w h
						if (!Game1.textures.ContainsKey(ws[1])) {
							Game1.textures.Add(ws[1], Game1.contentManager.Load<Texture2D>(ws[1]));
						}
						new InteractableObject(new Vector2(Convert.ToInt32(ws[2]), Convert.ToInt32(ws[3])), Game1.textures[ws[1]], new Vector2(Convert.ToInt32(ws[4]), Convert.ToInt32(ws[5])));
						break;
				}
			}
		}

		public void UpdateLevel(GameTime time)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))	Program.game.Exit();

			player.Update(time);

			if (player.pos.X > levelDimensions.X) player.SetValidPos();
			if (player.pos.Y > levelDimensions.Y) player.SetValidPos();
			if (player.pos.X < 0) player.SetValidPos();
			if (player.pos.Y < 0) player.SetValidPos();

			PhysicsManager.IterateCollisionList(collidableObjects);

			currentCamera.position = player.pos;
		}

		public void DrawLevel()
		{
			foreach (CollidableObject CollidableObject in collidableObjects) {
				CollidableObject.Draw();
			}
		}

	}
}
