using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MythsEngine.Screens
{
	
	public class SoundOptionsMenuScreen : MenuScreen
	{

		private MenuEntry musicVolumeMenuEntry;
		private MenuEntry effectVolumeMenuEntry;

		private static int musicVolume = 5;
		private static int effectVolume = 5;

		public SoundOptionsMenuScreen()
			: base("Display")
		{
			musicVolumeMenuEntry = new MenuEntry(string.Empty);
			effectVolumeMenuEntry = new MenuEntry(string.Empty);
			MenuEntry backMenuEntry = new MenuEntry("Back", true);
			SetMenuEntryText();
			musicVolumeMenuEntry.Increased += MusicVolumeMenuEntryIncreased;
			musicVolumeMenuEntry.Selected += MusicVolumeMenuEntryIncreased;
			musicVolumeMenuEntry.Decreased += MusicVolumeMenuEntryDecreased;
			effectVolumeMenuEntry.Increased += EffectVolumeMenuEntryIncreased;
			effectVolumeMenuEntry.Selected += EffectVolumeMenuEntryIncreased;
			effectVolumeMenuEntry.Decreased += EffectVolumeMenuEntryDecreased;
			backMenuEntry.Selected += OnCancel;
			MenuEntries.Add(musicVolumeMenuEntry);
			MenuEntries.Add(effectVolumeMenuEntry);
			MenuEntries.Add(backMenuEntry);
		}

		public void SetMenuEntryText()
		{
			musicVolumeMenuEntry.Text = "Music Volume: " + (musicVolume == 0 ? "off" : musicVolume.ToString());
			effectVolumeMenuEntry.Text = "Effects Volume: " + (effectVolume == 0 ? "off" : effectVolume.ToString());
		}

		public void MusicVolumeMenuEntryIncreased(object sender, PlayerIndexEventArgs evt)
		{
			if(musicVolume < 10)
			{
				musicVolume++;
			}
			SetMenuEntryText();
		}

		public void MusicVolumeMenuEntryDecreased(object sender, PlayerIndexEventArgs evt)
		{
			if(musicVolume > 0)
			{
				musicVolume--;
			}
			SetMenuEntryText();
		}

		public void EffectVolumeMenuEntryIncreased(object sender, PlayerIndexEventArgs evt)
		{
			if(effectVolume < 10)
			{
				effectVolume++;
			}
			SetMenuEntryText();
		}

		public void EffectVolumeMenuEntryDecreased(object sender, PlayerIndexEventArgs evt)
		{
			if(effectVolume > 0)
			{
				effectVolume--;
			}
			SetMenuEntryText();
		}
	}
}
