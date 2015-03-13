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
	
	public abstract class MenuScreen : GameScreen
	{

		private List<MenuEntry> menuEntries;
		private int selectedEntry;
		private string menuTitle;
		private InputAction menuUp;
		private InputAction menuDown;
		private InputAction menuSelect;
		private InputAction menuCancel;
		private InputAction menuIncrease;
		private InputAction menuDecrease;

		protected IList<MenuEntry> MenuEntries
		{
			get
			{
				return menuEntries;
			}
		}

		public MenuScreen(string menuTitle)
		{
			this.menuTitle = menuTitle;
			selectedEntry = 0;
			menuEntries = new List<MenuEntry>();
			TransitionOnTime = TimeSpan.FromSeconds(0.5);
			TransitionOffTime = TimeSpan.FromSeconds(0.5);
			menuUp = new InputAction(
				new Buttons[] 
				{ 
					Buttons.DPadUp,
					Buttons.LeftThumbstickUp
				},
				new Keys[]
				{
					Keys.Up
				},
				true
			);
			menuDown = new InputAction(
				new Buttons[]
				{
					Buttons.DPadDown,
					Buttons.LeftThumbstickDown
				},
				new Keys[]
				{
					Keys.Down
				},
				true
			);
			menuSelect = new InputAction(
				new Buttons[]
				{
					Buttons.A,
					Buttons.Start
				},
				new Keys[]
				{
					Keys.Enter,
					Keys.Space
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
			menuIncrease = new InputAction(
				new Buttons[]
				{
					Buttons.DPadRight,
					Buttons.LeftThumbstickRight
				},
				new Keys[]
				{
					Keys.Right,
					Keys.D
				},
				true
			);
			menuDecrease = new InputAction(
				new Buttons[]
				{
					Buttons.DPadLeft,
					Buttons.LeftThumbstickLeft
				},
				new Keys[]
				{
					Keys.Left,
					Keys.A
				},
				true
			);
		}

		public override void HandleInput(GameTime gameTime, InputState input)
		{
			PlayerIndex playerIndex;
			if(menuUp.Evaluate(input, ControllingPlayer, out playerIndex))
			{
				selectedEntry--;
				if(selectedEntry < 0)
				{
					selectedEntry = menuEntries.Count - 1;
				}
			}
			if(menuDown.Evaluate(input, ControllingPlayer, out playerIndex))
			{
				selectedEntry++;
				if(selectedEntry >= menuEntries.Count)
				{
					selectedEntry = 0;
				}
			}
			if(menuCancel.Evaluate(input, ControllingPlayer, out playerIndex))
			{
				OnCancel(playerIndex);
			}
			if(menuSelect.Evaluate(input, ControllingPlayer, out playerIndex))
			{
				OnSelectEntry(selectedEntry, playerIndex);
			}
			if(menuIncrease.Evaluate(input, ControllingPlayer, out playerIndex))
			{
				OnIncreaseEntry(selectedEntry, playerIndex);
			}
			if(menuDecrease.Evaluate(input, ControllingPlayer, out playerIndex))
			{
				OnDecreaseEntry(selectedEntry, playerIndex);
			}
		}

		protected virtual void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
		{
			menuEntries[entryIndex].OnSelectEntry(playerIndex);
		}

		protected virtual void OnIncreaseEntry(int entryIndex, PlayerIndex playerIndex)
		{
			menuEntries[entryIndex].OnIncreaseEntry(playerIndex);
		}

		protected virtual void OnDecreaseEntry(int entryIndex, PlayerIndex playerIndex)
		{
			menuEntries[entryIndex].OnDecreaseEntry(playerIndex);
		}

		protected virtual void OnCancel(PlayerIndex playerIndex)
		{
			ExitScreen();
		}

		protected void OnCancel(object sender, PlayerIndexEventArgs evt)
		{
			OnCancel(evt.PlayerIndex);
		}

		protected virtual void UpdateMenuEntryLocations()
		{
			float transitionOffset = (float) Math.Pow(TransitionPosition, 2);

			float y = 0;
			foreach(MenuEntry menuEntry in menuEntries)
			{
				y += menuEntry.GetHeight(this);
				if(menuEntry.DoubleHeight)
				{
					y += menuEntry.GetHeight(this);
				}
			}
			Vector2 position = new Vector2(0f, (ScreenManager.GraphicsDevice.Viewport.Height - y) / 2);
			foreach(MenuEntry menuEntry in menuEntries)
			{
				if(menuEntry.DoubleHeight)
				{
					position.Y += menuEntry.GetHeight(this);
				}
				position.X = ScreenManager.GraphicsDevice.Viewport.Width / 2 - menuEntry.GetWidth(this) / 2;
				if(ScreenState == ScreenState.TransitionOn)
				{
					position.X -= transitionOffset * 256;
				} else
				{
					position.X += transitionOffset * 512;
				}
				menuEntry.Position = position;
				position.Y += menuEntry.GetHeight(this);

			}
		}

		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
			for(int i = 0; i < menuEntries.Count; i++)
			{
				menuEntries[i].Update(this, (IsActive && (i == selectedEntry)), gameTime);
			}
		}

		public override void Draw(GameTime gameTime)
		{
			UpdateMenuEntryLocations();
			GraphicsDevice graphics = ScreenManager.GraphicsDevice;
			SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
			SpriteFont font = ScreenManager.Font;
			spriteBatch.Begin();
			for(int i = 0; i < menuEntries.Count; i++)
			{
				menuEntries[i].Draw(this, (IsActive && (i == selectedEntry)), gameTime);
			}
			float transitionOffset = (float) Math.Pow(TransitionPosition, 2);
			Vector2 titlePosition = new Vector2(graphics.Viewport.Width / 2, 80);
			Vector2 titleOrigin = font.MeasureString(menuTitle) / 2;
			Color titleColor = new Color(179, 223, 255) * TransitionAlpha;
			float titleScale = 1.25f;
			titlePosition.Y -= transitionOffset * 100;
			spriteBatch.DrawString(font, menuTitle, titlePosition, titleColor, 0, titleOrigin, titleScale, SpriteEffects.None, 0);
			spriteBatch.End();
		}
	}
}
