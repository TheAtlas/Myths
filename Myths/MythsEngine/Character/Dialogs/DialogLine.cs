using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MythsEngine.Character.Dialogs
{
	
	public class DialogLine
	{

		public bool playerSpeaking;
		public string line;

		public DialogLine()
		{
			this.playerSpeaking = false;
			this.line = "";
		}

		public DialogLine(bool playerSpeaking, string line)
		{
			this.playerSpeaking = playerSpeaking;
			this.line = line;
		}
	}
}
