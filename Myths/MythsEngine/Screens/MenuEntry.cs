using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MythsEngine.GameState;

namespace MythsEngine.Screens
{
	
	public class MenuEntry
	{

		private string text;
		private float selectionFade;
		private Vector2 position;
		private bool doubleHeight;

		public event EventHandler<PlayerIndexEventArgs> Selected;
		public event EventHandler<PlayerIndexEventArgs> Increased;
		public event EventHandler<PlayerIndexEventArgs> Decreased;

		public string Text
		{
			get
			{
				return text;
			}
			set
			{
				text = value;
			}
		}

		public bool DoubleHeight
		{
			get
			{
				return doubleHeight;
			}
			set
			{
				doubleHeight = value;
			}
		}

		public Vector2 Position
		{
			get
			{
				return position;
			}
			set
			{
				position = value;
			}
		}

		protected internal virtual void OnSelectEntry(PlayerIndex playerIndex)
		{
			if(Selected != null)
			{
				Selected(this, new PlayerIndexEventArgs(playerIndex));
			}
		}

		protected internal virtual void OnIncreaseEntry(PlayerIndex playerIndex)
		{
			if(Increased != null)
			{
				Increased(this, new PlayerIndexEventArgs(playerIndex));
			}
		}

		protected internal virtual void OnDecreaseEntry(PlayerIndex playerIndex)
		{
			if(Decreased != null)
			{
				Decreased(this, new PlayerIndexEventArgs(playerIndex));
			}
		}

		public MenuEntry(string text)
		{
			this.text = text;
			this.doubleHeight = false;
		}

		public MenuEntry(string text, bool doubleHeight)
		{
			this.text = text;
			this.doubleHeight = doubleHeight;
		}

		public virtual void Update(MenuScreen screen, bool isSelected, GameTime gameTime)
		{
			float fadeSpeed = (float) gameTime.ElapsedGameTime.TotalSeconds * 4;
			selectionFade = isSelected ? Math.Min(selectionFade + fadeSpeed, 1) : Math.Max(selectionFade - fadeSpeed, 0);
		}

		public virtual void Draw(MenuScreen screen, bool isSelected, GameTime gameTime)
		{
			Color color = isSelected ? Color.Yellow : Color.White;
			double time = gameTime.TotalGameTime.TotalSeconds;
			float pulsate = (float) Math.Sin(time * 6) + 1;
			float scale = 1 + pulsate * 0.05f * selectionFade;
			color *= screen.TransitionAlpha;
			ScreenManager screenManager = screen.ScreenManager;
			SpriteBatch spriteBatch = screenManager.SpriteBatch;
			SpriteFont font = screenManager.Font;
			Vector2 origin = new Vector2(0, font.LineSpacing / 2);
			spriteBatch.DrawString(font, text, position, color, 0, origin, scale, SpriteEffects.None, 0);
		}

		public virtual int GetHeight(MenuScreen screen)
		{
			return screen.ScreenManager.Font.LineSpacing;
		}

		public virtual int GetWidth(MenuScreen screen)
		{
			return (int) screen.ScreenManager.Font.MeasureString(Text).X;
		}
	}
}
