using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MythsEngine.Screens
{

	public class PauseMenuScreen : MenuScreen
	{

		public PauseMenuScreen()
			: base("Paused")
		{
			MenuEntry resumeGameMenuEntry = new MenuEntry("Resume Game");
			MenuEntry quitGameMenuEntry = new MenuEntry("Quit Game");
			resumeGameMenuEntry.Selected += OnCancel;
			quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;
			MenuEntries.Add(resumeGameMenuEntry);
			MenuEntries.Add(quitGameMenuEntry);
		}

		public void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs evt)
		{
			const string message = "Are you sure you want to quit this game?";
			MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);
			confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;
			ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
		}

		public void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs evt)
		{
			LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
		}
	}
}
