using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MythsEngine.Character.Dialogs;
using Microsoft.Xna.Framework.Graphics;

namespace MythsEngine.Character
{
	
	public class Hermes : NPC
	{

		private int dialogStage;

		public Hermes(Game game) : base(1, "Hermes", new Vector2(100, 245), "Textures/NPCs/hermes", game.Content.Load<Dialog>("Dialogs/HermesStart"), true, true, game)
		{
			dialogStage = 1;
		}

		public int DialogStage
		{
			get
			{
				return dialogStage;
			}
			set
			{
				dialogStage = value;
			}
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if(Visible)
			{
				spriteBatch.Begin();
				spriteBatch.Draw(Texture, new Vector2((int) Position.X, (int) Position.Y), new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White);
				if(InDialog)
				{
					bool playerSpeaking = Dialog.dialogLines.ElementAt(Dialog.dialogStage).playerSpeaking;
					string dialogName = (Dialog.dialogLines.ElementAt(Dialog.dialogStage).playerSpeaking ? "Heracles" : "Hermes") + ":";
					spriteBatch.DrawString(Font, dialogName, new Vector2(35, Game.GraphicsDevice.Viewport.Height - 25), Color.Black, 0.0f, Vector2.Zero, 0.3f, SpriteEffects.None, 1.0f);
					spriteBatch.DrawString(Font, Dialog.dialogLines.ElementAt(Dialog.dialogStage).line, new Vector2(45 + (Font.MeasureString(dialogName).X * 0.3f), Game.GraphicsDevice.Viewport.Height - 25), new Color(60, 60, 60), 0.0f, Vector2.Zero, 0.3f, SpriteEffects.None, 1.0f);
					//spriteBatch.Draw(dialogGradient, new Rectangle(25, Game.GraphicsDevice.Viewport.Height - 32, Game.GraphicsDevice.Viewport.Width - 50, 25), Color.White);
				} else if(ReadyForDialog && Dialog != null)
				{
					spriteBatch.Draw(DialogTexture, new Vector2(Position.X + (Texture.Width - 20), Position.Y - 5), DialogTexture.Bounds, Color.White, 0.0f, new Vector2(DialogTexture.Width / 2, DialogTexture.Height / 2), 0.5f, SpriteEffects.None, 0.0f);
				}
				
				spriteBatch.End();
			}
		}
	}
}
