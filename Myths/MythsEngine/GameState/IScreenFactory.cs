using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MythsEngine.GameState
{
	
	public interface IScreenFactory
	{

		GameScreen CreateScreen(Type screenType);

	}
}
