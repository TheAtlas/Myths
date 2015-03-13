using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MythsEngine.GameState
{
	
	public class ScreenFactory : IScreenFactory
	{

		public GameScreen CreateScreen(Type screenType)
		{
			return Activator.CreateInstance(screenType) as GameScreen;
		}
	}
}
