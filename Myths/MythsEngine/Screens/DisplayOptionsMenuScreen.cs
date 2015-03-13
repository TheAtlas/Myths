using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MythsEngine.Screens
{

	public class DisplayOptionsMenuScreen : MenuScreen
	{

		private MenuEntry fullScreenMenuEntry;
		private MenuEntry resolutionMenuEntry;

		private static Size[] resolutions = 
		{
			new Size(600, 400),
			new Size(800, 600),
			new Size(1024, 768),
			new Size(1280, 720),
			new Size(1280, 1024),
			new Size(1680, 1200)
		};



		private static bool fullscreen = false;
		private static int currentResolution = 0;

		public DisplayOptionsMenuScreen()
			: base("Display")
		{
			fullScreenMenuEntry = new MenuEntry(string.Empty);
			resolutionMenuEntry = new MenuEntry(string.Empty);
			MenuEntry backMenuEntry = new MenuEntry("Back", true);
			SetMenuEntryText();
			fullScreenMenuEntry.Increased += FullScreenMenuEntryChanged;
			fullScreenMenuEntry.Selected += FullScreenMenuEntryChanged;
			fullScreenMenuEntry.Decreased += FullScreenMenuEntryChanged;
			resolutionMenuEntry.Increased += ResolutionMenuEntryIncreased;
			resolutionMenuEntry.Selected += ResolutionMenuEntryIncreased;
			resolutionMenuEntry.Decreased += ResolutionMenuEntryDecreased;
			backMenuEntry.Selected += OnCancel;
			MenuEntries.Add(fullScreenMenuEntry);
			MenuEntries.Add(resolutionMenuEntry);
			MenuEntries.Add(backMenuEntry);
		}

		public static bool Fullscreen
		{
			get
			{
				return fullscreen;
			}
		}

		public static Size Resolution
		{
			get
			{
				return resolutions[currentResolution];
			}
		}

		public void SetMenuEntryText()
		{
			resolutionMenuEntry.Text = "Resolution: " + resolutions[currentResolution].Width + "x" + resolutions[currentResolution].Height;
			fullScreenMenuEntry.Text = "Fullscreen: " + (fullscreen ? "Yes" : "No");
		}

		public void FullScreenMenuEntryChanged(object sender, PlayerIndexEventArgs evt)
		{
			fullscreen = fullscreen ? false : true;
			SetMenuEntryText();
		}

		public void ResolutionMenuEntryIncreased(object sender, PlayerIndexEventArgs evt)
		{
			currentResolution++;
			if(currentResolution > (resolutions.Length - 1))
			{
				currentResolution = 0;
			}
			SetMenuEntryText();
		}

		public void ResolutionMenuEntryDecreased(object sender, PlayerIndexEventArgs evt)
		{
			currentResolution--;
			if(currentResolution < 0)
			{
				currentResolution = resolutions.Length - 1;
			}
			SetMenuEntryText();
		}

		protected override void OnCancel(PlayerIndex playerIndex)
		{
			GraphicsDeviceManager graphics = (GraphicsDeviceManager) ScreenManager.Game.Services.GetService(typeof(GraphicsDeviceManager));
			int index = Array.IndexOf(resolutions, new Size(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
			if(graphics.IsFullScreen != fullscreen || currentResolution != index)
			{
				const string message = "Do you want to apply these settings?";
				MessageBoxScreen confirmApplyMessageBox = new MessageBoxScreen(message);
				confirmApplyMessageBox.Accepted += ConfirmApplyMessageBoxAccepted;
				confirmApplyMessageBox.Cancelled += ConfirmApplyMessageBoxCancelled;
				ScreenManager.AddScreen(confirmApplyMessageBox, playerIndex);
			} else
			{
				base.OnCancel(playerIndex);
			}
		}

		public void ConfirmApplyMessageBoxAccepted(object sender, PlayerIndexEventArgs evt)
		{
			GraphicsDeviceManager graphics = (GraphicsDeviceManager) ScreenManager.Game.Services.GetService(typeof(GraphicsDeviceManager));
			graphics.IsFullScreen = fullscreen;
			graphics.PreferredBackBufferWidth = Resolution.Width;
			graphics.PreferredBackBufferHeight = Resolution.Height;
			graphics.ApplyChanges();
			base.OnCancel(evt.PlayerIndex);
		}

		public void ConfirmApplyMessageBoxCancelled(object sender, PlayerIndexEventArgs evt)
		{
			GraphicsDeviceManager graphics = (GraphicsDeviceManager) ScreenManager.Game.Services.GetService(typeof(GraphicsDeviceManager));
			currentResolution = Array.IndexOf(resolutions, new Size(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
			fullscreen = graphics.IsFullScreen;
			base.OnCancel(evt.PlayerIndex);
		}
	}
}
