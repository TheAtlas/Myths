using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MythsEngine.Screens.Levels;

namespace MythsEngine.Screens
{
	
	public class MainMenuScreen : MenuScreen
	{

		public MainMenuScreen() : base("")
		{
			MenuEntry playGameMenuEntry = new MenuEntry("Play Game");
			MenuEntry optionsMenuEntry = new MenuEntry("Options");
			MenuEntry creditsMenuEntry = new MenuEntry("Credits");
			MenuEntry exitMenuEntry = new MenuEntry("Exit");
			playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
			optionsMenuEntry.Selected += OptionsMenuEntrySelected;
			creditsMenuEntry.Selected += CreditsMenuEntrySelected;
			exitMenuEntry.Selected += OnCancel;
			MenuEntries.Add(playGameMenuEntry);
			MenuEntries.Add(optionsMenuEntry);
			MenuEntries.Add(creditsMenuEntry);
			MenuEntries.Add(exitMenuEntry);
		}

		public void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs evt)
		{
			LoadingScreen.Load(ScreenManager, true, evt.PlayerIndex, new Tutorial());
		}

		public void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs evt)
		{
			ScreenManager.AddScreen(new OptionsMenuScreen(), evt.PlayerIndex);
		}

		public void CreditsMenuEntrySelected(object sender, PlayerIndexEventArgs evt)
		{
			//ScreenManager.AddScreen(new CreditsMenuScreen(), evt.PlayerIndex);
		}

		protected override void OnCancel(PlayerIndex playerIndex)
		{
			const string message = "Are you sure you want to exit the game?";
			MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);
			confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;
			ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
		}

		public void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs evt)
		{
			ScreenManager.Game.Exit();
		}
	}
}
