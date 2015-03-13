using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MythsEngine.GameState;
using MythsEngine.Screens;
using MythsEngine.Character;
using MythsEngine.Character.Dialogs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MythsEngine.Screens.Levels
{
	
	public class Tutorial : GameScreen
	{

		private ContentManager content;
		private Texture2D levelTexture;
		private SpriteFont font;
		private float pauseAlpha;
		private Player player;
		private InputAction pauseAction;
		private EntityList entityList;
		private NPC testNPC;

		public Tutorial()
		{
			TransitionOnTime = TimeSpan.FromSeconds(1.5);
			TransitionOffTime = TimeSpan.FromSeconds(0.5);
			pauseAction = new InputAction(new Buttons[] { Buttons.Back, Buttons.Start }, new Keys[] { Keys.Escape }, true);
		}

		public override void Activate(bool instancePreserved)
		{
			if(!instancePreserved)
			{
				if(content == null)
				{
					content = new ContentManager(ScreenManager.Game.Services, "Content");
				}
				/*if(entityList == null)
				{
					entityList = new EntityList(ScreenManager.Game);
				}*/
				if(player == null)
				{
					player = new Player(ScreenManager.Game);
					EntityList.GetInstance(ScreenManager.Game).AddEntity(player);
				}
				if(testNPC == null)
				{
					testNPC = new NPC(1, "Test NPC", new Vector2(100, 265), "Textures/player", content.Load<Dialog>("Dialogs/TestDialog"), true, true, ScreenManager.Game);
					EntityList.GetInstance(ScreenManager.Game).AddEntity(testNPC);
				}
				levelTexture = content.Load<Texture2D>("Textures/tutorialLevel");
				font = content.Load<SpriteFont>("Fonts/gamefont");
				Thread.Sleep(1000);
				ScreenManager.Game.ResetElapsedTime();
			}
			base.Activate(instancePreserved);
		}

		public override void Deactivate()
		{
			base.Deactivate();
		}

		public override void Unload()
		{
			//ScreenManager.EntityList.UnloadContent();
			EntityList.GetInstance(ScreenManager.Game).UnloadContent();
			content.Unload();
		}

		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
			if(coveredByOtherScreen)
			{
				pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
			} else
			{
				pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);
			}
			EntityList.GetInstance(ScreenManager.Game).Update(gameTime);
		}

		public override void HandleInput(GameTime gameTime, InputState input)
		{
			PlayerIndex playerIndex;
			if(pauseAction.Evaluate(input, ControllingPlayer, out playerIndex))
			{
				ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
			}
			EntityList.GetInstance(ScreenManager.Game).HandleInput(gameTime, input, ControllingPlayer);
		}

		public override void Draw(GameTime gameTime)
		{
			ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 0, 0);
			SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
			spriteBatch.Begin();

			spriteBatch.Draw(levelTexture, ScreenManager.GraphicsDevice.Viewport.Bounds, Color.White);
			spriteBatch.End();
			EntityList.GetInstance(ScreenManager.Game).Draw(gameTime, spriteBatch);
			if(TransitionPosition > 0 || pauseAlpha > 0)
			{
				float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);
				ScreenManager.FadeBackBufferToBlack(alpha);
			}
		}
	}
}
