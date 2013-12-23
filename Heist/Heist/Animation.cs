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

namespace Heist {
	class Animation {

		public int currentFrame = 0;
		public int totalFrames;
		public int horFrameNum;
		public int verFrameNum;
		public float interval;
		public float angle;
		public Texture2D texture;
		public Vector2 position;
		public Rectangle source;
		float timer = 0;
		public int[] frames; // List of frames to loop, change it according to your needs. eg. player {walk-left: [ 0 1 0 2 ], walk-right: [0 3 0 4]}


		public Animation(Texture2D texture, ref Vector2 pos, Rectangle source, int[] frames, float interval, float angle) {
			if (texture.Height % source.Height != 0 || texture.Width % source.Width != 0)
				throw new Exception("Source rect must fit exactly an integer number of times in the texture.");
			this.texture = texture;
			this.horFrameNum = texture.Width / source.Width;
			this.verFrameNum = texture.Height / source.Height;
			this.totalFrames = horFrameNum * verFrameNum;
			this.frames = frames;
			this.interval = interval;
			this.position = pos;
			this.source = source;
			this.angle = angle;
		}

		void nextFrame()
		{
			currentFrame++;
			int n = frames[currentFrame % frames.Count()];
			source.X = source.Width * (n % texture.Width);
			source.Y = source.Height * (n / texture.Height);
		}

		public void Update(GameTime time)
		{
			if (interval != 0) {
				float elapsed = (float)time.ElapsedGameTime.TotalSeconds;
				timer -= elapsed;
				if (timer < 0) {
					timer = interval;
					nextFrame();
				}
			}
		}

		public void Draw()
		{
			Vector2 posc = Level.currentCamera.posInCamera(position);
			Rectangle dest = new Rectangle((int)posc.X, (int)posc.Y, source.Width, source.Height);
			Game1.sb.Draw(texture, dest, source, Color.White, angle, Level.currentCamera.topPos(), SpriteEffects.None, 0);
		}
	}
}