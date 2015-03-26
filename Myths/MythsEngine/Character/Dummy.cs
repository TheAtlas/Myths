using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MythsEngine.Screens.Levels;

namespace MythsEngine.Character
{

	public class Dummy : NPC
	{

		private Vector2 targetPosition;

		public Dummy(Game game) : base(2, "Dummy", new Vector2(1100, 255), "Textures/NPCs/dummy", null, true, true, game)
		{
			targetPosition = new Vector2(480, 255);
		}

		public override void Update(GameTime gameTime)
		{
			Position.X = targetPosition.X + Tutorial.Bounds.Width - Tutorial.Position.X;
		}
	}
}
