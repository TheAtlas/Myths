using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MythsEngine.Character.Dialogs
{
	
	public class Dialog
	{

		public int dialogStage;
		public List<DialogLine> dialogLines;

		public Dialog()
		{
			this.dialogStage = 0;
			this.dialogLines = new List<DialogLine>();
		}

		public Dialog(int dialogStage, List<DialogLine> dialogLines)
		{
			this.dialogStage = dialogStage;
			this.dialogLines = dialogLines;
		}

		public void AddLine(DialogLine line)
		{
			dialogLines.Add(line);
		}
	}
}
