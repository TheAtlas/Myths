using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MythsEngine.Character
{
	
	public class PlayerStateEventArgs : EventArgs
	{

		private Player.PlayerState previousState;
		private Player.PlayerState currentState;

		public PlayerStateEventArgs(Player.PlayerState previousState, Player.PlayerState currentState)
		{
			this.previousState = previousState;
			this.currentState = currentState;
		}

		public Player.PlayerState PreviousState
		{
			get
			{
				return previousState;
			}
		}

		public Player.PlayerState CurrentState
		{
			get
			{
				return currentState;
			}
		}

		public override string ToString()
		{
			return "PlayerStateEventArgs{previousState=" + previousState + ", currentState=" + currentState + "}";
		}
	}
}
