using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using MythsEngine.Character;
using MythsEngine.Character.Dialogs;
using MythsEngine.GameState;
using MythsEngine.Screens;

namespace Myths
{

	public class GameEngine : Microsoft.Xna.Framework.Game
	{

		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private ScreenFactory screenFactory;
		private ScreenManager screenManager;
		private EntityList entityList;

		public GameEngine()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			this.graphics.IsFullScreen = DisplayOptionsMenuScreen.Fullscreen;
			this.graphics.PreferredBackBufferWidth = DisplayOptionsMenuScreen.Resolution.Width;
			this.graphics.PreferredBackBufferHeight = DisplayOptionsMenuScreen.Resolution.Height;
			TargetElapsedTime = TimeSpan.FromTicks(333333);
			screenFactory = new ScreenFactory();

			screenManager = new ScreenManager(this);
			//entityList = new EntityList(this);
			//Services.AddService(typeof(EntityList), entityList);
			Components.Add(screenManager);
			Components.Reverse();
			//Components.Add(entityList);
			Services.AddService(typeof(IScreenFactory), screenFactory);
			Services.AddService(typeof(GraphicsDeviceManager), graphics);

			AddInitialScreens();
		}


		public GraphicsDeviceManager Graphics
		{
			get
			{
				return graphics;
			}
		}
        
		private void AddInitialScreens()
		{
			screenManager.AddScreen(new BackgroundScreen(), null);
			screenManager.AddScreen(new MainMenuScreen(), null);
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			base.Draw(gameTime);
		}
	}
}
