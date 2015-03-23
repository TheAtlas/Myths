using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MythsEngine.Character.Dialogs
{
	
	public class Dialog
	{

		public int maxDialogStages;
		public int dialogStage;
		public List<DialogLine> dialogLines;

		public Dialog()
		{
			this.maxDialogStages = 0;
			this.dialogStage = 0;
			this.dialogLines = new List<DialogLine>();
		}

		public Dialog(int maxDialogStages, int dialogStage, List<DialogLine> dialogLines)
		{
			this.maxDialogStages = maxDialogStages;
			this.dialogStage = dialogStage;
			this.dialogLines = dialogLines;
		}

		public void AddLine(DialogLine line)
		{
			dialogLines.Add(line);
		}
	}
}
