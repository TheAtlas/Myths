using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MythsEngine.Screens
{
	
	public class OptionsMenuScreen : MenuScreen
	{

		public OptionsMenuScreen()
			: base("Options")
		{
			MenuEntry displayMenuEntry = new MenuEntry("Display");
			MenuEntry soundMenuEntry = new MenuEntry("Sound");
			MenuEntry backMenuEntry = new MenuEntry("Back", true);
			displayMenuEntry.Selected += DisplayMenuEntrySelected;
			soundMenuEntry.Selected += SoundMenuEntrySelected;
			backMenuEntry.Selected += OnCancel;
			MenuEntries.Add(displayMenuEntry);
			MenuEntries.Add(soundMenuEntry);
			MenuEntries.Add(backMenuEntry);

		}

		public void DisplayMenuEntrySelected(object sender, PlayerIndexEventArgs evt)
		{
			ScreenManager.AddScreen(new DisplayOptionsMenuScreen(), evt.PlayerIndex);
		}

		public void SoundMenuEntrySelected(object sender, PlayerIndexEventArgs evt)
		{
			ScreenManager.AddScreen(new SoundOptionsMenuScreen(), evt.PlayerIndex);
		}
	}
}
