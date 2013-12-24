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

namespace Heist
{
	class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		public static SpriteBatch spriteBatch; // Make it static to be able to Draw;
		public static ContentManager contentManager; // Make it static to be able to Load;
		public static int WINDOW_WIDTH = 800;
		public static int WINDOW_HEIGTH = 600;
		public Level currentLevel;
		public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferHeight = WINDOW_HEIGTH;
			graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
			Content.RootDirectory = "Content";
			contentManager = Content;
			TargetElapsedTime = TimeSpan.FromSeconds(1 / 60.0);
		}

		protected override void Initialize()
		{
			currentLevel = new Level("0");
			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (gameTime.IsRunningSlowly)
				Console.WriteLine("SLOW!!!");

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			currentLevel.UpdateLevel(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			currentLevel.DrawLevel();
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}