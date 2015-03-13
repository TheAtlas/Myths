using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MythsEngine.Character
{
	
	public abstract class Entity
	{

		private int id;
		private string name;
		private Texture2D texture;
		private Animation animation;
		private List<Animation> animationQueue;
		private int maxHealth;
		private int health;
		private int attackDamage;
		private double attackSpeed;
		private int movementSpeed;
		private Entity interactingWith;
		private int direction;
		private bool dead;
		private bool visible;
		private bool enabled;
		private double animationTransitionTime;
		private Game game;

		public Vector2 Position;

		public override string ToString()
		{
			return base.ToString() + "Id=" + id + ", Name=" + name + ", Position=" + Position + ", Texture=" + texture + ", Animation=" + animation + ", AnimationQueue=" + animationQueue + ", MaxHealth=" + maxHealth + ", Health=" + health
				+ ", AttackDamage=" + attackDamage + ", AttackSpeed=" + attackSpeed + ", MovementSpeed=" + movementSpeed + ", InteractingWith=" + interactingWith + ", Dead=" + dead + ", Visible=" + visible + ", AnimationTransitionTime=" + animationTransitionTime;
		}

		public Entity(Game game)
		{
			this.game = game;
			this.id = 0;
			this.name = "Entity";
			this.Position = Vector2.Zero;
			this.texture = null;
			this.animation = null;
			this.animationQueue = new List<Animation>();
			this.maxHealth = 0;
			this.health = 0;
			this.attackDamage = 0;
			this.attackSpeed = 0.0;
			this.movementSpeed = 0;
			this.interactingWith = null;
			this.dead = true;
			this.visible = false;
			this.enabled = false;
			this.direction = 0;
			this.animationTransitionTime = 0;
		}

		public Game Game
		{
			get
			{
				return game;
			}
		}

		public int Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public Texture2D Texture
		{
			get
			{
				return texture;
			}
			set
			{
				texture = value;
			}
		}

		public Animation Animation
		{
			get
			{
				return animation;
			}
			set
			{
				animation = value;
			}
		}

		public List<Animation> AnimationQueue
		{
			get
			{
				return animationQueue;
			}
			set
			{
				animationQueue = value;
			}
		}

		public int MaxHealth
		{
			get
			{
				return maxHealth;
			}
			set
			{
				maxHealth = value;
			}
		}

		public int Direction
		{
			get
			{
				return direction;
			}
			set
			{
				direction = value;
			}
		}

		public int Health
		{
			get
			{
				return health;
			}
			set
			{
				health = value;
			}
		}

		public int AttackDamage
		{
			get
			{
				return attackDamage;
			}
			set
			{
				attackDamage = value;
			}
		}

		public double AttackSpeed
		{
			get
			{
				return attackSpeed;
			}
			set
			{
				attackSpeed = value;
			}
		}

		public int MovementSpeed
		{
			get
			{
				return movementSpeed;
			}
			set
			{
				movementSpeed = value;
			}
		}

		public Entity InteractingWith
		{
			get
			{
				return interactingWith;
			}
			set
			{
				interactingWith = value;
			}
		}

		public bool Dead
		{
			get
			{
				return dead;
			}
			set
			{
				dead = value;
			}
		}


		public bool Visible
		{
			get
			{
				return visible;
			}
			set
			{
				visible = value;
			}
		}

		public bool Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				enabled = value;
			}
		}

		public double AnimationTransitionTime
		{
			get
			{
				return animationTransitionTime;
			}
			set
			{
				animationTransitionTime = value;
			}
		}

		public abstract void Initialize();

		public abstract void LoadContent();

		public abstract void UnloadContent();

		public abstract void Update(GameTime gameTime);

		public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

		public abstract void Dispose();

	}
}
