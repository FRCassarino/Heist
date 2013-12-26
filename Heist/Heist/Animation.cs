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
	class Animation
	{

		/* An animation is created from a texture which is cut
		 * into a {horizontalFrameCount} by {verticalFrameCount} grid.
		 * Its frames are indexed from 0 from left to right and top to
		 * bottom. eg.: 4x4
		 *   _______________
		 *  |  0|  1|  2|  3|
		 *  |___|___|___|__ |
		 *	|  4|  5|  6|  7|
		 *	|___|___|___|__ |
		 *  |  8|  9| 10| 11|
		 *  |___|___|___|__ |
		 *  | 12| 13| 14| 15|
		 *  |___|___|___|__ |
		 * 
		 * An {ActiveFrames} property can be set restricting the set
		 * of frames that are looped through and drawn, changing every
		 * {interval} milliseconds. Setting {ActiveFrames = null} will
		 * not restrict the set, leaving every frame in the loop.
		 */

		public Texture2D texture;
		private int[] activeFrames;
		public int[] ActiveFrames
		{
			get { return activeFrames; }
			set {
				if (value == null) activeFrames = frames;
				else activeFrames = value.Intersect(frames).ToArray();
			}
		}
		public int interval;
		public float angle;
		public Rectangle destination
		{
			get { return destin; }
			set { destin = value; }
		}

		int[] frames;
		Rectangle source;
		Rectangle destin;
		int currentFrame = 0;
		int horizontalFrameCount;
		int verticalFrameCount;

		public Animation(Texture2D texture, int interval, float angle = 0f, int horizontalFrameCount = 1, int verticalFrameCount = 1, Rectangle destination = default(Rectangle))
		{
			this.texture = texture;

			source = new Rectangle(0, 0, texture.Width / horizontalFrameCount, texture.Height / verticalFrameCount);
			if (destination == default(Rectangle))
				destin = new Rectangle(0, 0, texture.Width, texture.Height);
			else
				destin = destination;
			
			this.interval = interval;
			this.angle = angle;
			this.horizontalFrameCount = horizontalFrameCount;
			this.verticalFrameCount = verticalFrameCount;
			frames = Enumerable.Range(0, horizontalFrameCount * verticalFrameCount).ToArray();
			activeFrames = frames;
			currentFrame = activeFrames[0];
		}


		int last = 0;
		int now = 0;
		public void Update(GameTime time)
		{
			if (interval != 0) {
				now = (int)time.TotalGameTime.TotalMilliseconds;
				if (now - last > interval) {
					last = now;
					currentFrame = activeFrames[(currentFrame + 1) % activeFrames.Count()];
					source.X = source.Width * (currentFrame % horizontalFrameCount);
					source.Y = source.Height * (currentFrame / horizontalFrameCount);
				}
			}
		}

		public void Draw(Vector2 position, float angle, bool debug = false)
		{
			Vector2 sourceOrigin = new Vector2(source.Width / 2, source.Height / 2);
			Vector2 drawCoords = Level.currentCamera.posInCamera(position);

			Game1.spriteBatch.Draw(texture,
				new Rectangle((int)drawCoords.X, (int)drawCoords.Y, destin.Width, destin.Height),
				source,
				Color.White,
				angle,
				sourceOrigin, // Move the image origin to the center of the texture so rotation is from the center of the image.
				SpriteEffects.None,
				0);
		}
	}
}