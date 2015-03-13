using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MythsEngine.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MythsEngine.Screens
{

	public class MessageBoxScreen : GameScreen
	{

		private string message;
		private Texture2D gradientTexture;
		private Texture2D buttonATexture;
		private InputAction menuSelect;
		private InputAction menuCancel;

		public event EventHandler<PlayerIndexEventArgs> Accepted;
		public event EventHandler<PlayerIndexEventArgs> Cancelled;

		public MessageBoxScreen(string message)
			: this(message, false)
		{
		}

		public MessageBoxScreen(string message, bool includeUsageText)
		{
			const string usageText = "\n     ,     ,      = ok\n    ,      = cancel";
			this.message = includeUsageText ? message + usageText : message;
			IsPopup = true;
			TransitionOnTime = TimeSpan.FromSeconds(0.2);
			TransitionOffTime = TimeSpan.FromSeconds(0.2);
			menuSelect = new InputAction(
				new Buttons[]
				{
					Buttons.A,
					Buttons.Start
				},
				new Keys[]
				{
					Keys.Space,
					Keys.Enter
				},
				true
			);
			menuCancel = new InputAction(
				new Buttons[]
				{
					Buttons.B,
					Buttons.Back
				},
				new Keys[]
				{
					Keys.Escape,
					Keys.Back
				},
				true
			);
		}

		public override void Activate(bool instancePreserved)
		{
			if(!instancePreserved)
			{
				gradientTexture = ScreenManager.Game.Content.Load<Texture2D>("Textures/gradient");
				buttonATexture = ScreenManager.Game.Content.Load<Texture2D>("Textures/XboxController/Xbox360_Button_A");
			}
		}

		public override void HandleInput(GameTime gameTime, InputState input)
		{
			PlayerIndex playerIndex;
			if(menuSelect.Evaluate(input, ControllingPlayer, out playerIndex))
			{
				if(Accepted != null)
				{
					Accepted(this, new PlayerIndexEventArgs(playerIndex));
				}
				ExitScreen();
			} else if(menuCancel.Evaluate(input, ControllingPlayer, out playerIndex))
			{
				if(Cancelled != null)
				{
					Cancelled(this, new PlayerIndexEventArgs(playerIndex));
				}
				ExitScreen();
			}
		}

		public override void Draw(GameTime gameTime)
		{
			SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
			SpriteFont font = ScreenManager.Font;
			ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);
			Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
			Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
			Vector2 textSize = font.MeasureString(message);
			Vector2 textPosition = (viewportSize - textSize) / 2;
			const int hPad = 32;
			const int vPad = 16;
			Rectangle backgroundRectangle = new Rectangle((int) textPosition.X - hPad, (int) textPosition.Y - vPad, (int) textSize.X + hPad * 2, (int) textSize.Y + vPad * 4);
			Color color = Color.White * TransitionAlpha;
			spriteBatch.Begin();
			spriteBatch.Draw(gradientTexture, backgroundRectangle, color);
			spriteBatch.DrawString(font, message, textPosition, color);
			spriteBatch.Draw(buttonATexture, new Rectangle((int) textPosition.X, (int) (textPosition.Y + textSize.Y), (int) textSize.Y, (int) textSize.Y), Color.White);
			spriteBatch.End();
		}
	}
}
