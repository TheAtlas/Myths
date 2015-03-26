using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MythsEngine.Character
{
	
	public class Animation
	{

		private int frameCount;
		private Texture2D texture;
		private float timePerFrame;
		private int frame;
		private float totalElapsed;
		private bool paused;

		private float rotation;
		private float scale;
		private float depth;
		private Vector2 origin;

		public Animation(Vector2 origin, float rotation, float scale, float depth)
		{
			this.origin = origin;
			this.rotation = rotation;
			this.scale = scale;
			this.depth = depth;
		}

		public int FrameCount
		{
			get
			{
				return frameCount;
			}
		}

		public int CurrentFrame
		{
			get
			{
				return frame;
			}
		}

		public void Load(ContentManager contentManager, string assetName, int frameCount, int framesPerSecond)
		{
			texture = contentManager.Load<Texture2D>(assetName);
			this.frameCount = frameCount;
			timePerFrame = (float) 1 / framesPerSecond;
			frame = 0;
			totalElapsed = 0;
			paused = false;
		}

		public void Update(float timeElapsed)
		{
			if(paused)
			{
				return;
			}
			totalElapsed += timeElapsed;
			if(totalElapsed > timePerFrame)
			{
				frame++;
				frame = frame % frameCount;
				totalElapsed -= timePerFrame;
			}
		}

		public void DrawFrame(SpriteBatch spriteBatch, int frame, Vector2 position, SpriteEffects spriteEffects)
		{
			int frameWidth = texture.Width / frameCount;
			Rectangle sourceRect = new Rectangle(frameWidth * frame, 0, frameWidth, texture.Height);
			spriteBatch.Draw(texture, position, sourceRect, Color.White, rotation, origin, scale, spriteEffects, depth);
		}

		public void DrawFrame(SpriteBatch spriteBatch, int frame, Vector2 position)
		{
			DrawFrame(spriteBatch, frame, position, SpriteEffects.None);
		}

		public void DrawFrame(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
		{
			DrawFrame(spriteBatch, frame, position, spriteEffects);
		}

		public void DrawFrame(SpriteBatch spriteBatch, Vector2 position)
		{
			DrawFrame(spriteBatch, frame, position, SpriteEffects.None);
		}

		public bool IsPaused
		{
			get
			{
				return paused;
			}
		}

		public void Reset()
		{
			frame = 0;
			totalElapsed = 0f;
		}

		public void Stop()
		{
			paused = true;
			frame = 0;
			totalElapsed = 0f;
		}

		public void Play()
		{
			paused = false;
		}

		public void Pause()
		{
			paused = true;
		}
	}
}
