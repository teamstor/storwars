using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using TeamStor.Engine;
using TeamStor.Engine.Graphics;

namespace TeamStor.TBS.Gameplay
{
	/// <summary>
	/// Game state where the lobby is set up.
	/// </summary>
	public class LobbySetupState : GameState
	{
		/// <summary>
		/// Net peer, either server or client.
		/// </summary>
		public NetPeer Peer
		{
			get;
			private set;
		}

		public LobbySetupState(NetPeer peer)
		{
			Peer = peer;
		}
		
		public override void OnEnter(GameState previousState)
		{
			throw new System.NotImplementedException();
		}

		public override void OnLeave(GameState nextState)
		{
			throw new System.NotImplementedException();
		}

		public override void Update(double deltaTime, double totalTime, long count)
		{
			throw new System.NotImplementedException();
		}

		public override void FixedUpdate(long count)
		{
			throw new System.NotImplementedException();
		}

		public override void Draw(SpriteBatch batch, Vector2 screenSize)
		{
			throw new System.NotImplementedException();
		}
	}
}