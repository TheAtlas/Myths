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
		private Texture2D healthbarGreen;
		private Texture2D healthbarRed;

		public Dummy(Game game) : base(2, "Dummy", new Vector2(1100, 255), "Textures/NPCs/dummy", null, true, true, game)
		{
			targetPosition = new Vector2(480, 255);
			MaxHealth = 3;
			Health = 3;
		}

		public override void LoadContent()
		{
			base.LoadContent();
			healthbarGreen = Game.Content.Load<Texture2D>("Textures/NPCs/Healthbar-groen");
			healthbarRed = Game.Content.Load<Texture2D>("Textures/NPCs/Healthbar-rood");
		}

		public override void Update(GameTime gameTime)
		{
			Position.X = targetPosition.X + Tutorial.Bounds.Width - Tutorial.Position.X;
			if(Health == 0)
			{
				Visible = false;
			}
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			base.Draw(spriteBatch, gameTime);
			spriteBatch.Begin();
			int maxWidth = (Texture.Width / MaxHealth) * MaxHealth;
			int width = (Texture.Width / MaxHealth) * Health;
			spriteBatch.Draw(healthbarRed, new Vector2(Position.X, Position.Y - 20), new Rectangle(0, 0, maxWidth, healthbarRed.Height), Color.White);
			spriteBatch.Draw(healthbarGreen, new Vector2(Position.X, Position.Y - 20), new Rectangle(0, 0, width, healthbarGreen.Height), Color.White);
			
			spriteBatch.End();
		}
	}
}
