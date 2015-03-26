using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MythsEngine.Character.Dialogs;

namespace MythsEngine.Character
{
	
	public class NPC : Entity
	{

		private Dialog dialog;
		private string textureAssetName;
		private bool readyForDialog;
		private Texture2D dialogTexture;
		private Texture2D dialogGradient;
		private SpriteFont font;
		private bool inDialog;

		public NPC(int id, string name, Vector2 position, string textureAssetName, Dialog dialog, bool enabled, bool visible, Game game)
			: base(game)
		{
			Id = id;
			Name = name;
			Position = position;
			this.textureAssetName = textureAssetName;
			Visible = visible;
			Enabled = enabled;
			readyForDialog = false;
			inDialog = false;
			this.dialog = dialog;
		}

		public SpriteFont Font
		{
			get
			{
				return font;
			}
			set
			{
				font = value;
			}
		}

		public Texture2D DialogTexture
		{
			get
			{
				return dialogTexture;
			}
			set
			{
				dialogTexture = value;
			}
		}

		public Dialog Dialog
		{
			get
			{
				return dialog;
			}
			set
			{
				dialog = value;
			}
		}

		public bool ReadyForDialog
		{
			get
			{
				return readyForDialog;
			}
			set
			{
				readyForDialog = value;
			}
		}

		public bool InDialog
		{
			get
			{
				return inDialog;
			}
			set
			{
				inDialog = value;
			}
		}

		public override void Initialize()
		{
		}

		public override void LoadContent()
		{
			Texture = Game.Content.Load<Texture2D>(textureAssetName);
			dialogTexture = Game.Content.Load<Texture2D>("Textures/XboxController/Xbox360_Button_A");
			dialogGradient = Game.Content.Load<Texture2D>("Textures/gradient");
			font = Game.Content.Load<SpriteFont>("Fonts/gamefont");
		}

		public override void UnloadContent()
		{
		}

		public override void Update(GameTime gameTime)
		{
			if(Enabled)
			{
			}
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if(Visible)
			{
				spriteBatch.Begin();
				spriteBatch.Draw(Texture, new Rectangle((int) Position.X, (int) Position.Y, Texture.Width, Texture.Height), Color.White);
				if(inDialog)
				{
					spriteBatch.DrawString(font, dialog.dialogLines.ElementAt(dialog.dialogStage).line, new Vector2(35, Game.GraphicsDevice.Viewport.Height - 30), Color.Black, 0.0f, Vector2.Zero, 0.3f, SpriteEffects.None, 1.0f);
					//spriteBatch.Draw(dialogGradient, new Rectangle(25, Game.GraphicsDevice.Viewport.Height - 32, Game.GraphicsDevice.Viewport.Width - 50, 25), Color.White);
				} else if(readyForDialog && dialog != null)
				{
					spriteBatch.Draw(dialogTexture, new Vector2(Position.X + (Texture.Width / 2), Position.Y - 20), dialogTexture.Bounds, Color.White, 0.0f, new Vector2(dialogTexture.Width / 2, dialogTexture.Height / 2), 0.5f, SpriteEffects.None, 0.0f);
				}
				
				spriteBatch.End();
			}
		}

		public override void Dispose()
        {
		}
	}
}
