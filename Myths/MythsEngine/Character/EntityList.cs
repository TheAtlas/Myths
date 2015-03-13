using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MythsEngine.GameState;

namespace MythsEngine.Character
{
	
	public class EntityList
	{

		private static EntityList instance = null;

		public static EntityList GetInstance(Game game)
		{
			if(instance == null)
			{
				instance = new EntityList(game);
			}
			return instance;
		}

		private List<Entity> entities;
		private Game game;

		private EntityList(Game game)
		{
			entities = new List<Entity>();
			this.game = game;
		}

		public void Initialize()
		{
		}

		public void LoadContent()
		{
		}

		public void UnloadContent()
		{
		}

		public void Update(GameTime gameTime)
		{
			foreach(Entity entity in entities)
			{
				if(entity.Enabled)
				{
					entity.Update(gameTime);
				}
			}
		}

		public void HandleInput(GameTime gameTime, InputState input, PlayerIndex? controllingPlayer)
		{
			foreach(Entity entity in entities)
			{
				if(entity is Player && entity.Enabled)
				{
					Player player = (Player) entity;
					player.HandleInput(gameTime, input, controllingPlayer);
				}
			}
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			foreach(Entity entity in entities)
			{
				if(entity.Visible)
				{
					entity.Draw(spriteBatch, gameTime);
				}
			}
		}

		public void AddEntity(Entity entity)
		{
			entity.Initialize();
			entity.LoadContent();
			entities.Add(entity);
		}

		public List<Entity> GetEntities()
		{
			return entities;
		}

		public void RemoveEntity(Entity entity)
		{
			entity.UnloadContent();
			entity.Dispose();
			entities.Remove(entity);
		}

		public void Clear()
		{
			entities.Clear();
		}
	}
}
