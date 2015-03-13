using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace MythsEngine.GameState
{

	public enum ScreenState
	{
		TransitionOn,
		Active,
		TransitionOff,
		Hidden
	}
	
	public abstract class GameScreen
	{

		private bool isPopup = false;
		private TimeSpan transitionOnTime = TimeSpan.Zero;
		private TimeSpan transitionOffTime = TimeSpan.Zero;
		private float transitionPosition = 1;
		private ScreenState screenState = ScreenState.TransitionOn;
		private bool isExiting = false;
		private bool otherScreenHasFocus;
		private ScreenManager screenManager;
		private PlayerIndex? controllingPlayer;
		private GestureType enabledGestures = GestureType.None;
		private bool isSerializable = true;

		public bool IsPopup
		{
			get
			{
				return isPopup;
			}
			protected set
			{
				isPopup = value;
			}
		}

		public TimeSpan TransitionOnTime
		{
			get
			{
				return transitionOnTime;
			}
			protected set
			{
				transitionOnTime = value;
			}
		}

		public TimeSpan TransitionOffTime
		{
			get
			{
				return transitionOffTime;
			}
			protected set
			{
				transitionOffTime = value;
			}
		}

		public float TransitionPosition
		{
			get
			{
				return transitionPosition;
			}
			protected set
			{
				transitionPosition = value;
			}
		}

		public float TransitionAlpha
		{
			get
			{
				return 1f - TransitionPosition;
			}
		}

		public ScreenState ScreenState
		{
			get
			{
				return screenState;
			}
			protected set
			{
				screenState = value;
			}
		}

		public bool IsExiting
		{
			get
			{
				return isExiting;
			}
			protected internal set
			{
				isExiting = value;
			}
		}

		public bool IsActive
		{
			get
			{
				return !otherScreenHasFocus && (screenState == ScreenState.TransitionOn || screenState == ScreenState.Active);
			}
		}

		public ScreenManager ScreenManager
		{
			get
			{
				return screenManager;
			}
			internal set
			{
				screenManager = value;
			}
		}

		public PlayerIndex? ControllingPlayer
		{
			get
			{
				return controllingPlayer;
			}
			internal set
			{
				controllingPlayer = value;
			}
		}

		public GestureType EnabledGestures
		{
			get
			{
				return enabledGestures;
			}
			protected set
			{
				enabledGestures = value;
				if(ScreenState == ScreenState.Active)
				{
					TouchPanel.EnabledGestures = value;
				}
			}
		}

		public bool IsSerializable
		{
			get
			{
				return isSerializable;
			}
			protected set
			{
				isSerializable = value;
			}
		}

		public virtual void Activate(bool instancePreserved)
		{
		}

		public virtual void Deactivate()
		{
		}

		public virtual void Unload()
		{
		}

		public virtual void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			this.otherScreenHasFocus = otherScreenHasFocus;
			if(isExiting)
			{
				screenState = ScreenState.TransitionOff;
				if(!UpdateTransition(gameTime, transitionOffTime, 1))
				{
					ScreenManager.RemoveScreen(this);
				}
			} else if(coveredByOtherScreen)
			{
				if(UpdateTransition(gameTime, transitionOffTime, 1))
				{
					screenState = ScreenState.TransitionOff;
				} else
				{
					screenState = ScreenState.Hidden;
				}
			} else
			{
				if(UpdateTransition(gameTime, transitionOnTime, -1))
				{
					screenState = ScreenState.TransitionOn;
				} else
				{
					screenState = ScreenState.Active;
				}
			}
		}

		public bool UpdateTransition(GameTime gameTime, TimeSpan time, int direction)
		{
			float transitionDelta = (time == TimeSpan.Zero) ? 1 : (float) (gameTime.ElapsedGameTime.TotalMilliseconds / time.TotalMilliseconds);
			transitionPosition += transitionDelta * direction;
			if(((direction < 0) && (transitionPosition <= 0)) || ((direction > 0) && (transitionPosition >= 1)))
			{
				transitionPosition = MathHelper.Clamp(transitionPosition, 0, 1);
				return false;
			}
			return true;
		}

		public virtual void HandleInput(GameTime gameTime, InputState input)
		{
		}

		public virtual void Draw(GameTime gameTime)
		{
		}

		public void ExitScreen()
		{
			if(TransitionOffTime == TimeSpan.Zero)
			{
				ScreenManager.RemoveScreen(this);
			} else
			{
				isExiting = true;
			}
		}
	}
}
