using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MythsEngine.Character;

namespace MythsEngine.GameState
{
	
	public class ScreenManager : DrawableGameComponent
	{

		private const string StateFilename = "ScreenManagerState.xml";
		private List<GameScreen> screens = new List<GameScreen>();
		private List<GameScreen> tempScreensList = new List<GameScreen>();
		private InputState input = new InputState();
		private SpriteBatch spriteBatch;
		private SpriteFont font;
		private Texture2D blankTexture;
		private bool isInitialized;
		private bool traceEnabled;
		private EntityList entityList;

		public SpriteBatch SpriteBatch
		{
			get
			{
				return spriteBatch;
			}
		}

		public SpriteFont Font
		{
			get
			{
				return font;
			}
		}

		public bool TraceEnabled
		{
			get
			{
				return traceEnabled;
			}
			set
			{
				traceEnabled = value;
			}
		}

		public Texture2D BlankTexture
		{
			get
			{
				return blankTexture;
			}
		}

		public ScreenManager(Game game)
			: base(game)
		{
			TouchPanel.EnabledGestures = GestureType.None;
			//entityList = new EntityList(game);
			game.Services.AddService(typeof(InputState), input);
		}

		public EntityList EntityList
		{
			get
			{
				return entityList;
			}
		}

		public override void Initialize()
		{
			base.Initialize();
			isInitialized = true;
			//entityList.Initialize();
		}

		protected override void LoadContent()
		{
			ContentManager content = Game.Content;
			spriteBatch = new SpriteBatch(GraphicsDevice);
			font = content.Load<SpriteFont>("Fonts/menufont");
			blankTexture = content.Load<Texture2D>("Textures/blank");
			foreach(GameScreen screen in screens)
			{
				screen.Activate(false);
			}
			//entityList.LoadContent();
		}

		protected override void UnloadContent()
		{
			foreach(GameScreen screen in screens)
			{
				screen.Unload();
			}
			//entityList.UnloadContent();
		}

		public override void Update(GameTime gameTime)
		{
			input.Update();
			tempScreensList.Clear();
			foreach(GameScreen screen in screens)
			{
				tempScreensList.Add(screen);
			}
			bool otherScreenHasFocus = !Game.IsActive;
			bool coveredByOtherScreen = false;
			while(tempScreensList.Count > 0)
			{
				GameScreen screen = tempScreensList[tempScreensList.Count - 1];
				tempScreensList.RemoveAt(tempScreensList.Count - 1);
				screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
				if(screen.ScreenState == ScreenState.TransitionOn || screen.ScreenState == ScreenState.Active)
				{
					if(!otherScreenHasFocus)
					{
						screen.HandleInput(gameTime, input);
						//entityList.HandleInput(gameTime, input);
						otherScreenHasFocus = true;
					}
					if(!screen.IsPopup)
					{
						coveredByOtherScreen = true;
					}
				}
			}
			if(traceEnabled)
			{
				TraceScreens();
			}
			//entityList.Update(gameTime);
		}

		public void TraceScreens()
		{
			List<string> screenNames = new List<string>();
			foreach(GameScreen screen in screens)
			{
				screenNames.Add(screen.GetType().Name);
			}
			Debug.WriteLine(string.Join(", ", screenNames.ToArray()));
		}

		public override void Draw(GameTime gameTime)
		{
			foreach(GameScreen screen in screens)
			{
				if(screen.ScreenState == ScreenState.Hidden)
				{
					continue;
				}
				screen.Draw(gameTime);
			}
			//entityList.Draw(gameTime);
		}

		public void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer)
		{
			screen.ControllingPlayer = controllingPlayer;
			screen.ScreenManager = this;
			screen.IsExiting = false;
			if(isInitialized)
			{
				screen.Activate(false);
			}
			screens.Add(screen);
			TouchPanel.EnabledGestures = screen.EnabledGestures;
		}

		public void RemoveScreen(GameScreen screen)
		{
			if(isInitialized)
			{
				screen.Unload();
			}
			screens.Remove(screen);
			tempScreensList.Remove(screen);
			if(screens.Count > 0)
			{
				TouchPanel.EnabledGestures = screens[screens.Count - 1].EnabledGestures;
			}
		}

		public GameScreen[] GetScreens()
		{
			return screens.ToArray();
		}

		public void FadeBackBufferToBlack(float alpha)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(blankTexture, GraphicsDevice.Viewport.Bounds, Color.Black * alpha);
			spriteBatch.End();
		}

		public void Deactivate()
		{
			return;
		}

		public bool Activate(bool instancePreserved)
		{
			return false;
		}
	}
}
