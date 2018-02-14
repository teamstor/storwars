using System.Collections.Generic;
using Lidgren.Network;
using TeamStor.Engine;
using TeamStor.TBS.Map;

namespace TeamStor.TBS.Gameplay
{
	/// <summary>
	/// Game data.
	/// </summary>
	public class GameData
	{
		/// <summary>
		/// All players in this game.
		/// </summary>
		public SortedDictionary<int, Player> Players { get; private set; } = new SortedDictionary<int, Player>();

		/// <summary>
		/// The local player.
		/// </summary>
		public Player LocalPlayer;

		/// <summary>
		/// The map data.
		/// </summary>
		public MapData Map;
		
		public GameData(MapData map)
		{
			Map = map;
		}

		/// <summary>
		/// Finds a player by their connection to the server.
		/// Only works if you're host.
		/// </summary>
		/// <param name="connection">The connection.</param>
		/// <returns>The player that has the connection.</returns>
		public Player FindPlayerByNetConnection(NetConnection connection)
		{
			foreach(Player p in Players.Values)
			{
				if(p.ConnectionToServer == connection)
					return p;
			}
			
			return null;
		}
	}
}