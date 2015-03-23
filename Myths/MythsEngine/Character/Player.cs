using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MythsEngine.GameState;
using MythsEngine.Screens.Levels;

namespace MythsEngine.Character
{
	
	public class Player : Entity
	{

		public enum PlayerState
		{
			Idle,
			Moving,
			Jumping,
			Attacking,
			InDialog,
			InCinematic,
			InMenu
		}

		private Animation idleAnimation;

		private event EventHandler stateChanged;

		private PlayerState playerState;
		private InputAction jumpControl;
		private InputAction pauseAction;
		private InputAction moveLeftControl;
		private InputAction moveRightControl;
		private InputAction actionControl;

		public Player(Game game)
			: base(game)
		{
			stateChanged += new EventHandler(PlayerStateChanged);
			Id = 1;
			Name = "Player";
			MaxHealth = 100;
			Health = 100;
			AttackDamage = 10;
			AttackSpeed = 1.0;
			MovementSpeed = 3;
			Dead = false;
			Visible = true;
			Enabled = true;
			playerState = PlayerState.Idle;
			jumpControl = new InputAction(Buttons.X, Keys.Space, true);
			moveLeftControl = new InputAction(Buttons.LeftThumbstickLeft, Keys.Left, false);
			moveRightControl = new InputAction(Buttons.LeftThumbstickRight, Keys.Right, false);
			pauseAction = new InputAction(new Buttons[] { Buttons.Start, Buttons.Back }, new Keys[] { Keys.Escape }, true);
			actionControl = new InputAction(Buttons.A, Keys.X, true);
			idleAnimation = new Animation(Vector2.Zero, 0, 1f, 1f);
		}

		public PlayerState State
		{
			get
			{
				return playerState;
			}
			set
			{
				if(playerState != value && stateChanged != null)
				{
					stateChanged(this, new PlayerStateEventArgs(playerState, value));
				}
				playerState = value;
			}
		}

		public override void Initialize()
		{

		}

		public override void LoadContent()
		{
			Texture = Game.Content.Load<Texture2D>("Textures/Character/character");
			Position = new Vector2((float) (Game.GraphicsDevice.Viewport.Width - (Texture.Width * 1.5)), (float) Game.GraphicsDevice.Viewport.Height - 145);

			idleAnimation.Load(Game.Content, "Textures/Character/character-idle", 2, 1);
		}

		public override void UnloadContent()
		{
		}

		public override void Update(GameTime gameTime)
		{
			if(Enabled)
			{
				idleAnimation.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
				foreach(Entity entity in EntityList.GetInstance(Game).GetEntities())
				{
					if(entity is NPC)
					{
						NPC npc = entity as NPC;
						if(npc.Position.X - (npc.Texture.Width / 2) >= (Position.X - 130) && npc.Position.X + (npc.Texture.Width / 2) <= (Position.X + 130))
						{
							npc.ReadyForDialog = true;
						} else
						{
							npc.ReadyForDialog = false;
						}
					}
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if(Visible)
			{
				spriteBatch.Begin();
				SpriteEffects effects = Direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

				idleAnimation.DrawFrame(spriteBatch, Position, effects);

				//spriteBatch.Draw(Texture, new Rectangle((int) Position.X, (int) Position.Y, Texture.Width, Texture.Height), Texture.Bounds, Color.White, 0.0f, new Vector2(0, 0), effects, 0.0f);

				//spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height), Color.Black);
				spriteBatch.End();
			}
		}

		public override void Dispose()
		{

		}

		public void HandleInput(GameTime gameTime, InputState input, PlayerIndex? controllingPlayer)
		{
			PlayerIndex playerIndex;
			if(State == PlayerState.InDialog || State == PlayerState.InCinematic || State == PlayerState.InMenu)
			{
				if(actionControl.Evaluate(input, controllingPlayer, out playerIndex))
				{
					if(InteractingWith is NPC)
					{
						NPC npc = (NPC) InteractingWith;
						npc.Dialog.dialogStage++;
						if(npc.Dialog.dialogStage >= npc.Dialog.maxDialogStages)
						{
							npc.Dialog.dialogStage = 1;
							npc.InteractingWith = null;
							npc.InDialog = false;
							this.InteractingWith = null;
							this.State = PlayerState.Idle;
						}
					}
				}
			} else
			{
				if(jumpControl.Evaluate(input, controllingPlayer, out playerIndex))
				{
					Console.WriteLine("[" + DateTime.Now.ToString("o") + "]: " + "Jump!");
				}
				if(moveLeftControl.Evaluate(input, controllingPlayer, out playerIndex))
				{
					State = PlayerState.Moving;
					Direction = 0;
					if(Position.X <= Tutorial.Bounds.X)
					{
						if(!Tutorial.BoundsLocked)
						{
							Tutorial.Position.X -= MovementSpeed;
						}
					} else
					{
						Position.X -= MovementSpeed;
					}
					//Position.X -= MovementSpeed;
					//Tutorial.Position.X -= MovementSpeed;
				}

				if(moveRightControl.Evaluate(input, controllingPlayer, out playerIndex))
				{
					State = PlayerState.Moving;
					Direction = 1;
					if(Position.X >= Tutorial.Bounds.Width - Texture.Width)
					{
						if(!Tutorial.BoundsLocked)
						{
							Tutorial.Position.X += MovementSpeed;
						}
					} else
					{
						Position.X += MovementSpeed;
					}
					//Position.X += MovementSpeed;
					//Tutorial.Position.X += MovementSpeed;
				}
				if (actionControl.Evaluate(input, controllingPlayer, out playerIndex))
				{
					foreach (Entity entity in EntityList.GetInstance(Game).GetEntities())
					{
						if (entity is NPC)
						{
							NPC npc = entity as NPC;
							if (npc.Position.X - (npc.Texture.Width / 2) >= (Position.X - 130) && npc.Position.X + (npc.Texture.Width / 2) <= (Position.X + 130))
							{
								StartDialog(npc);
								return;
							}
						}
					}
				}
				if(!moveLeftControl.Evaluate(input, controllingPlayer, out playerIndex) && !moveRightControl.Evaluate(input, controllingPlayer, out playerIndex))
				{
					State = PlayerState.Idle;
					//idleAnimation.Play();
				}
			}
		}

		private void StartDialog(NPC npc)
		{
			State = PlayerState.InDialog;
			npc.InDialog = true;
			npc.InteractingWith = this;
			InteractingWith = npc;
			Console.WriteLine("Starting dialog!");
		}

		private void PlayerStateChanged(object sender, EventArgs eventArgs)
		{
			PlayerStateEventArgs args = (PlayerStateEventArgs) eventArgs;
			switch(args.CurrentState)
			{
				case PlayerState.Idle:
				case PlayerState.InCinematic:
				case PlayerState.InDialog:
				case PlayerState.InMenu:
					idleAnimation.Play();
					break;
				case PlayerState.Moving:
					switch(args.PreviousState)
					{
						case PlayerState.Idle:
						case PlayerState.InCinematic:
						case PlayerState.InDialog:
						case PlayerState.InMenu:
							idleAnimation.Stop();
							break;
					}
					break;
			}
			Console.WriteLine("State changed: " + args.ToString());
		}
	}
}
