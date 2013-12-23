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
		private Rectangle source;
		float timer = 0;
		public int[] frames; // List of frames to loop, change it according to your needs. eg. player {walk-left: [ 0 1 0 2 ], walk-right: [0 3 0 4]}
		public Rectangle destination;
		private bool cambio = false;


		public Animation(Texture2D texture, Rectangle destination, Rectangle source, int[] frames, float interval, float angle) {

			if (texture.Height % source.Height != 0 || texture.Width % source.Width != 0)
				throw new Exception("Source rect must fit exactly an integer number of times in the texture.");

			this.texture = texture;
			this.horFrameNum = texture.Width / source.Width;
			this.verFrameNum = texture.Height / source.Height;
			this.totalFrames = horFrameNum * verFrameNum;
			this.frames = frames;

			this.interval = interval;
			this.destination = destination;
			this.source = source;
			this.angle = angle;
		}

		public void Update(GameTime time)
		{
			if (interval != 0) {
				float elapsed = (float)time.ElapsedGameTime.TotalMilliseconds;
				timer += elapsed;
				if (timer > interval && !cambio) {
					cambio = true;
					timer = 0;
					currentFrame = frames[(currentFrame + 1) % frames.Count()];
					source.X = source.Width * (currentFrame % horFrameNum);
					source.Y = source.Height * (currentFrame / verFrameNum);
				}
			}
		}

		public void Draw(Vector2 position, float angle)
		{
			cambio = false;
			Vector2 offset = new Vector2(this.source.Width / 2, this.source.Height / 2);
			Vector2 pc = Level.currentCamera.posInCamera(position) + offset;
			Game1.spriteBatchStatic.Draw(texture, new Rectangle((int)pc.X, (int)pc.Y, destination.Width, destination.Height), source, Color.White, angle, offset, SpriteEffects.None, 0);
		}
	}
}